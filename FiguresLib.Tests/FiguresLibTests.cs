namespace FiguresLib.Tests
{
    public class FiguresLibTests
    {
        #region circle tests
        [Fact]
        public void CorrectCircle()
        {
            var radius = 5.786;
            var circle = new Circle(radius);

            var factSquare = circle.GetSquare();
            var expectSquare = Math.PI * radius * radius;

            var factParimeter = circle.GetPerimeter();
            var expectPerimeter = Math.PI * 2 * radius;

            Assert.Equal(expectSquare, factSquare);
            Assert.Equal(expectPerimeter, factParimeter);
        }

        [Fact]
        public void IncorrectCircle()
        {
            var radius = -3;
            Assert.Throws<ArgumentException>(() => new Circle(radius));
        }

        [Fact]
        public void ZeroCircle()
        {
            var radius = 0;
            Assert.Throws<ArgumentException>(() => new Circle(radius));
        }
        #endregion

        #region triangle tests
        private void CheckTriangle(
            Triangle triangle,
            double a,
            double b,
            double c,
            double bcAngle,
            double acAngle,
            double abAngle,
            double square,
            double perimeter,
            bool isRight)
        {
            Assert.InRange(a - triangle.a, -0.1, 0.1);
            Assert.InRange(b - triangle.b, -0.1, 0.1);
            Assert.InRange(c - triangle.c, -0.1, 0.1);
            Assert.InRange(bcAngle - triangle.bcAngle, -0.1, 0.1);
            Assert.InRange(acAngle - triangle.acAngle, -0.1, 0.1);
            Assert.InRange(abAngle - triangle.abAngle, -0.1, 0.1);
            Assert.InRange(square - triangle.GetSquare(), -0.1, 0.1);
            Assert.InRange(perimeter - triangle.GetPerimeter(), -0.1, 0.1);
            Assert.Equal(isRight, triangle.IsRight());
        }

        [Fact]
        public void SimpleTriangle()
        {
            var a = 6;
            var b = 9;
            var c = 11;

            var bcAngle = 0.576;
            var acAngle = 0.957;
            var abAngle = 1.608;

            var expectedSquare = 26.981;
            var expectedPerimeter = a + b + c;
            var isRight = false;

            var triangle = new Triangle(a, b, c);
            CheckTriangle(triangle, a, b, c, bcAngle, acAngle, abAngle, expectedSquare, expectedPerimeter, isRight);
            triangle = Triangle.CreateByOneSideTwoAngles(a, abAngle, acAngle);
            CheckTriangle(triangle, a, b, c, bcAngle, acAngle, abAngle, expectedSquare, expectedPerimeter, isRight);
            triangle = Triangle.CreateByTwoSidesOneAngle(a, b, abAngle);
            CheckTriangle(triangle, a, b, c, bcAngle, acAngle, abAngle, expectedSquare, expectedPerimeter, isRight);
        }

        [Fact]
        public void RightTriangle()
        {
            var a = 9;
            var b = 15;
            var c = 12;

            var bcAngle = 0.644;
            var acAngle = 1.571;
            var abAngle = 0.927;

            var expectedSquare = 54;
            var expectedPerimeter = a + b + c;
            var isRight = true;

            var triangle = new Triangle(a, b, c);
            CheckTriangle(triangle, a, b, c, bcAngle, acAngle, abAngle, expectedSquare, expectedPerimeter, isRight);
            triangle = Triangle.CreateByOneSideTwoAngles(a, abAngle, acAngle);
            CheckTriangle(triangle, a, b, c, bcAngle, acAngle, abAngle, expectedSquare, expectedPerimeter, isRight);
            triangle = Triangle.CreateByTwoSidesOneAngle(a, b, abAngle);
            CheckTriangle(triangle, a, b, c, bcAngle, acAngle, abAngle, expectedSquare, expectedPerimeter, isRight);
        }

        [Fact]
        public void IsoscelesTriangle()
        {
            var a = 21;
            var b = 21;
            var c = 5;

            var bcAngle = 1.451;
            var acAngle = 1.451;
            var abAngle = 0.239;

            var expectedSquare = 52.127;
            var expectedPerimeter = a + b + c;
            var isRight = false;

            var triangle = new Triangle(a, b, c);
            CheckTriangle(triangle, a, b, c, bcAngle, acAngle, abAngle, expectedSquare, expectedPerimeter, isRight);
            triangle = Triangle.CreateByOneSideTwoAngles(a, abAngle, acAngle);
            CheckTriangle(triangle, a, b, c, bcAngle, acAngle, abAngle, expectedSquare, expectedPerimeter, isRight);
            triangle = Triangle.CreateByTwoSidesOneAngle(a, b, abAngle);
            CheckTriangle(triangle, a, b, c, bcAngle, acAngle, abAngle, expectedSquare, expectedPerimeter, isRight);
        }

        [Fact]
        public void RightIsoscelesTriangle()
        {
            var a = 14;
            var b = 14;
            var c = 19.799;

            var bcAngle = 0.785;
            var acAngle = 0.785;
            var abAngle = 1.571;

            var expectedSquare = 98;
            var expectedPerimeter = a + b + c;
            var isRight = true;

            var triangle = new Triangle(a, b, c);
            CheckTriangle(triangle, a, b, c, bcAngle, acAngle, abAngle, expectedSquare, expectedPerimeter, isRight);
            triangle = Triangle.CreateByOneSideTwoAngles(a, abAngle, acAngle);
            CheckTriangle(triangle, a, b, c, bcAngle, acAngle, abAngle, expectedSquare, expectedPerimeter, isRight);
            triangle = Triangle.CreateByTwoSidesOneAngle(a, b, abAngle);
            CheckTriangle(triangle, a, b, c, bcAngle, acAngle, abAngle, expectedSquare, expectedPerimeter, isRight);
        }

        [Fact]
        public void EquilateralTriangle()
        {
            var a = 7;
            var b = 7;
            var c = 7;

            var bcAngle = 1.047;
            var acAngle = 1.047;
            var abAngle = 1.047;

            var expectedSquare = 21.218;
            var expectedPerimeter = a + b + c;
            var isRight = false;

            var triangle = new Triangle(a, b, c);
            CheckTriangle(triangle, a, b, c, bcAngle, acAngle, abAngle, expectedSquare, expectedPerimeter, isRight);
            triangle = Triangle.CreateByOneSideTwoAngles(a, abAngle, acAngle);
            CheckTriangle(triangle, a, b, c, bcAngle, acAngle, abAngle, expectedSquare, expectedPerimeter, isRight);
            triangle = Triangle.CreateByTwoSidesOneAngle(a, b, abAngle);
            CheckTriangle(triangle, a, b, c, bcAngle, acAngle, abAngle, expectedSquare, expectedPerimeter, isRight);
        }

        [Fact]
        public void TooWideAngleTriangle()
        {
            var a = 6;
            var b = 9;
            var c = 11;

            var bcAngle = 0.576;
            var acAngle = 0.957;
            var abAngle = 1.608;

            var bcAngleWide = bcAngle + Math.PI;
            var acAngleWide = acAngle + 2 * Math.PI;

            var expectedSquare = 26.981;
            var expectedPerimeter = a + b + c;
            var isRight = false;

            //”глы шире 180 градусов 
            var triangle = Triangle.CreateByOneSideTwoAngles(c, acAngleWide, bcAngleWide);
            CheckTriangle(triangle, c, a, b, abAngle, bcAngle, acAngle, expectedSquare, expectedPerimeter, isRight);
        }

        [Fact]
        public void IncorrectSidesTriangle()
        {
            var a = 20;
            var b = 9;
            var c = 11;

            Assert.Throws<ArgumentException>(() => { var triangle = new Triangle(a, b, c); });
        }

        [Fact]
        public void IncorrectAngleTriangle()
        {
            var a = 20;
            var b = 9;

            var abAngle = -1.5;

            Assert.Throws<ArgumentException>(() => { var triangle = Triangle.CreateByTwoSidesOneAngle(a, b, abAngle); });
        }
        #endregion
    }
}