using System;
namespace Lab3
{

    public class InvalidMatrixSizeException : Exception
    {
        public InvalidMatrixSizeException(int size) : base($"Неправильный размер матрциы {size}")
        {
        }
    }

    public class MatrixNotInvertibleException : Exception
    {
        public MatrixNotInvertibleException() : base("Невозможно обратить матрицу")
        {
        }
    }
}