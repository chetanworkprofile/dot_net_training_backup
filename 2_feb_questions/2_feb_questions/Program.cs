using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// program 1
// program to count and display frequency of each element in array
// approach -> create freq array freq[i] = -1 not visited  freq [i] = 0 dupl  freq[i] = n frequency
/*namespace _2_feb_questions
{
    internal class Program
    {
        static void Main()
        {
            int n;
            Console.WriteLine("Enter number of elements in array \n");
            n = Convert.ToInt32(Console.ReadLine());

            int[] arr1 = new int[n];
            int[] freq = new int[n];
            try
            {

                Console.WriteLine("Enter each element of array");
                for (int i = 0; i < n; i++)
                {
                    arr1[i] = Convert.ToInt32(Console.ReadLine());
                    freq[i] = -1;
                }


                //processing
                int dupls = 0;
                for (int i = 0; i < n; i++)
                {
                    dupls = 1;
                    for (int j = i + 1; j < n; j++)
                    {
                        if (arr1[i] == arr1[j])
                        {
                            freq[j] = 0;
                            dupls++;
                        }
                    }
                    if (freq[i] != 0)
                    {
                        freq[i] = dupls;
                    }
                }
                //output
                Console.WriteLine("Frequency of each element ");
                for (int i = 0; i < n; i++)
                {
                    if (freq[i] != 0)
                    {
                        Console.WriteLine(arr1[i] + " -> " + freq[i] + "\n");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);

            }
            Console.ReadKey();
        }
    }
}*/


//program 2
// wap to delete an element from desired index
//appr ->  take index move elements from next index one step back or create new array and transfer accordingly

/*namespace _2_feb_questions
{
    internal class Program
    {
        static void Main()
        {
            int n;
            Console.WriteLine("Enter number of elements in array \n");
            n = Convert.ToInt32(Console.ReadLine());

            int[] arr1 = new int[n];
            int index;
            try
            {
                //input
                Console.WriteLine("Enter each element of array");
                for (int i = 0; i < n; i++)
                {
                    arr1[i] = Convert.ToInt32(Console.ReadLine());
                }
                int len = arr1.Length;
                
                Console.WriteLine("Enter index to delete");
                index = Convert.ToInt32(Console.ReadLine());
                if (index > len)
                {
                    Console.WriteLine("invalid input index out of range");
                    Console.ReadKey();
                    return;
                }
                //processing
                n--;
                for (int i = index; i < n; i++)
                {
                    arr1[i] = arr1[i + 1];

                }


                //output
                Console.WriteLine("Output array ");
                for (int i = 0; i < n; i++)
                {
                    Console.WriteLine(arr1[i] + " ");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            Console.ReadKey();
        }
    }
}*/


//program 3
//wap to find sum of both the diagonals of a square matrix
//apro ->  sum var = 0 traverse the matrix diagonally;
/*
namespace _2_feb_questions
{
    internal class Program
    {
        static void Main()
        {
            int n;
            Console.WriteLine("Enter dimension of square matrix \n");
            n = Convert.ToInt32(Console.ReadLine());

            int[,] arr1 = new int[n, n];
            try
            {
                //input
                Console.WriteLine("Enter each element of matrix row-wise: ");
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        arr1[i, j] = Convert.ToInt32(Console.ReadLine());
                    }
                }


                //processing
                int leftsum = 0;
                int rightsum = 0;

                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        if (i == j)
                        {
                            leftsum += arr1[i, j];
                        }
                        else if (i + j == (n - 1))
                        {
                            rightsum += arr1[i, j];
                        }
                        else
                        {
                            continue;
                        }
                    }
                }


                //output
                Console.WriteLine("total diagonal sum: " + (leftsum + rightsum));

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            Console.ReadKey();
        }
    }
}*/

//program 4 
//wap to find max occuring character in a string show char and count
//appr -> traverse string increment in freq array or hashmap
/*
namespace _2_feb_questions
{
    internal class Program
    {
        static void Main()
        {
            const int size = 256;
            try
            {
                //input
                Console.WriteLine("enter string to get max occuring character");
                string inp = Console.ReadLine();
                int n = inp.Length;

                //processing
                int[] freq = new int[size];
                for (int i = 0; i < n; i++)
                {
                    freq[inp[i]]++;
                }
                int index = 0;
                int max = int.MinValue;
                for (int i = 0; i < size; i++)
                {
                    if (freq[i] > max)
                    {
                        max = freq[i];
                        index = i;
                    }
                }
                char ch = (char)index;
                //output
                Console.WriteLine("max character in string: " + ch);
                Console.WriteLine("no of occurences: " + max);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);

            }
            Console.ReadKey();
        }
    }
}*/

