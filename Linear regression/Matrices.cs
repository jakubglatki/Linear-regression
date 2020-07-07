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

            double mean = MatrixMean(x);
            x = CenterMatix(x, mean);

            Matrix<double> xT = x.Transpose();

            Matrix<double> result = Matrix<double>.Build.Dense(x.ColumnCount, x.ColumnCount);

            xT.Multiply(x, result);

            Matrix<double> value = result.Inverse();

            Matrix<double> nextToLastStep = Matrix<double>.Build.Dense(x.ColumnCount, x.RowCount);

            value.Multiply(xT, nextToLastStep);


            Matrix<double> finalResult = Matrix<double>.Build.Dense(x.ColumnCount, 1);

            nextToLastStep.Multiply(y, finalResult);

            double b = finalResult[0, 0];

            double c = y[0, 0] - (b * data.xLists[0][0]);

            Console.WriteLine("\n Coefficient b: " + b + "\n");
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

        private double MatrixMean(Matrix<double> matrix)
        {
            double sum = 0;
            for (int i = 0; i < matrix.ColumnCount; i++)
            {
                for (int j = 0; j < matrix.RowCount; j++)
                {
                    sum += matrix[j, i];
                }
            }
            double rows = Convert.ToDouble(matrix.RowCount);
            double columns = Convert.ToDouble(matrix.ColumnCount);
            return (sum / (rows * columns));
        }

        private Matrix<double> CenterMatix(Matrix<double> matrix, double mean)
        {
            for (int i = 0; i < matrix.ColumnCount; i++)
            {
                for (int j = 0; j < matrix.RowCount; j++)
                {
                    matrix[j, i] -= mean;
                }
            }
            return matrix;
        }
    }
}
