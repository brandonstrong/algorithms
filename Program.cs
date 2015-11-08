using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace AlgoHW5
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread.CurrentThread.Name = "MAIN THREAD";

            // Initalize 2x2 arrays to multiply
            int[,] matrix = new int[2, 2]{
            {1, 2},
            {2, 1}};

            // print off results
            helper(matrix, matrix, true);
            helper(matrix, matrix, false);

            // Initalize 4x4 arrays to multiply
            matrix = new int[4, 4]{
            {1, 2, 3, 4},
            {2, 1, 2, 3},
            {3, 2, 1, 2},
            {4, 3, 2, 1}};

            // print off results
            helper(matrix, matrix, true);
            helper(matrix, matrix, false);

            // Initialize 8x8 arrays to multiply
            matrix = new int[8,8]{
            {1, 2, 3, 4, 5, 6, 7, 8},
            {2, 1, 2, 3, 4, 5, 6, 7},
            {3, 2, 1, 2, 3, 4, 5, 6},
            {4, 3, 2, 1, 2, 3, 4, 5},
            {5, 4, 3, 2, 1, 2, 3, 4},
            {6, 5, 4, 3, 2, 1, 2, 3},
            {7, 6, 5, 4, 3, 2, 1, 2},
            {8, 7, 6, 5, 4, 3, 2, 1}};

            // print off results
            helper(matrix, matrix, true);

            // Initialize 8x8 arrays to multiply
            matrix = new int[16, 16]{
            {1, 2, 3, 4, 5, 6, 7, 8, 1, 2, 3, 4, 5, 6, 7, 8},
            {2, 1, 2, 3, 4, 5, 6, 7, 1, 2, 3, 4, 5, 6, 7, 8},
            {3, 2, 1, 2, 3, 4, 5, 6, 1, 2, 3, 4, 5, 6, 7, 8},
            {4, 3, 2, 1, 2, 3, 4, 5, 1, 2, 3, 4, 5, 6, 7, 8},
            {5, 4, 3, 2, 1, 2, 3, 4, 1, 2, 3, 4, 5, 6, 7, 8},
            {6, 5, 4, 3, 2, 1, 2, 3, 1, 2, 3, 4, 5, 6, 7, 8},
            {7, 6, 5, 4, 3, 2, 1, 2, 1, 2, 3, 4, 5, 6, 7, 8},
            {8, 7, 6, 5, 4, 3, 2, 1, 1, 2, 3, 4, 5, 6, 7, 8},
            {7, 6, 5, 4, 3, 2, 1, 2, 1, 2, 3, 4, 5, 6, 7, 8},
            {7, 6, 5, 4, 3, 2, 1, 2, 1, 2, 3, 4, 5, 6, 7, 8},
            {7, 6, 5, 4, 3, 2, 1, 2, 1, 2, 3, 4, 5, 6, 7, 8},
            {7, 6, 5, 4, 3, 2, 1, 2, 1, 2, 3, 4, 5, 6, 7, 8},
            {7, 6, 5, 4, 3, 2, 1, 2, 1, 2, 3, 4, 5, 6, 7, 8},
            {7, 6, 5, 4, 3, 2, 1, 2, 1, 2, 3, 4, 5, 6, 7, 8},
            {7, 6, 5, 4, 3, 2, 1, 2, 1, 2, 3, 4, 5, 6, 7, 8},
            {7, 6, 5, 4, 3, 2, 1, 2, 1, 2, 3, 4, 5, 6, 7, 8},};

            // print off results
            helper(matrix, matrix, true);
           // helper(matrix, matrix, false);
            
            while (Console.ReadKey().Key != ConsoleKey.Escape) ;
        }

        static void helper(int[,] matrix1, int[,] matrix2, bool useSimple)
        {
            // Initialize array for return
            int size = (int)Math.Sqrt(matrix1.Length);
            int[,] final = null;
            if (useSimple)
            { 
                // Get the multiplied array
                Console.WriteLine("\n\nOutput for {0}x{1} matrix on simple multiplier:", size, size);

                int averageOver = 1000;

                // Initialize datetime for recording computation time
                DateTime now = DateTime.Now;

                for (int i = 0; i < averageOver; i++)
                {
                    // Get the matrix from the simple multiplier
                    final = simpleMult(matrix1, matrix2);
                }

                double simpleTime = (DateTime.Now - now).TotalMilliseconds / averageOver;

                Console.WriteLine("Simple multiplier took {0} milliseconds on average.\n\n", simpleTime);

                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        Console.Write(final[i, j] + ", ");
                    }
                    Console.WriteLine("\n");
                }
            }
            else
            {
                Console.WriteLine("\n\nOutput for {0}x{1} on recursive multiplier:", size, size);

                int averageOver = 1;
                
                // Initialize datetime for recording computation time
                DateTime now = DateTime.Now;

                for (int i = 0; i < averageOver; i++)
                {
                    // Get the matrix from the recursive multiplier
                    final = recurseMult(matrix1, matrix2, 0, 0, 0, 0, size);
                }

                double simpleTime = (DateTime.Now - now).TotalMilliseconds / averageOver;

                Console.WriteLine("Recursive multiplier took {0} milliseconds on average.\n\n", simpleTime);

                // Print out the final array
                Console.WriteLine("\n");
                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        Console.Write(final[i, j] + ", ");
                    }
                    Console.WriteLine("\n");
                }
            }
        }

        // This is the simple implementation of array multiplier
        static int[,] simpleMult(int[,] matrix1, int[,] matrix2)
        {
            // Get the n for nxn and make the return array
            int n = (int)Math.Sqrt(matrix1.Length);
            int[,] final = new int[n, n];

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    // This is the row - by - column multiplication to get the number for final[i, j]
                    final[i, j] = 0;
                    for (int k = 0; k < n; k++)
                    {
                        final[i, j] = final[i, j] + matrix1[i, k] * matrix2[k, j];
                    }
                }
            }
            return final;
        }

        // Assume n is a base 2 number.
        static int[,] recurseMult(int[,] matrix1, int[,] matrix2, int x1, int y1, int x2, int y2, int n)
        {
            // Get the n for nxn and make the return array
            int[,] final = new int[n, n];
            
            // Return the base case 
            if (n == 1)
            {                
                final[0, 0] = matrix1[x1, y1] * matrix2[x2, y2];
                return final;
            }
            else
            {
                int x = n / 2;
                int[,] c11 = new int[x, x];
                int[,] c12 = new int[x, x];
                int[,] c21 = new int[x, x];
                int[,] c22 = new int[x, x];
                int[,] t11 = new int[x, x];
                int[,] t12 = new int[x, x];
                int[,] t21 = new int[x, x];
                int[,] t22 = new int[x, x];

                // Start threads
                Thread one = new Thread(delegate() { c11 = recurseMult(matrix1, matrix2, x1, y1, x2, y2, x); }); one.Start();
                Thread two = new Thread(delegate() { c12 = recurseMult(matrix1, matrix2, x1, y1, x2, y2 + x, x); }); two.Start();
                Thread three = new Thread(delegate() { c21 = recurseMult(matrix1, matrix2, x1 + x, y1, x2, y2, x); }); three.Start();
                Thread four = new Thread(delegate() { c22 = recurseMult(matrix1, matrix2, x1 + x, y1, x2, y2 + x, x); }); four.Start();
                Thread five = new Thread(delegate() { t11 = recurseMult(matrix1, matrix2, x1, y1 + x, x2 + x, y2, x); }); five.Start();
                Thread six = new Thread(delegate() { t12 = recurseMult(matrix1, matrix2, x1, y1 + x, x2 + x, y2 + x, x); }); six.Start();
                Thread seven = new Thread(delegate() { t21 = recurseMult(matrix1, matrix2, x1 + x, y1 + x, x2 + x, y2, x); }); seven.Start();
                Thread eight = new Thread(delegate() { t22 = recurseMult(matrix1, matrix2, x1 + x, y1 + x, x2 + x, y2 + x, x); }); eight.Start();

                // Sync threads
                while (
                    (one.ThreadState != ThreadState.Stopped) ||
                    (two.ThreadState != ThreadState.Stopped) ||
                    (three.ThreadState != ThreadState.Stopped) ||
                    (four.ThreadState != ThreadState.Stopped) ||
                    (five.ThreadState != ThreadState.Stopped) ||
                    (six.ThreadState != ThreadState.Stopped) ||
                    (seven.ThreadState != ThreadState.Stopped) ||
                    (eight.ThreadState != ThreadState.Stopped)
                    ) ;


                // Get our Cij's + Tij;s
                for (int i = 0; i < x; i++)
                {
                    for (int j = 0; j < x; j++)
                    {
                        final[i, j] = c11[i, j] + t11[i, j];
                        final[i, j + x] = c12[i, j] + t12[i, j];
                        final[i + x, j] = c21[i, j] + t21[i, j];
                        final[i + x, j + x] = c22[i, j] + t22[i, j];
                    }                        
                }
            }
            return final;
        }
    }
}
