namespace PZ_02
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите значения двух переменных:\nc - целое\nd - действительное"); //вывод на экран текста
            Console.Write("\nc = ");
            int c = int.Parse(Console.ReadLine()); //пользователь вводит значение переменной c
            Console.Write("d = ");
            double d = double.Parse(Console.ReadLine()); //пользователь вводит значение перемdенной d
            double s, t, v; //обьявление переменных s, t, v

            if (d == 0) { Console.WriteLine("d не может быть равен 0, т.к. деление на 0 невозможно"); } //вывод на экран текста
            else
            {

                if (c < 0) //если c меньше 0, то...
                    s = c - (d * Math.Pow(c, 3) / d); //вычисление s
                else //иначе...
                    s = c * d - 16; //вычисление s

                if (s <= 4.5) //если s меньше или равен 4.5, то...
                    t = Math.Cos(2 * c) + 2 * c; //вычисление t
                else //иначе...
                    t = Math.Pow(Math.Cos((c - d) / (s - d)), 2); //вычисление t

                v = (c + 3 * d - 1.1 * s * t) / 1.8; //вычисление v

                Console.WriteLine($"\ns = {s}"); //вывод на экран значение s
                Console.WriteLine($"t = {t}"); //вывод на экран значение t
                Console.WriteLine($"v = {v}"); //вывод на экран значение v
            }
        }
    }
}