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
            var circle = new Circle(radius);

            var factSquare = circle.GetSquare();
            var expectSquare = 0;

            var factParimeter = circle.GetPerimeter();
            var expectPerimeter = 0;

            Assert.Equal(expectSquare, factSquare);
            Assert.Equal(expectPerimeter, factParimeter);
        }
        #endregion

        #region triangle tests
        [Fact]
        public void ThreeSidesRightTriangle()
        {
            var a = 6;
            var b = 10;
            var c = 8;

            var triangle = new Triangle(a, b, c);

            var factSquare = triangle.GetSquare();
            var expectSquare = a * c / 2.0;

            var factPerimeter = triangle.GetPerimeter();
            var expectPerimeter = a + b + c;

            Assert.Equal(expectSquare, factSquare);
            Assert.Equal(expectPerimeter, factPerimeter);

            Assert.True(triangle.isRight());
        }

        [Fact]
        public void ThreeSidesSimpleTriangle()
        {
            var a = 18;
            var b = 3;
            var c = 20;

            var triangle = new Triangle(a, b, c);

            var factSquare = Math.Round(triangle.GetSquare(), 2);
            var expectSquare = 21.18;

            var factPerimeter = triangle.GetPerimeter();
            var expectPerimeter = a + b + c;

            Assert.Equal(expectSquare, factSquare);
            Assert.Equal(expectPerimeter, factPerimeter);

            Assert.False(triangle.isRight());
        }

        [Fact]
        public void ThreeSidesIncorrectTriangle()
        {
            var a = 5;
            var b = 30;
            var c = 41;

            Assert.Throws<ArgumentException>(() => new Triangle(a, b, c));
        }

        [Fact]
        public void TwoSidesIncorrectTriangle()
        {
            var a = 5;
            var b = 30;

            Assert.Throws<ArgumentException>(() => new Triangle(a, b, null));
        }

        [Fact]
        public void TwoSidesOneAngleRightTriangle()
        {
            var a = 6;
            var b = 8;
            var expectC = 10;
            var abAngle = 90 * Math.PI / 180;

            var triangle = new Triangle(a, b, null, abAngle);

            var factSquare = triangle.GetSquare();
            var expectSquare = a * b / 2.0;

            var factPerimeter = triangle.GetPerimeter();
            var expectPerimeter = a + b + expectC;

            Assert.Equal(expectSquare, factSquare);
            Assert.Equal(expectPerimeter, factPerimeter);
            Assert.Equal(expectC, triangle.c);

            Assert.True(triangle.isRight());
        }

        [Fact]
        public void TwoSidesOneAngleSimpleTriangle()
        {
            var b = 19;
            var c = 19;
            var expectA = 6.5;

            var bcAngle = 19.7 * Math.PI / 180;

            var triangle = new Triangle(null, b, c, null, null, bcAngle);

            var factSquare = Math.Round(triangle.GetSquare(), 2);
            var expectSquare = 60.84;

            var factPerimeter = triangle.GetPerimeter();
            var expectPerimeter = b+c+expectA;

            Assert.Equal(expectSquare, factSquare);
            Assert.Equal(expectPerimeter, factPerimeter);
            Assert.Equal(expectA, triangle.a);

            Assert.False(triangle.isRight());
        }

        [Fact]
        public void TwoSidesOneTrickyAngleRightTriangle()
        {
            var a = 19;
            var b = 19;
            var expectC = 26.87;
            
            var abAngle = 270 * Math.PI / 180; // 90 Градусов

            var triangle = new Triangle(a, b, null, abAngle);

            var factSquare = triangle.GetSquare();
            var expectSquare = 180.5;

            var factPerimeter = triangle.GetPerimeter();
            var expectPerimeter = a + b + expectC;

            Assert.InRange(factSquare, expectSquare - 0.01, expectSquare + 0.01);
            Assert.InRange(factPerimeter, expectPerimeter - 0.01, expectPerimeter + 0.01);
            Assert.InRange(triangle.c, expectC - 0.01, expectC + 0.01);

            Assert.True(triangle.isRight());
        }

        [Fact]
        public void OneSideTwoAnglesTriangle()
        {
            var a = 15;
            var expectB = 9.53;
            var expectC = 17.49;

            var abAngle = 88 * Math.PI / 180;
            var acAngle = 33 * Math.PI / 180;

            var triangle = new Triangle(a, null, null, abAngle, acAngle);

            var factSquare = triangle.GetSquare();
            var expectSquare = 71.44;

            var factPerimeter = triangle.GetPerimeter();
            var expectPerimeter = a + expectB + expectC;

            Assert.InRange(factSquare, expectSquare - 0.01, expectSquare + 0.01);
            Assert.InRange(factPerimeter, expectPerimeter - 0.01, expectPerimeter + 0.01);
            Assert.InRange(triangle.b, expectB - 0.01, expectB + 0.01);
            Assert.InRange(triangle.c, expectC - 0.01, expectC + 0.01);

            Assert.False(triangle.isRight());
        }

        [Fact]
        public void ConflicTriangle1()
        {
            var a = 4;
            var b = 4;
            var c = 4;

            var abAngle = 90 * Math.PI / 180;

            Assert.Throws<ArgumentException>(() => new Triangle(a, b, c, abAngle));
        }

        [Fact]
        public void ConflicTriangle2()
        {
            var a = 4;
            var b = 1;
            var c = 4;

            var abAngle = 90 * Math.PI / 180;
            var bcAngle = 45 * Math.PI / 180;
            var acAngle = 45 * Math.PI / 180;

            Assert.Throws<ArgumentException>(() => new Triangle(a, b, c, abAngle, acAngle, bcAngle));
        }
        #endregion
    }
}