using System;
using UnityEngine;

public class AI : MonoBehaviour
{
    private int inputsCount = 24;
    private int hiddenCount = 16;
    private int outputCount = 4;
    private GeneticAlgorithm geneticAlgorithm;
    private NN _neuralNetwork;
    private HeadController headController;

    private void Start()
    {
        headController = GetComponent<HeadController>();
        geneticAlgorithm = new GeneticAlgorithm(inputsCount * hiddenCount + hiddenCount * outputCount);
        geneticAlgorithm.Mutate(0.5f);
        Init(geneticAlgorithm); // наша змейка заспавнилась и мы ей инициализировали генку,
                                // или же можно сказать что скрестили генку с нейронкой
    }

    

    void FixedUpdate()
    {
        float[] outputs = _neuralNetwork.FeedForward(Distance.Init(transform.position)); // получаем ответы
        // 0 - up
        // 1 - down
        // 2 - right
        // 3 - left
        
        //print(outputs[0] + "; " + outputs[1] + "; " + outputs[2] + "; " + outputs[3]);
        
        headController.SetOutputs(outputs);

    }

    public void Init(GeneticAlgorithm g)
    {
        geneticAlgorithm = g;
        _neuralNetwork = new NN(inputsCount, hiddenCount, outputCount);
        for (int i = 0; i < inputsCount; i++)
        {
            for (int j = 0; j < hiddenCount; j++)
            {
                _neuralNetwork.layers[0].weights[i, j] = geneticAlgorithm.weights[i + j * inputsCount];
            }
        }
        for (int i = 0; i < hiddenCount; i++)
        {
            for (int j = 0; j < outputCount; j++)
            {
                _neuralNetwork.layers[1].weights[i, j] = geneticAlgorithm.weights[i + j * hiddenCount + inputsCount * hiddenCount];
            }
        }
    }

}
