using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class HeadController : MonoBehaviour
{
    public float time;
    public BodyController body;
    public int fitness = 1;
    public int rating = 0;
    private const int foodCoef = 100;
    private const int timeCoef = 1;

    private Vector2 dir = Vector2.up;
    private bool b = true;
    private float[] outputs = new float[4];
    private Queue<Vector2> orders = new Queue<Vector2>();

    public SnakeDiedEvent Died { get; set; } = new SnakeDiedEvent();
    public AI AI { get; set; }

    int Max(float a, float b, float c, float d)
    {
        var max = Mathf.Max(Mathf.Max(a, b), Mathf.Max(c, d));
        if (max == a) return 0;
        if (max == b) return 1;
        if (max == c) return 2;
        if (max == d) return 3;
        return 0;
    }

    public void Start()
    {
        AI = GetComponent<AI>();
    }

    public void Update()
    {
        /*if (Max(outputs[0], outputs[1], outputs[2], outputs[3]) == 0)
            setDir(Vector2.up);
        if (Max(outputs[0], outputs[1], outputs[2], outputs[3]) == 1)
            setDir(Vector2.down);
        if (Max(outputs[0], outputs[1], outputs[2], outputs[3]) == 2)
            setDir(Vector2.right);
        if (Max(outputs[0], outputs[1], outputs[2], outputs[3]) == 3)
            setDir(Vector2.left);
        */
        if (Max(outputs[0], outputs[1], outputs[2], outputs[3]) == 3)
            orders.Enqueue(Vector2.left);
        if (Max(outputs[0], outputs[1], outputs[2], outputs[3]) == 0)
            orders.Enqueue(Vector2.up);
        if (Max(outputs[0], outputs[1], outputs[2], outputs[3]) == 1)
            orders.Enqueue(Vector2.right);
        if (Max(outputs[0], outputs[1], outputs[2], outputs[3]) == 2)
            orders.Enqueue(Vector2.down);
    }

    private void FixedUpdate()
    {
        if (b)
        {
            rating += timeCoef;
            var child = Instantiate(body, transform.position, transform.rotation);
            child.headController = this;

            if (orders.Count > 0)
            {
                var newDir = orders.Dequeue();

                if (dir != -newDir)
                {
                    dir = newDir;
                }
            }

            transform.Translate(dir);

            StartCoroutine(delay());
        }

        if (transform.localPosition.x < -14 || transform.localPosition.x > 14 || transform.localPosition.y > 14 ||
            transform.localPosition.y < -14)
        {
            Destroy(gameObject);
        }

    }

    public void OnDestroy()
    {
        Died.Invoke(this);
        Died.RemoveAllListeners();
    }

    public void setDir(Vector2 direction)
    {
        dir = direction;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Body"))
        {
            Destroy(gameObject);
        }
        if (other.gameObject.name == "Food")
        {
            fitness++;
            Vector2 pos = new Vector2(Random.Range(-14, 14), Random.Range(-14, 14));
            other.gameObject.transform.localPosition = pos;
            rating += foodCoef;
        }
    }

    IEnumerator delay()
    {
        b = false;
        yield return new WaitForSeconds(time);
        b = true;
    }

    public void SetOutputs(float[] outputs)
    {
        this.outputs = outputs;
    }
}
