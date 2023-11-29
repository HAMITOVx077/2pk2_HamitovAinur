namespace PZ_13
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Задание 1
            Console.WriteLine("----- Задание 1 -----");
            Console.Write("Введите значение n: ");
            int n = int.Parse(Console.ReadLine());
            int a1 = -4;
            int d = -7;
            int An = ArithmeticProgression(a1, d, n);
            Console.WriteLine($"значение члена {n}: {An} \n");

            //задание 2
            Console.WriteLine("----- Задание 2 -----");
            Console.Write("Введите значение n: ");
            double k = double.Parse(Console.ReadLine());
            double b1 = 2;
            double q = -0.15;
            double Bn = GeometricProgression(b1, q, k);
            Console.WriteLine($"значение члена {n}: {Bn} \n");

            //Задание 3
            Console.WriteLine("----- Задание 3 -----");
            Console.Write("Введите переменную A: ");
            int a = int.Parse(Console.ReadLine());
            Console.Write("Введите переменную B: ");
            int b = int.Parse(Console.ReadLine());
            Console.WriteLine("Вывод согласно заданному алгоритму: ");
            PrintInRange(a, b);
            Console.WriteLine();
            Console.WriteLine();

            //Задание 4 (задание 1)
            Console.Write("Введите значение n: ");
            int x = int.Parse(Console.ReadLine());
            Console.WriteLine("Сумма чисел от 1 до n: " + Summ(x));

        }
        static int Summ(int x) //метод вычисляет числа от 1 до x
        {
            if (x == 1)
            {
                return 1;
            }
            else
            {
                x = x + Summ(x - 1);
                return x;
            }
        }
        static void PrintInRange(int a, int b) //метод определяющий какой метод использовать для вывода в 3-ем задании
        {
            if (a > b)
                PrintAtoBdown(a, b);
            if (a < b)
                PrintAtoBup(a, b);
        }
        static void PrintAtoBup(int a, int b) //метод выводит числа в диапазоне [A; B] в порядке возрастания (для 3-го задания)
        {
            if (a <= b)
            {
                Console.Write(a + " ");
                PrintAtoBup(a + 1, b);
            }
        }
        static void PrintAtoBdown(int a, int b) //метод выводит числа в диапазоне [A; B] в порядке убывания (для 3-го задания)
        {
            if (a >= b)
            {
                Console.Write(a + " ");
                PrintAtoBdown(a - 1, b);
            }
        }
        static int ArithmeticProgression(int a1, int d, int n) //метод считает арифметическую прогрессию (для 1-го задания)
        {
            int An = 0;
            if (n != 0)
            {
                An = a1 + d * (n - 1);
                ArithmeticProgression(a1, d, n - 1);
            }
            return An;
        }
        static double GeometricProgression(double b1, double q, double k) //метод считает геометрическую прогрессию (для 2-го задания)
        {
            double Bn = 0;
            if (k != 0)
            {
                Bn = b1 * Math.Pow(q, k - 1);
                GeometricProgression(b1, q, k - 1);
            }
            return Bn;
        }
    }
}