//program 5
// wap to insert a substring before the first occurence of a string
//ln1 = <input string>   ln2 = <string to be searched>  ln3 = <string to be inserted>
//appr -> search ln2 in ln1 break from before first occurence and then 

/*
namespace _2_feb_questions
{
    internal class Program
    {
        static void Main()
        {
            try
            {
                //input
                Console.WriteLine("Enter original string: ");
                string org_string = Console.ReadLine();
                int org_length = org_string.Length;

                Console.WriteLine("Enter string to be searched : ");
                string find_string = Console.ReadLine();

                Console.WriteLine("Enter string to insert : ");
                string insert_string = Console.ReadLine();

                //processing
                //locate position of first occurence
                int i = 0;
                bool found = false;
                //i = org_string.IndexOf(find_string);                  //predefined functions
                //org_string = org_string.Insert(i, insert_string);

                for (int j = 0; j < org_length; j++)
                {
                    if (org_string[j] == find_string[0])
                    {
                        int temp = j;
                        for (int k = 1; k < find_string.Length; k++)
                        {
                            j++;
                            if (org_string[j] != find_string[k])
                            {
                                break;
                            }
                            if (k == find_string.Length - 1)
                            {
                                found = true;
                            }
                        }
                        if (found)
                        {
                            i = temp;
                        }
                    }
                }
                org_string = org_string.Insert(i, insert_string);

                //output

                Console.WriteLine("output \n" + org_string);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);

            }
            Console.ReadKey();
        }
    }
}


*/


// USING FILE IO

//pro 1
/*
namespace _2_feb_questions
{
    internal class Program
    {
        static void Main()
        {

            try
            {
                *//*string fileName = @"pro1.txt";
                using (StreamWriter fileStr = File.CreateText(fileName))
                {
                }*//*
                string fileName = @"pro1.txt";
                string[] data = File.ReadAllLines(fileName);
                int size = Convert.ToInt32(data[0]);
                string[] ar = data[1].Split(' ');

                int[] arr1 = new int[size];
                int[] freq = new int[size];

                for (int i = 0; i < size; i++)
                {
                    arr1[i] = Convert.ToInt32(ar[i]);
                    freq[i] = -1;
                }


                //processing
                int dupls = 0;
                for (int i = 0; i < size; i++)
                {
                    dupls = 1;
                    for (int j = i + 1; j < size; j++)
                    {
                        if (arr1[i] == arr1[j])
                        {
                            freq[j] = 0;
                            dupls++;
                        }
                    }
                    if (freq[i] != 0)
                    {
                        freq[i] = dupls;
                    }
                }
                //output

                StringBuilder sb = new StringBuilder();

                File.AppendAllText(fileName, "\n Output \n");
                for (int i = 0; i < size; i++)
                {
                    if (freq[i] != 0)
                    {
                        sb.Append(arr1[i].ToString() + " -> " + freq[i].ToString() + "\n");
                    }
                }
                File.AppendAllText(fileName, sb.ToString());

            }
            catch (Exception e)
            {
                Console.WriteLine(e);

            }
            Console.ReadKey();
        }
    }
}*/

//pro2

