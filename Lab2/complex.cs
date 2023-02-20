
using System.Reflection.Metadata.Ecma335;

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
        public void ShowXY()
        {
            Console.WriteLine("{0} {1}", x, y);
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
}
