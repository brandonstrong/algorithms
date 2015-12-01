using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CuckooHash
{
    class Program
    {
        // This keeps track of the current table length
        private static int tablelength = 3;
        private static Hash hash1;
        private static Hash hash2;
        private static Hash hash3;

        private static bool rehashing = false;

        static void Main(string[] args)
        {

            int[] numbers = new int[] { 21, 30, 19, 89, 70, 31, 5, 44, 58, 88, 72, 4, 32, 24, 61, 33 };
            int runTimes = 0;

            /*
            runTwo(numbers: numbers);
            runThree(numbers: numbers);
            */

            runTimes = 1000;
            writeFile(runTwo(times: runTimes), runTimes, true);
            writeFile(runThree(times: runTimes), runTimes, false);

            runTimes = 5000;
            writeFile(runTwo(times: runTimes), runTimes, true);
            writeFile(runThree(times: runTimes), runTimes, false);

            runTimes = 10000;
            writeFile(runTwo(times: runTimes), runTimes, true);
            writeFile(runThree(times: runTimes), runTimes, false);

            runTimes = 15000;
            writeFile(runTwo(times: runTimes), runTimes, true);
            writeFile(runThree(times: runTimes), runTimes, false);

            runTimes = 20000;
            writeFile(runTwo(times: runTimes), runTimes, true);
            writeFile(runThree(times: runTimes), runTimes, false);

            runTimes = 50000;
            writeFile(runTwo(times: runTimes), runTimes, true);
            writeFile(runThree(times: runTimes), runTimes, false);

            runTimes = 100000;
            writeFile(runTwo(times: runTimes), runTimes, true);
            writeFile(runThree(times: runTimes), runTimes, false);

            runTimes = 200000;
            writeFile(runTwo(times: runTimes), runTimes, true);
            writeFile(runThree(times: runTimes), runTimes, false);

            runTimes = 300000;
            writeFile(runTwo(times: runTimes), runTimes, true);
            writeFile(runThree(times: runTimes), runTimes, false);

            runTimes = 500000;
            writeFile(runTwo(times: runTimes), runTimes, true);
            writeFile(runThree(times: runTimes), runTimes, false);

            runTimes = 1000000;
            writeFile(runTwo(times: runTimes), runTimes, true);
            writeFile(runThree(times: runTimes), runTimes, false);

            runTimes = 10000000;
            writeFile(runTwo(times: runTimes), runTimes, true);
            writeFile(runThree(times: runTimes), runTimes, false);

            runTimes = 100000000;
            writeFile(runTwo(times: runTimes), runTimes, true);
            writeFile(runThree(times: runTimes), runTimes, false);

            runTimes = 1000000000;
            writeFile(runTwo(times: runTimes), runTimes, true);
            writeFile(runThree(times: runTimes), runTimes, false);

            Console.WriteLine("Fin");

            while (Console.ReadKey().Key != ConsoleKey.Enter) ;
        }

        private static void writeFile(int time, int numVals, bool twoTable)
        {
            string path = "c:/users/brandons/desktop/CuckooHash.txt";
            string n = Environment.NewLine;
            string nn = n + n;
            if (twoTable)
            {
                File.AppendAllText(path, n + "For " + numVals + " values CuckooHash   took " + time + "ms.");
            }
            else
            {
                File.AppendAllText(path, n + "For " + numVals + " values CuckooHash+1 took " + time + "ms." + nn);
            }

        }

        private static int runTwo(int[] numbers = null, int times = 0)
        {
            tablelength = 3;
            hash1 = new Hash(5, tablelength);
            hash2 = new Hash(7, tablelength);

            Console.WriteLine("================CUCKOOHASH OUTPUT: {0}================", times);

            DateTime start = DateTime.Now;

            if (numbers != null)
            {
                for (int i = 0; i < numbers.Length; i++)
                {
                    addValueTwoTable(numbers[i]);
                }
            }
            else
            {
                for (int i = 0; i < times; i++)
                {
                    Random r = new Random(i);
                    int j = r.Next() % 100000000;
                    addValueTwoTable(j);
                }
            }

            return (int)(DateTime.Now - start).TotalMilliseconds;
        }

        private static int runThree(int[] numbers = null, int times = 0)
        {
            tablelength = 3;
            hash1 = new Hash(5, tablelength);
            hash2 = new Hash(7, tablelength);
            hash3 = new Hash(11, tablelength);

            Console.WriteLine("================CUCKOOHASH+1 OUTPUT: {0}================\n", times);
            DateTime start = DateTime.Now;

            if (numbers != null)
            {
                for (int i = 0; i < numbers.Length; i++)
                {
                    addValueThreeTable(numbers[i]);
                }
            }
            else
            {
                for (int i = 0; i < times; i++)
                {
                    Random r = new Random(i);
                    int j = r.Next()%100000000;
                    addValueThreeTable(j);
                }
            }
            return (int)(DateTime.Now - start).TotalMilliseconds;
        }

        private static bool addValueTwoTable(int value)
        {
            // Add value to table 1 if its null
            int index1 = hash1.getHash(value);
           // Console.WriteLine("Adding value {0} to table 1 , index {1}", value, index1);
            if (hash1.table[index1] == 0)
            {
                hash1.table[index1] = value;                
            }
            // If table 1 not null, add value in table 1 to table 2 then add new value to table 1
            else
            {
              //  Console.WriteLine("\nCollision in table one!");
                int index2 = hash2.getHash(hash1.table[index1]);
              //  Console.WriteLine("Moving value {0} to table 2 , index {1}", hash1.table[index1], index2);
              //  Console.WriteLine("Adding value {0} to table 2 , index {1}\n", value, index1);
                if (hash2.table[index2] == 0)
                {
                    hash2.table[index2] = hash1.table[index1];                    
                    hash1.table[index1] = value;                    
                }
                // If both table spots are full, rehash.
                else
                {
                 //   Console.WriteLine("\nCollision in table two!\n");
                    // Dont rehash if already rehashing
                    if (rehashing)
                    {
                  //      Console.WriteLine("\nCollision while rehashing\n");
                        return false;
                    }
                    else
                    {
                        rehashing = true;
                        rehashTwoTable(value);
                    }                    
                }
            }

            return true;
        }

        private static void rehashTwoTable(int value)
        {
          //  Console.WriteLine("\n\n========Rehashing tables.========\n\n");
            // Save old tables so values can be re-added to new table.
            int[] tempTable1 = hash1.table;
            int[] tempTable2 = hash2.table;

            // Increase the table length and reset table sizes
            tablelength = tablelength * 2;
            hash1.table = new int[tablelength];
            hash2.table = new int[tablelength];

            try
            {
                for (int i = 0; i < tempTable1.Length; i++)
                {
                    if (tempTable1[i] != 0)
                    {
                        // if a collsion happens in table 2 on reshash, we need to re-try rehashing.
                        if (!addValueTwoTable(tempTable1[i]))
                        {
                            throw new Exception();   
                        }
                    }

                    if (tempTable2[i] != 0)
                    {
                        // if a collsion happens in table 2 on reshash, we need to re-try rehashing.
                        if (!addValueTwoTable(tempTable2[i]))
                        {
                            throw new Exception(); 
                        }
                    }
                }
            }
            catch
            {

          //      Console.WriteLine("\nRe-attempting rehashing tables.\n");
                // reset hash tables and re-try rehashing
                hash1.table = tempTable1;
                hash2.table = tempTable2;
                rehashTwoTable(value);
            }
            rehashing = false;
          //  Console.WriteLine("\n\n========Rehashing Complete.========\n\n");
            addValueTwoTable(value);
        }

        private static bool addValueThreeTable(int value)
        {
            // Add value to table 1 if its null
            int index1 = hash1.getHash(value);
      //      Console.WriteLine("Adding value {0} to table 1 , index {1}", value, index1);
            if (hash1.table[index1] == 0)
            {
                hash1.table[index1] = value;
            }
            // If table 1 not null, add value in table 1 to table 2 then add new value to table 1
            else
            {
      //          Console.WriteLine("\nCollision in table one!");
                int index2 = hash2.getHash(hash1.table[index1]);
      //          Console.WriteLine("Moving value {0} to table 2 , index {1}", hash1.table[index1], index2);
      //          Console.WriteLine("Adding value {0} to table 1 , index {1}\n", value, index1);
                if (hash2.table[index2] == 0)
                {
                    hash2.table[index2] = hash1.table[index1];
                    hash1.table[index1] = value;
                }
                // If both table spots are full, rehash.
                else
                {
      //              Console.WriteLine("\nCollision in table two!");
                    int index3 = hash3.getHash(hash2.table[index2]);
      //              Console.WriteLine("Moving value {0} to table 3 , index {1}", hash2.table[index2], index3);
      //              Console.WriteLine("Moving value {0} to table 2 , index {1}", hash1.table[index1], index2);
      //              Console.WriteLine("Adding value {0} to table 1 , index {1}\n", value, index1);

                    if (hash3.table[index3] == 0)
                    {
                        hash3.table[index3] = hash2.table[index2];
                        hash2.table[index2] = hash1.table[index1];
                        hash1.table[index1] = value;
                    }
                    else
                    {
      //                  Console.WriteLine("\nCollision in table three!");
                        // Dont rehash if already rehashing
                        if (rehashing)
                        {
        //                    Console.WriteLine("\nCollision while rehashing\n");
                            return false;
                        }
                        else
                        {
                            rehashing = true;
                            rehashThreeTable(value);
                        }
                    }                    
                }
            }

            return true;
        }

        private static void rehashThreeTable(int value)
        {
     //       Console.WriteLine("\n\n========Rehashing tables.========\n\n");
            // Save old tables so values can be re-added to new table.
            int[] tempTable1 = hash1.table;
            int[] tempTable2 = hash2.table;
            int[] tempTable3 = hash3.table;

            // Increase the table length and reset table sizes
            tablelength = tablelength * 2;
            hash1.table = new int[tablelength];
            hash2.table = new int[tablelength];
            hash3.table = new int[tablelength];

            try
            {
                for (int i = 0; i < tempTable1.Length; i++)
                {
                    if (tempTable1[i] != 0)
                    {
                        // if a collsion happens in table 2 on reshash, we need to re-try rehashing.
                        if (!addValueThreeTable(tempTable1[i]))
                        {
                            throw new Exception();
                        }
                    }

                    if (tempTable2[i] != 0)
                    {
                        // if a collsion happens in table 2 on reshash, we need to re-try rehashing.
                        if (!addValueThreeTable(tempTable2[i]))
                        {
                            throw new Exception();
                        }
                    }

                    if (tempTable3[i] != 0)
                    {
                        // if a collsion happens in table 3 on reshash, we need to re-try rehashing.
                        if (!addValueThreeTable(tempTable3[i]))
                        {
                            throw new Exception();
                        }
                    }
                }
            }
            catch
            {

     //           Console.WriteLine("\nRe-attempting rehashing tables.\n");
                // reset hash tables and re-try rehashing
                hash1.table = tempTable1;
                hash2.table = tempTable2;
                hash3.table = tempTable3;
                rehashThreeTable(value);
            }
            rehashing = false;
     //       Console.WriteLine("\n\n========Rehashing Complete.========\n\n");
            addValueThreeTable(value);
        }
    }

    // Hash object
    class Hash
    {
        // Hash object constructor
        public Hash(int in_multipler, int tableLength)
        {
            multiplier = in_multipler;
            table = new int[tableLength];
        }

        // Multiplier for hash function
        private int multiplier { get; set; }

        // Hash table
        public int[] table { get; set; }

        // Return the hashed index value
        public int getHash(int value)
        {
            int i = value * multiplier;
            i = i % table.Length;
            return i;
        }
    }
}
