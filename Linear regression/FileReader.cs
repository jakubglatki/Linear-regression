using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Linear_regression
{
  public class FileReader
    {
        public List<List<double>> xLists;
        public List<double> y;
        public FileReader() { }
        public void ReadFile(string path, List<List<double>> xLists, List<double> y)
        {
            int columnLenght = 0;

            using (StreamReader file = new StreamReader(path))
            {
                string row;

                row = file.ReadLine();
                columnLenght = CountValuesNumber(row);
                CreateColumns(xLists, columnLenght);

                while ((row = file.ReadLine()) != null)
                {
                    List<string> sValue = row.Split(' ').ToList(); ;
                    PutValuesIntoColumns(xLists, y, columnLenght, sValue);

                }
                file.Close();
            }
            this.xLists = xLists;
            this.y = y;
        }

        private int CountValuesNumber(string row)
        {
            int number = 1; 
            foreach (char x in row)
            {
                if (x == ' ')
                    number++;
            }

            return number;
        }
        private void CreateColumns(List<List<double>> columns, int columnsLength)
        {
            int numberOfColumns = columnsLength - 1;
            for (int i = 0; i < numberOfColumns; i++)
            {
                List<double> newColumn = new List<double>();
                columns.Add(newColumn);
            }
        }

        private void PutValuesIntoColumns(List<List<double>> xLists, List<double> y, int columnLenght, List<string> sValue)
        {

            for (int i = 0; i < columnLenght; i++)
            {
                if (!(i == (columnLenght - 1)))
                {
                    double value = double.Parse(sValue[i]);
                    xLists[i].Add(value);
                }
                else
                {
                    double value = double.Parse(sValue[i]);
                    y.Add(value);
                }
            }
        }

        public string GetFileName()
        {
            Console.WriteLine("File name(LinearS / LinearM / Polynomial): ");
            string path = Console.ReadLine();
            return path;
        }
    }
}
