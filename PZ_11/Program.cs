namespace PZ_11
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*Описать процедуру SortInc3(A, B, C), меняющую содержимое переменных A, B, C таким
            образом, чтобы их значения оказались упорядоченными по возрастанию(A, B, C —
            вещественные параметры, являющиеся одновременно входными и выходными). С помощью этой
            процедуры упорядочить по возрастанию два данных набора из трех чисел: (A1, B1, C1) и(A2, B2, C2).*/

            Console.WriteLine("Введите значения переменных первого набора:"); //ввод значений первого набора
            Console.Write("A1: ");
            double A1 = double.Parse(Console.ReadLine());
            Console.Write("B1: ");
            double B1 = double.Parse(Console.ReadLine());
            Console.Write("C1: ");
            double C1 = double.Parse(Console.ReadLine());

            Console.WriteLine("\nВведите значения переменных второго набора:"); //ввод значений второго набора
            Console.Write("A2: ");
            double A2 = double.Parse(Console.ReadLine());
            Console.Write("B2: ");
            double B2 = double.Parse(Console.ReadLine());
            Console.Write("C2: ");
            double C2 = double.Parse(Console.ReadLine());

            SortInc3(ref A1, ref B1, ref C1); //вызов метода SortInc3 для сортировки первого набора
            Console.WriteLine($"\nПервый упорядоченный набор: {A1}, {B1}, {C1}"); //вывод первого упорядоченного набора

            SortInc3(ref A2, ref B2, ref C2); //вызов метода SortInc3 для сортировки второго набора
            Console.WriteLine($"\nВторой упорядоченный набор: {A2}, {B2}, {C2}"); //вывод второго упорядоченного набора
        }
        static void SortInc3(ref double a, ref double b, ref double c)
        {
            double storageDouble = 0; //переменная, которая храненит значение элемента массива SortDoubleArray для последующего переопределения его элементов
            double[] SortDoubleArray = { a, b, c };
            for (int i = 0; i < SortDoubleArray.Length; i++) //цикл для сортировки переменных a, b, c по возрастанию
            {
                for (int j = 0; j < SortDoubleArray.Length; j++)
                {
                    if (SortDoubleArray[i] < SortDoubleArray[j]) //если следующий элемент массива больше предшествующего, то они меняются местами
                    {
                        storageDouble = SortDoubleArray[i];
                        SortDoubleArray[i] = SortDoubleArray[j];
                        SortDoubleArray[j] = storageDouble;
                    }
                }
            }
            //присвоение новых упорядоченных значений переменным a,b,c
            a = SortDoubleArray[0];
            b = SortDoubleArray[1];
            c = SortDoubleArray[2];
        }
    }
}