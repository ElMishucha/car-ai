using System;

public class GeneticAlgorithm
{
    public float[] weights;

    public GeneticAlgorithm(int size)
    {
        weights = new float[size];
        for(int i = 0; i < size; i++)
        {
            weights[i] = UnityEngine.Random.Range(-1f, 1f);
        }
    }

    public GeneticAlgorithm(GeneticAlgorithm a)
    {
        weights = new float[a.weights.Length];
        Array.Copy(a.weights, 0, weights, 0, a.weights.Length);
    }

    public void Mutate(float range)
    {
        for(int i = 0; i < weights.Length; i++)
        {
            if(UnityEngine.Random.value < 0.1) weights[i] += UnityEngine.Random.Range(-range, range);
        }
    }
    
}