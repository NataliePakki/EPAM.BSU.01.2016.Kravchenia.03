﻿using System;
using System.Text;

namespace Task1
{
    public sealed class Polynomial {
        public readonly double[] Coefficients;
        public readonly int Degree;

        public Polynomial(params double[] coeff) {
            Degree = coeff.Length - 1;
            Coefficients = coeff;
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
        public static Polynomial operator *(Polynomial p, int number) {
            return number*p;
        }
        public static Polynomial operator /(Polynomial p, int number) {
            return Devide(p, number);
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

        public static Polynomial Add(Polynomial p1, Polynomial p2)
        {
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
            double[] resultCoeff = new double[resultDegree];
            for (int i = 0; i < resultDegree; i++) {
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
            if (p2.Degree == 0) return Multiply(p2[0], p2);
            int resultDegree = p1.Degree + p2.Degree;
            double[] resultCoeff = new double[resultDegree + 1];
            for(int i = p1.Degree; i >= 0; i--) 
                for(int j = p2.Degree; j >= 0; j--){
                    resultCoeff[i + j] += p1[i]*p2[j];
            }
            return new Polynomial(resultCoeff);
        }

        public static Polynomial Devide(Polynomial p, double number) {
            return Multiply(1/number,p);

        }

        public override string ToString() {
            var result = new StringBuilder();
            for (int i = Degree; i > 0; i--) {
                if(this[i] == 0) continue;
                string sign = this[i] < 0 ? "" : "+";
                result.Append(sign + this[i] + "x^" + i);
             }
            if(this[0] != 0)
                result.Append("+" + this[0]);
            return result.ToString();
        }

        public override bool Equals(object obj) {
            if (obj == null) return false;
            if (GetType() != obj.GetType()) return false;
            Polynomial p2 = (Polynomial) obj;
            if (Degree != p2.Degree) return false;
            for (int i = 0; i <= Degree; i++) {
                if (!this[i].Equals(p2[i]))
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
