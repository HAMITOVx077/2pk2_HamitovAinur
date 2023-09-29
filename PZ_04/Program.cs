﻿namespace PZ_04
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //ЗАДАНИЕ 1
            //Вывести на экран построчно целые числа из указанного диапазона и с указанным шагом
            Console.WriteLine("1 задание\n");
            for (int i = 30; i <= 150; i += 15) //цикл выводит значение i с последующим прибавлением к нему числа 15
            {
                Console.WriteLine(i);
            }

            //ЗАДАНИЕ 2
            //Вывести на экран в одну строку n-символов в алфавитном порядке, начиная с указанного символа
            Console.WriteLine("\n2 задание\n");
            char simv = 'н';
            for (int h = 0; h < 5; h++) //цикл выводит символ 'н' и последующие после него до тех пор, пока h < 5
            {
                Console.Write($" {simv}");
                simv++;
            }

            //ЗАДАНИЕ 3
            Console.WriteLine();
            //Вывести на экран посимвольно n знаков ‘#’ в m строках.
            Console.WriteLine("\n3 задание\n");
            int n = 6;
            int m = 10;
            for (int v = 0; v < m; v++) //цикл переводит на новую строку до тех пор, пока v < m
            {
                for (int g = 0; g < n; g++) //цикл выводит символ '#' до тех пор, пока g < n
                {
                    Console.Write(" #");
                }
                Console.Write("\n");
            }

            //ЗАДАНИЕ 4
            /*Из диапазона значений вывести на экран значения кратные числу, выбранному в соответствии с
            вариантом(см.табл.) через пробел, используя один цикл. В конце вывести количество кратных
            чисел*/
            Console.WriteLine();
            Console.WriteLine("4 задание\n");
            int sum = 0;
            for (int i = -500; i <= -200; i++) //цикл определяет диапазон значений
            {
                if (i % 18 == 0) //если i делится на 18 без остатка, то
                {
                    Console.Write($" {i}"); //вывод на экран i
                    sum++;
                }
                else //иначе
                    continue; //переход к следующей итерации
            }
            Console.WriteLine($"\nКоличество кратных чисел - {sum}"); //вывод количества кратных чисел

            //ЗАДАНИЕ 5
            /*Выводить на экран значение двух переменные i и j, на каждом шаге итерации одну переменную
            инкрементировать, а вторую декрементировать до тех пор, пока разница между ними не будет
            равна(или меньше) указанной по варианту*/
            Console.WriteLine();
            Console.WriteLine("5 задание\n");
            for (int i = 0, j = 35; j - i > 3; i++, j--) //цикл выводит значения i и j до тех пор, пока i - j > 3
            {
                Console.WriteLine($"i = {i}, j = {j}");
            }
        }
    }
}