using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practice8
{
    class RangeOfArray
    {
        int[] array;
        int upperOrig;
        int lowerOrig;
        public RangeOfArray(int lower, int upper)
        {
            upperOrig = upper;
            lowerOrig = lower;
            array = new int[upper - lower + 1];
        }

        public int this[int index] {
            get
            {
                if (index >= lowerOrig && index <= upperOrig)
                {
                    return array[index - lowerOrig];
                }
                else
                {
                    throw new IndexOutOfRangeException("Index is out of range.");
                }
            }
            set 
            {
                if (index >= lowerOrig && index <= upperOrig)
                {
                    array[index - lowerOrig] = value;
                }
                else
                {
                    throw new IndexOutOfRangeException("Index is out of range.");
                }
            } 
        }
    }
    class Product
    {
        public int category { get; set; }
        public string name { get; set; }
        public double price { get; set; }

    }

    class Supermarket
    {
        List<Product> products = null;
        public Supermarket()
        {
            products = new List<Product>();
        }

        public double this[int index]
        {
            get
            {
                double sum = 0;
                TimeSpan start = new TimeSpan(8, 0, 0);
                TimeSpan end = new TimeSpan(12, 0, 0);

                foreach (Product prd in products)
                {
                    sum += prd.price;
                }

                if (DateTime.Now.TimeOfDay > start && DateTime.Now.TimeOfDay < end)
                {
                    return sum * 0.95;
                } else
                {
                    return sum; 
                }
            }
        }
    }

    class SalesForecast
    {
        private double[] salesData;
        private int[] months;

        public SalesForecast(double[] data)
        {
            salesData = data;
            months = new int[data.Length];
            for (int i = 0; i < data.Length; i++)
            {
                months[i] = i + 1;
            }
        }

        public double[] PerformLinearRegression()
        {
            double[,] X = new double[salesData.Length, 2];
            double[] Y = salesData;
            for (int i = 0; i < salesData.Length; i++)
            {
                X[i, 0] = 1;
                X[i, 1] = months[i];
            }
            double[] coefficients = LinearRegression(X, Y);
            return coefficients;
        }

        public double[] ForecastNextMonths(double[] coefficients, int numMonths)
        {
            double a = coefficients[1];
            double b = coefficients[0];
            double[] forecast = new double[numMonths];
            for (int month = 1; month <= numMonths; month++)
            {
                forecast[month - 1] = a * (months[months.Length - 1] + month) + b;
            }
            return forecast;
        }
        public static double[] LinearRegression(double[,] X, double[] Y)
        {
            int n = X.GetLength(0);
            int m = X.GetLength(1);
            double[] coefficients = new double[m];
            double[,] XT = new double[m, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    XT[j, i] = X[i, j];
                }
            }
            double[,] XTX = MatrixMultiply(XT, X);
            double[] XTY = MatrixVectorMultiply(XT, Y);
            coefficients = SolveLinearSystem(XTX, XTY);
            return coefficients;
        }
        public static double[,] MatrixMultiply(double[,] A, double[,] B)
        {
            int rowsA = A.GetLength(0);
            int colsA = A.GetLength(1);
            int colsB = B.GetLength(1);
            double[,] C = new double[rowsA, colsB];
            for (int i = 0; i < rowsA; i++)
            {
                for (int j = 0; j < colsB; j++)
                {
                    C[i, j] = 0;
                    for (int k = 0; k < colsA; k++)
                    {
                        C[i, j] += A[i, k] * B[k, j];
                    }
                }
            }
            return C;
        }
        public static double[] MatrixVectorMultiply(double[,] A, double[] B)
        {
            int rowsA = A.GetLength(0);
            int colsA = A.GetLength(1);
            if (colsA != B.Length)
            {
                throw new Exception("Матрица и вектор имеют несовместные размеры для умножения.");
            }
            double[] C = new double[rowsA];
            for (int i = 0; i < rowsA; i++)
            {
                C[i] = 0;
                for (int j = 0; j < colsA; j++)
                {
                    C[i] += A[i, j] * B[j];
                }
            }
            return C;
        }
        public static double[] SolveLinearSystem(double[,] A, double[] B)
        {
            int n = B.Length;
            double[] X = new double[n];
            for (int i = n - 1; i >= 0; i--)
            {
                X[i] = B[i];
                for (int j = i + 1; j < n; j++)
                {
                    X[i] -= A[i, j] * X[j];
                }
                X[i] /= A[i, i];
            }
            return X;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            RangeOfArray rangeOfArray = new RangeOfArray(-9, 15);
            for (int i = -9; i <= 15; i++)
            {
                rangeOfArray[i] = i;
            }
            Console.WriteLine(rangeOfArray[-8]);

        }
    }
}
