using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Numerics;
using System.Text;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace Linear_regression
{
    class Matrices
    {
        public Matrix<double> x;
        public Matrix<double> y;
        private FunctionData data;
        private Matrix<double> finalResult;
        private Matrix<double> inversed;
        private Matrix<double> xT;
        private double[] b;
        private double c;
        public Matrices()
        {
            data = new FunctionData();

            while (true)
            {
                if (data.isLinear)
                {
                    x = Matrix<double>.Build.Dense(data.xLists[0].Count, data.xLists.Count);
                    FillMatrix(data.xLists);
                }

                else
                {
                    x = Matrix<double>.Build.Dense(data.xLists[0].Count, data.degree);
                    FillMatrix(data.xLists[0], data.degree);
                }

                y = Matrix<double>.Build.Dense(data.y.Count, 1);
                FillVectorWithValues(data.y);

                CalculateCoefficientsAndConstant();

                if (data.isLinear)
                    break;

                if (CheckIfPolynomialDegreeIsOk(data.degree))
                    break;

                data.degree++;
            }

            FindValue findValue = new FindValue(data.xLists, data.isLinear, data.degree, b, c);

        }

        private void CalculateCoefficientsAndConstant()
        {

            double[] mean = MatrixMean(x);

                x = CenterMatix(x, mean);

                xT = x.Transpose();

                Matrix<double> xTxMultiplied = Matrix<double>.Build.Dense(x.ColumnCount, x.ColumnCount);

                xT.Multiply(x, xTxMultiplied);

                inversed = xTxMultiplied.Inverse();

                Matrix<double> xTxInversedMultiplied = Matrix<double>.Build.Dense(x.ColumnCount, x.RowCount);

            inversed.Multiply(xT, xTxInversedMultiplied);


               finalResult = Matrix<double>.Build.Dense(x.ColumnCount, 1);

            xTxInversedMultiplied.Multiply(y, finalResult);

            if (data.isLinear)
                CountBandCLinear(finalResult);

            else
                CountBandCPolynomial(finalResult);
        }

        private bool CheckIfPolynomialDegreeIsOk(int degree)
        {
            b = new double[degree];
            double coefficientValue = 0;
            for (int i = 0; i < degree; i++)
            {
                b[i] = this.finalResult[i, 0];
                coefficientValue += b[i] * Math.Pow(data.xLists[0][1], i + 1);
            }

            if (y[1, 0] - c - coefficientValue < 1 && y[1, 0] - c - coefficientValue > -1)
            {
                for(int i=0;i<degree;i++)
                {
                    Console.WriteLine("\n Coefficient b" + i + ": " + b[i] + "\n");
                }
                Console.WriteLine("\n Constant c: " + c + "\n");
                return true;
            }
            else return false;
        }

            private void CountBandCLinear(Matrix<double> finalResult)
        {
            b = new double[x.ColumnCount];
            double coefficientValue = 0;
            for (int i = 0; i < x.ColumnCount; i++)
            {
                b[i] = finalResult[i, 0];
                coefficientValue += b[i] * data.xLists[i][0];
                Console.WriteLine("\n Coefficient b" + i + ": " + b[i] + "\n");
            }

             c = y[0, 0] - (coefficientValue);

            Console.WriteLine("\n Constant c: " + c + "\n");
        }
        private void CountBandCPolynomial(Matrix<double> finalResult)
        {
            b = new double[x.ColumnCount];
            double coefficientValue = 0;
            for (int i = 0; i < x.ColumnCount; i++)
            {
                b[i] = finalResult[i, 0];
                coefficientValue += b[i] * Math.Pow(data.xLists[0][0],i+1);
            }

            c = y[0, 0] - (coefficientValue);

        }



        private void FillVectorWithValues(List<double> Y)
        {
            for (int i = 0; i < Y.Count; i++)
            {
                y[i, 0] = Y[i];
            }
        }

        private void FillMatrix(List<List<double>> columns)
        {
            for (int i = 0; i < columns.Count; i++)
            {
                for (int j = 0; j < columns[i].Count; j++)
                {
                    x[j, i] = columns[i][j];
                }
            }
        }

        private void FillMatrix(List<double> columns, int degree)
        {
            int power = 1;
            for (int i = 0; i < degree; i++)
            {
                for (int j = 0; j < columns.Count; j++)
                {
                    x[j, i] = Math.Pow(columns[j], power);
                }
                power++;
            }
        }

        private double[] MatrixMean(Matrix<double> matrix)
        {
            double[] sum = new double[matrix.ColumnCount];
            for (int i = 0; i < matrix.ColumnCount; i++)
            {
                for (int j = 0; j < matrix.RowCount; j++)
                {
                    sum[i] += matrix[j, i];
                }
            }
            double rows = Convert.ToDouble(matrix.RowCount);
            double columns = Convert.ToDouble(matrix.ColumnCount);
            for(int i=0;i<matrix.ColumnCount;i++)
            {
                sum[i] = (sum[i] / rows);
            }
            return sum;
        }

        private Matrix<double> CenterMatix(Matrix<double> matrix, double[] mean)
        {
            for (int i = 0; i < matrix.ColumnCount; i++)
            {
                for (int j = 0; j < matrix.RowCount; j++)
                {
                    matrix[j,i] -= mean[i];
                }
            }
            return matrix;
        }
    }
}
