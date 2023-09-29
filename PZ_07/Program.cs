namespace PZ_07
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("---Автозаполнение элементов массива---\n---Нахождение максимального элемента побочной диагонали матрицы---\n");
            Random rnd = new Random(); //объявление нового экземпляра класса Random
            int[,] arr = new int[8, 8]; //объявление и инициализация двумерного массива
            for (int i = 0; i < 8; i++) //цикл для автозаполнения элементов массива, где i=строка, j=колонна
            {
                for (int j = 0; j < 8; j++)
                {
                    arr[i, j] = rnd.Next(-128, 257);
                    Console.Write(arr[i, j] + "\t");
                }
                Console.WriteLine();
            }
            int max = Int32.MinValue; //переменная для нахождения максимального элемента побочной диагонали
            int ind1 = 0; //переменная для индекса i
            int ind2 = 0; //переменная для индекса j
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++) //цикл для нахождения максимального элемента побочной диагонали
                {
                    if (i + j == 7)
                    {
                        if (arr[i, j] > max)
                        {
                            max = arr[i, j];
                            ind1 = i;
                            ind2 = j;
                        }
                    }
                }
            }
            Console.WriteLine($"\nМаксимальный элемент побочной диагонали: arr[{ind1},{ind2}] = {max}"); //вывод результатов
        }
    }
}