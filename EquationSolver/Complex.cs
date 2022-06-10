using System;

namespace EquationSolver
{

    public struct Complex
    {
        private double x, y;

        public Complex(double x, double y)
        {
            this.x = x;
            this.y = y;
        }
        public override string ToString()
        {
            double eps = 1.0e-12;
            if (Math.Abs(y) < eps)             // z=real
                return string.Format("{0}", x);

            if (Math.Abs(x) < eps)             // z=imaginar
            {
                if (Math.Abs(y - 1) < eps)     //z=i
                    return "i";
                if (Math.Abs(y + 1) < eps)     //z=-i
                    return "-i";
                return string.Format("{0}i", y);
            }
            if (y > 0) return string.Format("{0}+{1}i", x, y);
            return string.Format("{0}{1}i", x, y);
        }

        public void show()
        {
            Console.WriteLine(this);
        }
        public double Ro
        {
            get
            {
                return Math.Sqrt(x * x + y * y);
            }
            set
            {
                x = value * Math.Cos(Theta);
                y = value * Math.Sin(Theta);
            }
        }
        public double Ro2
        {
            get
            {
                return x * x + y * y;
            }
        }
        public double Theta
        {
            get
            {
                return Math.Atan2(y, x);
            }
            set
            {
                x = Ro * Math.Cos(value);
                y = Ro * Math.Sin(value);
            }
        }
        public double Re
        {
            get
            {
                return x;
            }
            set
            {
                x = value;
            }
        }
        public double Im
        {
            get
            {
                return y;
            }
            set
            {
                y = value;
            }
        }

        public Complex Conj
        {
            get
            {
                return new Complex(x, -y);
            }
            set
            {
                x = value.x;
                y = -value.y;
            }
        }

        public static Complex setRoTheta(double Ro, double theta)
        {
            return new Complex(Ro * Math.Cos(theta), Ro * Math.Sin(theta));
        }
        public static Complex setReIm(double x, double y)
        {
            return new Complex(x, y);
        }

        public static Complex operator +(Complex zst, Complex zdr)
        {
            return new Complex(zst.x + zdr.x, zst.y + zdr.y);
        }

        public static Complex operator +(Complex zst)
        {
            return new Complex(zst.x, zst.y);
        }

        public static Complex operator -(Complex zst, Complex zdr)
        {
            return new Complex(zst.x - zdr.x, zst.y - zdr.y);
        }
        public static Complex operator -(Complex zst)
        {
            return new Complex(-zst.x, -zst.y);
        }
        public static Complex operator *(Complex zst, Complex zdr)
        {
            return new Complex(zst.x * zdr.x - zst.y * zdr.y, zst.y * zdr.x + zst.x * zdr.y);
        }
        public static Complex operator /(Complex zst, Complex zdr)
        {
            double r = zdr.Ro2;
            if (r < 1.0e-12) return Complex.setRoTheta(Double.MaxValue, zst.Theta - zdr.Theta);
            return new Complex((zst.x * zdr.x + zst.y * zdr.y) / r, (zst.y * zdr.x - zst.x * zdr.y) / r);
        }

        public static implicit operator Complex(double x)
        {
            return new Complex(x, 0);
        }

        public static bool operator ==(Complex zst, Complex zdr)
        {
            return (zst - zdr).Ro2 < 1.0e-16;
        }
        public static bool operator !=(Complex zst, Complex zdr)
        {
            return (zst - zdr).Ro2 >= 1.0e-16;
        }

        public override bool Equals(object o)
        {
            if (o.GetType() != this.GetType()) return false;
            else return this == (Complex)o;
        }

        public override int GetHashCode()
        {
            return x.GetHashCode() + y.GetHashCode();
        }

        /****************************************************/
        public static Complex Sqrt(Complex z)
        {
            double xRe, xIm;
            Complex x;

            if (z.Re > 0 || z.Im != 0)
            {
                //sqrt(z.Re + z.Im i) = a + bi |^2 => z.Re + z.Im i = a^2 + 2ab i - b^2 => z.Re = a^2 - b^2 ; z.Im = 2ab => ...
                xRe = Math.Sqrt((z.Re + Math.Sqrt(z.Re * z.Re + z.Im * z.Im)) / 2);
                xIm = z.Im / (2 * xRe);   
            }
            else
            {
                xRe = 0;
                xIm = Math.Sqrt(-1 * z.Re);
            }

            x = new Complex(xRe, xIm);

            return x;
        }

        public static Complex Cbrt(Complex z)
        {
            Complex x = new Complex();
            double r = z.Ro;
            double theta = z.Theta;
            x.Re = Math.Pow(r, (double)1/3) * Math.Cos(theta/3);
            x.Im = Math.Pow(r, (double)1/3) * Math.Sin(theta/3);
            return x;
        }

        public static Complex Pow(Complex z, double n)
        {
            Complex x = new Complex();
            double r = z.Ro;
            double theta = z.Theta;
            x.Re = Math.Pow(r, n) * Math.Cos(theta * n);
            x.Im = Math.Pow(r, n) * Math.Sin(theta * n);
            return x;
        }

        public static Complex Round(Complex z)
        {
            z.Re = Math.Round(z.Re, 3);
            z.Im = Math.Round(z.Im, 3);

            return z;
        }
    }
}
