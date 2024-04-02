namespace Treblecross
{
    public static class Common<T>
    {
        public static T[] Convert1dArray(T[,] array){
            T[] ret = new T[array.GetLength(1)];
            for (int j = 0; j < array.GetLength(1); j++) {
                ret[j] = array[0, j];
            }
            return ret;
        }
    } 
}