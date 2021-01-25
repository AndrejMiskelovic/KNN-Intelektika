using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;



namespace KNN_Intelektika
{
    public partial class Form1 : Form
    {
        Reader reader = new Reader();
        List<MyClass> dataset = new List<MyClass>();
        MyClass myClass1 = new MyClass();
        public Form1()
        {
            InitializeComponent();
            button2.Enabled = false;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataset = reader.read();
            KNN();
            button2.Enabled = true;
        }
        private void KNN()
        {
            listBox1.Items.Clear();
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            MyClass myClass = new MyClass();
            List<double> res = new List<double>();

            for (int i = 0; i < dataset.Count; i++)
            {                
                if(comboBox2.SelectedItem == null)
                {
                    MessageBox.Show("Please Select Game");
                    return;
                }
                if(comboBox1.SelectedItem == null)
                {
                    MessageBox.Show("Please Select Algorithm");
                    return;
                }
                myClass = dataset[i];
                if ((string)comboBox2.SelectedItem == (string)myClass.game) 
                { 
                    if ((string)comboBox1.SelectedItem == "KNN1")
                        dataset[i].res = Math.Sqrt(Math.Pow((double)numericUpDown1.Value - myClass.x, 2) + Math.Pow((double)numericUpDown2.Value - myClass.y, 2));
                    else if ((string)comboBox1.SelectedItem == "KNN2")
                        dataset[i].res = Math.Max(Math.Abs((double)numericUpDown1.Value - myClass.x), Math.Abs((double)numericUpDown2.Value - myClass.y));
                }

            }
            var newList = dataset.OrderBy(x => x.res).ToList();
            for (int i = 0; i < newList.Count; i++)
            {
                if ((string)newList[i].game != (string)comboBox2.SelectedItem)
                {
                    newList.RemoveAt(i);
                    i--;
                }
            } 
            int help = (int)numericUpDown3.Value;
            try
            {
                if (newList[(int)numericUpDown3.Value].res == newList[(int)numericUpDown3.Value + 1].res)
                {
                    help--;
                }
            }
            catch
            {
                help = dataset.Count;
            }
            List<string> clas = new List<string>(); 
            List<int> clasCount1 = new List<int>();
            for (int i = 0; i < newList.Count; i++)
            {
                if (!clas.Contains(newList[i].clas))
                {
                    clasCount1.Add(0);
                    clas.Add(newList[i].clas);
                }
            }

            for (int i = 0; i < help; i++)
            {
                
                for (int j = 0; j < clas.Count; j++)
                {
                    if(newList[i].clas == clas[j])
                    {
                        clasCount1[j] += 1;
                    }
                }

            }
            int max;
            int count = 0;
            max = clasCount1.Max();
            for (int i = 0; i < clasCount1.Count; i++)
            {
                if (clasCount1[i] == max)
                    count += 1;
            }
            if(count > 1)
            {
                myClass1.x = (int)numericUpDown1.Value;
                myClass1.y = (int)numericUpDown2.Value;
                myClass1.clas = "Nera";
                myClass1.game = (string)comboBox2.SelectedItem;
                myClass1.res = 0;
                newList.Add(myClass1);
            }
            else
            {
                myClass1.x = (int)numericUpDown1.Value;
                myClass1.y = (int)numericUpDown2.Value;
                int index = clasCount1.FindIndex(a => a == max);
                myClass1.clas = clas[index];
                myClass1.game = (string)comboBox2.SelectedItem;
                myClass1.res = 0;
                newList.Add(myClass1);
            }
            stopwatch.Stop();
            label1.Text = "Time in Milliseconds: " + stopwatch.ElapsedMilliseconds.ToString();
            foreach (var item in newList)
            {
                listBox1.Items.Add(item.x + "," + item.y + "," + item.game + "," + item.clas + "," + item.res.ToString("0.###"));
            }
            newList.Clear();
            dataset.Clear();
        }

        private void callfunc()
        {
            using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(@"data.txt", true))
            {
                if (myClass1.clas != "Nera")
                    file.WriteLine(myClass1.x + "," + myClass1.y + "," + myClass1.game + "," + myClass1.clas);
                else
                    MessageBox.Show("Class was not assigned");
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            callfunc();
        }
    }
}
