using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EquationSolver
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btn_Click(object sender, EventArgs e)
        {
            Complex a, b, c, d;
            x.Text = ""; y.Text = ""; z.Text = "";

            try
            {
                if (aRe.Text == "") aRe.Text = "0"; if (aIm.Text == "") aIm.Text = "0";
                a = Complex.setReIm(Double.Parse(aRe.Text), Double.Parse(aIm.Text));

                if (bRe.Text == "") bRe.Text = "0"; if (bIm.Text == "") bIm.Text = "0";
                b = Complex.setReIm(Double.Parse(bRe.Text), Double.Parse(bIm.Text));

                if (cRe.Text == "") cRe.Text = "0"; if (cIm.Text == "") cIm.Text = "0";
                c = Complex.setReIm(Double.Parse(cRe.Text), Double.Parse(cIm.Text));

                if (dRe.Text == "") dRe.Text = "0"; if (dIm.Text == "") dIm.Text = "0";
                d = Complex.setReIm(Double.Parse(dRe.Text), Double.Parse(dIm.Text));

                EcGr3 equation = new EcGr3(a, b, c, d);
                EquationBox.Text = equation.WriteEc();

                if (a != 0 || b != 0 || c != 0)
                {
                    x.Text = EcGr3.roots(equation)[0].ToString();
                    y.Text = EcGr3.roots(equation)[1].ToString();

                    if (a == 0) //Ecuatie de gradul 2
                        z.Text = "";
                    else
                        z.Text = EcGr3.roots(equation)[2].ToString();
                }
                else
                {
                    throw (new Exception("No solutions"));
                }                   
            }
            catch(Exception exc)
            {
                MessageBox.Show("Invalid input." + '\n' + exc.Message);
            }
        }
    }
}
