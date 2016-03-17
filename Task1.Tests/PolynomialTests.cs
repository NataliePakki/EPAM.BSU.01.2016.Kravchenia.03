using System;
using NUnit.Framework;

namespace Task1.Tests
{
    [TestFixture]
    public class PolynomialTests{

        [Test]
        [TestCase(new double[]{1,3,4}, new double[]{ 34,3535,0,1267},new double[] {35,3538,4,1267})]
        [TestCase(new [] { 1, 456.4567, 45.5 }, new [] { 34, 3535.45, 0, 1267.34,127 },new [] {35,3991.9067,45.5, 1267.34,127})]
        [TestCase(new double[] {  }, new double[] { 34, 3535, 0, 1267, 127 },new double[] { 34, 3535, 0, 1267, 127 })]
        public void AddTest(double[] coeff1, double[] coeff2, double[] coeffRes) {
            Polynomial pol1 = new Polynomial(coeff1);
            Polynomial pol2 = new Polynomial(coeff2);
            Polynomial result = new Polynomial(coeffRes);
            Polynomial pol3 = pol1 + pol2;

            Assert.AreEqual(pol3,result);
         }

        [Test]
        [TestCase(new double[] { 1, 3, 4 }, 4, new double[] { 5, 3, 4 })]
        public void AddNumberTest(double[] coeff1, double number, double[] coeffRes) {
            Polynomial pol1 = new Polynomial(coeff1);
            Polynomial result = new Polynomial(coeffRes);

            Polynomial pol3 = pol1 + number;

            Assert.AreEqual(pol3, result);
        }

        [Test]
        [TestCase(new double[] { 1, 3, 4 }, new double[] { -1,-3, -4 })]
        public void NegateTest(double[] coeff, double[] coeffRes) {
            Polynomial pol1 = new Polynomial(coeff);
            Polynomial result = new Polynomial(coeffRes);

            Polynomial pol3 = -pol1;

            Assert.AreEqual(pol3, result);
        }

        [Test]
        [TestCase(new double[] { 1, 3, 4 }, new double[] { 1 }, new double[] { 0, 3, 4 })]
        [TestCase(new double[]  { 1, 2.3, 45.5 }, new double[]{ 6, 7, 0, 20, 22 }, new double[] { -5, -4.7, 45.5, -20, -22 })]
        public void SubtractTest(double[] coeff1, double[] coeff2, double[] coeffRes) {
            Polynomial pol1 = new Polynomial(coeff1);
            Polynomial pol2 = new Polynomial(coeff2);
            Polynomial result = new Polynomial(coeffRes);

            Polynomial pol3 = pol1 - pol2;

            Assert.AreEqual(pol3, result);
        }
        [Test]
        [TestCase(new double[] { 1, 3, 4 }, 3, new double[] { 3, 9, 12 })]
        [TestCase(new double[] { 1, 3, 12 }, 0.1, new double[] { 0.1, 0.3, 1.2 })]
        public void MultiplyNumberTest(double[] coeff1, double number, double[] coeffRes) {
            Polynomial pol1 = new Polynomial(coeff1);
            Polynomial result = new Polynomial(coeffRes);

            Polynomial pol3 = pol1 * number;

            Assert.AreEqual(pol3, result);
        }
        [Test]
        [TestCase(new double[] { 1, 3, 4 }, new double[] { 3, 4 }, new double[] { 3, 13, 24, 16})]
        [TestCase(new double[] { 1, 2.3, 45.5 }, new double[] { 6, 7, 0, 20, 22 }, new double[] { 6, 20.8,289.1,338.5,68,960.6,1001})]
        public void MultiplyTest(double[] coeff1, double[] coeff2, double[] coeffRes) {
            Polynomial pol1 = new Polynomial(coeff1);
            Polynomial pol2 = new Polynomial(coeff2);
            Polynomial result = new Polynomial(coeffRes);

            Polynomial pol3 = pol1 * pol2;

            Assert.AreEqual(pol3, result);
        }
        [Test]
        [TestCase(new double[] { 12, 3, 27 }, 3, new double[] { 4, 1, 9})]
        public void DivideTest(double[] coeff1,double number, double[] coeffRes) {
            Polynomial pol1 = new Polynomial(coeff1);
            Polynomial result = new Polynomial(coeffRes);

            Polynomial pol3 = pol1 / number;

            Assert.AreEqual(pol3, result);
        }

        [Test]
        [ExpectedException(typeof(DivideByZeroException))]
        [TestCase(new double[] { 12, 3, 27 }, 0, new double[] { 4, 1, 9 })]
        public void DivideTest_Exception(double[] coeff1, double number, double[] coeffRes)        {
            Polynomial pol1 = new Polynomial(coeff1);
            Polynomial result = new Polynomial(coeffRes);

            Polynomial pol3 = pol1 / number;

            Assert.AreEqual(pol3, result);
        }
        [Test]
        [TestCase(new double[] { 12, 3, 27 }, 2, Result = 126)]
        public double CalculatePolunomialTest(double[] coeff1, double x){
            Polynomial pol1 = new Polynomial(coeff1);
            return pol1.CalculatePolynomial(x);

        }
    }
}
