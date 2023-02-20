using System;
using System.Collections.Generic;
using MyMath;
using number = MyNumber.Complex;
using System.Text;
using System.Text.RegularExpressions;

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
                        Regex[] complexForms = new Regex[6] { new Regex(@"-\d+,?\d*-\d+,?\d*i"),//-xxx-yyyi 
                                                              new Regex(@"-\d+,?\d*[+]\d+,?\d*i"),//-xxx+yyyi 
                                                              new Regex(@"\d+,?\d*[+]\d+,?\d*i"), // xxx+yyyi
                                                              new Regex(@"\d+,?\d*-\d+,?\d*i"), // xxx-yyyi
                                                              new Regex(@"\d*,?\d*i"),          //yyyi or -yyyi
                                                              new Regex(@"\d*,?\d*")};          //xxx or -xxx

                        for (int i = 0; i < size; i++)
                        {
                            tmp.Add(new List<number>());
                            Console.WriteLine($"Введите {i + 1}-ую строку матрицы (через пробел)");
                            foreach (var element in Console.ReadLine().Split(" "))
                            {
                                string[] tmpInput;
                                int formVar = 0;
                                for (int j = 0;j<complexForms.Length;j++)
                                {
                                    if (complexForms[j].Matches(element).Count>0)
                                    {
                                        formVar = j;
                                        break;
                                    }
                                }
                                switch (formVar) 
                                {
                                    //-xxx-yyyi
                                    case 0:
                                        tmpInput = element.Substring(1).Split("-");
                                        tmp[i].Add(new number(-Convert.ToDouble(tmpInput[0]), -
                                                              Convert.ToDouble(tmpInput[1].Substring(0, tmpInput[1].Length - 1))));
                                        break;
                                    //-xxx+yyyi
                                    case 1:
                                        tmpInput = element.Split("+");
                                        tmp[i].Add(new number(Convert.ToDouble(tmpInput[0]),
                                                              Convert.ToDouble(tmpInput[1].Substring(0, tmpInput[1].Length - 1))));
                                        break;
                                    //xxx+yyyi
                                    case 2:
                                        tmpInput = element.Split("+");
                                        tmp[i].Add(new number(Convert.ToDouble(tmpInput[0]),
                                                              Convert.ToDouble(tmpInput[1].Substring(0, tmpInput[1].Length - 1))));
                                        break;
                                    //xxx-yyyi
                                    case 3:
                                        tmpInput = element.Split("-");
                                        tmp[i].Add(new number(Convert.ToDouble(tmpInput[0]),
                                                                  -Convert.ToDouble(tmpInput[1].Substring(0, tmpInput[1].Length - 1))));
                                        break;
                                    //yyyi or -yyyi
                                    case 4:
                                        tmp[i].Add(new number(0, Convert.ToDouble(element.Substring(0, element.Length - 1))));
                                        break;
                                    //xxx or -xxx
                                    case 5:
                                        tmp[i].Add(new number(Convert.ToDouble(element), 0));
                                        break;
                                }
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