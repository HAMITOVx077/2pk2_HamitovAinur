﻿namespace PZ_05
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*Написать программу: с клавиатуры ввести целое положительное число n. Из числа n получить число m –
            логической операцией установить флаг нечетности (если число нечетное, оно не меняется, если число четное, то
            оно увеличивается на единицу). На консоль вывести сумму всех нечетных чисел диапазона от m до m х2.*/
            Console.Write("Введите целое положительное число: ");
            int n = int.Parse(Console.ReadLine()); //пользователь вводит число
            if (n >= 0) //если число больше или равно нулю, то...
            {
                int sum = 0; //переменная для подсчёта суммы
                int m = n; //присваивание значения
                int vr = m * 2; //переменная для диапазона в цикле
                while (m <= vr) //пока m меньше или равно vr, то...
                {
                    if (m % 2 == 0) //если m чётный, то
                        m++;
                    else //иначе
                    {
                        sum = sum + m; //подсчёт суммы нечётных чисел
                        m++;
                    }
                }
                Console.WriteLine("Cумма всех нечетных чисел диапазона от m до mх2: " + sum); //вывод результата
            }
            else //иначе...
                Console.WriteLine("ОШИБКА! Введите число согласно требованиям!");
        }
    }
}