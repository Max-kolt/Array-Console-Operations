using System;
using System.Linq;

namespace ArrayConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //Ввод данных о массиве
            int arrSize, arrMin, arrMax;
            
            arrSize = NumberInput(1,"Введите размер массива: ");
            arrMin = NumberInput("Введите левую границу массива: ");
            arrMax = NumberInput(arrMin+1,"Введите правую границу массива: ");
            
            //Создание массива
            int[] array = CreateArray(arrSize,arrMin,arrMax);
            int page;
            Console.Clear();
            
            //Работа с массивом
            while (true)
            {
                string arrayStr = string.Join(" ", array);
                page = NumberInput(1,4, $"Исходный массив:\n{arrayStr}\n" +
                    "Выбирите действие, которое применится к массиву:\n" +
                    "1. На оценку удовлетворительно\n" +
                    "2. На оценку хорошо\n" +
                    "3. На оценку отлично\n" +
                    "4. Выйти из программы\n" +
                    "Цифра: ");
                if (page == 4) break;
                Console.Clear();
                Console.WriteLine($"Наш массив:\n{arrayStr}\n");
                switch (page)
                {
                    case 1:
                        OnOneOutput(array);
                        break;

                    case 2:
                        OnTwoOutput(array);
                        break;

                    case 3:
                        OnThreeOutput(array);
                        break;

                    default:
                        break;
                }
                Console.WriteLine("\nНажмите любую кнопку если готовы продолжить...");
                Console.ReadKey();
                Console.Clear();

            }
            Console.WriteLine("Пока");
            Console.ReadKey();
        }

        // Проверка ввода цифр (с ограничением и без), создание массива
        static int NumberInput(string writeText = "Write: ")
        {
            bool repeat = true;
            int num = 0;
            while (repeat)
            {
                Console.Write(writeText);
                string str = Console.ReadLine();
                try
                {
                    num = int.Parse(str);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"{e.Message} Повторите попытку.");
                    continue;
                }
                repeat = false;
            }
            return num;
        }
        static int NumberInput(int min, string writeText = "Write: ")
        {
            bool repeat = true;
            int num = 0;
            while (repeat)
            {
                Console.Write(writeText);
                string str = Console.ReadLine();
                try
                {
                    num = int.Parse(str);
                }
                catch(Exception e)
                {
                    Console.WriteLine($"{e.Message} Повторите попытку.");
                    continue;
                }
                if(num>=min) repeat = false;
            }
            return num;
        }
        static int NumberInput(int min, int max, string writeText = "Write: ")
        {
            bool repeat = true;
            int num = 0;
            while (repeat)
            {
                Console.Write(writeText);
                string str = Console.ReadLine();
                try
                {
                    num = int.Parse(str);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"{e.Message} Повторите попытку.");
                    continue;
                }
                if (num >= min && num <= max)repeat = false;
            }
            return num;
        }
        static int[] CreateArray(int size, int min, int max)
        {
            int[] newArray = new int[size];
            Random rndNum = new Random();
            for(int i = 0; i<size; i++)
            {
                newArray[i] = rndNum.Next(min, max);
            }
            return newArray;
        }

        /*
        Методы действий с массивом на оценку удовлетворительно:
        1. Сумма элементов массива;
        2. Среднее арифметическое;
        3. Новый массив, каждый элемент которого вычисляется как
        отклонение исходного элемента массива от среднего значения;
        4. Увеличить все элементы массива на необходимое значение (вводится
        пользователем);
        5. Обменять местами нужные элементы массива (позиции переставляемых
        элементов вводятся пользователем с контролем границ массива).
        */

        static int[] NewArrayDeviation(int[] array)
        {
            int avarage = (int) array.Average();
            int[] newArray = new int[array.Length];
            for(int i = 0; i < array.Length; i++)
            {
                newArray[i] = avarage - array[i];
            }
            return newArray;
        }
        static int[] ArrayChangeValue(ref int[] array, int numUp)
        {
            for (int i = 0; i < array.Length; i++)
            {
                array[i] += numUp;
            }
            return array;
        }

        private static int[] ArrayChangeTwoElements(ref int[] array, int firstIndex, int secondIndex)
        {
            (array[firstIndex], array[secondIndex]) = (array[secondIndex], array[firstIndex]);
            return array;
        }

        //этот метод создан для вывода результатов подсчета первого пункта
        private static void OnOneOutput(int[] array)
        {
            Console.WriteLine($"Сумма всех элементов массива = {array.Sum()}");
            Console.WriteLine($"Среднее арифметическое элементов массива = {array.Average()}");
            Console.WriteLine("Новый массив, каждый элемент которого вычисляется как " +
                              $"отклонение исходного элемента массива от среднего значения: \n{string.Join(" ",NewArrayDeviation(array))}");
            Console.WriteLine("Увеличить все элементы массива на необходимое значение:\n" + 
                string.Join(" ", ArrayChangeValue(ref array, NumberInput("Для следующей операции надо ввести число," +
                " на которое увеличатся элементы массива: "))));
            Console.WriteLine("Обменять местами нужные элементы массива:\n" + 
                string.Join(" ", ArrayChangeTwoElements(ref array, 
                NumberInput(0,array.Length-1,"Введите индекс первого числа: "), 
                NumberInput(0, array.Length-1, "Введите индекс второго числа числа: "))));
        }

        /*
        Методы действий с массивом на оценку хорошо:
        1 Найти позицию максимального элемента в массиве.
        2 Найти позицию минимального элемента в массиве.
        3 Осуществить поиск первой позиции вхождения искомого элемента в
        массив (искомый элемент вводится пользователем).
        4 Осуществить поиск последней позиции вхождения искомого элемента в
        массив (искомый элемент вводится пользователем). 
        */
        static int MaxElementPos(int[] array)
        {
            int position = 0, maxValue = 0;
            for(int i = 0; i < array.Length; i++) 
            {
                if (maxValue < array[i])
                {
                    position = i;
                    maxValue = array[i];
                }
            }
            return position;
        }
        static int MinElementPos(int[] array)
        {
            int position = 0, minValue = array[0];
            for (int i = 0; i < array.Length; i++)
            {
                if (minValue > array[i])
                {
                    position = i;
                    minValue = array[i];
                }
            }
            return position;
        }
        static int FirstPosSearch(int[] array, int e)
        {
            int position = -1;
            for(int i = 0; i < array.Length; i++)
            {
                if (array[i] == e)
                {
                    position = i;
                    break;
                }
            }
            return position;
        }
        static int LastPosSearch(int[] array, int e)
        {
            int position = -1;
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == e)
                {
                    position = i;
                }
            }
            return position;
        }

        //этот метод создан для вывода результатов подсчета второго пункта
        static void OnTwoOutput(int[] array)
        {
            Console.WriteLine($"Позиция максимального элемента массива = {MaxElementPos(array)}");
            Console.WriteLine($"Позиция минимального элемента массива = {MinElementPos(array)}");
            int num = NumberInput("Введите число для его поиска в массиве: ");
            Console.WriteLine($"Позиция первого вхождения искомого элемента = {FirstPosSearch(array, num)}");
            Console.WriteLine($"Позиция последнего вхождения искомого элемента = {LastPosSearch(array, num)}");
        }

        /*
        Методы действий с массивом на оценку отлично:
        1 Найти все позиции вхождения искомого элемента в массив (результатом
        будет – массив позиций).
        2 Найти сумму элементов в массиве между указанными позициями.
        3 Найти максимальный элементов в массиве между указанными
        позициями (контроль границ).
        4 Найти минимальный элементов в массиве между указанными позициями
        (контроль границ).
        */
        static int[] AllPosSearch(int[] array, int e)
        {
            int[] positions = new int[array.Count(c => c == e)];
            int j = 0;
            for(int i = 0; i < array.Length; i++)
            {
                if (array[i] == e)
                {
                    positions[j] = i;
                    j++;
                }
            } 


            return positions;
        }
        static int SumInRange(int[] array, int firstIndex, int secondIndex)
        {
            int sum = 0;
            for (int i = firstIndex; i < secondIndex; i++)
            {
                sum = sum + array[i];
            }
            return sum;
        }
        static int MaxElementInRange(int[] array, int firstIndex, int secondIndex)
        {
            int max = 0;
            for(int i = firstIndex; i<secondIndex; i++)
            {
                if (max < array[i]) max = array[i];
            }
            return max;
        }
        static int MinElementInRange(int[] array, int firstIndex, int secondIndex)
        {
            int min = array[firstIndex];
            for (int i = firstIndex; i < secondIndex; i++)
            {
                if (min > array[i]) min = array[i];
            }
            return min;
        }

        //этот метод создан для вывода результатов подсчета третьего пункта
        static void OnThreeOutput(int[] array)
        {
            Console.WriteLine($"Все позиции вхождения искомого элемента = {string.Join(", ", AllPosSearch(array, NumberInput("Введите число для его поиска в массиве: ")))}");
            int firstIndex = NumberInput(0, array.Length - 2, "Введите первый индекс отсчета (он не может быть последним): ");
            int secondIndex = NumberInput(firstIndex + 1, array.Length - 1, "Введите второй индекс (он не может быть меньше первого): ");
            Console.WriteLine($"Сумма элементов в диапозоне от {firstIndex} до {secondIndex} = {SumInRange(array, firstIndex, secondIndex)}");
            Console.WriteLine($"Максимальный элемент в диапозоне от {firstIndex} до {secondIndex} = {MaxElementInRange(array, firstIndex, secondIndex)}");
            Console.WriteLine($"Минимальный элемент в диапозоне от {firstIndex} до {secondIndex} = {MinElementInRange(array, firstIndex, secondIndex)}");

        }
    }
}
