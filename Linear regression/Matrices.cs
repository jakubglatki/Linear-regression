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
            
            if(data.isLinear)
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

            double mean = MatrixMean(x);
        }



        private void FillMatrix(List<List<double>> xLists)
        {
            for (int i = 0; i < xLists.Count; i++)
            {
                for (int j = 0; j < xLists[i].Count; j++)
                {
                    x[j, i] = xLists[i][j];
                }
            }
        }

        private void FillMatrix(List<double> xLists, int degree)
        {
            int power = 1;
            for (int i = 0; i < degree; i++)
            {
                for (int j = 0; j < xLists.Count; j++)
                {
                    x[j, i] = Math.Pow(xLists[j], power);
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
    }
}
