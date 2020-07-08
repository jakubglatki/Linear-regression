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

        public Matrices()
        {
            data = new FunctionData();

            if (data.isLinear)
            {
                x = Matrix<double>.Build.Dense(data.xLists[0].Count, data.xLists.Count);
                FillMatrix(data.xLists);
            }

            else
            {
                x = Matrix<double>.Build.Dense(data.xLists[0].Count, data.xLists.Count);
                FillMatrix(data.xLists[0], data.degree);
            }

            y = Matrix<double>.Build.Dense(data.y.Count, 1);
            FillVectorWithValues(data.y);

            CalculateCoefficientsAndConstant();
        }

        private void CalculateCoefficientsAndConstant()
        {

            double[] mean = MatrixMean(x);

                x = CenterMatix(x, mean);

                Matrix<double> xT = x.Transpose();

                Matrix<double> xTxMultiplied = Matrix<double>.Build.Dense(x.ColumnCount, x.ColumnCount);

                xT.Multiply(x, xTxMultiplied);

                Matrix<double> inversed = xTxMultiplied.Inverse();

                Matrix<double> xTxInversedMultiplied = Matrix<double>.Build.Dense(x.ColumnCount, x.RowCount);

            inversed.Multiply(xT, xTxInversedMultiplied);


                Matrix<double> finalResult = Matrix<double>.Build.Dense(x.ColumnCount, 1);

            xTxInversedMultiplied.Multiply(y, finalResult);
            CountBandC(finalResult);
        }

        private void CountBandC(Matrix<double> finalResult)
        {
            double[] b = new double[x.ColumnCount];
            double coefficientValue = 0;
            for (int i = 0; i < x.ColumnCount; i++)
            {
                b[i] = finalResult[i, 0];
                coefficientValue += b[i] * data.xLists[i][0];
                Console.WriteLine("\n Coefficient b" + i + ": " + b[i] + "\n");
            }

            double c = y[0, 0] - (coefficientValue);

            Console.WriteLine("\n Constant c: " + c + "\n");
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
