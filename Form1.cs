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
                myClass = dataset[i];
                if (comboBox1.SelectedItem == "KNN1")
                    dataset[i].res = Math.Sqrt(Math.Pow((double)numericUpDown1.Value - myClass.x, 2) + Math.Pow((double)numericUpDown2.Value - myClass.y, 2));
                else if (comboBox1.SelectedItem == "KNN2")
                    dataset[i].res = Math.Max(Math.Abs((double)numericUpDown1.Value - myClass.x), Math.Abs((double)numericUpDown2.Value - myClass.y));              
                else
                {
                    MessageBox.Show("Please Select Algorithm");
                    return;
                }
            }
            var newList = dataset.OrderBy(x => x.res).ToList();

            int count1=0;
            int count2=0;
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
            for (int i = 0; i < help; i++)
            {

                    if (newList[i].clas == "+")
                        count1++;
                    else
                        count2++;

            }
            if (count1 > count2) 
            {
                myClass1.x = (int)numericUpDown1.Value;
                myClass1.y = (int)numericUpDown2.Value;
                myClass1.clas = "+";
                myClass1.res = 0;
                newList.Add(myClass1);
            }
            else if(count1 < count2)
            {
                myClass1.x = (int)numericUpDown1.Value;
                myClass1.y = (int)numericUpDown2.Value;
                myClass1.clas = "-";
                myClass1.res = 0;
                newList.Add(myClass1);
            }
            else
            {
                myClass1.x = (int)numericUpDown1.Value;
                myClass1.y = (int)numericUpDown2.Value;
                myClass1.clas = "Nera";
                myClass1.res = 0;
                newList.Add(myClass1);
            }
            stopwatch.Stop();
            label1.Text = "Time in Milliseconds: " + stopwatch.ElapsedMilliseconds.ToString();
            foreach (var item in newList)
            {
                listBox1.Items.Add(item.x + "," + item.y + "," + item.clas + "," + item.res.ToString("0.###"));
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
                    file.WriteLine(myClass1.x + "," + myClass1.y + "," + myClass1.clas);
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
