namespace PZ_14
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Данная программа выполняет следующее:\n" +
                "Осуществляет запись строк из файла f1.txt длиной более 10 символов во второй файл f2.txt\n");

            string firstFile = @"D:\MyCodes\C#\2pk2_HamitovAinur\PZ_14\Files for PZ_14\f1.txt"; //файл с произвольным текстом
            string secondFile = @"D:\MyCodes\C#\2pk2_HamitovAinur\PZ_14\Files for PZ_14\f2.txt"; //файл для записи строк

            if (!File.Exists(firstFile) | !File.Exists(secondFile)) //если хотя бы один из файлов по указанному пути отсутствует, то
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ошибка: проверьте наличие обоих файлов");
                Console.ResetColor();
            }
            else
            {
                string[] lines = File.ReadAllLines(firstFile); //массив, содержащий все строки файла f1.txt

                if (lines.Length == 0) //проверка на наличие текста в файле
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Ошибка: файл f1.txt не содержит в себе текста");
                    Console.ResetColor();
                }
                else
                {
                    StreamWriter writer = new StreamWriter(secondFile); //создаём потоковый писатель для записи строк во второй файл
                    foreach (string line in lines)
                    {
                        if (line.Length > 10) //если строка длиной больше 10 символов, то
                        {
                            writer.Write(line + "\n"); //записывает данную строку
                            Console.WriteLine($"Записана строка: {line}");
                        }
                          
                    }
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\nЗапись строк успешно произведена"); //информационное сообщение
                    Console.ResetColor();
                    writer.Close(); //закрываем поток
                }
            }
        }
    }
}