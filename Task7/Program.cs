using System;


//	Сгенерировать все сочетания из N элементов по K без повторений и выписать их в лексикографическом порядке.

namespace Task7
{
    class Program
    {
        static int[] arr;
        static int N, K; 
        static double count;
        static void Main(string[] args)
        {
            bool end = false;
            do
            {
                N = CheckInt("Введите N", true);
                K = CheckInt("Введите K", false);
                count = CountNum(N, K);
                if (count <= 0) Console.WriteLine("Количество сочетаний из {0} по {1} слишком велико", N, K);
                else Console.WriteLine("Количество сочетаний из {0} по {1}: {2}", N, K, count);
                arr = new int[K];
                Console.WriteLine("Сочетания из {0} по {1} в лексикографическом порядке без повторений:", N, K);
                CreateCombs(0, 0);
                end = CheckKey();
            } while (!end);
        }
        //генератор сочетаний
        public static void CreateCombs(int pos, int maxUsed)
        {
            if (pos == K)
            {
                PrintMas(arr);
            } 
            else
                for (int i = maxUsed + 1; i <= N; i++)
                {
                    arr[pos] = i;
                    CreateCombs(pos + 1, i);
                }
        }
        //вывод сочетания
        public static void PrintMas(int[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                Console.Write(arr[i] + " ");
                if ((i + 1) % K == 0) Console.WriteLine();
            }
        }
        //вычисление факториала
        public static int GetFactorial(int num, bool notAll)
        {
            int res = num;
            if (notAll)
                if (K > N - K)
                    while (num > K + 1)
                    {
                        num--;
                        res *= num;
                    }
                else
                    while (num > N - K + 1)
                    {
                        num--;
                        res *= num;
                    }
            else while (num > 1)
                {
                    num--;
                    res *= num;
                }
            return res;
        }
        //подсчет количества сочетаний
        public static int CountNum(int N, int K)
        {
            int res = 0;
            if (N == K) res = 1;
            else if (K >= N - K) res = GetFactorial(N, true) / GetFactorial(N - K, false);
            else res = GetFactorial(N, true) / GetFactorial(K, false);
            return res;
        }
        //проверка ввода целого числа
        public static int CheckInt(string s, bool isN)
        {
            int num;
            bool okay = false;
            Console.WriteLine(s);
            do
            {
                if (isN) okay = Int32.TryParse(Console.ReadLine(), out num) && num > 0 && num < 31;
                else okay = Int32.TryParse(Console.ReadLine(), out num) && num > 0 && num <= N;
                if (!okay)
                    if (isN) PrintError("Ошибка ввода. N должно быть целым положительным числом, не превышающим 30");
                    else PrintError("Ошибка ввода. K должно быть целым положительным числом, не превышающим N");
            } while (!okay);
            return num;
        }
        //вывод соощений об ошибке
        public static void PrintError(string s)
        {
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(s);
            Console.ResetColor();
        }
        public static bool CheckKey()
        {
            bool next, end = false;
            int keyNum;
            Console.WriteLine("Для выхода из программы нажмите Esc, для генерации других сочетаний нажмите Enter.");
            do
            {
                keyNum = Console.ReadKey().KeyChar;
                next = (keyNum == 27) || (keyNum == 13);
            } while (!next);
            if (keyNum == 27) end = true;
            Console.Clear();
            return end;
        }
        
    }
}
