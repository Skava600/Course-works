using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Numerics;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Diagnostics;
using System.IO;

namespace Cursovaya
{
    public partial class Form1 : Form
    {
        public static int N;
        Stopwatch stopwatch = new Stopwatch();
        public Form1()
        {
            InitializeComponent();
        }

        private void Return_Search_Click(object sender, EventArgs e)
        {        
            stopwatch.Start();
            if (!int.TryParse(textBox1.Text,  out int x) || x <= 0)
            {
                MessageBox.Show("Wrong Inpput", "Message");
                return;
            }
            N = Convert.ToInt32(textBox1.Text);
            InitializeDataGridView(Convert.ToInt32(textBox1.Text));           
            QueensProblem cb = new QueensProblem();
            cb.Init(N);
            label1.Text = cb.Solve().ToString();
            foreach (int[] i in cb.GetResults())
            {
                AddToList(i, N);
            }
            stopwatch.Stop();
            label5.Text = stopwatch.ElapsedMilliseconds.ToString() + "msec";
            TextWriter writer = new StreamWriter(@"C:\Users\vladi\OneDrive\Рабочий стол\Cursovaya\answers.txt");
            foreach (var item in listBox1.Items)
                writer.WriteLine(item.ToString());
            writer.Close();

        }

    

        void InitializeDataGridView(int N)
        {
            dataGridView1.ClearSelection();
            dataGridView1.Columns.Clear();
            dataGridView1.Rows.Clear();
            dataGridView1.DataSource = null;
            listBox1.Items.Clear();
            // сформировать dataGridView1 - добавить столбцы
            for (int i = 1; i <= N; i++)
            {
                dataGridView1.Columns.Add("i" + i.ToString(), i.ToString());

                // ширина столбца в пикселах
                dataGridView1.Columns[i - 1].Width = 30;
            }

            // добавить строки
            dataGridView1.Rows.Add(N);

            // установить номер в каждой строке
            for (int i = 1; i <= N; i++)
                dataGridView1.Rows[i - 1].HeaderCell.Value = i.ToString();

            // забирает последнюю строку, чтобы нельзя было добавлять строки в режиме выполнения
            dataGridView1.AllowUserToAddRows = false;

            // выравнивание по центру во всех строках
            dataGridView1.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }
        void AddToList(int []M, int N)
        {
            // добавить к listBox1 строку
            string s = "";
                for (int i = 0; i < N; i++)
                    s = s +  (i+1).ToString() + "-" + (M[i] + 1).ToString() + ", ";
            s = s.Remove(s.Length - 2);
                listBox1.Items.Add(s);
            
        }
        void ShowDataGridView(string s, int N)
        {
            Sound.PlaySound(@"C:\Users\vladi\OneDrive\Рабочий стол\Semester 2\chess.wav", new System.IntPtr(), Sound.SoundFlags.SND_ASYNC);//чтобы был звук нужно поменять директорию
            int i;
            int j;
            int x, y;
            // сначала очистить dataGridView1
            for (i = 0; i < N; i++)
                for (j = 0; j < N; j++)
                    dataGridView1.Rows[i].Cells[j].Value = "";
            s = s.Trim(' ');
            string[] cages = s.Split(new char[] { ',' });
            foreach (string cage in cages)
            {
                x = Convert.ToInt32(cage.Split(new char[] { '-' })[0]);
                y = Convert.ToInt32(cage.Split(new char[] { '-' })[1]);
                dataGridView1.Rows[y - 1].Cells[x - 1].Value = "♛";
            }


        }
        private new void Click(object sender, EventArgs e)
        {
            if (listBox1.Items.Count <= 0) return;
            int num;
            string s;
            num = listBox1.SelectedIndex;
            if (num != -1)
            {
                s = listBox1.Items[num].ToString();
                ShowDataGridView(s, N);
            }
        }

