using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FiguresLib
{
    public class Triangle : IFigure
    {
        #region визуализация треугольника 
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

        public double a { get; private set; }
        public double b { get; private set; }
        public double c { get; private set; }

        public double abAngle { get; private set; }
        public double acAngle { get; private set; }
        public double bcAngle { get; private set; }

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
        public Triangle(double a, double b, double c)
        {
            if (a + b < c || a + c < b || b + c < a)
                throw new ArgumentException("Triangle is not exists");

            this.a = a;
            this.b = b;
            this.c = c;

            this.abAngle = CalcAngleByThreeSides(a, b, c);
            this.acAngle = CalcAngleByThreeSides(c, a, b);
            this.bcAngle = CalcAngleByThreeSides(b, c, a);
        }

        /// <summary>
        /// Приватный конструктор. используется только в статичных методах создания экземпляра класса
        /// </summary>
        private Triangle() { }

        /// <summary>
        /// Создать треугольник по двум сторонам и углу между ними
        /// </summary>
        /// <param name="a">сторона a</param>
        /// <param name="b">сторона b</param>
        /// <param name="abAngle">угол между сторонами a и b</param>
        /// <returns>Треугольник</returns>
        /// <exception cref="ArgumentException">Невозможно инициализировать треугольник по заданным аргументам</exception>
        public static Triangle CreateByTwoSidesOneAngle(double a, double b, double abAngle)
        {
            if (abAngle > Math.PI)
                abAngle = 2 * Math.PI - abAngle;

            if (abAngle == 0)
                throw new ArgumentException("Triangle is not exists");

            var triangle = new Triangle();

            triangle.a = a;
            triangle.b = b;
            triangle.abAngle = abAngle;

            var c = CalcSideByTwoSidesOneAngle(a, b, abAngle);

            triangle.c = c;
            triangle.bcAngle = CalcAngleByThreeSides(b, c, a);
            triangle.acAngle = CalcAngleByThreeSides(a, c, b);

            return triangle;
        }

        /// <summary>
        /// Создать треугольник по стороне и двум прилегающим углам
        /// </summary>
        /// <param name="a">сторона a</param>
        /// <param name="abAngle">угол между сторонами a и b</param>
        /// <param name="acAngle">угол между сторонами a и c</param>
        /// <returns>Треугольник</returns>
        /// <exception cref="ArgumentException">Невозможно инициализировать треугольник по заданным аргументам</exception>
        public static Triangle CreateByOneSideTwoAngles(double a, double abAngle, double acAngle)
        {
            if (abAngle > Math.PI)
                abAngle = 2 * Math.PI - abAngle;

            if (acAngle > Math.PI)
                acAngle = 2 * Math.PI - acAngle;

            if (abAngle == 0 || acAngle == 0 || abAngle + acAngle > Math.PI)
                throw new ArgumentException("Triangle is not exists");

            var triangle = new Triangle();

            triangle.a = a;
            triangle.abAngle = abAngle;
            triangle.acAngle = acAngle;

            var bcAngle = CalcAngleByTwoAngles(abAngle, acAngle);

            triangle.bcAngle = bcAngle;
            triangle.b = CalcSideByTwoAnglesOneSide(acAngle, bcAngle, a);
            triangle.c = CalcSideByTwoAnglesOneSide(abAngle, bcAngle, a);

            return triangle;
        }

        /// <summary>
        /// Вычислить сторону треугольника по двум другим сторонам и углу между ними
        /// </summary>
        /// <param name="a">сторона a</param>
        /// <param name="b">сторона b</param>
        /// <param name="abAngle">угол между сторонами a и b</param>
        /// <returns>Длину стороны c</returns>
        private static double CalcSideByTwoSidesOneAngle(double a, double b, double abAngle)
        {
            return Math.Pow(a * a + b * b - 2 * a * b * Math.Cos(abAngle), 0.5);
        }

        /// <summary>
        /// Вычислить угол треугольника по двум другим углам
        /// </summary>
        /// <param name="abAngle">первый угол</param>
        /// <param name="acAngle">второй угол</param>
        /// <returns>Величину угла в радианах</returns>
        private static double CalcAngleByTwoAngles(double abAngle, double acAngle)
        {
            //сумма углов треугольника в радианах - число Пи
            return Math.PI - abAngle - acAngle;
        }

        /// <summary>
        /// Получить сторону по двум углам и стороне
        /// </summary>
        /// <param name="abAngle">угол ab</param>
        /// <param name="bcAngle">угол bc</param>
        /// <param name="a">сторона a</param>
        /// <returns>Длину стороны c</returns>
        /// <remarks>Возвращает сторону противоположную углу в первом аргументе</remarks>
        private static double CalcSideByTwoAnglesOneSide(double abAngle, double bcAngle, double a)
        {
            return a * Math.Sin(abAngle) / Math.Sin(bcAngle);
        }

        /// <summary>
        /// Получить угол ab по трём сторонам треугольника
        /// </summary>
        /// <param name="a">сторона a</param>
        /// <param name="b">сторона b</param>
        /// <param name="c">сторона c</param>
        /// <returns>Величину угла ab в радианах</returns>
        /// <remarks>Возвращается угол между сторонами в 1 и 2 аргументах</remarks>
        private static double CalcAngleByThreeSides(double a, double b, double c)
        {
            return Math.Acos((a * a + b * b - c * c) / (2 * a * b));
        }

        /// <summary>
        /// Вычислить периметр треугольника
        /// </summary>
        /// <returns>Периметр</returns>
        public double GetPerimeter() => a + b + c;

        /// <summary>
        /// Вычислить площадб треугольника
        /// </summary>
        /// <returns>Площадь</returns>
        public double GetSquare()
        {
            var halfPerimeter = GetPerimeter() / 2;
            return Math.Round(Math.Pow(halfPerimeter * (halfPerimeter - a) * (halfPerimeter - b) * (halfPerimeter - c), 0.5), 2);
        }

        /// <summary>
        /// Проверить является ли треугольник прямоугольным
        /// </summary>
        /// <returns>Результат проверки</returns>
        public bool IsRight()
        {
            var RightAngle = Math.Round(Math.PI / 2, 2);
            return Math.Round(abAngle, 2) == RightAngle || Math.Round(bcAngle, 2) == RightAngle || Math.Round(acAngle, 2) == RightAngle;
        }
    }
}
