using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmHW4
{
    class Program
    {
        static int n = 6;
        static int step = 0;
        static int[] board = new int[ n * n ];

        /// <summary>
        /// Метод делает массив нулевым
        /// </summary>
        /// <param name="arr"></param>
        static void BoardZero(ref int[] arr)
        {
            for(int i = 0; i < arr.Length; i++)
            {
                arr[ i ] = 0;
            }
        }

        /// <summary>
        /// Вывод матрицы
        /// </summary>
        /// <param name="arr"></param>
        static void ShowBoard(int[] arr)
        {
            for(int i = 0; i < n; i++)
            {
                for(int j = 0; j < n; j++)
                {
                    Console.Write($"{arr[ i * n + j ], 3} ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Сам алгоритм поиска пути коня. Начальные координаты должны быть угловыми, т.е. (0,0), (0, n-1), (n-1, 0), (n-1, n-1).
        /// Размерность поля должна быть четной и больше 4, т.е., например, 6х6, 8х8 и т.д.
        /// </summary>
        /// <param name="posI">Начальная координата по строке</param>
        /// <param name="posJ">Начальная координата по столбцу</param>
        /// <param name="arr"></param>
        /// <returns></returns>
        static int Algorithm(int posI, int posJ, ref int[] arr)
        {
            if(IsNotZero(arr)) return 1;
            step++;
            arr[ posI * n + posJ ] = step;
            if(!IsNotZero(arr))
            {
                if(IsEmptyAround(posI, posJ, arr))
                {
                    int i = NextPoint(EmptyPoints(posI, posJ, arr), arr).i;
                    int j = NextPoint(EmptyPoints(posI, posJ, arr), arr).j;
                    if(Algorithm(i, j, ref arr) == 1) return 1;
                }
            }
            return 0;
        }

        /// <summary>
        /// Метод возвращает координаты следующей точки. Он основан на правиле Варнсдорфа:
        /// "При обходе доски коня следует всякий раз ставить на поле, из которого он может сделать наименьшее число ходов на еще не пройденные поля."
        /// </summary>
        /// <param name="points">Коллекция точек, в которые можно пойти из заданной</param>
        /// <param name="arr">Заданный массив точек</param>
        /// <returns></returns>
        static (int i, int j) NextPoint(List<(int, int)> points, int[] arr)
        {
            (int i, int j) tpl = points[0];
            int min = arr.Length;
            foreach(var point in points)
            {
                if(ValueOfEmptyPoints(point.Item1, point.Item2, arr) > 0 & ValueOfEmptyPoints(point.Item1, point.Item2, arr) < min)
                {
                    min = ValueOfEmptyPoints(point.Item1, point.Item2, arr);
                    tpl = (point.Item1, point.Item2);
                }
            }
            return tpl;
        }

        /// <summary>
        /// Метод возвращает количество пустых ходов для заданной точки
        /// </summary>
        /// <param name="i">Координата заданной точки по строке</param>
        /// <param name="j">Координата заданной точки по столбцу</param>
        /// <param name="arr">Заданный массив точек</param>
        /// <returns></returns>
        static int ValueOfEmptyPoints(int i, int j, int[] arr)
        {
            int value = 0;
            if(( i + 2 < n ) & ( j + 1 < n ))
            {
                if(arr[ ( i + 2 ) * n + j + 1 ] == 0)
                {
                    value++;
                }
            }
            if(( i + 1 < n ) & ( j + 2 < n ))
            {
                if(arr[ ( i + 1 ) * n + j + 2 ] == 0)
                {
                    value++;
                }
            }
            if(( i - 2 >= 0 ) & ( j + 1 < n ))
            {
                if(arr[ ( i - 2 ) * n + j + 1 ] == 0)
                {
                    value++;
                }
            }
            if(( i - 1 >= 0 ) & ( j + 2 < n ))
            {
                if(arr[ ( i - 1 ) * n + j + 2 ] == 0)
                {
                    value++;
                }
            }
            if(( i + 1 < n ) & ( j - 2 >= 0 ))
            {
                if(arr[ ( i + 1 ) * n + j - 2 ] == 0)
                {
                    value++;
                }
            }
            if(( i + 2 < n ) & ( j - 1 >= 0 ))
            {
                if(arr[ ( i + 2 ) * n + j - 1 ] == 0)
                {
                    value++;
                }
            }
            if(( i - 1 >= 0 ) & ( j - 2 >= 0 ))
            {
                if(arr[ ( i - 1 ) * n + j - 2 ] == 0)
                {
                    value++;
                }
            }
            if(( i - 2 >= 0 ) & ( j - 1 >= 0 ))
            {
                if(arr[ ( i - 2 ) * n + j - 1 ] == 0)
                {
                    value++;
                }
            }
            return value;
        }

        /// <summary>
        /// Метод возвращает коллекцию точек, в которые можно еще пойти от заданной
        /// </summary>
        /// <param name="i">Координата заданной точки по строке</param>
        /// <param name="j">Координата заданной точки по столбцу</param>
        /// <param name="arr">Заданный массив точек</param>
        /// <returns></returns>
        static List<ValueTuple<int, int>> EmptyPoints(int i, int j, int[] arr)
        {
            List<(int, int)> points = new List<(int, int)>();
            if(( i + 2 < n ) & ( j + 1 < n ))
            {
                if(arr[ ( i + 2 ) * n + j + 1 ] == 0)
                {
                    points.Add((i + 2, j + 1));
                }
            }
            if(( i + 1 < n ) & ( j + 2 < n ))
            {
                if(arr[ ( i + 1 ) * n + j + 2 ] == 0)
                {
                    points.Add((i + 1, j + 2));
                }
            }
            if(( i - 2 >= 0 ) & ( j + 1 < n ))
            {
                if(arr[ ( i - 2 ) * n + j + 1 ] == 0)
                {
                    points.Add((i - 2, j + 1));
                }
            }
            if(( i - 1 >= 0 ) & ( j + 2 < n ))
            {
                if(arr[ ( i - 1 ) * n + j + 2 ] == 0)
                {
                    points.Add((i - 1, j + 2));
                }
            }
            if(( i + 1 < n ) & ( j - 2 >= 0 ))
            {
                if(arr[ ( i + 1 ) * n + j - 2 ] == 0)
                {
                    points.Add((i + 1, j - 2));
                }
            }
            if(( i + 2 < n ) & ( j - 1 >= 0 ))
            {
                if(arr[ ( i + 2 ) * n + j - 1 ] == 0)
                {
                    points.Add((i + 2, j - 1));
                }
            }
            if(( i - 1 >= 0 ) & ( j - 2 >= 0 ))
            {
                if(arr[ ( i - 1 ) * n + j - 2 ] == 0)
                {
                    points.Add((i - 1, j - 2));
                }
            }
            if(( i - 2 >= 0 ) & ( j - 1 >= 0 ))
            {
                if(arr[ ( i - 2 ) * n + j - 1 ] == 0)
                {
                    points.Add((i - 2, j - 1));
                }
            }
            return points;
        }

        /// <summary>
        /// Метод проверяет, заполнен ли массив
        /// </summary>
        /// <param name="arr">Проверяемый массив</param>
        /// <returns></returns>
        static bool IsNotZero(int[] arr)
        {
            var notZero = from x in arr
                          where x != 0
                          select x;
            if(notZero.Count() == arr.Length)
            {
                return true;
            }
            else return false;
        }

        /// <summary>
        /// Метод проверяет, есть ли вокруг заданной точки пустые места
        /// </summary>
        /// <param name="i">Координата по строке</param>
        /// <param name="j">Координата по столбцу</param>
        /// <param name="arr">Заданный массив точек</param>
        /// <returns></returns>
        static bool IsEmptyAround(int i, int j, int[] arr)
        {
            if(( i + 2 < n ) & ( j + 1 < n ) && arr[ ( i + 2 ) * n + j + 1 ] == 0)
            {
                return true;
            }
            else if(( i + 1 < n ) & ( j + 2 < n ) && arr[ ( i + 1 ) * n + j + 2 ] == 0)
            {
                return true;
            }
            else if(( i - 2 >= 0 ) & ( j + 1 < n ) && arr[ ( i - 2 ) * n + j + 1 ] == 0)
            {
                return true;
            }
            else if(( i - 1 >= 0 ) & ( j + 2 < n ) && arr[ ( i - 1 ) * n + j + 2 ] == 0)
            {
                return true;
            }
            else if(( i + 1 < n ) & ( j - 2 >= 0 ) && arr[ ( i + 1 ) * n + j - 2 ] == 0)
            {
                return true;
            }
            else if(( i + 2 < n ) & ( j - 1 >= 0 ) && arr[ ( i + 2 ) * n + j - 1 ] == 0)
            {
                return true;
            }
            else if(( i - 1 >= 0 ) & ( j - 2 >= 0 ) && arr[ ( i - 1 ) * n + j - 2 ] == 0)
            {
                return true;
            }
            else if(( i - 2 >= 0 ) & ( j - 1 >= 0 ) && arr[ ( i - 2 ) * n + j - 1 ] == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        static void Main( string[] args )
        {
            BoardZero(ref board);
            ShowBoard(board);
            Algorithm(0, 0, ref board);
            ShowBoard(board);
            Console.ReadLine();
        }
    }
}
