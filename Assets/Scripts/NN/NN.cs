using System;
using UnityEngine;

public class NN
{
    public Layer[] layers; // ������� ������ �����

    public NN(params int[] neuronsPerLayers) // �� �������� �� ���� ������ ����� - ������� �����, ������������� neuronsPerLayers.Length - ����������� �����
    {
        layers = new Layer[neuronsPerLayers.Length]; // �������������� ���� ����

        for (int i = 0; i < neuronsPerLayers.Length; i++) // ���������� �� ���� �����
        {
            int nextSize = 0; // ������ ���������� ����. ��������� ��� ����� ���� ��� ��������� ����

            if (i < neuronsPerLayers.Length - 1)
            {
                nextSize = neuronsPerLayers[i + 1];
            }
            layers[i] = new Layer(neuronsPerLayers[i], nextSize); // �� �������� ��� �� ���� ���� �������� ������ �������� ���� � ����������

            for (int j = 0; j < neuronsPerLayers[i]; j++)                        // - 
            {                                                                    // -
                for (int k = 0; k < nextSize; k++)                               // -
                {                                                                // - ����� ����� ��������� �������� �� -1f, �� 1f
                    layers[i].weights[j, k] = UnityEngine.Random.Range(-1f, 1f); // -
                }                                                                // -
            }                                                                    // -
        }
    }

    private const float neuronsMinValue = 0f;
    private const float outputNeuronsMinValue = -1f;

    // ������������ ���� � ����������� �� ������� ������ || *����������� ���� � ���������*
    public float[] FeedForward(float[] inputs)
    {
        Array.Copy(inputs, 0, layers[0].neurons, 0, inputs.Length); // �������� ������� ������ � ��� ������� ����

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