namespace PZ_15
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*В консоли пользователь вводит строку с полным путем к каталогу в котором содержаться
            другие каталоги. Реализовать рекурсивный обход вложенных каталогов и вывод их
            содержимого.*/

            string pathExample = @"C:\Program Files\AMD";
            Console.WriteLine($"Введите строку с полным путем к каталогу: (пример: \"{pathExample}\")");
            string path = Console.ReadLine();
            if (!Directory.Exists(path)) //если по указанному пути не найдено каталога
                Console.WriteLine("Ошибка: указанный каталог не найден");
            else
                PrintAllSubDirectories(path);

            static void PrintAllSubDirectories(string path)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine($"\nСодержимое каталога: {path}");
                try
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("--------Подкаталоги--------");
                    string[] dirs = Directory.GetDirectories(path); //получаем все каталоги в path
                    foreach (string dir in dirs) //вывод данных каталогов
                    {
                        Console.WriteLine(dir);
                    }
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.WriteLine("--------Файлы--------");
                    string[] files = Directory.GetFiles(path); //получаем все файлы в path
                    foreach (string file in files) //вывод данных файлов и их содержимого
                    {
                        Console.WriteLine($"Название файла: {file}");
                        FileStream infoInFile = new FileStream($@"{file}", FileMode.Open, FileAccess.Read); //открываем поток для файла
                        StreamReader reader = new StreamReader(infoInFile); //потоковый читатель
                        Console.WriteLine($"Содержимое файла: {reader.ReadToEnd()}");
                        reader.Close(); //закрываем поток
                    }
                    Console.ResetColor();

                    foreach (string dir in dirs) //цикл для захода в следующие каталоги внутри path и вывода их содержимого по алгоритму сверху
                    {
                        PrintAllSubDirectories(dir);
                    }
                }
                catch (Exception e)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Ошибка при обращении к каталогу: {e.Message}"); //в случае ошибки выводится данный текст
                    Console.ResetColor();
                }
            }
        }
    }
}