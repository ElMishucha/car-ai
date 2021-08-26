using System;
using UnityEngine;

public class NN
{
    public Layer[] layers; // создаем массив слоев

    public NN(params int[] neuronsPerLayers) // мы получаем на вход массив чисел - размеры слоев, соответсвенно neuronsPerLayers.Length - колличество слоев
    {
        layers = new Layer[neuronsPerLayers.Length]; // инициалезируем наши слои

        for (int i = 0; i < neuronsPerLayers.Length; i++) // проходимся по всем слоям
        {
            int nextSize = 0; // размер следующего слоя. Оставляем его нулем если это последний слой

            if (i < neuronsPerLayers.Length - 1)
            {
                nextSize = neuronsPerLayers[i + 1];
            }
            layers[i] = new Layer(neuronsPerLayers[i], nextSize); // не забываем что на вход слой получает размер текущего слоя и следующего

            for (int j = 0; j < neuronsPerLayers[i]; j++)                        // - 
            {                                                                    // -
                for (int k = 0; k < nextSize; k++)                               // -
                {                                                                // - Задаём весам случайное значение от -1f, до 1f
                    layers[i].weights[j, k] = UnityEngine.Random.Range(-1f, 1f); // -
                }                                                                // -
            }                                                                    // -
        }
    }

    private const float neuronsMinValue = 0f;
    private const float outputNeuronsMinValue = -1f;

    // просчитываем веса в зависимости от входных данных || *перемножаем веса с нейронами*
    public float[] FeedForward(float[] inputs)
    {
        Array.Copy(inputs, 0, layers[0].neurons, 0, inputs.Length); // копируем входные данные в наш входной слой

        for (int i = 1; i < layers.Length; i++) 
        {
            float min = neuronsMinValue;

            if (i == layers.Length - 1)
            {
                min = outputNeuronsMinValue;
            }   

            Layer l = layers[i - 1];
            Layer l1 = layers[i];

            for (int j = 0; j < l1.size; j++)
            {
                l1.neurons[j] = 0;
                
                for (int k = 0; k < l.size; k++)
                {
                    l1.neurons[j] += l.neurons[k] * l.weights[k, j];
                }
                l1.neurons[j] = Mathf.Min(1f, Mathf.Max(min, l1.neurons[j]));
            }
        }
        return layers[layers.Length - 1].neurons;
    }

}