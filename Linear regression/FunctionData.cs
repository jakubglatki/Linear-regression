using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Linear_regression
{

    public class FunctionData
    {

        public List<List<double>> xLists { get; set; }
        public List<double> y { get; set; }
        public bool isLinear { get; set; }
        public int degree { get; set; }
        string fileName;
        public FunctionData() 
        {
            xLists = new List<List<double>>();
            y = new List<double>();
            getDataFromFile();
            this.isLinear = checkIfFunctionIsLinear();
            degree = 1;
        }

        public void getDataFromFile()
        {
            FileReader fileReader = new FileReader();
            string path = getFilePath(fileReader);
            fileReader.ReadFile(path, xLists, y);
            xLists = fileReader.xLists;
            y = fileReader.y;

        }

        private string getFilePath(FileReader fileReader)
        {
            string path = "..\\..\\..\\..\\Linear regression\\TestData";
            fileName = fileReader.GetFileName();

            if (fileName == "LinearS")
            {
                fileName = "\\LinearS\\data.txt";
            }
            else if (fileName == "LinearM")
            {
                fileName = "\\LinearM\\data.txt";
            }
            else
            {
                fileName = "\\Polynomial\\data.txt";
            }

            path = path + fileName;
            return path;
        }

        private bool checkIfFunctionIsLinear()
        {
            if (xLists.Count()>1)
            {
                return true;
            }

            var tempY = y.ToArray();
            var tempX = xLists[0].ToArray();
            Array.Sort(tempX, tempY);

            double yVal0 = tempY[2]-tempY[1];
            double yVal1 = tempY[1]-tempY[0];

            double xVal0 = tempX[2] - tempX[1];
            double xVal1 = tempX[1] - tempX[0];


            if (((yVal1) / (xVal1) != ((yVal0) / (xVal0))) && fileName=="\\Polynomial\\data.txt" )
                return false;

            else return true;

        }

        
    }
}
