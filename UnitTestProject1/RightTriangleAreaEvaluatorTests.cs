using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShapeAreaEvaluator;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

namespace UnitTestProject1
{
    [TestClass]
    public class RightTriangleAreaEvaluatorTests
    {
        [TestMethod]
        public void Success()
        {
            double hypo = 55.0;
            double cat = hypo / Math.Sqrt(2);
            double result;
            double result2;

            try
            {
                {
                    result = RightTriangleAreaEvaluator.GetArea(hypo, cat, cat);
                }

                {
                    result2 = RightTriangleAreaEvaluator.GetArea(cat, hypo, cat);
                    Assert.AreEqual<double>(result, result2, "Заменил порядок сторон и поменялся результат");
                }

                {
                    result2 = RightTriangleAreaEvaluator.GetArea(cat, cat, hypo);
                    Assert.AreEqual<double>(result, result2, "Заменил порядок сторон и поменялся результат");
                }
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [TestMethod]
        public void ExceptionOnNegativeLengths()
        {
            List<double[]> NegLengths = new List<double[]>() {new double[] {2.0,  3.1,    -4 },
                                                            new double[] {2.0,  -3.1,   -4 },
                                                            new double[] {-2.0, -3.1,   -4 }
                                                                };

            bool exCatched = false;

            foreach (double[] arLengths in NegLengths)
            {
                try
                {
                    double result = RightTriangleAreaEvaluator.GetArea(arLengths);
                }
                catch(AreaEvaluationException e)
                {
                    exCatched = true;
                }
                catch(Exception e)
                {
                    Assert.Fail(e.Message);
                }

                Assert.IsTrue(exCatched);
            }
        }

        [TestMethod]
        public void ExceptionOnOverflow()
        {
            bool exCatched = false;
            try
            {
                double result = RightTriangleAreaEvaluator.GetArea(double.MaxValue/2, double.MaxValue/2, double.MaxValue);
            }
            catch (AreaEvaluationException e)
            {
                if(e.InnerException is OverflowException) 
                    exCatched = true;
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
            Assert.IsTrue(exCatched);
        }

        [TestMethod]
        public void ExceptionOnIncosistentLengths()
        {
            bool exCatched = false;
            try
            {
                double result = RightTriangleAreaEvaluator.GetArea(1.0, 1.0, 1.0);
            }
            catch (AreaEvaluationException e)
            {
                exCatched = true;
            }
            catch (Exception e)
            {
                Assert.Fail();
            }
            Assert.IsTrue(exCatched);
        }
    }
}
