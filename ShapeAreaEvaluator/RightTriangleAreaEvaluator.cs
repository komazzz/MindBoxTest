using System;



namespace ShapeAreaEvaluator
{
   
    public static class RightTriangleAreaEvaluator
    {
        /// <summary>
        /// Вычисляет площадь прямоугольного треугольника
        /// длины сторон треугольника берутся из первых трех элементов массива
        /// </summary>
        /// <param name="Sides"> Массив длин сторон типа Double</param>
        /// <returns>Площадь площадь прямоугольного треугольника</returns>
        /// <exception cref="System.IndexOutOfRangeException">Выбрасывается если длина массива менее трех</exception>
        /// <exception cref="ShapeAreaEvaluator.AreaEvaluationException">Выбрасывается при любых проблемах в вычислении площади</exception>
        public static double GetArea(Double[] Sides)
        {
            return GetArea(Sides[0], Sides[1], Sides[2]);
        }
        

        /// <summary>
        /// Вычисляет площадь прямоугольного треугольника
        /// </summary>
        /// <param name="ASideLength">Длина стороны A</param>
        /// <param name="BSideLength">Длина стороны B</param>
        /// <param name="CSideLength">Длина стороны С</param>
        /// <returns>Площадь площадь прямоугольного треугольника</returns>
        /// <exception cref="ShapeAreaEvaluator.AreaEvaluationException">Выбрасывается при любых проблемах в вычислении площади</exception>
        public static  double GetArea(Double ASideLength, Double BSideLength, Double CSideLength)
        {
            double[] sides = new double[]{ASideLength,BSideLength,CSideLength};
                try
                {
                    CheckSides(sides);
                    var triSides = GetRightSides(sides);
                    CheckRightTriangle(triSides);

                    double area = 0.5 * triSides.Cath1 * triSides.Cath2;
                    return area;
                }
                catch (Exception e)
                {
                    throw new AreaEvaluationException("При вычислении площади прямоугольного треугольника произошло исключение", e);
                }
        }


        private static void  CheckSides(Double[] sides)
        {
            for (int i = 0; i < 3;i++ )
            {
                if (sides[i] < 0)
                    throw new ArgumentException(String.Format("Сторона {0} имеет отрицательную длину.",i));
            }
        }

        private static RightTriangleSides GetRightSides(Double[] sides)
        {
            //Чтобы не создавать зависимостей  на Linq вычислять гипотенузу будем руками
            
            //Ищем максимальный элемент - гипотенузу треугольника
            int MaxElementIndex = 0;
            for(int i=1;i<3;i++)
            {
                if (sides[MaxElementIndex] < sides[i])
                    MaxElementIndex = i;
            }

            RightTriangleSides retVal = new RightTriangleSides() { Hypo = sides[MaxElementIndex], Cath1 = -1.0, Cath2 = -1.0 };

            for (int i = 0; i < 3; i++)
            {
                if (i != MaxElementIndex)
                {
                    if (retVal.Cath1 < 0.0)
                        retVal.Cath1 = sides[i];
                    else
                        retVal.Cath2 = sides[i];
                }
            }
            return retVal;
        }

        private static void CheckRightTriangle(RightTriangleSides Sides)
        {
            if (!Sides.IsRightTriangle())
                throw new ArgumentException("Соотношения длин сторон не соответствуют прямоугольному треугольнику");
        }


        private struct RightTriangleSides
        {
            public double Cath1;
            public double Cath2;
            public double Hypo;

            public bool IsRightTriangle()
            {
                    double hypo_2 = Math.Pow(Hypo,2);
                    if(Double.IsInfinity(hypo_2))
                        throw new OverflowException("Квадрат гипотенузы ушел в бесконечность.");

                    double SumCath2 = Math.Pow(Cath1,2) + Math.Pow(Cath2,2);
                    if (Double.IsInfinity(SumCath2))
                        throw new OverflowException("Сумма квадратов катетов ушла в бесконечность.");

                    if (Math.Abs(hypo_2 - SumCath2) < 1.0E-12)
                        return true;
                    else
                        return false;
            }
        }
    }







}
