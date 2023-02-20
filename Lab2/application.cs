using System;
using System.Collections.Generic;
using MyMath;
using number = MyNumber.Complex;


namespace consoleApp
{
    public class application
    {
        public void SetConsoleApp()
        {
            int pick = 0;
            while (true)
            {
                Console.WriteLine("     Матричный калькулятор V2.0     ");
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
                            Console.WriteLine($"Введите {i + 1}-ую строку матрицы (через пробел)");
                            foreach (var element in Console.ReadLine().Split(" "))
                            {
                                string[] input = element.Split('+');
                                if (input.Length == 1)
                                {
                                    //если просто одно число
                                    if (!input[0].Contains("-"))
                                    {
                                        //если только мнимая часть
                                        if (input[0].Contains("i"))
                                            tmp[i].Add(new number(0, Convert.ToDouble(input[0].Substring(0, input[0].Length - 1))));
                                        else tmp[i].Add(new number(Convert.ToDouble(input[0]), 0));
                                    }
                                    //если -x-yi
                                    else if ((input[0][0] == '-') && (input[0].Substring(1).Contains("-")))
                                    {
                                        input = input[0].Substring(1).Split("-");
                                        tmp[i].Add(new number(-Convert.ToDouble(input[0]), -
                                                              Convert.ToDouble(input[1].Substring(0, input[1].Length - 1))));
                                    }
                                    //если -yi
                                    else if ((input[0][0] == '-' ) && (!input[0].Substring(1).Contains("-")))
                                    {
                                        tmp[i].Add(new number(0, Convert.ToDouble(input[0].Substring(0, input[0].Length - 1))));
                                    }
                                    //если х-уi
                                    else
                                    {
                                        input = input[0].Split("-");
                                        tmp[i].Add(new number(Convert.ToDouble(input[0]),
                                                              -Convert.ToDouble(input[1].Substring(0, input[1].Length - 1))));
                                    }
                                }
                                //если x+yi
                                else tmp[i].Add(new number(Convert.ToDouble(input[0]),
                                                           Convert.ToDouble(input[1].Substring(0, input[1].Length-1))));
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