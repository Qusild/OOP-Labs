using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms.VisualStyles;

namespace MyNumber
{
    public class Complex
    {
        private double x;
        private double y;
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
            return ((c.x == d)&&(c.y==0));
        }
        public static bool operator !=(Complex c, double d) 
        {
            return (((c.x != d)&&(c.y==0))||(c.y!=0));
        }
        public override string ToString()
        {
            return this.y == 0 ? $"{this.x}" : 
                   this.x == 0 ? $"{this.y}i" : 
                   this.y >= 0 ? $"{this.x}+{this.y}i" : $"{this.x}{this.y}i";
        }

        public Complex round()
        {
            this.x = Math.Round(this.x, 12, MidpointRounding.AwayFromZero);
            this.y = Math.Round(this.y, 12, MidpointRounding.AwayFromZero);
            return this;
        }
    }


    public class Rational
    {
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
        private void NOK()
        {
            int tmp = this.numerator > this.denumerator ? this.numerator : this.denumerator;
            List<int> ints = new List<int>();
            for (int i = 1; i < Math.Sqrt(tmp); i++)
            {
                if (tmp % i == 0)
                {
                    ints.Add(i);
                    if (tmp % i != i)
                        ints.Add(tmp / i);
                }
            }
            foreach (int divider in ints)
            {
                if ((this.numerator%divider == 0)&& (this.denumerator % divider == 0))
                {
                    this.numerator = this.numerator/divider;
                    this.denumerator = this.denumerator/divider; 
                }
            }
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
            temp.NOK();
            return temp;
        }
        public static Rational operator +(Rational r1, Rational r2)
        {
            Rational temp = new Rational();
            temp.numerator = r1.numerator * r2.denumerator + r2.numerator * r1.denumerator;
            temp.denumerator = r1.denumerator * r2.denumerator;
            temp.NOK();
            return temp;
        }
        public static Rational operator -(Rational r1, Rational r2)
        {
            Rational temp = new Rational();
            temp.numerator = r1.numerator*r2.denumerator - r2.numerator*r1.denumerator;
            temp.denumerator = r1.denumerator * r2.denumerator;
            temp.NOK();
            return temp;
        }
        public static Rational operator *(Rational r1, Rational r2)
        {
            Rational temp = new Rational();
            temp.numerator = r1.numerator * r2.numerator;
            temp.denumerator = r1.denumerator * r2.denumerator;
            temp.NOK();
            return temp;
        }
        public static Rational operator /(Rational r1, Rational r2)
        {
            Rational temp = new Rational();
            temp.numerator = r1.numerator * r2.denumerator;
            if (r2.numerator!= 0)
                temp.denumerator = r1.denumerator * r2.numerator;
            temp.NOK();
            return temp;
        }
        public static bool operator ==(Rational r, double d)
        {
            return (r.numerator/r.denumerator == d);
        }
        public static bool operator !=(Rational r, double d)
        {
            return (r.numerator/r.denumerator != d);
        }
        public int getNumerator() { return numerator; }
        public int getDenumerator() { return denumerator; }

        private int numerator;
        private int denumerator;
    }
}
