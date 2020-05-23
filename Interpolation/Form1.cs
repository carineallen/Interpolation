using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FractionsFunc;

namespace Interpolation
{
    public partial class Form1 : Form
    {

        private void Form1_Resize(object sender, EventArgs e)
        {

            int formHeight = 692;
            int formWidth = 1534;
            int diffX = this.Width - formWidth;
            int diffY = this.Height - formHeight;

            dataGridView1.Height = 621 + diffY;

            chart1.Width = 1094 + diffX;
            chart1.Height = 459 + diffY;


        }

        public Form1()
        {
            InitializeComponent();
        }

        double Interval;
        DataTable ValuesTable;

        private void button1_Click(object sender, EventArgs e)
        {
            int i,g;
            bool Fineshed = false;
            if(textBox6.Text == "")
            {
                MessageBox.Show("you have to choose a X interval");
                return;
            }
            if (textBox5.Text == "")
            {
                MessageBox.Show("you have to choose a Y interval");
                return;
            }
            if (textBox7.Text == "")
            {
                MessageBox.Show("you have to choose a Y Max.");
                return;
            }
            if (textBox8.Text == "")
            {
                MessageBox.Show("you have to choose a Y Min.");
                return;
            }

            if (Convert.ToString(dataGridView1.Rows[0].Cells[0].Value) == "" || Convert.ToString(dataGridView1.Rows[0].Cells[1].Value) == "" )
            {
                MessageBox.Show("You have to put some data in the table");
                return;
            }
            Interval = Convert.ToDouble(textBox6.Text);
            ValuesTable = new DataTable();
            ValuesTable.Columns.Add();
            ValuesTable.Columns.Add();
            if (chart1.Series.Count > 1)
            {
                try
                {
                    chart1.Series.Remove(chart1.Series[1]);
                }
                catch
                {

                }
            }

            for (g = 0; g < dataGridView1.Rows.Count - 1; g++)
            {
                //if(dataGridView1.Rows[g].Cells[0].Value)
                ValuesTable.Rows.Add(dataGridView1.Rows[g].Cells[0].Value,dataGridView1.Rows[g].Cells[1].Value);
            }

            while (Fineshed == false)
            {
                DataTable DataTable1 = new DataTable();
                DataTable1.Columns.Add("X");
                DataTable1.Columns.Add("Y");
                decimal X1, X2, Y1, Y2;
                if (dataGridView1.Rows.Count > 0)
                {
                    X1 = Convert.ToDecimal(dataGridView1.Rows[0].Cells[0].Value);
                    X2 = 0;
                    Y1 = Convert.ToDecimal(dataGridView1.Rows[0].Cells[1].Value);
                    Y2 = 0;
                    for (i = 0; i < dataGridView1.Rows.Count - 1; i++)
                    {
                        X2 = Convert.ToDecimal(dataGridView1.Rows[i].Cells[0].Value);
                        Y2 = Convert.ToDecimal(dataGridView1.Rows[i].Cells[1].Value);
                        if (X2 - X1 > Convert.ToDecimal(Interval))
                        {
                            //if (X1 > 0)
                            //{
                            //    X1 -= Convert.ToDecimal(Interval);
                            //}
                            //else
                            //{
                            //    X1 += Convert.ToDecimal(Interval);
                            //}
                            X1 += Convert.ToDecimal(Interval);
                            Y2 = Get_NewValue_Lagrange(X1);
                            Y2 = Math.Round(Y2, 3);
                            DataTable1.Rows.Add(X1, Y2);
                            //DataTable1.Rows[DataTable1.Rows.Count - 1].ItemArray[0] = X1;
                            //DataTable1.Rows[DataTable1.Rows.Count - 1].ItemArray[1] = Y2;

                            int j;

                            for (j = i; j < dataGridView1.Rows.Count - 1; j++)
                            {
                                DataTable1.Rows.Add(dataGridView1.Rows[j].Cells[0].Value, dataGridView1.Rows[j].Cells[1].Value);
                                //DataTable1.Rows[DataTable1.Rows.Count - 1].ItemArray[0] = dataGridView1.Rows[j].Cells[0].Value;
                                //DataTable1.Rows[DataTable1.Rows.Count - 1].ItemArray[1] = dataGridView1.Rows[j].Cells[1].Value;
                            }

                            try
                            {
                                dataGridView1.Rows.Clear();
                                dataGridView1.Columns.Clear();
                            }
                            catch {
                                dataGridView1.DataSource = "";
                            }
                            dataGridView1.DataSource = DataTable1;
                            break;
                        }
                        DataTable1.Rows.Add(X2,Y2);
                        //DataTable1.Rows[DataTable1.Rows.Count - 1].ItemArray[0] = X2;
                        //DataTable1.Rows[DataTable1.Rows.Count - 1].ItemArray[1] = Y2;
                        X1 = X2;
                        Y1 = Y2;

                        if (i >= dataGridView1.Rows.Count - 2)
                        {
                            Fineshed = true;
                        }

                    }
                }

            

            }

            var chart = chart1.ChartAreas[0];
            chart.AxisX.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            chart.AxisX.LabelStyle.Format = "";
            chart.AxisY.LabelStyle.Format = "";
            chart.AxisY.LabelStyle.IsEndLabelVisible = true;
            chart.AxisX.LabelStyle.IsEndLabelVisible = true;

            chart.AxisX.Minimum = Convert.ToDouble(dataGridView1.Rows[0].Cells[0].Value);
            chart.AxisX.Maximum = Convert.ToDouble(dataGridView1.Rows[dataGridView1.Rows.Count - 2].Cells[0].Value);
            chart.AxisY.Minimum = Convert.ToDouble(textBox8 .Text);
            chart.AxisY.Maximum = Convert.ToDouble(textBox7.Text);

            chart.AxisY.Interval = Convert.ToDouble(textBox5.Text);
            chart.AxisX.Interval = Interval;

            chart1.Series.Add("Lagrange Interpolation");
            chart1.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            chart1.Series[1].Color = Color.Red;
            chart1.Series[0].IsVisibleInLegend = false;


            int s;
            for (s = 0; s < dataGridView1.Rows.Count - 1; s++)
            {

                chart1.Series[1].Points.AddXY(float.Parse(Convert.ToString(dataGridView1.Rows[s].Cells[0].Value)), float.Parse(Convert.ToString(dataGridView1.Rows[s].Cells[1].Value)));
            }

            chart1.Series[1].Enabled = true;
        }

