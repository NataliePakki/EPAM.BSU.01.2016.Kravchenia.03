using System;
using System.Linq;
using System.Text;

namespace Task1
{
    public sealed class Polynomial {
        public readonly double[] Coefficients;
        public readonly int Degree;
        private const double Eps = 1E-6;

        public Polynomial(params double[] coeff) {
            Coefficients = coeff;
            Degree = GetDegree(coeff);
        }


        public static Polynomial operator +(Polynomial p1, Polynomial p2) {
            return Add(p1, p2);
        }
        public static Polynomial operator +(Polynomial p, double number) {
            return Add(p, number);
        }
        public static Polynomial operator +(double number, Polynomial p1){
            return Add(p1, number);
        }
        public static Polynomial operator +(Polynomial p) {
            return p;
        }
        public static Polynomial operator -(Polynomial p1, Polynomial p2){
            return Subtract(p1, p2);
        }
        public static Polynomial operator -(Polynomial p, double number){
            return Subtract(p,number);
        }
        public static Polynomial operator -(double number, Polynomial p){
            return Subtract(-p, number);
        }
        public static Polynomial operator -(Polynomial p){
            return Negate(p);
        }

        public static Polynomial operator *(Polynomial p1, Polynomial p2) {
            return Multiply(p1, p2);
        }

        public static Polynomial operator * (int number,Polynomial p) {
            return Multiply(number,p);
        }
        public static Polynomial operator *(Polynomial p, double number) {
            return Multiply(number,p);
        }
        public static Polynomial operator /(Polynomial p, double number) {
            return Divide(p, number);
        }

        public static bool operator ==(Polynomial p1, Polynomial p2) {
            return p1.Equals(p2);
        }
        public static bool operator !=(Polynomial p1, Polynomial p2) {
            return !p1.Equals(p2);
        }

        public double this[int i] {
            get {
                if (i >= 0 && i <= Degree)
                    return Coefficients[i];
                throw new IndexOutOfRangeException();
            }
        }

        public static Polynomial Add(Polynomial p1, Polynomial p2){
            if (p1.Degree == 0) return Add(p2, p1[0]);
            if (p2.Degree == 0) return Add(p1, p2[0]);
            int minDegree;
            double[] resultCoeff;
            if (p1.Degree > p2.Degree){
                resultCoeff = p1.Coefficients;
                minDegree = p2.Degree;
            }
            else{
                resultCoeff = p2.Coefficients;
                minDegree = p1.Degree;
            }
            for (int i = 0; i <= minDegree; i++){
                resultCoeff[i] = p1[i] + p2[i];
            }
            return new Polynomial(resultCoeff);
        }

        public static Polynomial Add(Polynomial p, double number) {
            double[] resultCoeff = p.Coefficients;
            resultCoeff[0] += number;
            return new Polynomial(resultCoeff);
        }

        public static Polynomial Subtract(Polynomial p1, Polynomial p2) {
            return Add(p1, -p2);
        }

        public static Polynomial Subtract(Polynomial p, double number) {
            return Add(p,-number);
        }
        public static Polynomial Negate(Polynomial p) {
            int resultDegree = p.Degree;
            double[] resultCoeff = new double[resultDegree + 1];
            for (int i = 0; i <= resultDegree; i++) {
                resultCoeff[i] = -p[i];
            }
            return new Polynomial(resultCoeff);
        }

        public static Polynomial Multiply(double number, Polynomial p) {
            double[] resultCoeff = new double[p.Degree + 1];
            for (int i = 0; i <= p.Degree; i++)
                resultCoeff[i] = number*p[i];
            return new Polynomial(resultCoeff);
        }

        public static Polynomial Multiply(Polynomial p1, Polynomial p2) {
            if (p1.Degree == 0) return Multiply(p1[0], p2);
            if (p2.Degree == 0) return Multiply(p2[0], p1);
            int resultDegree = p1.Degree + p2.Degree;
            double[] resultCoeff = new double[resultDegree + 1];
            for(int i = p1.Degree; i >= 0; i--) 
                for(int j = p2.Degree; j >= 0; j--){
                    resultCoeff[i + j] += p1[i]*p2[j];
            }
            return new Polynomial(resultCoeff);
        }

        public static Polynomial Divide(Polynomial p, double number) {
            if(Math.Abs(number) > Eps) return Multiply(1/number,p);
            throw  new DivideByZeroException();
        }

        public double CalculatePolynomial(double x) {
            double result = 0;
            for (int i = 0; i <= Degree; i++) {
                result += this[i] * Math.Pow(x, i);
            }
            return result;
        }

        private static int GetDegree(double[] coeff){
            int degree = coeff.Length - 1;
            int countZero = 0;
            for (int i = degree; i > 0; i--){
                if (Math.Abs(coeff[i]) < Eps)
                    countZero++;
                else break;
            }
            return degree - countZero;
        }

        public override string ToString() {
            var result = new StringBuilder();
            for (int i = Degree; i >= 0; i--) {
                if(Math.Abs(this[i]) < Eps) continue;
                string sign = "";
                if (this[i] > 0 && i != Degree)
                    sign = "+";
                if (i == 1) {
                    if (Math.Abs((int) (this[1] - 1)) < Eps)
                        result.Append(sign + "x");
                    else
                        result.Append(sign + this[1] + "x");
                    continue;
                }
                if (i == 0) {
                    result.Append(sign + this[i]);
                    continue;
                }
                result.Append(sign + this[i] + "x^" + i);
            }
           return result.ToString();
        }

        public override bool Equals(object obj) {
            if (obj == null) return false;
            if (GetType() != obj.GetType()) return false;
            return Equals(this, (Polynomial) obj);           
        }

        public bool Equals(Polynomial p1, Polynomial p2) {
            if (Degree != p2.Degree) return false;
            for (int i = 0; i <= Degree; i++){
                if (Math.Abs(this[i] - p2[i]) > Eps)
                    return false;
            }
            return true;
        }

        public override int GetHashCode() {
            int hash = 19;
            for (int i = 0; i <= Degree; i++) {
                hash = hash*31 + Coefficients[i].GetHashCode();
            }
            return hash;
        }
    }
}
