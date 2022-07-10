using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiguresLib
{
    public class Triangle : IFigure
    {
        public double a { get; protected set; }
        public double b { get; protected set; }
        public double c { get; protected set; }

        //public double abAngle { get; protected set; }
        //public double acAngle { get; protected set; }
        //public double bcAngle { get; protected set; }

        /// <summary>
        /// Создать фигуру типа "Треугольник"
        /// </summary>
        /// <param name="a">сторона "a"</param>
        /// <param name="b">сторона "b"</param>
        /// <param name="c">сторона "c"</param>
        /// <param name="abAngle">угол между "a" и "b" в радианах</param>
        /// <param name="acAngle">угол между "a" и "c" в радианах</param>
        /// <param name="bcAngle">угол между "b" и "c" в радианах</param>
        /// <remarks>При нехватке аргументов или их противоречии возникнет ArgumentException</remarks>
        public Triangle(double? a = null, double? b = null, double? c = null, double? abAngle = null, double? acAngle = null, double? bcAngle = null)
        {
            this.a = CalcSide(a, b, c, abAngle, acAngle, bcAngle);
            this.b = CalcSide(b, c, a, bcAngle, abAngle, acAngle);
            this.c = CalcSide(c, a, b, acAngle, bcAngle, abAngle);

            #region треугольник 
            //.........................
            //............*............
            //.........../.\...........
            //........../ac.\..........
            //........./.....\.........
            //.....a../.......\..c.....
            //......./.........\.......
            //....../...........\......
            //...../.ab.......bc.\.....
            //....*---------------*....
            //............b............
            #endregion

            //Вычислить длину сторону "a" (ref-параметр "a") по остальным параметрам треугольника. При конфликте выбросит ошибку
            double CalcSide(double? a, double? b = null, double? c = null, double? abAngle = null, double? acAngle = null, double? bcAngle = null)
            {
                double? tempA = a;

                if (b.HasValue)
                {
                    if (bcAngle.HasValue)
                    {
                        if (c.HasValue)
                            tempA = CalcByTwoSidesOneAngle(b.Value, c.Value, bcAngle.Value);
                        else if (abAngle.HasValue)
                            tempA = CalcBySideBAnglesAbBc(b.Value, abAngle.Value, bcAngle.Value);
                        else if (acAngle.HasValue)
                            tempA = CalcBySideBAnglesAcBc(b.Value, acAngle.Value, bcAngle.Value);
                    }
                    else if (abAngle.HasValue && acAngle.HasValue)
                        tempA = CalcBySideBAnglesAbAc(b.Value, abAngle.Value, acAngle.Value);
                }
                else if (c.HasValue)
                {
                    if (bcAngle.HasValue)
                    {
                        if (abAngle.HasValue)
                            tempA = CalcBySideBAnglesAcBc(c.Value, abAngle.Value, bcAngle.Value);
                        else if (acAngle.HasValue)
                            tempA = CalcBySideBAnglesAbBc(c.Value, acAngle.Value, bcAngle.Value);
                    }
                    else if (abAngle.HasValue && acAngle.HasValue)
                        tempA = CalcBySideBAnglesAbAc(c.Value, acAngle.Value, abAngle.Value);
                }

                if (!tempA.HasValue)
                    throw new ArgumentException("Недостаточно параметров для определения треугольника");
                else if (a.HasValue && a.Value != tempA.Value || b + c < tempA)
                    throw new ArgumentException("Параметры треугольника противоречат друг другу");
                else 
                    return tempA.Value;
            }

            //Получить сторону по двум сторонам и углу между ними
            double CalcByTwoSidesOneAngle(double b, double c, double angle)
            {
                return Math.Round(Math.Pow(b * b + c * c - 2 * b * c * Math.Cos(angle), 1.0 / 2), 2);
            }

            //Получить сторону "a" по стороне b и двум углам ab и ac
            double CalcBySideBAnglesAbAc(double b, double abAngle, double acAngle)
            {
                return Math.Round(b * Math.Sin(abAngle + acAngle) / Math.Sin(acAngle), 2);
            }

            //Получить сторону "a" по стороне b и двум углам ab и bc
            double CalcBySideBAnglesAbBc(double b, double abAngle, double bcAngle)
            {
                return Math.Round(b * Math.Sin(bcAngle) / Math.Sin(abAngle + bcAngle), 2);
            }

            //Получить сторону "a" по стороне b и двум углам ac и bc
            double CalcBySideBAnglesAcBc(double b, double acAngle, double bcAngle)
            {
                return Math.Round(b * Math.Sin(bcAngle) / Math.Sin(acAngle), 2);
            }
        }

        /// <summary>
        /// Проверка является ли треугольник прямоугольным
        /// </summary>
        /// <returns>результат проверки</returns>
        public bool isRight()
        {
            var sides = new List<double> { a, b, c };
            sides.Sort();

            var axa = sides[0] * sides[0];
            var bxb = sides[1] * sides[1];
            var hypoxhypo = sides[2] * sides[2];

            var error = 0.01;
            return hypoxhypo - error <= axa + bxb && axa + bxb  <= hypoxhypo + error; //погрешность в 0.01 может быть при работе с double
        }

        public double GetPerimeter() => a + b + c;

        public double GetSquare()
        {
            var p = (a + b + c) / 2;
            return Math.Pow(p * (p - a) * (p - b) * (p - c), 1.0/2);
        }
    }
}