        private decimal Get_NewValue_Lagrange(decimal Xp)
        {

            decimal[] X = new decimal[ValuesTable.Rows.Count];
            decimal[] Y = new decimal[ValuesTable.Rows.Count];
            int x1,y1;
            decimal Yp, P;
            int i, j;
            x1 = 0;
            y1 = 0;
            int z;
            for (z = 0; z <= ValuesTable.Rows.Count - 1; z++) {
                X[x1] = Convert.ToDecimal(ValuesTable.Rows[z].ItemArray[0]);
                Y[y1] = Convert.ToDecimal(ValuesTable.Rows[z].ItemArray[1]);
                x1 += 1;
                y1 += 1;

            }

            int n = ValuesTable.Rows.Count - 1; //degre of the polynomial

            Yp = 0;
            for (i = 0; i <= n; i++)
            {
                P = 1.00m;
                for (j = 0; j <= n; j++)
                {
                    if (j != i)
                    {
                        try
                        {
                            P *= (Xp - X[j]) / (X[i] - X[j]);
                        }
                        catch
                        {
                            P *= 1;
                        }
                    }

                }

                Yp += Y[i] * P;

            }

            return Yp;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.Columns.Add("X", "X");
            dataGridView1.Columns.Add("Y", "Y");
            dataGridView1.Rows.Add();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("you have to choose a X value to calculate Y");
                return;
            }
            ValuesTable = new DataTable();
            ValuesTable.Columns.Add();
            ValuesTable.Columns.Add();
            int g;
            for (g = 0; g < dataGridView1.Rows.Count - 1; g++)
            {
                ValuesTable.Rows.Add(dataGridView1.Rows[g].Cells[0].Value, dataGridView1.Rows[g].Cells[1].Value);
            }

