namespace FiguresLib
{
    public class Circle : IFigure
    {
        public double radius { get; private set; }

        /// <summary>
        /// Создать фигуру типа "Круг"
        /// </summary>
        /// <param name="radius">Радиус</param>
        public Circle(double radius)
        {
            if (radius <= 0)
                throw new ArgumentException("Circle is not exists!");

            this.radius = radius;
        }

        public double GetSquare() => Math.PI * Math.Pow(radius, 2);

        public double GetPerimeter() => 2 * Math.PI * radius;
    }
}