using LiveCharts;
using LiveCharts.Wpf;
using LiveCharts.Wpf.Charts.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace GraphForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        bool uz = false; Bitmap memoryImage;

        public void UpdateGraph()
        {
            cartesianChart1.Series.Clear();
            LiveCharts.SeriesCollection sc = new LiveCharts.SeriesCollection();

            //if (valuesBindingSource.DataSource != null) { 
            if (valuesBindingSource.DataSource.ToString()!=/*"{Name = \"Values\" FullName = \"GraphForm.Values\"}"*/"{Method = {System.String ToString()}}"){
                var years = (from o in valuesBindingSource.DataSource as List<Values> select new { Year = o.Year }).Distinct();

                foreach (var y in years)
                {
                    List<double> values = new List<double>();

                    for (int month = 1; month <= 12; month++)
                    {
                        double val = 0;

                        var data = from o in valuesBindingSource.DataSource as List<Values>
                                   where o.Year.Equals(y.Year) && o.Month.Equals(month)
                                   orderby o.Month ascending
                                   select new { o.Value, o.Month };

                        // Sum the values for the same month
                        foreach (var item in data)
                        {
                            val += item.Value;
                        }

                        values.Add(val);
                    }

                    sc.Add(new LineSeries()
                    {
                        Title = y.Year.ToString(),
                        Values = new ChartValues<double>(values)
                    });
                }

                cartesianChart1.Series = sc;
            }
        }


        private void pieChart1_ChildChanged(object sender, System.Windows.Forms.Integration.ChildChangedEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            valuesBindingSource.DataSource = new List<Values>();

            cartesianChart1.AxisX.Add(new LiveCharts.Wpf.Axis
            {
                Title = "Month",
            });
            cartesianChart1.AxisY.Add(new LiveCharts.Wpf.Axis
            {
                Title = "Value",
            });

            cartesianChart1.LegendLocation = LiveCharts.LegendLocation.Right;
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (valuesBindingSource.DataSource == null)
            {
                return;
            }
            if(uz)
                UpdateGraph();
        }

        private void exportAsPNGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //using(var bitmap = new Bitmap(cartesianChart1 ))
            var chart = cartesianChart1;

            using (var bmp = new Bitmap(chart.Width, chart.Height))
            {
                chart.DrawToBitmap(bmp, new Rectangle(0, 0, chart.Width, chart.Height));
                bmp.Save("screenshotchart1.png");
            }
            //this.cartesianChart1.SaveImage("C:\\mycode\\mychart.png", ChartImageFormat.Png);
        }

        private void uzToolStripMenuItem_Click(object sender, EventArgs e)
        {
            uz = true;
        }

        private void printujTyJoToolStripMenuItem_Click(object sender, EventArgs e)
        {/*
            var pDq = new PrintDialog();
            var pDoc = new PrintDocument   ();
            pDoc.PrintPage += new PrintPageEventHandler(pr)*//*
            Graphics myGraphics = this.CreateGraphics();
            Size s = this.Size;
            memoryImage = new Bitmap(s.Width, s.Height, myGraphics);
            Graphics memoryGraphics = Graphics.FromImage(memoryImage);
            memoryGraphics.CopyFromScreen(this.Location.X, this.Location.Y, 0, 0, s);

            printDocument1.Print();*/

            var pDq = new PrintDialog();
            var pDoc = new PrintDocument();
            pDoc.PrintPage += new PrintPageEventHandler(PpPppp); 

            if(pDq.ShowDialog() == DialogResult.OK)
            {
                pDoc.Print();
            }
        }


        private void PrintDocument1_PrintPage(System.Object sender,
           System.Drawing.Printing.PrintPageEventArgs e) =>
               e.Graphics.DrawImage(memoryImage, 0, 0);

        void PpPppp(object sender, PrintPageEventHandler e)
        {
            var chart = cartesianChart1;
            var bmp = new Bitmap(chart.Width, chart.Height);
            chart.DrawToBitmap(bmp, new Rectangle(0, 0, chart.Width, chart.Height));
            //e.gra
        }

        void PpPppp(object sender, PrintPageEventArgs e)
        {
            var chart = cartesianChart1;
            var bmp = new Bitmap(chart.Width, chart.Height);
            chart.DrawToBitmap(bmp, new Rectangle(0, 0, chart.Width, chart.Height));
            //e.gra
        }
        /*
        void PpPppp(object sender, PrintPageEventArgs e)
        {
            var chart = cartesianChart1;
            var bmp = new Bitmap(chart.Width, chart.Height);
            chart.DrawToBitmap(bmp, new Rectangle(0, 0, chart.Width, chart.Height));
            //e.gra
        }*/

    }
}
