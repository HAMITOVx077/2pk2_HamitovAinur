namespace PZ_16
{
    internal class Program
    {
        static int mapSize = 25; //размер карты
        static char[,] map = new char[mapSize, mapSize + 1]; //карта
        //координаты на карте игрока
        static int playerY = mapSize / 2;
        static int playerX = mapSize / 2;
        static byte enemies = 10; //количество врагов
        static byte buffs = 3; //количество усилений
        static int health = 5;  // количество аптечек
        static int playerHealth = 50; //здоровье персонажа
        static int enemyHealth = 30; //здоровье врага
        static int playerDamage = 10; //урон персонажа
        static int enemyDamage = 5; //урон врага
        static int enemiesCount = enemies; //подсчёт врагов
        static int steps = 0; //для подсчёта пройденных шагов
        static int oldStep = 0; //для проверки окончания баффа (применяется в методе CheckStepsOfGetBuff)
        static int playerOldX; //предыдущие координаты игрока
        static int playerOldY; //предыдущие координаты игрока
        static string path; //путь для загрузки определённых сохранений игры
        static int winCount = 0; //для запуска уровней

        static void Main(string[] args)
        {
            StartDisplay();
            Move();
        }

        /// <summary>
        /// генерация карты с расставлением врагов, аптечек, баффов
        /// </summary>
        static void GenerationMap()
        {
            Random random = new Random();
            //создание пустой карты
            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    map[i, j] = '_';
                }
            }

            //чтобы при рестарте игры игрок помещался в середину карты
            playerY = mapSize / 2;
            playerX = mapSize / 2;

            map[playerX, playerY] = 'Т'; // в середину карты ставится игрок

            //временные координаты для проверки занятости ячейки
            int x;
            int y;
            //добавление врагов
            while (enemies > 0)
            {
                x = random.Next(0, mapSize);
                y = random.Next(1, mapSize); //от 1 до mapSize - чтобы по правому краю (где не работает контакт с существами) не спавнились

                //если ячейка пуста  - туда добавляется враг
                if (map[x, y] == '_')
                {
                    map[x, y] = 'Ж';
                    enemies--; //при добавлении врагов уменьшается количество нерасставленных врагов
                }
            }
            //добавление баффов
            while (buffs > 0)
            {
                x = random.Next(0, mapSize);
                y = random.Next(1, mapSize);

                if (map[x, y] == '_')
                {
                    map[x, y] = 'b';
                    buffs--;
                }
            }
            //добавление аптечек
            while (health > 0)
            {
                x = random.Next(0, mapSize);
                y = random.Next(1, mapSize);

                if (map[x, y] == '_')
                {
                    map[x, y] = '+';
                    health--;
                }
            }

            UpdateMap(); //отображение заполненной карты на консоли
        }
        /// <summary>
        /// перемещение персонажа
        /// </summary>
        static void Move()
        {
            while (true)
            {
                playerOldX = playerX;
                playerOldY = playerY;

                StatisticInGame(); //вывод статистики в момент игры
                CheckStepsOfGetBuff(); //проверка оставшихся шагов до окончания действия баффа, если он подобран

                //смена координат в зависимости от нажатия клавиш
                switch (Console.ReadKey().Key)
                {
                    //управление стрелочками на клавиатуре
                    case ConsoleKey.UpArrow:
                        steps++; //подсчёт шагов при каждом движении
                        playerX--;
                        Console.Beep(1500, 1); //звук ходьбы
                        break;
                    case ConsoleKey.DownArrow:
                        playerX++;
                        steps++;
                        Console.Beep(1500, 1); //звук ходьбы
                        break;
                    case ConsoleKey.LeftArrow:
                        playerY--;
                        steps++;
                        Console.Beep(1500, 1); //звук ходьбы
                        break;
                    case ConsoleKey.RightArrow:
                        playerY++;
                        steps++;
                        Console.Beep(1500, 1); //звук ходьбы
                        break;
                    case ConsoleKey.Q:
                        Console.Beep(750, 5); //звук нажатия клавиши
                        Quit();
                        break;
                    case ConsoleKey.Z:
                        Console.Beep(750, 5); //звук нажатия клавиши
                        winCount++;
                        DefaultSettings();
                        GenerationMap();
                        UpdateMap();
                        break;
                    default:
                        break;
                }

                Teleport();

                switch (map[playerX, playerY]) //считывает с чем контактирует игрок
                {
                    case 'Ж': //если напал на врага
                        Fight();
                        Console.Beep(500, 5); //звук при битве
                        break;
                    case '+': //если подобрал аптечку
                        playerHealth = 50;
                        Console.Beep(1000, 5); //звук подбора аптечки
                        break;
                    case 'b': //если подобрал бафф
                        GetBuff();
                        Console.Beep(1000, 5); //звук подбора баффа
                        break;
                }

                //предыдущее положение игрока затирается
                map[playerOldX, playerOldY] = '_';
                Console.SetCursorPosition(playerOldY, playerOldX);
                Console.Write('_');

                //обновление положения персонажа
                map[playerX, playerY] = 'Т';
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.SetCursorPosition(playerY, playerX);
                Console.Write('Т');
                Console.ResetColor();
            }
        }
        /// <summary>
        /// вывод карты на консоль
        /// </summary>
        static void UpdateMap()
        {
            Console.Clear();
            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    //раскраска сущностей и их вывод
                    switch (map[i, j])
                    {
                        case '+':
                            Console.BackgroundColor = ConsoleColor.Gray;
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.Write(map[i, j]);
                            Console.ResetColor();
                            break;
                        case 'b':
                            Console.BackgroundColor = ConsoleColor.DarkGreen;
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write(map[i, j]);
                            Console.ResetColor();
                            break;
                        case 'Ж':
                            if (winCount == 2)
                            {
                                Console.BackgroundColor = ConsoleColor.DarkYellow;
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.Write(map[i, j]);
                                Console.ResetColor();
                            }
                            else
                            {
                                Console.BackgroundColor = ConsoleColor.DarkRed;
                                Console.ForegroundColor = ConsoleColor.Gray;
                                Console.Write(map[i, j]);
                                Console.ResetColor();
                            }
                            break;
                        case 'Т':
                            Console.BackgroundColor = ConsoleColor.White;
                            Console.ForegroundColor = ConsoleColor.DarkBlue;
                            Console.Write(map[i, j]);
                            Console.ResetColor();
                            break;
                        default:
                            Console.Write(map[i, j]);
                            break;
                    }
                }
                Console.WriteLine(map[i, 0]);
            }
        }
        static void Fight()
        {
            enemyHealth -= playerDamage; //отнимание хп у врага
            if (enemyHealth > 0) //если враг ещё жив
            {
                FightAnimation();
                playerHealth -= enemyDamage; //отнимание хп у игрока
                //смена местоположения врага (меняются местами с игроком)
                if (winCount == 2)
                {
                    Console.BackgroundColor = ConsoleColor.DarkYellow;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.SetCursorPosition(playerOldY, playerOldX);
                    map[playerOldX, playerOldY] = 'Ж';
                    Console.Write('Ж');
                    Console.ResetColor();
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.DarkRed;
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.SetCursorPosition(playerOldY, playerOldX);
                    map[playerOldX, playerOldY] = 'Ж';
                    Console.Write('Ж');
                    Console.ResetColor();
                }

                //чтобы при повторном вызове Fight враг мог встать на место игрока
                playerOldX = playerX;
                playerOldY = playerY;
                if (playerHealth <= 0)//если игрок погиб
                {
                    winCount = 0;
                    DeathDisplay();
                }
            }
            else if (enemyHealth <= 0) //если враг 'погиб'
            {
                FightAnimation();
                enemiesCount--;
                enemyHealth = 30;
                if (enemiesCount == 0) //если врагов не осталось
                {
                    if (winCount < 3)
                    {
                        winCount++;
                        DefaultSettings();
                        GenerationMap();
                        UpdateMap();
                    }
                    if (winCount == 3)
                    {
                        winCount = 0;
                        WinDisplay();
                    }
                }
            }
        }
        static void GetBuff()
        {
            oldStep = steps;
            playerDamage = playerDamage * 2;
        }
        static void CheckStepsOfGetBuff()
        {
            if (playerDamage > 10) //если урон игрока уже увеличен
            {
                if ((20 - (steps - oldStep)) % 2 == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.SetCursorPosition(mapSize + 12, 22);
                    Console.Write($"До окончания действия зелья силы осталось шагов: {20 - (steps - oldStep)} ");
                    Console.ResetColor();
                }
                else if (20 - (steps - oldStep) % 2 != 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.SetCursorPosition(mapSize + 12, 22);
                    Console.Write($"До окончания действия зелья силы осталось шагов: {20 - (steps - oldStep)} ");
                    Console.ResetColor();
                }

                if (steps - oldStep == 20) //если время действия баффа закончилось
                {
                    playerDamage = 10; //дефолтное значение дамага игрока
                    Console.SetCursorPosition(mapSize + 12, 22);
                    //заполнение строки, где была информации о времени действия баффа пустотой
                    Console.Write("                                                              ");
                }
            }
        }
        static void StatisticInGame()
        {
            if (playerHealth <= 15)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.SetCursorPosition(mapSize + 12, 10);
                Console.Write($"Здоровье игрока: {playerHealth}    ");
                Console.ResetColor();
            }
            else
            {
                Console.SetCursorPosition(mapSize + 12, 10);
                Console.Write($"Здоровье игрока: {playerHealth}    ");
            }

            Console.SetCursorPosition(mapSize + 12, 11);
            Console.Write($"Урон игрока: {playerDamage}     ");
            Console.SetCursorPosition(mapSize + 12, 12);
            Console.Write($"Осталось врагов: {enemiesCount}   ");
            Console.SetCursorPosition(mapSize + 12, 13);
            Console.Write($"Пройдено шагов: {steps}    ");

            Console.SetCursorPosition(mapSize + 12, 15);
            Console.Write("Координаты:");
            Console.SetCursorPosition(mapSize + 12, 16);
            Console.Write($"   X: {playerX}     ");
            Console.SetCursorPosition(mapSize + 12, 17);
            Console.Write($"   Y: {playerY}     ");

            Console.SetCursorPosition(mapSize + 12, 5);
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.Write("Т");
            Console.ResetColor();
            Console.Write(" - ваш персонаж");
            Console.SetCursorPosition(mapSize + 12, 6);
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("Ж");
            Console.ResetColor();
            Console.Write(" - жуки");
            Console.SetCursorPosition(mapSize + 12, 7);
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write("+");
            Console.ResetColor();
            Console.Write(" - аптечка (восстанавливает здоровье до макс.)");
            Console.SetCursorPosition(mapSize + 12, 8);
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("b");
            Console.ResetColor();
            Console.Write(" - зелье урона (умножает урон вдвое)");

            Console.SetCursorPosition(mapSize + 12, 19);
            Console.Write("Q - открыть меню настроек");
            Console.SetCursorPosition(mapSize + 12, 20);
            Console.Write("Z - переход к следующему уровню");

            switch (winCount)
            {
                case 0:
                    Console.SetCursorPosition(0, 26);
                    Console.Write("Уровень игры: 1");
                    Console.SetCursorPosition(0, 27);
                    Console.Write("Урон жуков: 5");
                    Console.SetCursorPosition(0, 28);
                    Console.Write($"Здоровье жука: {enemyHealth}  ");
                    Console.ResetColor();
                    break;
                case 1:
                    Console.SetCursorPosition(0, 26);
                    Console.Write("Уровень игры: 2");
                    Console.SetCursorPosition(0, 27);
                    Console.Write("Урон жуков повышен до 15");
                    Console.SetCursorPosition(0, 28);
                    Console.Write($"Здоровье жука: {enemyHealth}  ");
                    Console.ResetColor();
                    break;
                case 2:
                    Console.SetCursorPosition(0, 26);
                    Console.Write("Уровень игры: 3");
                    Console.SetCursorPosition(0, 27);
                    Console.Write("Урон босса-жука: 20");
                    Console.SetCursorPosition(0, 28);
                    Console.Write($"Здоровье босса-жука: {enemyHealth}  ");
                    Console.ResetColor();
                    break;
                default:
                    Console.SetCursorPosition(0, 26);
                    Console.Write("Уровени закончились, ожидайте обновления");
                    break;
            }
        }
        static void StartDisplay()
        {
            Console.Clear();
            Console.CursorVisible = false; //скрытный курсов
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.SetCursorPosition(40, 10);
            Console.Write("═════════════════════════════════");
            Console.ResetColor();
            Console.SetCursorPosition(40, 11);
            Console.Write("N - начать новую игру");
            Console.SetCursorPosition(40, 12);
            Console.Write("L - загрузить сохраненную игру");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.SetCursorPosition(40, 13);
            Console.Write("═════════════════════════════════");
            Console.ResetColor();
            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.L: //запуск последнего сохранения
                    Console.Beep(750, 5); //звук нажатия клавиши
                    Console.Clear();
                    LoadGame();
                    break;
                case ConsoleKey.N: //запуск новой игры
                    Console.Beep(750, 5); //звук нажатия клавиши
                    //установка дефолтных значений параметров и генерация карты
                    winCount = 0;
                    DefaultSettings();
                    GenerationMap();
                    break;
                default: //если игрок нажимает на другие кнопки то ничего не происходит, метод просто вызывается заново
                    StartDisplay();
                    break;
            }
        }
        static void WinDisplay()
        {
            Console.Clear();
            Console.SetCursorPosition(40, 9);
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("═══════════════════════════════════════════");
            Console.ResetColor();
            Console.SetCursorPosition(40, 10);
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" Вы победили :) ");
            Console.ResetColor();
            Console.SetCursorPosition(40, 11);
            Console.Write($"Количество пройденных шагов: {steps}");
            Console.SetCursorPosition(40, 12);
            Console.Write($"Вернуться на стартовый экран - клавиша B");
            Console.SetCursorPosition(40, 13);
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("═══════════════════════════════════════════");
            Console.ResetColor();

            //звук победы
            Console.Beep(400, 200);
            Console.Beep(600, 200);
            Console.Beep(700, 200);
            Console.Beep(800, 200);
            Console.Beep(900, 200);
            Thread.Sleep(20);

            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.B: //если игрок нажал на B(back)
                    Console.Beep(750, 5); //звук нажатия клавиши
                    StartDisplay();
                    break;
                default:
                    WinDisplay();
                    break;
            }
        }
        static void DeathDisplay()
        {
            Console.Clear();
            Console.SetCursorPosition(40, 9);
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("═══════════════════════════════════════════");
            Console.ResetColor();
            Console.SetCursorPosition(40, 10);
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" Вы проиграли :( ");
            Console.ResetColor();
            Console.SetCursorPosition(40, 11);
            Console.Write($"Количество пройденных шагов: {steps}");
            Console.SetCursorPosition(40, 12);
            Console.Write($"Вернуться на стартовый экран - клавиша B");
            Console.SetCursorPosition(40, 13);
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("═══════════════════════════════════════════");
            Console.ResetColor();

            //звук проигрыша
            Console.Beep(400, 500);
            Console.Beep(300, 500);
            Console.Beep(200, 500);
            Thread.Sleep(50);

            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.B: //если игрок нажал на B(back)
                    Console.Beep(750, 5); //звук нажатия клавиши
                    StartDisplay();
                    break;
                default:
                    DeathDisplay();
                    break;
            }
        }
        static void Quit()
        {
            Console.Clear();
            Console.SetCursorPosition(40, 9);
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("═══════════════════════════════════");
            Console.ResetColor();
            Console.SetCursorPosition(40, 10);
            Console.Write("R - продолжить игру");
            Console.SetCursorPosition(40, 11);
            Console.Write("F - сохранить игру");
            Console.SetCursorPosition(40, 12);
            Console.Write("B - вернуться на стартовый экран");
            Console.SetCursorPosition(40, 13);
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("═══════════════════════════════════");
            Console.ResetColor();
            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.R: //если игрок продолжил игру
                    Console.Beep(750, 5); //звук нажатия клавиши
                    //вывод карты
                    UpdateMap();
                    break;
                case ConsoleKey.F: //если игрок сохранил игру
                    Console.Beep(750, 5); //звук нажатия клавиши
                    Console.Clear();
                    SaveGame();
                    DisplayAfterSave();
                    break;
                case ConsoleKey.B: //если игрок решил вернуться на стартовый экран
                    Console.Beep(750, 5); //звук нажатия клавиши
                    StartDisplay();
                    break;
                default:
                    Quit();
                    break;
            }
        }
        static void DefaultSettings()
        {
            //логика уровней
            switch (winCount)
            {
                case 0:
                    enemies = 10;
                    enemiesCount = enemies;
                    buffs = 3;
                    health = 5;
                    playerHealth = 50;
                    enemyHealth = 30;
                    playerDamage = 10;
                    steps = 0;
                    oldStep = 0;
                    break;
                case 1:
                    enemies = 10;
                    enemyDamage = 15;
                    enemiesCount = enemies;
                    buffs = 3;
                    health = 4;
                    playerHealth = 50;
                    enemyHealth = 30;
                    playerDamage = 10;
                    oldStep = 0;
                    break;
                case 2:
                    enemies = 1;
                    enemyDamage = 20;
                    enemiesCount = enemies;
                    buffs = 3;
                    health = 3;
                    playerHealth = 50;
                    enemyHealth = 150;
                    playerDamage = 10;
                    oldStep = 0;
                    break;
            }
        }
        static void Teleport()
        {
            //если координаты игрока выходят за край, то игрока телепортирует на противоположную сторону карты
            if (playerX == -1)
                playerX = mapSize - 1;
            if (playerY == -1)
                playerY = mapSize;
            if (playerX == mapSize)
                playerX = 0;
            if (playerY == mapSize + 1)
                playerY = 0;
        }
        static void SaveGame()
        {
            Console.CursorVisible = true;
            Console.SetCursorPosition(40, 10);
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("═════════════════════════════════════");
            Console.ResetColor();
            Console.SetCursorPosition(40, 11);
            Console.Write("Введите название сохранения: ");
            Console.SetCursorPosition(40, 12);
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("═════════════════════════════════════");
            Console.ResetColor();
            Console.SetCursorPosition(69, 11);
            path = Console.ReadLine(); //файл с символами сущностей, координатами и параметрами
            Console.Beep(750, 5); //звук нажатия клавиши
            Console.CursorVisible = false;
            try
            {
                using (FileStream file = new FileStream(path + ".txt", FileMode.OpenOrCreate, FileAccess.Write))
                {
                    using (StreamWriter writer = new StreamWriter(file))
                    {
                        for (int i = 0; i < mapSize; i++) //цикл для записи в файл координат сущностей и параметров игры
                        {
                            for (int j = 0; j < mapSize; j++)
                            {
                                if (map[i, j] != '_' && map[i, j] != 'Т')
                                {
                                    writer.Write(map[i, j] + " ");
                                    writer.Write(i + " ");
                                    writer.Write(j + " ");
                                }
                            }
                        }
                        //запись всех параметров в момент сохранения игры
                        writer.Write("* "); //стоп-символ (применяется в LoadGame()
                        writer.Write(playerX + " ");
                        writer.Write(playerY + " ");
                        writer.Write(enemiesCount + " ");
                        writer.Write(buffs + " ");
                        writer.Write(health + " ");
                        writer.Write(playerHealth + " ");
                        writer.Write(enemyHealth + " ");
                        writer.Write(playerDamage + " ");
                        writer.Write(steps + " ");
                        writer.Write(oldStep + " ");
                        writer.Write(winCount + " ");
                    }
                }
            }
            catch
            {
                Console.Clear();
                Console.SetCursorPosition(38, 14);
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.Write(" Ошибка: запрещённые символы в названии, повторите попытку ");
                Console.ResetColor();
                SaveGame();
            }
        }
        static void DisplayAfterSave()
        {
            Console.Clear();
            Console.SetCursorPosition(40, 9);
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("═══════════════════════════════════════════");
            Console.ResetColor();
            Console.SetCursorPosition(40, 10);
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.Write(" Игра успешно сохранена! ");
            Console.ResetColor();
            Console.SetCursorPosition(40, 11);
            Console.Write($"Название сохранения: ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"{path}  ");
            Console.ResetColor();
            Console.SetCursorPosition(40, 12);
            Console.Write("Вернуться назад - клавиша G");
            Console.SetCursorPosition(40, 13);
            Console.Write("Вернуться на стартовый экран - клавиша B");
            Console.SetCursorPosition(40, 14);
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("═══════════════════════════════════════════");
            Console.ResetColor();
            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.B:
                    Console.Beep(750, 5); //звук нажатия клавиши
                    StartDisplay();
                    break;
                case ConsoleKey.G:
                    Console.Beep(750, 5); //звук нажатия клавиши
                    Quit();
                    break;
                default:
                    DisplayAfterSave();
                    break;
            }
        }
        static void LoadGame()
        {
            Console.CursorVisible = true;
            Console.SetCursorPosition(38, 10);
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("══════════════════════════════════════════════════════");
            Console.ResetColor();
            Console.SetCursorPosition(38, 11);
            Console.Write("Введите название сохранения для загрузки игры: ");
            Console.SetCursorPosition(38, 12);
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("══════════════════════════════════════════════════════");
            Console.ResetColor();
            Console.SetCursorPosition(85, 11);
            path = Console.ReadLine(); //файл с символами сущностей, координатами и параметрами
            Console.Beep(750, 5); //звук нажатия клавиши
            Console.CursorVisible = false;
            try
            {
                using (FileStream file = new FileStream(path + ".txt", FileMode.OpenOrCreate, FileAccess.Read))
                {
                    using (StreamReader reader = new StreamReader(file))
                    {
                        Console.Clear();
                        string[] lines = reader.ReadToEnd().Split(); //заполнение массива данными из файла
                        int count = 0; //для подсчёта данных с линий
                        int X = 0; int Y = 0; //для считывания координат сущностей

                        //создание пустой карты
                        for (int i = 0; i < mapSize; i++)
                        {
                            for (int j = 0; j < mapSize; j++)
                            {
                                map[i, j] = '_';
                                Console.Write(map[i, j]);
                            }
                            Console.WriteLine(map[i, 0]);
                        }

                        //запись в массив map сущностей, согласно их координатам
                        while (true)
                        {
                            if (Convert.ToChar(lines[count]) == 'Ж')
                            {
                                Y = Convert.ToInt32(lines[count + 1]);
                                X = Convert.ToInt32(lines[count + 2]);
                                map[Y, X] = Convert.ToChar(lines[count]);
                                count += 3;
                            }
                            else if (Convert.ToChar(lines[count]) == 'b')
                            {
                                Y = Convert.ToInt32(lines[count + 1]);
                                X = Convert.ToInt32(lines[count + 2]);
                                map[Y, X] = Convert.ToChar(lines[count]);
                                count += 3;
                            }
                            else if (Convert.ToChar(lines[count]) == '+')
                            {
                                Y = Convert.ToInt32(lines[count + 1]);
                                X = Convert.ToInt32(lines[count + 2]);
                                map[Y, X] = Convert.ToChar(lines[count]);
                                count += 3;
                            }
                            else if (Convert.ToChar(lines[count]) == '*')
                            {
                                //переход к записи параметров
                                break;
                            }
                        }

                        //запись параметров
                        playerX = Convert.ToInt32(lines[count + 1]);
                        playerY = Convert.ToInt32(lines[count + 2]);
                        enemiesCount = Convert.ToInt32(lines[count + 3]);
                        buffs = Convert.ToByte(lines[count + 4]);
                        health = Convert.ToInt32(lines[count + 5]);
                        playerHealth = Convert.ToInt32(lines[count + 6]);
                        enemyHealth = Convert.ToInt32(lines[count + 7]);
                        playerDamage = Convert.ToInt32(lines[count + 8]);
                        steps = Convert.ToInt32(lines[count + 9]);
                        oldStep = Convert.ToInt32(lines[count + 10]);
                        winCount = Convert.ToInt32(lines[count + 11]);

                        //запись в массив игрока
                        map[playerX, playerY] = 'Ʌ';

                        UpdateMap(); //вывод карты по получившимся параметрам
                    }
                }
            }
            catch
            {
                Console.Clear();
                Console.SetCursorPosition(38, 14);
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.Write(" Ошибка: такого сохранения не найдено, повторите попытку ");
                Console.ResetColor();
                LoadGame();
            }
        }
        static void FightAnimation()
        {
            //происходит условный бой, после чего на свободных ячейках появлятся 'кровь' персонажа и врагов, которая практически сразу же пропадает
            while (true)
            {
                //генерация 'крови'
                if (playerOldY + 1 != 26)
                {
                    if (map[playerOldX, playerOldY + 1] == '_')
                    {
                        Console.SetCursorPosition(playerOldY + 1, playerOldX);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("_");
                        Console.ResetColor();
                    }
                }
                if (playerOldY - 1 != -1)
                {
                    if (map[playerOldX, playerOldY - 1] == '_')
                    {
                        Console.SetCursorPosition(playerOldY - 1, playerOldX);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("_");
                        Console.ResetColor();
                    }
                }
                if (playerOldX + 1 != 25)
                {
                    if (map[playerOldX + 1, playerOldY] == '_')
                    {
                        Console.SetCursorPosition(playerOldY, playerOldX + 1);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("_");
                        Console.ResetColor();
                    }
                }
                if (playerOldX - 1 != -1)
                {
                    if (map[playerOldX - 1, playerOldY] == '_')
                    {
                        Console.SetCursorPosition(playerOldY, playerOldX - 1);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("_");
                        Console.ResetColor();
                    }
                }
                if (playerY + 1 != 26)
                {
                    if (map[playerX, playerY + 1] == '_')
                    {
                        Console.SetCursorPosition(playerY + 1, playerX);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("_");
                        Console.ResetColor();
                    }
                }
                if (playerY - 1 != -1)
                {
                    if (map[playerX, playerY - 1] == '_')
                    {
                        Console.SetCursorPosition(playerY - 1, playerX);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("_");
                        Console.ResetColor();
                    }
                }
                if (playerX + 1 != 25)
                {
                    if (map[playerX + 1, playerY] == '_')
                    {
                        Console.SetCursorPosition(playerY, playerX + 1);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("_");
                        Console.ResetColor();
                    }
                }
                if (playerX - 1 != -1)
                {
                    if (map[playerX - 1, playerY] == '_')
                    {
                        Console.SetCursorPosition(playerY, playerX - 1);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("_");
                        Console.ResetColor();
                    }
                }
                Thread.Sleep(50);
                //стирание крови
                if (playerOldY + 1 != 26)
                {
                    if (map[playerOldX, playerOldY + 1] == '_')
                    {
                        Console.SetCursorPosition(playerOldY + 1, playerOldX);
                        Console.Write("_");
                    }
                }
                if (playerOldY - 1 != -1)
                {
                    if (map[playerOldX, playerOldY - 1] == '_')
                    {
                        Console.SetCursorPosition(playerOldY - 1, playerOldX);
                        Console.Write("_");
                    }
                }
                if (playerOldX + 1 != 25)
                {
                    if (map[playerOldX + 1, playerOldY] == '_')
                    {
                        Console.SetCursorPosition(playerOldY, playerOldX + 1);
                        Console.Write("_");
                    }
                }
                if (playerOldX - 1 != -1)
                {
                    if (map[playerOldX - 1, playerOldY] == '_')
                    {
                        Console.SetCursorPosition(playerOldY, playerOldX - 1);
                        Console.Write("_");
                    }
                }
                if (playerY + 1 != 26)
                {
                    if (map[playerX, playerY + 1] == '_')
                    {
                        Console.SetCursorPosition(playerY + 1, playerX);
                        Console.Write("_");
                    }
                }
                if (playerY - 1 != -1)
                {
                    if (map[playerX, playerY - 1] == '_')
                    {
                        Console.SetCursorPosition(playerY - 1, playerX);
                        Console.Write("_");
                    }
                }
                if (playerX + 1 != 25)
                {
                    if (map[playerX + 1, playerY] == '_')
                    {
                        Console.SetCursorPosition(playerY, playerX + 1);
                        Console.Write("_");
                    }
                }
                if (playerX - 1 != -1)
                {
                    if (map[playerX - 1, playerY] == '_')
                    {
                        Console.SetCursorPosition(playerY, playerX - 1);
                        Console.Write("_");
                    }
                }
                break;
            }
        }
    }
}