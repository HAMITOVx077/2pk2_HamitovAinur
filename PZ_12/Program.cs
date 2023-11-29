namespace PZ_12
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*Реализуйте метод, принимающий в качестве параметра массив целых чисел nums и
            возвращающих массив строк strings по правилу:
            строка содержит в себе символ ‘+’ количество повторений которого в строке зависит от
            индекса данной строки*/

            Console.WriteLine("Задача:" +
                "\nРеализуйте метод, принимающий в качестве параметра массив целых чисел nums и" +
                "\nвозвращающих массив строк strings по правилу:" +
                "\nстрока содержит в себе символ ‘+’ количество повторений которого в строке зависит от" +
                "\nиндекса данной строки\n");

            Console.Write("Введите количество элементов массива: ");
            int limit = int.Parse(Console.ReadLine());
            Console.WriteLine("Введите значения элементов массива: (целые числа)");
            int[] numbers = new int[limit];
            for (int i = 0; i < limit; i++) //цикл для заполнение массива числами
            {
                Console.Write($"Элемент {i}: ");
                numbers[i] = int.Parse(Console.ReadLine());
            }
            string[] plusArray = CreatePlusArray(numbers); //присвоение массиву plusArray значение возвращаемого массива после вызова метода CreatePlusArray
            Console.WriteLine("\nСоздание нового массива по заданному алгоритму и вывод его элементов: ");
            for (int i = 0; i < limit; i++) //вывод элементов получившегося массива
            {
                Console.WriteLine($"Элемент {i}: {plusArray[i]}");
            }

        }
        static string[] CreatePlusArray(int[] nums)
        {
            string[] plusArrayResult = new string[nums.Length]; //итоговый массив со знаками "+" который мы будем возвращать
            for (int i = 0; i < nums.Length; i++)
            {
                string[] storagePlus = new string[nums[i]]; //массив для хранения n-го количества знаков "+"
                for (int j = 0; j < nums[i]; j++) //цикл заполняет массив знаками "+"
                {
                    storagePlus[j] = "+";
                }
                plusArrayResult[i] = string.Join("", storagePlus); //присваивает элелементу массива plusArrayResult строку из элементов массива storagePlus
            }
            return plusArrayResult;
        }
    }
}