        private void Fractal_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox1.Text, out int x) || x <= 0)
            {
                MessageBox.Show("Wrong Inpput", "Message");
                return;
            }
            stopwatch.Start();
            N = Convert.ToInt32(textBox1.Text);
            InitializeDataGridView(Convert.ToInt32(textBox1.Text));
            int mod = N % 12;
            ArrayList List = new ArrayList();
            for (int i = 2; i <= N; i += 2)
                List.Add(i);
            if (mod == 3 || mod == 9)
            {
                List.Remove(2);
                List.Add(2);
            }
            if (mod == 8)
            {
                ArrayList List2 = new ArrayList();
                for (int i = 3; i <= N;)
                {
                    List2.Add(i);
                    i -= 2;
                    List2.Add(i);
                    i += 6;
                }
                List.AddRange(List2);
               
            }
            else
            {
                for (int i = 1; i <= N; i += 2)
                    List.Add(i);
            }
            if(mod == 2 && N >= 3)
            {
                List.RemoveAt(List.IndexOf(1));
                List.Insert(List.IndexOf(3) + 1, 1);
                if(N >= 5)
                {
                    List.Remove(5);
                    List.Add(5);
                }
            }
            if( mod == 3 || mod == 9)
            {
                List.Remove(1);
                List.Remove(3);
                List.Add(1);
                List.Add(3);
            }
           
            int[] M = new int[N];
            for (int i = 0; i < N; i++)
            {                
                M[i] = Convert.ToInt32(List[i]);
                M[i]--;
            }
            AddToList(M, N);
            for (int i = 0; i < N; i++)
            {
                M[i] = N - 1 - M[i];
            }
            AddToList(M, N);
            stopwatch.Stop();
            label5.Text = stopwatch.ElapsedMilliseconds.ToString() + "msec";
            TextWriter writer = new StreamWriter(@"C:\Users\vladi\OneDrive\Рабочий стол\Semester 2\answers.txt");
            foreach (var item in listBox1.Items)
                writer.WriteLine(item.ToString());
            writer.Close();
        }
        private void FractalSolvingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox1.Text, out int x) || x <= 0)
            {
                MessageBox.Show("Wrong Inpput", "Message");
                return;
            }
            stopwatch.Start();
            N = Convert.ToInt32(textBox1.Text);
            InitializeDataGridView(Convert.ToInt32(textBox1.Text));
            int mod = N % 12;
            ArrayList List = new ArrayList();
            for (int i = 2; i <= N; i += 2)
                List.Add(i);
            if (mod == 3 || mod == 9)
            {
                List.Remove(2);
                List.Add(2);
            }
            if (mod == 8)
            {
                ArrayList List2 = new ArrayList();
                for (int i = 3; i <= N;)
                {
                    List2.Add(i);
                    i -= 2;
                    List2.Add(i);
                    i += 6;
                }
                List.AddRange(List2);

            }
            else
            {
                for (int i = 1; i <= N; i += 2)
                    List.Add(i);
            }
            if (mod == 2 && N >= 3)
            {
                List.RemoveAt(List.IndexOf(1));
                List.Insert(List.IndexOf(3) + 1, 1);
                if (N >= 5)
                {
                    List.Remove(5);
                    List.Add(5);
                }
            }
            if (mod == 3 || mod == 9)
            {
                List.Remove(1);
                List.Remove(3);
                List.Add(1);
                List.Add(3);
            }

            int[] M = new int[N];
            for (int i = 0; i < N; i++)
            {
                M[i] = Convert.ToInt32(List[i]);
                M[i]--;
            }
            AddToList(M, N);
            for (int i = 0; i < N; i++)
            {
                M[i] = N - 1 - M[i];
            }
            AddToList(M, N);
            stopwatch.Stop();
            label5.Text = stopwatch.ElapsedMilliseconds.ToString() + "msec";
        }

        private void ReturnSearchSolvingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            stopwatch.Start();
            if (!int.TryParse(textBox1.Text, out int x) || x <= 0)
            {
                MessageBox.Show("Wrong Inpput", "Message");
                return;
            }
            N = Convert.ToInt32(textBox1.Text);
            InitializeDataGridView(Convert.ToInt32(textBox1.Text));
            QueensProblem cb = new QueensProblem();
            cb.Init(N);
            label1.Text = cb.Solve().ToString();
            foreach (int[] i in cb.GetResults())
            {
                AddToList(i, N);
            }
            stopwatch.Stop();
            label5.Text = stopwatch.ElapsedMilliseconds.ToString() + "msec";
            
        }

        private void SaveAnswersIntoTxtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TextWriter writer = new StreamWriter(@"C:\Users\vladi\OneDrive\Рабочий стол\Cursovaya\answers.txt");
            foreach (var item in listBox1.Items)
                writer.WriteLine(item.ToString());
            writer.Close();
        }

        private void InfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Developer - Skovorodnik Vladislav, BSUIR 2020", "Info");
        }

        
    }
}
