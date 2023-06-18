using System;

namespace Lab3
{
    class Program
    {
        static void Main(string[] args)
        {
            var random = new Random();

            
            var Matrix1 = new Matrix(3);
            for (int ColumnCounter = 0; ColumnCounter < 3; ++ColumnCounter)
            {
                for (int RowCounter = 0; RowCounter < 3; ++RowCounter)
                {
                    Matrix1[ColumnCounter, RowCounter] = random.Next(10);
                }
            }
            Console.WriteLine($"Матрица(1) =\n{Matrix1}");

            var
            Matrix2 = new Matrix(3);
            for (int ColumnCounter = 0; ColumnCounter < 3; ++ColumnCounter)
            {
                for (int RowCounter = 0; RowCounter < 3; ++RowCounter)
                {
                    Matrix2[ColumnCounter, RowCounter] = random.Next(10);
                }
            }
            Console.WriteLine($"Матрица(2) =\n{Matrix2}");

        
            Console.WriteLine($"Матрица(1) + Матрица(2) =\n{Matrix1 + Matrix2}");

            Console.WriteLine($"Матрица(1) * Матрица(2) =\n{Matrix1 * Matrix2}");

            Console.WriteLine($"Матрица(1) > Матрица(2): {Matrix1 > Matrix2}");
            Console.WriteLine($"Матрица(1) >= Матрица(2): {Matrix1 >= Matrix2}");
            Console.WriteLine($"Матрица(1) <= Матрица(2): {Matrix1 <= Matrix2}");
            Console.WriteLine($"Матрица(1) == Матрица(2): {Matrix1 == Matrix2}");
            Console.WriteLine($"Матрица(1) != Матрица(2): {Matrix1 != Matrix2}");

            Console.WriteLine($"Детерминант Матрица(1) равен: {Matrix1.Determinant()}");

            try
            {
                var inverseA = Matrix1.Inverse();
                Console.WriteLine($"Инверсия Матрица(1) равна:\n{inverseA}");
            }
            catch (MatrixNotInvertibleException ex)
            {
                Console.WriteLine(ex.Message);
            }

         
            var c = Matrix1.Clone();
            Console.WriteLine($"Матрица(3) =\n{c}");
            Console.WriteLine($"Матрица(1) == Матрица(3): {Matrix1 == c}");

            
            try
            {
                var d = new Matrix(0);
            }
            catch (InvalidMatrixSizeException ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadKey();
        }
    }
}

