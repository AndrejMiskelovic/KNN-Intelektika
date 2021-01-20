using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace KNN_Intelektika
{
    class Reader
    {
        //public List<int> x = new List<int>();
        //public List<int> y = new List<int>();
        //public List<string> clas = new List<string>();
        //public List<double> res = new List<double>();
        public List<MyClass> dataset = new List<MyClass>();
        public List<MyClass> read()
        {
            
            string filename = @"data.txt";
            string[] lines = File.ReadAllLines(filename);
            
            for (int i = 0; i < lines.Length; i++)
            {
                MyClass myClass = new MyClass();
                string[] help = lines[i].Split(',');
                myClass.x = Convert.ToInt32(help[0]);
                myClass.y = Convert.ToInt32(help[1]);
                myClass.clas = help[2];
                dataset.Add(myClass);
            }
            return dataset;
        }
    }
    class MyClass
    {
        public int x { get; set; }
        public int y { get; set; }
        public string clas { get; set; }
        public double res { get; set; }
    }
}