            decimal Result = Get_NewValue_Lagrange(Convert.ToDecimal(textBox1.Text));
            textBox2.Text = Convert.ToString(Result);
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.V)
            {
                int ColumnCount = dataGridView1.Columns.Count;
                int RowsCount = dataGridView1.Rows.Count;

                try
                {
                    for (int d = 0; d <= RowsCount - 1; d++)
                    {
                        dataGridView1.Rows.Remove(dataGridView1.Rows[dataGridView1.Rows.Count - 1]);
                    }
                }
                catch
                {

                }


                try
                {
                    for (int d = 0; d <= ColumnCount - 1; d++)
                    {
                        dataGridView1.Columns.Remove(dataGridView1.Columns[dataGridView1.Columns.Count - 1]);
                    }
                }
                catch
                {

                }

                int i;
                string Line = Clipboard.GetText();
                string[] Split1 = Line.Split('\n');
                dataGridView1.DataSource = null;
                dataGridView1.Columns.Add("X", "X");
                dataGridView1.Columns.Add("Y", "Y");
                dataGridView1.Rows.Add();
                for (i=0; i <= Split1.Length - 1 ; i++)
                {
                    try
                    {
                        string Values;
                        Values = Split1[i].Substring(0, Split1[i].Length - 1);
                        string[] Split2 = Values.Split('\t');
                        if (Split2.Length >= 2)
                        {
                            if (i == 0)
                            {
                                dataGridView1.Rows[0].Cells[0].Value = Split2[0];
                                dataGridView1.Rows[0].Cells[1].Value = Split2[1];
                            }
                            else
                            {
                                dataGridView1.Rows.Add();
                                dataGridView1.Rows[i].Cells[0].Value = Split2[0];
                                dataGridView1.Rows[i].Cells[1].Value = Split2[1];
                            }
                        }
                    }
                    catch
                    {

                    }                 
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int i, g;
            bool Fineshed = false;
            if (textBox6.Text == "")
            {
                MessageBox.Show("you have to choose a X interval");
                return;
            }
            if (textBox5.Text == "")
            {
                MessageBox.Show("you have to choose a Y interval");
                return;
            }
            if (textBox7.Text == "")
            {
                MessageBox.Show("you have to choose a Y Max.");
                return;
            }
            if (textBox8.Text == "")
            {
                MessageBox.Show("you have to choose a Y Min.");
                return;
            }

            if (Convert.ToString(dataGridView1.Rows[0].Cells[0].Value) == "" || Convert.ToString(dataGridView1.Rows[0].Cells[1].Value) == "")
            {
                MessageBox.Show("You have to put some data in the table");
                return;
            }
            Interval = Convert.ToDouble(textBox6.Text);
            ValuesTable = new DataTable();
            ValuesTable.Columns.Add();
            ValuesTable.Columns.Add();
            if (chart1.Series.Count > 1)
            {
                try
                {
                    chart1.Series.Remove(chart1.Series[1]);
                }
                catch
                {

                }
            }

            for (g = 0; g < dataGridView1.Rows.Count - 1; g++)
            {
                ValuesTable.Rows.Add(dataGridView1.Rows[g].Cells[0].Value, dataGridView1.Rows[g].Cells[1].Value);
            }

           
            var chart = chart1.ChartAreas[0];
            chart.AxisX.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            chart.AxisX.LabelStyle.Format = "";
            chart.AxisY.LabelStyle.Format = "";
            chart.AxisY.LabelStyle.IsEndLabelVisible = true;
            chart.AxisX.LabelStyle.IsEndLabelVisible = true;

            chart.AxisX.Minimum = Convert.ToDouble(dataGridView1.Rows[0].Cells[0].Value);
            chart.AxisX.Maximum = Convert.ToDouble(dataGridView1.Rows[dataGridView1.Rows.Count - 2].Cells[0].Value);
            chart.AxisY.Minimum = Convert.ToDouble(textBox8.Text);
            chart.AxisY.Maximum = Convert.ToDouble(textBox7.Text);

            chart.AxisY.Interval = Convert.ToDouble(textBox5.Text);
            chart.AxisX.Interval = Interval;

            chart1.Series.Add("Cubic Splines Interpolation");
            chart1.Series[1].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            chart1.Series[1].Color = Color.Red;
            chart1.Series[0].IsVisibleInLegend = false;


            int s;
            for (s = 0; s < dataGridView1.Rows.Count - 1; s++)
            {

                chart1.Series[1].Points.AddXY(float.Parse(Convert.ToString(dataGridView1.Rows[s].Cells[0].Value)), float.Parse(Convert.ToString(dataGridView1.Rows[s].Cells[1].Value)));
            }

            chart1.Series[1].Enabled = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox4.Text == "")
            {
                MessageBox.Show("you have to choose a X value to calculate Y");
                return;
            }
            ValuesTable = new DataTable();
            ValuesTable.Columns.Add();
            ValuesTable.Columns.Add();
            int g;
            for (g = 0; g < dataGridView1.Rows.Count - 1; g++)
            {
                ValuesTable.Rows.Add(dataGridView1.Rows[g].Cells[0].Value, dataGridView1.Rows[g].Cells[1].Value);
            }

            decimal Result = Get_NewValue_Cubic(Convert.ToDecimal(textBox4.Text));
            textBox3.Text = Convert.ToString(Result);
        }

