public class Layer
{
    public int size; // размер текущего слоя
    public float[] neurons; // значения нейронов
    public float[,] weights; // матрица значений весов между этим слоем и следующим

    public Layer(int size, int nextSize) // мы получаем размер этого слоя и следуещего, чтобы узнать сколько у нас весов
    {
        this.size = size;
        neurons = new float[size];
        weights = new float[size, nextSize]; // создаем матрицу весов !!!ВАЖНО!!! в начале у нас идет нейрон текущего слоя, а затем следующего
    }
}
