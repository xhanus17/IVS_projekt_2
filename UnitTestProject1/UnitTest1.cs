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
            Assert.AreEqual(8.3266, math.Sum(3.8451,4.4815));
            Assert.AreEqual(66808, math.Sum(15324, 51484));
            Assert.AreEqual(0, math.Sum(5, -5));
        }

        [TestMethod]
        public void Sub_test()
        {
            Assert.AreEqual(-36160, math.Sub(15324, 51484));
            Assert.AreEqual(0, math.Sub(5, 5));
            Assert.AreEqual(15, math.Sub(94575.15, 94560.15));
        }

        [TestMethod]
        public void Mul_test()
        {
            Assert.AreEqual(1848, math.Mul(154, 12));
            Assert.AreEqual(25, math.Mul(5, 5));
            Assert.AreEqual(15, math.Mul(3, 5));
            Assert.AreEqual(3.9184, math.Mul(2.48, 1.58));
            Assert.AreEqual(0, math.Mul(78.48458, 0));
            Assert.AreEqual(0, math.Mul(0, 554.72));
        }

        [TestMethod]
        public void Div_test()
        {
            Assert.AreEqual(12.833333333333333333333, math.Div(154, 12));
            Assert.AreEqual(1, math.Div(5, 5));
            Assert.AreEqual(0.6, math.Div(3, 5));
            Assert.AreEqual(4.5, math.Div(9, 2));
            Assert.AreEqual(0, math.Div(78.48458, 0));
            Assert.AreEqual(0, math.Div(0, 554.72));
        }

        [TestMethod]
        public void Pow_Test()
        {
            Assert.AreEqual(1953.125, math.Pow(12.5, 3));
            Assert.AreEqual(25, math.Pow(5, 2));
            Assert.AreEqual(81, math.Pow(3, 4));
            Assert.AreEqual(251, math.Pow(251, 1));
            Assert.AreEqual(1, math.Pow(3654.5155, 0));
            Assert.AreEqual(0.25, math.Pow(2, -2));
            Assert.AreEqual(2, math.Pow(4, 0.5));
            Assert.AreEqual(-8, math.Pow(-8, 1));
            Assert.AreEqual(16, math.Pow(-4, 2));
            Assert.AreEqual(-125, math.Pow(-5, 3));
            Assert.AreEqual(625, math.Pow(-5, 4));
        }

        [TestMethod]
        public void Sqrt_Test()
        {
            Assert.AreEqual(2, math.Sqrt(4,16));
            Assert.AreEqual(8, math.Sqrt(2, 64));
            Assert.AreEqual(1, math.Sqrt(3, 1));
            Assert.AreEqual(0, math.Sqrt(2, 0));
        }
    }
}
