using System;
using System.Collections.Generic;
using MyMath;

class Complex  
{  
    private double x;  
    private double y;  
    public Complex()  
    {  
        x = 0;
        y = 0;
    }  
    public Complex(int i, int j)  
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
        temp.x = ((c1.x * c2.x) + (c1.y * c2.y))/(c2.x*c2.x + c2.y*c2.y);  
        temp.y = ((c1.y * c2.x) + (c1.x * c2.y))/(c2.x*c2.x + c2.y*c2.y);  
        return temp;  
    }  
}  