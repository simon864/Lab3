﻿using System;
namespace Lab3
{

    public class Matrix
    {
        private double[,] matrix;

        public int Size { get; }
        public Matrix(int size)
        {
            Size = size;
            matrix = new double[Size, Size];
        }

        public Matrix(int size, double minValue, double maxValue)
        {
            Size = size;
            matrix = new double[Size, Size];
            var random = new Random();
            for (int ColumnCounter = 0; ColumnCounter < Size; ++ColumnCounter)
            {
                for (int RowCounter = 0; RowCounter < Size; ++RowCounter)
                {
                    matrix[ColumnCounter, RowCounter] = random.NextDouble() * (maxValue - minValue) + minValue;
                }
            }
        }

        public double this[int ColumnCounter, int RowCounter]
        {
            get { return matrix[ColumnCounter, RowCounter]; }
            set { matrix[ColumnCounter, RowCounter] = value; }
        }

        public static Matrix operator +(Matrix MatrixA, Matrix MatrixB)
        {
            if (MatrixA.Size != MatrixB.Size)
                throw new ArgumentException("Матрицы должны быть одного размера");

            var result = new Matrix(MatrixA.Size);

            for (int ColumnCounter = 0; ColumnCounter < result.Size; ++ColumnCounter)
            {
                for (int RowCounter = 0; RowCounter < result.Size; ++RowCounter)
                {
                    result[ColumnCounter, RowCounter] = MatrixA[ColumnCounter, RowCounter] + MatrixB[ColumnCounter, RowCounter];
                }
            }

            return result;
        }

        public static Matrix operator *(Matrix MatrixA, Matrix MatrixB)
        {
            if (MatrixA.Size != MatrixB.Size)
                throw new ArgumentException("Матрицы должны быть одного размера");

            var result = new Matrix(MatrixA.Size);

            for (int ColumnCounter = 0; ColumnCounter < result.Size; ++ColumnCounter)
            {
                for (int RowCounter = 0; RowCounter < result.Size; ++RowCounter)
                {
                    double sum = 0;
                    for (int Index = 0; Index < result.Size; ++Index)
                    {
                        sum += MatrixA[ColumnCounter, Index] * MatrixB[Index, RowCounter];
                    }
                    result[ColumnCounter, RowCounter] = sum;
                }
            }

            return result;
        }

