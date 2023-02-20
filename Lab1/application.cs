using System;
using System.Collections.Generic;
using MyMath;
using number = System.Double;


namespace consoleApp
{
    public class application
    {
        public void SetConsoleApp()
        {
            int pick = 0;
            while (true)
            {
                Console.WriteLine("     Матричный калькулятор V1.0     ");
                Console.WriteLine("------------------------------------");
                Console.WriteLine("|1. Изменить Матрицу               |");
                Console.WriteLine("|2. Транспонировать Матрицу        |");
                Console.WriteLine("|3. Найти Определитель Матрицы     |");
                Console.WriteLine("|4. Найти Ранг Матрицы             |");
                Console.WriteLine("|5. Выход                          |");
                Console.WriteLine("------------------------------------");
                Console.WriteLine("Текущая матрица:");
                matrix.PrintMatrix();
                pick = Convert.ToInt32(Console.ReadLine());
                Console.Clear();
                    switch (pick)
                    {
                        case 1:
                            Console.WriteLine("Введите размер новой матрицы");
                            int size = Convert.ToInt32(Console.ReadLine());
                            List<List<number>> tmp = new List<List<number>>();
                            for (int i = 0; i < size; i++)
                            {
                                tmp.Add(new List<number>());
                                Console.WriteLine($"Введите {i+1}-ую строку матрицы (через пробел)");
                                foreach (var element in Console.ReadLine().Split(' '))
                                {
                                    tmp[i].Add(Convert.ToDouble(element));
                                }
                            }
                            matrix.SetMatrix(size, tmp);
                            Console.Clear();
                            break;
                        case 2:
                            matrix.Transposition();
                            break;
                        case 3:
                            Console.WriteLine($"Определитель матрицы = {matrix.GetDeterminant()}");
                            break;
                        case 4:
                            Console.WriteLine($"Ранг матрицы = {matrix.GetRank()}");
                            break;
                        case 5:
                            return;
                        default:
                            Console.WriteLine(" ОШИБКА: ВЫБРАН НЕВЕРНЫЙ ПУНКТ МЕНЮ ");
                            break;
                    }
            }
        }
        private SquareMatrix matrix = new SquareMatrix();
    }
}
