using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainController : MonoBehaviour
{
    private const int Step = 50;

    public GameObject arena;

    public GameObject best = null;

    public int bestRating = 0;
    public int width = 25;
    public int height = 5;

    private int _headsCount;
    private readonly List<HeadController> _heads = new List<HeadController>();
    private readonly List<GameObject> _arenas = new List<GameObject>();


    // отбор лучших

    private void Start()
    {
        GenerateArenas();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("Snake");
        }
    }

    private void OnSnakeDied(HeadController head)
    {
        _headsCount--;

        if (_headsCount == 0)
        {
            // All snakes are dead!!!

            var winner = _heads.OrderBy(x => x.rating).First();
            var ai = winner.AI;
            
            _heads.Clear();
            RemoveAllArenas();

            // Do what you want with AI
            Debug.Log($"Winner Rating {winner.rating}");
        }
    }

    private void RemoveAllArenas()
    {
        foreach (var obj in _arenas)
        {
            Destroy(obj);
        }
    }

    private void GenerateArenas()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                var obj = Instantiate(arena, new Vector3(i * Step, j * Step, 0f), transform.rotation);
                _arenas.Add(obj);
                var head = obj.GetComponentInChildren<HeadController>();
                head.Died.AddListener(OnSnakeDied);
                _heads.Add(head);
            }
        }

        _headsCount = _heads.Count;
    }
}