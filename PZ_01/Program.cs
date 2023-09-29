namespace PZ_01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите значения переменных:"); //ввывод на экран текста в кавычках
            Console.Write("a = ");
            double a = double.Parse(Console.ReadLine()); //пользователь вводит значение переменной a
            Console.Write("b = ");
            double b = double.Parse(Console.ReadLine()); //пользователь вводит значение переменной b
            Console.Write("c = ");
            double c = double.Parse(Console.ReadLine()); //пользователь вводит значение переменной c
            double r1 = (1.5 * Math.Pow(a - b, 2)) / (Math.Abs(a - b) * c); //первая часть вычислений
            double r2 = Math.Pow(10, 3) * Math.Sqrt(Math.Abs(a - b)); //вторая часть вычислений
            double r3 = (2.5 * (Math.Pow(a, 2) + 2.75) * Math.Sin(-2 * a)) / (3 + Math.Pow(a, 2) * b * c); //третья часть вычислений
            double result = r1 + r2 - r3; //итоговый результат вычислений
            Console.WriteLine($"Результат вычислений: {result}"); //вывод на экран текста с результатом вычислений
        }
    }
}