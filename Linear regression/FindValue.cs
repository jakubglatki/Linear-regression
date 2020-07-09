using System;
using System.Collections.Generic;
using System.Text;

namespace Linear_regression
{
    class FindValue
    {
        private List<double> userValues;
        private List<List<double>> xLists;
        private bool isLinear;
        private int degree;
        private double c;
        private double[] b;
        public FindValue(List<List<double>> xLists, bool isLinear, int degree, double[] b, double c)
        {
            this.xLists = xLists;
            this.isLinear = isLinear;
            this.degree = degree;
            this.b = b;
            this.c = c;
            userValues = new List<double>();
            AskUserForValue();
        }

            
        public void AskUserForValue()
        {
            bool finish = false;
            while(!finish)
            {
                Console.WriteLine("\n Do you want to get the value of the function for parameters of your choice? (Y/N)");
                string input = Console.ReadLine();
                input = input.ToUpper();

                if (input == "Y")
                {
                    if (userValues.Count > 0)
                        userValues.Clear();
                    GetValue();
                    GiveValue();
                }
                else
                {
                    finish = true;
                }
            }
        }

        private void GetValue()
        {
            if(isLinear)
            {
                GetValueLinear();
            }
            else
            {
                GetValuePolynomal();
            }
        }

        private void GetValueLinear()
        {
            for (int i = 0; i < xLists.Count; i++)
            {
                Console.WriteLine("\n Put value of X" + i);
                double value = Convert.ToDouble(Console.ReadLine().Replace(".", ","));
                userValues.Add(value);
            }
        }

        private void GetValuePolynomal()
        {
            Console.WriteLine("\n Put value of X");
            double value = Convert.ToDouble(Console.ReadLine().Replace(".", ","));
            userValues.Add(value);
        }
        private void GiveValue()
        {

            double y = 0;
            y += c;

            if (isLinear)
            {
                GiveValueLinear(y);
            }

            else
            {
                GiveValuePolynomal(y);
            }
        }

        private void GiveValueLinear(double y)
        {
            for (int i = 0; i < userValues.Count; i++)
            {
                y += b[i] * userValues[0];
            }
            Console.WriteLine("\n Value of function in your point is: " + y);
        }


        private void GiveValuePolynomal(double y)
        {
            for(int i=0;i<degree;i++)
            {
                y += b[i] * Math.Pow(userValues[0], i + 1);
            }
            Console.WriteLine("\n Value of function in your point is: " + y);
        }
    }
}
