using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MyNumbers
{
    public interface IMyNumbers<T> where T: IMyNumbers<T>
    {
        public static abstract T operator +(T n1, T n2);
        public static abstract T operator -(T n1, T n2);
        public static abstract T operator -(T n2);
        public static abstract T operator *(T n1, T n2);
        public static abstract T operator /(T n1, T n2);
        public static abstract bool operator ==(T n1, T n2);
        public static abstract bool operator !=(T n1, T n2);
        public abstract string ToString();
        public abstract bool isZero();

    }
    public abstract class AMyNumber : IMyNumbers<AMyNumber>
    {
        public static AMyNumber operator +(AMyNumber n1, AMyNumber n2) { return default(AMyNumber); }
        public static AMyNumber operator -(AMyNumber n1, AMyNumber n2) { return default(AMyNumber); }
        public static AMyNumber operator -(AMyNumber n2) { return default(AMyNumber); }
        public static AMyNumber operator *(AMyNumber n1, AMyNumber n2) { return default(AMyNumber); }
        public static AMyNumber operator /(AMyNumber n1, AMyNumber n2) { return default(AMyNumber); }
        public static bool operator ==(AMyNumber? n1, AMyNumber? n2)
        {
            if (n1 is null) return true;
            if (n2 is null) return false; 
            return false; 
        }
        public static bool operator !=(AMyNumber? n1, AMyNumber n2)
        {
            if (n1 is null) return false;
            if (n2 is null) return true;
            return false;
        }
        public abstract bool isZero();
    }
        public class Complex : AMyNumber, IMyNumbers<Complex>
    {
        private double x = 0;
        [DefaultValue(0)]
        private double y = 0;
        [DefaultValue(0)]
        public Complex(double i = 0, double j = 0)
        {
            x = i;
            y = j;
        }
        public static Complex operator -(Complex c)
        {
            Complex temp = new Complex();
            temp.x = -c.x;
            temp.y = -c.y;
            return temp;
        }
        public static Complex operator +(Complex c1, Complex c2)
        {
            Complex temp = new Complex();
            temp.x = c1.x + c2.x;
            temp.y = c1.y + c2.y;
            return temp;
        }
        public static Complex operator -(Complex c1, Complex c2)
        {
            Complex temp = new Complex();
            temp.x = c1.x - c2.x;
            temp.y = c1.y - c2.y;
            return temp;
        }
        public static Complex operator *(Complex c1, Complex c2)
        {
            Complex temp = new Complex();
            temp.x = (c1.x * c2.x) - (c1.y * c2.y);
            temp.y = (c1.x * c2.y) + (c1.y * c2.x);
            return temp;
        }
        public static Complex operator /(Complex c1, Complex c2)
        {
            Complex temp = new Complex();
            temp.x = ((c1.x * c2.x) + (c1.y * c2.y)) / (c2.x * c2.x + c2.y * c2.y);
            temp.y = ((c1.y * c2.x) + (c1.x * c2.y)) / (c2.x * c2.x + c2.y * c2.y);
            return temp;
        }
        public static bool operator ==(Complex c, double d)
        {
            return ((c.x == d) && (c.y == 0));
        }
        public static bool operator !=(Complex c, double d)
        {
            return (((c.x != d) && (c.y == 0)) || (c.y != 0));
        }
        public static bool operator ==(Complex? c, Complex? d)
        {
            if (c is null) return true;
            if (d is null) return false;
            return ((c.x==d.x)&&(c.y==d.y));
        }
        public static bool operator !=(Complex? c, Complex? d)
        {
            if (c is null) return false;
            if (d is null) return true;
            return (!(c.x == d.x) && (c.y == d.y));
        }
        public override string ToString()
        {
            return this.y == 0 ? $"{this.x}" :
                   this.x == 0 ? $"{this.y}i" :
                   this.y >= 0 ? $"{this.x}+{this.y}i" : $"{this.x}{this.y}i";
        }
        public double getRe() { return this.x; }
        public double getIm() { return this.y; }
        public Complex round()
        {
            this.x = Math.Round(this.x, 12, MidpointRounding.AwayFromZero);
            this.y = Math.Round(this.y, 12, MidpointRounding.AwayFromZero);
            return this;
        }
        public override bool isZero()
        {
            return this == 0;
        }
    }


    public class Rational : AMyNumber, IMyNumbers<Rational>
    {
        private int numerator = 0;
        [DefaultValue(0)]
        private int denumerator = 1;
        [DefaultValue(1)]
        public Rational(int n = 0, int d = 0)
        {
            numerator = n;
            denumerator = d;
        }

        public void Swap()
        {
            int tmp;
            tmp = numerator;
            numerator = denumerator;
            denumerator = tmp;
        }
        private void NOD()
        {
            int a = Math.Max(numerator, denumerator), b = Math.Min(numerator, denumerator);

            while (b != 0)
            {
                int temp = b;
                b = a % b;
                a = temp;
            }
            if (a == 0)
            {
                numerator = 0; 
                denumerator = 1;
                return;
            }
            this.numerator /= a;
            this.denumerator /= a;
            this.zeroCheck();
        }
        private void zeroCheck()
        {
            if ((denumerator == numerator) && (denumerator == 0))
                denumerator = 1;
        }
        public static Rational operator -(Rational r)
        {
            Rational temp = new Rational();
            temp.numerator = -r.numerator;
            temp.denumerator = r.denumerator;
            temp.NOD();
            return temp;
        }
        public static Rational operator +(Rational r1, Rational r2)
        {
            Rational temp = new Rational();
            temp.numerator = r1.numerator * r2.denumerator + r2.numerator * r1.denumerator;
            temp.denumerator = r1.denumerator * r2.denumerator;
            temp.NOD();
            return temp;
        }
        public static Rational operator -(Rational r1, Rational r2)
        {
            Rational temp = new Rational();
            temp.numerator = r1.numerator * r2.denumerator - r2.numerator * r1.denumerator;
            temp.denumerator = r1.denumerator * r2.denumerator;
            temp.NOD();
            return temp;
        }
        public static Rational operator *(Rational r1, Rational r2)
        {
            Rational temp = new Rational();
            temp.numerator = r1.numerator * r2.numerator;
            temp.denumerator = r1.denumerator * r2.denumerator;
            temp.NOD();
            return temp;
        }
        public static Rational operator /(Rational r1, Rational r2)
        {
            Rational temp = new Rational();
            temp.numerator = r1.numerator * r2.denumerator;
            if (r2.numerator != 0)
                temp.denumerator = r1.denumerator * r2.numerator;
            temp.NOD();
            return temp;
        }
        public static bool operator ==(Rational r, double d)
        {
            return (r.numerator == r.denumerator * d);
        }
        public static bool operator !=(Rational r, double d)
        {
            return (r.numerator != r.denumerator * d);
        }
        public static bool operator ==(Rational? r, Rational? d)
        {

            if (r is null) return true;
            if (d is null) return false;
            return ((r.numerator == d.numerator)&&(r.denumerator == d.denumerator));
        }
        public static bool operator !=(Rational? r, Rational? d)
        {
            if (r is null) return false;
            if (d is null) return true;
            return !((r.numerator == d.numerator) && (r.denumerator == d.denumerator));
        }
        public override string ToString()
        {
            if ((this.numerator < 0) || (this.denumerator < 0))
                return ($"  {Math.Abs(this.numerator)}" +
                $"\n- _____\n" +
                $"  {Math.Abs(this.denumerator)}");
            else return ($"{this.numerator}" +
                $"\n_____\n" +
                $"{this.denumerator}");
        }
        public int getNumerator() { return numerator; }
        public int getDenumerator() { return denumerator; }

        public override bool isZero() { return this == 0; }
    }

    public class Double : AMyNumber, IMyNumbers<Double>
    {
        private double number;
        public Double(double number)
        {
            this.number = number;
        }
        public static Double operator -(Double r)
        {
            return new Double(0) - r;
        }
        public static Double operator +(Double d1, Double d2)
        {
            return new Double(d1.number + d2.number);
        }
        public static Double operator -(Double d1, Double d2)
        {
            return new Double(d1.number - d2.number);
        }
        public static Double operator *(Double d1, Double d2)
        {
            return new Double(d1.number * d2.number);
        }
        public static Double operator /(Double d1, Double d2)
        {
            return new Double (d1.number / d2.number);
        }
        public static bool operator ==(Double r, double d)
        {
            return (r.number == d);
        }
        public static bool operator !=(Double r, double d)
        {
            return (r.number != d);
        }
        public static bool operator ==(Double? r, Double? d)
        {

            if (r is null) return true;
            if (d is null) return false;
            return (r.number == d.number);
        }
        public static bool operator !=(Double? r, Double? d)
        {
            if (r is null) return false;
            if (d is null) return true;
            return !(r.number == d.number);
        }
        public double getDouble() { return number; }
        public override string ToString()
        {
            return number.ToString();
        }
        public override bool isZero() { return number == 0; }
    }
}
