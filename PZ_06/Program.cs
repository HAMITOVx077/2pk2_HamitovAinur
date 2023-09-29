namespace PZ_06
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите 10 символов в массив:");
            char[] array = new char[10]; //объявление и инициализация массива
            for (int i = 0; i < array.Length; i++) //цикл для заполнения элементов массива
            {
                Console.Write(i + 1 + " символ: ");
                array[i] = char.Parse(Console.ReadLine());
            }

            char max = char.MinValue;
            for (int i = 0; i < array.Length; i++) //цикл на нахождение максимального элемента массива
            {
                if (array[i] > max)
                    max = array[i];
            }

            char simv = ' ';
            for (int i = 0; i < array.Length; i++) //цикл для сортировки элементов массива в порядке возрастания (сравнивает 1 элемент со всеми остальными)
            {
                for (int j = i + 1; j < array.Length; j++)
                {
                    if (array[i] > array[j])
                    {
                        simv = array[i];
                        array[i] = array[j];
                        array[j] = simv;
                    }
                }
            }

            Console.WriteLine("Элементы массива в порядке возрастания: ");
            foreach (char i in array) //цикл для вывода элементов массива в порядке возрастания
            {
                Console.Write(i + "\t");
            }
        }
    }
}