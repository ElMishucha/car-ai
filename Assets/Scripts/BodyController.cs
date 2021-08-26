using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyController : MonoBehaviour
{

    public float time;
    public HeadController headController;

    void Start()
    {

        time = headController.time * (float)headController.fitness;
        StartCoroutine(Delay());
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}