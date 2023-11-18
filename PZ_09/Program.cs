namespace PZ_09
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*Пользователь вводит произвольный текст с консоли. Осуществить анализ введенного текста
            и вывести текст с раскраской слов по следующему правилу:
            Числа – красный цвет
            Слова длиной больше 5 символов – зеленый цвет
            Все остальные - синий цвет*/

            Console.WriteLine("---Программа раскраски слов в тексте по заданному алгоритму---\n");
            Console.Write("Введите текст: ");
            string text = Console.ReadLine();
            string[] textArray = text.Split(); //определение слов и занесение их в массив
            Console.Write("Результат раскраски: ");
            foreach (var i in textArray) //проверка слов в массиве отталкиваясь от условий
            {
                if (i.Length > 5) //если длина слова больше 5-ти букв, то
                {
                    Console.ForegroundColor = ConsoleColor.Green; //раскраска слова в зелёный цвет
                    Console.Write(i + " "); //вывод раскрашенного слова
                }
                else
                {
                    for (int j = 0; j < i.Length; j++) //цикл для проверки каждой буквы слова
                    {
                        if (Char.IsDigit(i[j])) //если элемент слова является числом, то
                        {
                            Console.ForegroundColor = ConsoleColor.Red; //раскраска элемента слова в красный цвет
                            Console.Write(i[j]);
                        }
                        else if (Char.IsLetter(i[j])) //если элемент слова является буквой, то
                        {
                            Console.ForegroundColor = ConsoleColor.Blue; //раскраска элемента слова в синий цвет
                            Console.Write(i[j]);
                        }
                    }
                    Console.Write(' '); //отделение слов друг от друга
                }
            }
            Console.ResetColor(); //возвращает цветовой фон по умолчанию
            Console.WriteLine("\n\nПрограмма распределила цвета по словам по следующему алгортиму:\n" +
                "1) Числа – красный цвет\n" +
                "2) Слова длиной больше 5 символов – зеленый цвет\n" +
                "3) Все остальные - синий цвет");
            Console.ReadKey();
        }
    }
}