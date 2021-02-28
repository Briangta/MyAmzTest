using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazon2ExArthur
{
    class Program
    {
        static void Main(string[] args)
        {
            ///Graph Ex.   Sum of (Sqrt of each group Lenght)
            var res = GenerateData(11, new string[]
            {
                "1 2",
                "4 11",
                "1 3",
                "3 4",
                "3 5",
                "8 9",
                "4 10",
            });

            Console.WriteLine($"Ex. 1: sqrt per group(tree) length {res}");



            /// Ex 2: the biggest square (deleting row/columns Separators)
            int area = ColumnCalc(10, 7, new int[] { 2, 4 }, new int[] { 2, 6, 7 });
            Console.WriteLine($"Ex. 2: max square area  {area}");

            Console.ReadLine();
        }

        /// <summary>
        /// Generate calc (Ex 1)
        /// </summary>
        /// <param name="len">nodes count</param>
        /// <param name="nodes">nodes</param>
        /// <returns>///Graph Ex.   Sum of (Sqrt of each group Lenght)</returns>
        public static int GenerateData(int len, string[] connections)
        {
            List<List<int>> array = new List<List<int>>();
            int[][] conn = connections.Select(r => r.Split(' ').Select(m => int.Parse(m)).ToArray())
                .OrderBy(r => r[0]).ToArray();

            foreach (var co in conn)
            {
                var gFound = array.Where(m => m.Contains(co[0]) || m.Contains(co[1])).FirstOrDefault();
                if (gFound != null)
                    array[array.IndexOf(gFound)].AddRange(new int[] { co[0], co[1] });
                else
                    array.Add(new int[] { co[0], co[1] }.ToList());
            }

            for (int i = 1; i <= len; i++)
            {
                if (array.Where(m => m.Contains(i)).Count() == 0)
                    array.Add(new int[] { i }.ToList());
            }

            var result = array.Select(r => new { Len = r.Distinct().Count(), sqr = Math.Ceiling(Math.Sqrt(r.Distinct().Count())) })
                .ToList();

            return result.Select(r => (int)r.sqr).Sum();
        }

        /// <summary>
        /// calc de area
        /// </summary>
        /// <param name="w">width</param>
        /// <param name="h">height</param>
        /// <param name="cr">columns to remove</param>
        /// <param name="rr">rows to remove</param>
        /// <returns>the biggest area of ( square (deleting row/columns Separators))</returns>
        public static int ColumnCalc(int w, int h, int[] cr, int[] rr)
        {
            var matrix = new int[h + 1, w + 1];

            for (int i = 0; i <= h; i++)
            {
                for (int ww = 0; ww <= w; ww++)
                {
                    if (cr.Contains(i + 1) && rr.Contains(ww + 1))
                    {
                        matrix[i, ww] = 1;
                        matrix[i + 1, ww] = 1;
                        matrix[i, ww + 1] = 1;
                        matrix[i + 1, ww + 1] = 1;
                    }

                    Console.Write(matrix[i, ww]);
                }

                Console.WriteLine();
            }

            int max = 0;

            while (!(matrix.Cast<int>().ToList().Where(r => r == 1).Count() == 0))
            {
                int sum = 0, firstX = 0;
                bool found1 = false;

                for (int i = 0; i <= h; i++)
                {
                    for (int ww = 0; ww <= w; ww++)
                    {
                        if (matrix[i, ww] == 1)
                        {

                            if (!found1)
                            {
                                found1 = true;
                                firstX = ww;
                            }
                            sum++;
                            matrix[i, ww] = -1;
                        }

                        if (found1 && ww > firstX && (ww + 1 > w || matrix[i, ww + 1] == 0))
                            break;
                    }


                    if (found1 && (i + 1 > h || matrix[i + 1, firstX] == 0))
                    {
                        Console.WriteLine("Square Length " + sum);
                        if (sum > max)
                            max = sum;
                        break;
                    }
                }
            }

            return max;
        }
    }
}