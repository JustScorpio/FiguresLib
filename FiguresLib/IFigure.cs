using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiguresLib
{
    internal interface IFigure
    {
        /// <summary>
        /// Вычислить площадь фигуры
        /// </summary>
        /// <returns>площадь фигуры</returns>
        public double GetSquare();

        /// <summary>
        /// Вычислить периметр фигуры
        /// </summary>
        /// <returns>периметр фигуры</returns>
        public double GetPerimeter();
    }
}
