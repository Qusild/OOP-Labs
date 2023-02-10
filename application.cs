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
            while (true)
            {
                Console.WriteLine("     Матричный калькулятор V1.0     ");

                Console.WriteLine("------------------------------------");
                Console.WriteLine("|1. Напечатать Матрицу             |");
                Console.WriteLine("|2. Изменить Матрицу               |");
                Console.WriteLine("|3. Транспонировать Матрицу        |");
                Console.WriteLine("|4. Найти Определитель Матрицы     |");
                Console.WriteLine("|5. Найти Ранг Матрицы             |");
                Console.WriteLine("|6. Выход                          |");
                Console.WriteLine("------------------------------------");
                pick = Convert.ToInt32(Console.ReadLine());
                Console.Clear();
                if ((pick > 0) && (pick < 7))
                {
                    switch (pick)
                    {
                        case 1:
                            Console.WriteLine("Текущая матрица:");
                            matrix.PrintMatrix();
                            break;
                        case 2:
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
                            Console.WriteLine("Матрица изменена, новая матрица:");
                            matrix.PrintMatrix();
                            break;
                        case 3:
                            matrix.Transposition();
                            Console.WriteLine("Матрица Транспонирована, новая матрица:");
                            matrix.PrintMatrix();
                            break;
                        case 4:
                            Console.WriteLine($"Определитель матрицы = {matrix.GetDeterminant()}");
                            break;
                        case 5:
                            Console.WriteLine($"Ранг матрицы = {matrix.GetRank()}");
                            break;
                        case 6:
                            return;
                    }
                }
                else Console.WriteLine(" ОШИБКА: ВЫБРАН НЕВЕРНЫЙ ПУНКТ МЕНЮ ");
            }
        }

        private int pick;
        private SquareMatrix matrix = new SquareMatrix();
    }
}