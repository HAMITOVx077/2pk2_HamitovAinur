namespace PZ_08
{
    internal class Program
    {
        static void Main(string[] args)
        {
            double[][] array = new double[10][]; //объявление и инициализация ступенчатого массива
            Random rnd = new Random();
            double[] last = new double[10]; //массив для определения последних элементов ступенчатого массива
            double[] arrmax = new double[10]; //массив для определения максимальных элементов ступенчатого массива
            double[][] arrchange = new double[10][]; //массив для смены мест первого и максимального элементов ступенчатого массива
            double max = Double.MinValue;
            double[] first = new double[10];
            int[] k = new int[10]; //переменная для координаты второго измерения ступенчатого массива
            int[] dvar = new int[10]; //переменная для хранения количества элементов в каждой строке
            double[][] revarr = new double[10][];
            for (int i = 0; i < 10; i++) //заполнение ступенчатого массива
            {
                int d = rnd.Next(3, 31);
                dvar[i] = d;
                array[i] = new double[d]; //количество элементов в строке
                for (int j = 0; j < d; j++)
                {
                    array[i][j] = (Math.Round(rnd.NextDouble()*10, 2)); //генерирование самих элементов
                    Console.Write(array[i][j] + " ");
                    last[i] = array[i][d - 1];
                    first[i] = array[i][0]; //присваивание first значение первого элемента ступенчатого массива с каждой строки
                    if (array[i][j] > max) //нахождение максимального элемента массива
                    {
                        max = array[i][j];
                        arrmax[i] = max;
                        k[i] = j;
                    }
                }
                max = Double.MinValue; //сброс значения max
                Console.WriteLine();
            }

            Console.WriteLine();

            Console.WriteLine("Последние элементы ступенчатого массива: ");
            foreach (double l in last) //вывод элементов массива last
                Console.Write(l + "  ");

            Console.WriteLine();

            Console.WriteLine("Максимальные элементы ступенчатого массива с каждой строки: ");
            foreach (double l in arrmax) //вывод элементов массива arrmax
                Console.Write(l + "  ");

            Console.WriteLine();
            Console.WriteLine();

            arrchange = array;

            for (int i = 0; i < 10;i++) //цикл для смены мест двух значений массива
            {
                arrchange[i][0] = arrmax[i]; //замена первого элемента максимальным элементом
                arrchange[i][k[i]] = first[i]; //замена максимального элемента первым элементом
            }

            Console.WriteLine("Вывод обновлённого массива с заменой первого и максимального:");
            for (int i = 0;i < 10;i++) //вывод обновлённого массива array (смена мест элементов)
            {
                for (int j = 0; j < dvar[i]; j++)
                {
                    Console.Write(arrchange[i][j] + " ");
                }
                Console.WriteLine();
            }

            Console.WriteLine();

            for (int i = 0; i < 10; i++) //переход к первоначальному виду ступенчатого массива
            {
                arrchange[i][0] = first[i];
                arrchange[i][k[i]] = arrmax[i];
            }

            revarr = array;

            for (int i = 0; i < 10; i++) //ревёрс ступенчатого массива
            {
                Array.Reverse(revarr[i]);
            }

            Console.WriteLine("Вывод ревёрснутого массива:");
            for (int i = 0; i < 10; i++) //вывод обновлённого массива array (ревёрс)
            {
                for (int j = 0; j < dvar[i]; j++)
                {
                    Console.Write(revarr[i][j] + " ");
                }
                Console.WriteLine();
            }

            Console.WriteLine("\nСреднее значение чисел в каждой строке:");
            double sum = 0;
            for (int i = 0; i < 10; i++) //вычисление среднего арифметического каждой строки
            {
                for (int j = 0; j < array[i].Length; j++)
                {
                    sum += array[i][j];
                }
                double nsum = sum / array[i].Length;
                Console.WriteLine($"строка {i}: {nsum}"); 
                sum = 0; //сброс суммы
            }
        }
    }
}