namespace PZ_10
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите текст: ");
            string text = Console.ReadLine();
            string[] textArray = text.Split();
            int count = 0; //переменная для хранения количества пробелов в массиве

            foreach (string s in textArray) //цикл для проверки является ли элемент массива пробелом
            {
                if (s.All(Char.IsWhiteSpace))
                {
                    count++;
                }
            }
            Console.WriteLine("Количество слов в тексте: " + (textArray.Length - count));

            string gap; //переменная для хранения значения меньшего элемента массива
            for (int i = 0; i < textArray.Length; i++) //цикл для сортировки элементов массива в поярдке убывания (определяет по количеству символов)
            {
                for (int j = i + 1; j < textArray.Length; j++)
                    if (textArray[i].Length < textArray[j].Length) //если следующий элемент массива больше предшествующего, то происходит переопределение этих элементов
                    {
                        gap = textArray[i];
                        textArray[i] = textArray[j];
                        textArray[j] = gap;
                    }
            }
            foreach (string str in textArray) { Console.WriteLine(str); } //вывод элементов массива
        }
    }
}