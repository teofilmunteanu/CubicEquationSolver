using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquationSolver
{
    class EcGr2
    {
        Complex a, b, c;

        public EcGr2(Complex x1, Complex x2, Complex x3)
        {
            a = x1;
            b = x2;
            c = x3;
        }

        public static Complex[] roots(EcGr2 e)
        {
            Complex[] x = new Complex[2];

            Complex a = e.a, b = e.b, c = e.c;

            Complex D = b * b - 4 * (a * c);

            if (a != 0)
            {
                x[0] = (-1 * b + Complex.Sqrt(D)) / (2 * a);
                x[1] = (-1 * b - Complex.Sqrt(D)) / (2 * a);
            }

            return x;
        }
    }

    public class EcGr3
    {
        Complex a, b, c, d;

        public EcGr3(Complex x1, Complex x2, Complex x3, Complex x4)
        {
            a = x1;
            b = x2;
            c = x3;
            d = x4;
        }

        public static Complex[] roots(EcGr3 e)
        {
            Complex[] x = new Complex[3];

            Complex a = e.a, b = e.b, c = e.c, d = e.d;

            Complex p = (-1 * (b * b) + 3 * a * c) / (3 * a * a);
            Complex q = (2 * b * b * b + 27 * d * a * a - 9 * a * b * c) / (27 * a * a * a);
            Complex r = Complex.Sqrt(-p / 3);

            if (a.Re == 0 && a.Im == 0) //ecuatie de gradul 2
            {
                Complex[] sol = EcGr2.roots(new EcGr2(e.b, e.c, e.d));
                x[0] = sol[0];
                x[1] = sol[1];
            }
            else
            if ((a.Re == 0 || a.Im == 0) && b == 0 && c == 0 && d == 0) //0 e singura solutie
            {
                x[0] = 0;
                x[1] = 0;
                x[2] = 0;
            }
            else //Coeficienti Reali
            if (a.Im == 0 && b.Im == 0 && c.Im == 0 && d.Im == 0) 
            {
                if (b == 0 && c == 0) //ecuatie de forma: ax^3 = d
                {
                    Complex[] j = new Complex[]{
                        1,
                        Complex.setReIm(-1.0 / 2, 1 * Math.Sqrt(3) / 2),
                        Complex.setReIm(-1.0 / 2, -1 * Math.Sqrt(3) / 2)
                    };

                    for (int k = 0; k < 3; k++)
                    {
                        x[k] = -1 * (Complex.Cbrt(d/a) * j[k]);
                    }
                }
                else
                {
                    Complex cosPhi = ((3 * q) / (2 * p)) * (1 / r);

                    System.Numerics.Complex phi;
                    phi = System.Numerics.Complex.Acos(cosPhi.Re);

                    Complex C = new Complex();
                    for (int k = 0; k < 3; k++)
                    {
                        C.Re = System.Numerics.Complex.Cos(phi / 3 + 2 * k * Math.PI / 3).Real;
                        C.Im = System.Numerics.Complex.Cos(phi / 3 + 2 * k * Math.PI / 3).Imaginary;
                        x[k] = 2 * r * C - b / (3 * a);
                    }
                }
                
            }
            else //Coeficienti Complecsi
            {
                if (p == 0 && q == 0)
                {
                    x[0] = -(b / (3 * a));
                    x[1] = -(b / (3 * a));
                    x[2] = -(b / (3 * a));
                }
                else if (q * q + (4 * p * p * p) / 27 == 0)
                {
                    x[0] = 3 * q / p - (b / (3 * a));
                    x[1] = -3 * q / (2 * p) - (b / (3 * a));
                    x[2] = x[1];
                }
                else
                {
                    Complex[] j = new Complex[]{
                        1,
                        Complex.setReIm((double)(-1 / 2), 1 * Math.Sqrt(3) / 2),
                        Complex.setReIm((double)(-1 / 2), -1 * Math.Sqrt(3) / 2)
                    };

                    EcGr2 tEc = new EcGr2(1, q, -(p * p * p) / 27);

                    Complex[] t = EcGr2.roots(tEc);

                    //Alpha: a1 = t^(1/3);   a2 = (-(t^(1/3)) + t^(1/3) * 3^(1/2) * i)/2;   a3 = (-(t^(1/3)) - t^(1/3) * 3^(1/2) * i)/2
                    Complex[] alpha = new Complex[3];
                    for (int i = 0; i < 3; i++)
                    {
                        alpha[i] = Complex.Cbrt(t[1]) * j[i];
                    }

                    for (int k = 0; k < 3; k++)
                    {
                        Complex y = (Complex.Pow(j[1], k) * alpha[k]) - (Complex.Pow(j[1], 2 * k) * (p / (3 * alpha[k])));
                        x[k] = y - b / (3 * a);
                    }
                }
            }

            return x;
        }

        //Returneaza ecuatia ca string
        public string WriteEc()
        {
            string output = "";

            if (a != Complex.setReIm(0, 0))
            {
                if (a.Re != 0 && a.Im != 0)
                    output += "(" + a + ")x^3";
                else
                    output += (a + "x^3");
            }

            if (b != Complex.setReIm(0, 0))
            {
                output += ((b.Re > 0 || (b.Re == 0 && b.Im > 0) || (b.Re != 0 && b.Im != 0)) && a != 0 ? "+" : "");
                if (b.Re != 0 && b.Im != 0)
                    output += ("(" + b + ")x^2");
                else
                    output += (b + "x^2");

            }

            if (c != Complex.setReIm(0, 0))
            {
                output += ((c.Re > 0 || (c.Re == 0 && c.Im > 0) || (c.Re != 0 && c.Im != 0)) && (a != 0 || b != 0) ? "+" : "");
                if (c.Re != 0 && c.Im != 0)
                    output += ("(" + c + ")x");
                else
                    output += (c + "x");

            }

            if (d != Complex.setReIm(0, 0))
            {
                output += ((d.Re > 0 || (d.Re == 0 && d.Im > 0) || (d.Re != 0 && d.Im != 0)) && (a != 0 || b != 0 || c != 0) ? "+" : "");
                if (d.Re != 0 && d.Im != 0)
                    output += d;
                else
                    output += d;

            }

            output += "=0";

            return output;
        }
    }
}