        private decimal Get_NewValue_Cubic(decimal Xp)
        {
            decimal Fx0, Fx1,X0,X1,Xi,Yi;
            int z;
            Xi = Xp;
            X0 = 0;
            X1 = 0;
            Fx0 = 0;
            Fx1 = 0;
            for (z = 0; z <= ValuesTable.Rows.Count - 1; z++)
            {
                X0 = Convert.ToDecimal(ValuesTable.Rows[z].ItemArray[0]);
                X1 = Convert.ToDecimal(ValuesTable.Rows[z + 1].ItemArray[0]);
                Fx0 = Convert.ToDecimal(ValuesTable.Rows[z].ItemArray[1]);
                Fx1 = Convert.ToDecimal(ValuesTable.Rows[z + 1].ItemArray[1]);

                if ((Xi > X0 && Xi < X1) || (Xi == X0) || (Xi == X1)) {

                    break;
                    }


            }

            Yi = (Fx0 / 6m) * ((Convert.ToDecimal(Math.Pow(Convert.ToDouble(X1 - Xi), 3)) / (X1 - X0)) - ((X1 - X0) * (X1 - Xi)));
            Yi = Yi + (Fx1 / 6m) * ((Convert.ToDecimal(Math.Pow(Convert.ToDouble(Xi - X0), 3)) / (X1 - X0)) - ((X1 - X0) * (Xi - X0)));
            Yi = Yi + ((Fx0) * (( X1 - Xi) / (X1 - X0))) + ((Fx1) * ((Xi - X0) / (X1 - X0)));
            return Yi;
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (textBox1.Text == "")
                {
                    MessageBox.Show("you have to choose a X value to calculate Y");
                    return;
                }
                ValuesTable = new DataTable();
                ValuesTable.Columns.Add();
                ValuesTable.Columns.Add();
                int g;
                for (g = 0; g < dataGridView1.Rows.Count - 1; g++)
                {
                    ValuesTable.Rows.Add(dataGridView1.Rows[g].Cells[0].Value, dataGridView1.Rows[g].Cells[1].Value);
                }

                decimal Result = Get_NewValue_Lagrange(Convert.ToDecimal(textBox1.Text));
                textBox2.Text = Convert.ToString(Result);
            }
        }

        private void textBox4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (textBox4.Text == "")
                {
                    MessageBox.Show("you have to choose a X value to calculate Y");
                    return;
                }
                ValuesTable = new DataTable();
                ValuesTable.Columns.Add();
                ValuesTable.Columns.Add();
                int g;
                for (g = 0; g < dataGridView1.Rows.Count - 1; g++)
                {
                    ValuesTable.Rows.Add(dataGridView1.Rows[g].Cells[0].Value, dataGridView1.Rows[g].Cells[1].Value);
                }

                decimal Result = Get_NewValue_Cubic(Convert.ToDecimal(textBox4.Text));
                textBox3.Text = Convert.ToString(Result);
            }
        }
    }
}