        public static bool operator >(Matrix MatrixA, Matrix MatrixB)
        {
            if (MatrixA.Size != MatrixB.Size)
                throw new ArgumentException("Матрицы должны быть одного размера");

            for (int ColumnCounter = 0; ColumnCounter < MatrixA.Size; ++ColumnCounter)
            {
                for (int RowCounter = 0; RowCounter < MatrixA.Size; ++RowCounter)
                {
                    if (MatrixA[ColumnCounter, RowCounter] <= MatrixB[ColumnCounter, RowCounter])
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public static bool operator <(Matrix MatrixA, Matrix MatrixB)
        {
            if (MatrixA.Size != MatrixB.Size)
                throw new ArgumentException("Матрицы должны быть одного размера");

            for (int ColumnCounter = 0; ColumnCounter < MatrixA.Size; ColumnCounter++)
            {
                for (int RowCounter = 0; RowCounter < MatrixA.Size; RowCounter++)
                {
                    if (MatrixA[ColumnCounter, RowCounter] >= MatrixB[ColumnCounter, RowCounter])
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public static bool operator >=(Matrix MatrixA, Matrix MatrixB)
        {
            if (MatrixA.Size != MatrixB.Size)
                throw new ArgumentException("Матрицы должны быть одного размера");

            for (int ColumnCounter = 0; ColumnCounter < MatrixA.Size; ++ColumnCounter)
            {
                for (int RowCounter = 0; RowCounter < MatrixA.Size; ++RowCounter)
                {
                    if (MatrixA[ColumnCounter, RowCounter] < MatrixB[ColumnCounter, RowCounter])
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public static bool operator <=(Matrix MatrixA, Matrix MatrixB)
        {
            if (MatrixA.Size != MatrixB.Size)
                throw new ArgumentException("Матрицы должны быть одного размера");

            for (int ColumnCounter = 0; ColumnCounter < MatrixA.Size; ColumnCounter++)
            {
                for (int RowCounter = 0; RowCounter < MatrixA.Size; RowCounter++)
                {
                    if (MatrixA[ColumnCounter, RowCounter] > MatrixB[ColumnCounter, RowCounter])
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public static bool operator ==(Matrix MatrixA, Matrix MatrixB)
        {
            if (MatrixA is null)
                return MatrixB is null;

            if (MatrixB is null || MatrixA.Size != MatrixB.Size)
                return false;

            for (int ColumnCounter = 0; ColumnCounter < MatrixA.Size; ColumnCounter++)
            {
                for (int RowCounter = 0; RowCounter < MatrixA.Size; RowCounter++)
                {
                    if
                                                (MatrixA[ColumnCounter, RowCounter] != MatrixB[ColumnCounter, RowCounter])
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public static bool operator !=(Matrix MatrixA, Matrix MatrixB)
        {
            return !(MatrixA == MatrixB);
        }

        public static explicit operator bool(Matrix matrix)
        {
            return matrix != null && matrix.Size > 0;
        }

        public double Determinant()
        {
            if (Size == 1)
            {
                return matrix[0, 0];
            }
            else if (Size == 2)
            {
                return matrix[0, 0] * matrix[1, 1] - matrix[0, 1] * matrix[1, 0];
            }
            else
            {
                double result = 0;
                int sign = 1;
                for (int ColumnCounter = 0; ColumnCounter < Size; ++ColumnCounter)
                {
                    var subMatrix = SubMatrix(ColumnCounter, 0);
                    result += sign * matrix[ColumnCounter, 0] * subMatrix.Determinant();
                    sign = -sign;
                }
                return result;
            }
        }

        public Matrix Inverse()
        {
            var determinant = Determinant();
            if (determinant == 0)
            {
                throw new InvalidOperationException("Невозможно обратить матрицу");
            }
            var result = new Matrix(Size);

            int sign = 1;
            for (int ColumnCounter = 0; ColumnCounter < Size; ColumnCounter++)
            {
                for (int RowCounter = 0; RowCounter < Size; RowCounter++)
                {
                    var subMatrix = SubMatrix(ColumnCounter, RowCounter);
                    result[RowCounter, ColumnCounter] = sign * subMatrix.Determinant() / determinant;
                    sign = -sign;
                }
            }

            return result;
        }

        private Matrix SubMatrix(int rowToRemove, int columnToRemove)
        {
            var subMatrix = new Matrix(Size - 1);

            int subRow = 0;
            for (int row = 0; row < Size; row++)
            {
                if (row == rowToRemove)
                    continue;

                int subColumn = 0;
                for (int column = 0; column < Size; column++)
                {
                    if (column == columnToRemove)
                        continue;

                    subMatrix[subRow, subColumn] = matrix[row, column];
                    subColumn++;
                }

                subRow++;
            }

            return subMatrix;
        }

        public override string ToString()
        {
            string result = "";
            for (int ColumnCounter = 0; ColumnCounter < Size; ++ColumnCounter)
            {
                for (int RowCounter = 0; RowCounter < Size; ++RowCounter)
                {
                    result += $"{matrix[ColumnCounter, RowCounter]} ";
                }
                result += "\n";
            }
            return result;
        }

        public int CompareTo(Matrix other)
        {
            if (other is null)
                return 1;

            if (Size != other.Size)
                return Size.CompareTo(other.Size);

            for (int ColumnCounter = 0; ColumnCounter < Size; ++ColumnCounter)
            {
                for (int RowCounter = 0; RowCounter < Size; ++RowCounter)
                {
                    int compare = matrix[ColumnCounter, RowCounter].CompareTo(other.matrix[ColumnCounter, RowCounter]);
                    if (compare != 0)
                        return compare;
                }
            }

            return 0;
        }

        public override bool Equals(object obj)
        {
            if (obj is null || !(obj is Matrix))
                return false;

            return this == (Matrix)obj;
        }

        public override int GetHashCode()
        {
            int hashCode = 17;
            for (int ColumnCounter = 0; ColumnCounter < Size; ++ColumnCounter)
            {
                for (int RowCounter = 0; RowCounter < Size; ++RowCounter)
                {
                    hashCode = hashCode * 23 + matrix[ColumnCounter, RowCounter].GetHashCode();
                }
            }
            return hashCode;
        }

        public Matrix Clone()
        {
            var clone = new Matrix(Size);
            for (int ColumnCounter = 0; ColumnCounter < Size; ++ColumnCounter)
            {
                for (int RowCounter = 0; RowCounter < Size; ++RowCounter)
                {
                    clone[ColumnCounter, RowCounter] = matrix[ColumnCounter, RowCounter];
                }
            }
            return clone;
        }
    }
}
