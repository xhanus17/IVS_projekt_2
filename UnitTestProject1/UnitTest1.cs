using Microsoft.VisualStudio.TestTools.UnitTesting;
using MathNS;
namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Sum_test()
        {
            Assert.AreEqual(8.3266, CalcMath.Sum(3.8451,4.4815));
            Assert.AreEqual(66808, CalcMath.Sum(15324, 51484));
            Assert.AreEqual(0, CalcMath.Sum(5, -5));
        }

        [TestMethod]
        public void Sub_test()
        {
            Assert.AreEqual(-36160, CalcMath.Sub(15324, 51484));
            Assert.AreEqual(0, CalcMath.Sub(5, 5));
            Assert.AreEqual(15, CalcMath.Sub(94575.15, 94560.15));
        }

        [TestMethod]
        public void Mul_test()
        {
            Assert.AreEqual(1848, CalcMath.Mul(154, 12));
            Assert.AreEqual(25, CalcMath.Mul(5, 5));
            Assert.AreEqual(15, CalcMath.Mul(3, 5));
            Assert.AreEqual(3.9184, CalcMath.Mul(2.48, 1.58));
            Assert.AreEqual(0, CalcMath.Mul(78.48458, 0));
            Assert.AreEqual(0, CalcMath.Mul(0, 554.72));
        }

        [TestMethod]
        public void Div_test()
        {
            Assert.AreEqual(12.833333333333333333333, CalcMath.Div(154, 12));
            Assert.AreEqual(1, CalcMath.Div(5, 5));
            Assert.AreEqual(0.6, CalcMath.Div(3, 5));
            Assert.AreEqual(4.5, CalcMath.Div(9, 2));
            Assert.AreEqual(0, CalcMath.Div(0, 554.72));
        }

        [TestMethod]
        public void Pow_Test()
        {
            Assert.AreEqual(1953.125, CalcMath.Pow(12.5, 3));
            Assert.AreEqual(25, CalcMath.Pow(5, 2));
            Assert.AreEqual(81, CalcMath.Pow(3, 4));
            Assert.AreEqual(251, CalcMath.Pow(251, 1));
            Assert.AreEqual(1, CalcMath.Pow(3654.5155, 0));
            Assert.AreEqual(0.25, CalcMath.Pow(2, -2));
            Assert.AreEqual(2, CalcMath.Pow(4, 0.5));
            Assert.AreEqual(-8, CalcMath.Pow(-8, 1));
            Assert.AreEqual(16, CalcMath.Pow(-4, 2));
            Assert.AreEqual(-125, CalcMath.Pow(-5, 3));
            Assert.AreEqual(625, CalcMath.Pow(-5, 4));
        }

        [TestMethod]
        public void Sqrt_Test()
        {
            Assert.AreEqual(2, CalcMath.Sqrt(4,16));
            Assert.AreEqual(8, CalcMath.Sqrt(2, 64));
            Assert.AreEqual(1, CalcMath.Sqrt(3, 1));
            Assert.AreEqual(0, CalcMath.Sqrt(2, 0));
        }
    }
}