namespace _2_feb_questions
{
    internal class Program
    {
        static void Main()
        {

            try
            {
                //input
                string fileName = @"pro2.txt";
                string[] data = File.ReadAllLines(fileName);
                int n = Convert.ToInt32(data[0]);
                string[] ar = data[1].Split(' ');

                int[] arr1 = new int[n];
                int index = Convert.ToInt32(data[2]);

                for (int i = 0; i < n; i++)
                {
                    arr1[i] = Convert.ToInt32(ar[i]);
                }
                int len = arr1.Length;
                if (index > len)
                {
                    Console.WriteLine("invalid input index out of range");
                    Console.ReadKey();
                    return;
                }
                //processing
                n--;
                for (int i = index; i < n; i++)
                {
                    arr1[i] = arr1[i + 1];

                }

                //output
                StringBuilder sb = new StringBuilder();

                File.AppendAllText(fileName, "\n Output \n");
                for (int i = 0; i < n; i++)
                {
                    sb.Append(arr1[i].ToString() + " ");
                }
                File.AppendAllText(fileName, sb.ToString());


            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            Console.ReadKey();
        }
    }
}

//pro3
/*
namespace _2_feb_questions
{
    internal class Program
    {
        static void Main()
        {


            try
            {
                //input
                string fileName = @"pro3.txt";
                string[] data = File.ReadAllLines(fileName);
                int n = Convert.ToInt32(data[0]);

                int[,] arr1 = new int[n, n];
                string[][] ar = new string[n][];

                for (int i = 0; i < n; i++)
                {
                    ar[i] = data[i + 1].Split(' ');
                }


                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        arr1[i, j] = Convert.ToInt32(ar[i][j]);
                    }
                }

                //processing
                int leftsum = 0;
                int rightsum = 0;

                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        if (i == j)
                        {
                            leftsum += arr1[i, j];
                        }
                        else if (i + j == (n - 1))
                        {
                            rightsum += arr1[i, j];
                        }
                        else
                        {
                            continue;
                        }
                    }
                }


                //output
                //Console.WriteLine("total diagonal sum: " + (leftsum + rightsum));

                File.AppendAllText(fileName, "\n Output \n");
                File.AppendAllText(fileName, "total sum of diagonals is " + (leftsum + rightsum).ToString());

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            Console.ReadKey();
        }
    }
}
*/

//pro 4
/*
namespace _2_feb_questions
{
    internal class Program
    {
        static void Main()
        {
            const int size = 256;
            try
            {
                //input
                string fileName = @"pro4.txt";
                string[] data = File.ReadAllLines(fileName);

                //Console.WriteLine("enter string to get max occuring character");
                string inp = data[0];
                int n = inp.Length;

                //processing
                int[] freq = new int[size];
                for (int i = 0; i < n; i++)
                {
                    freq[inp[i]]++;
                }
                int index = 0;
                int max = int.MinValue;
                for (int i = 0; i < size; i++)
                {
                    if (freq[i] > max)
                    {
                        max = freq[i];
                        index = i;
                    }
                }
                char ch = (char)index;
                //output
                StringBuilder sb = new StringBuilder();

                File.AppendAllText(fileName, "\n Output \n");

                sb.Append("Max character in string: " + ch.ToString());
                sb.Append("\n No. of Occurences: " + max.ToString());

                File.AppendAllText(fileName, sb.ToString());


            }
            catch (Exception e)
            {
                Console.WriteLine(e);

            }
            Console.ReadKey();
        }
    }
}
*/

//pro 5
/*
namespace _2_feb_questions
{
    internal class Program
    {
        static void Main()
        {
            try
            {
                //input
                string fileName = @"pro5.txt";
                string[] data = File.ReadAllLines(fileName);


                //Console.WriteLine("Enter original string: ");
                string org_string = data[0];
                int org_length = org_string.Length;

                //Console.WriteLine("Enter string to be searched : ");
                string find_string = data[1];

                //Console.WriteLine("Enter string to insert : ");
                string insert_string = data[2];

                //processing
                //locate position of first occurence
                int i = 0;
                bool found = false;
                //i = org_string.IndexOf(find_string);  

                Console.WriteLine(org_string + " " + find_string + " " + insert_string);

                for (int j = 0; j < org_length; j++)
                {
                    if (org_string[j] == find_string[0])
                    {
                        int temp = j;
                        for (int k = 1; k < find_string.Length; k++)
                        {
                            j++;
                            if (org_string[j] != find_string[k])
                            {
                                break;
                            }
                            if (k == find_string.Length - 1)
                            {
                                found = true;
                            }
                        }
                        if (found)
                        {
                            i = temp;
                        }
                    }
                }
                org_string = org_string.Insert(i, insert_string);

                //output

                //Console.WriteLine("output \n" + org_string);
                StringBuilder sb = new StringBuilder();

                File.AppendAllText(fileName, "\n Output \n");
                File.AppendAllText(fileName, org_string.ToString());

            }
            catch (Exception e)
            {
                Console.WriteLine(e);

            }
            Console.ReadKey();
        }
    }
}*/