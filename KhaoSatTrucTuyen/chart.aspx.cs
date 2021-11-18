using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;

namespace KhaoSatTrucTuyen
{
    public partial class chart : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DrawPieChart(1, 1, 1, 1, 1);
        }
        private void DrawPieChart(int value1, int value2, int value3, int value4, int value5)
        {
            //reset your chart series and legends
            chart1.Series.Clear();
            chart1.Legends.Clear();

            //Add a new Legend(if needed) and do some formating
            chart1.Legends.Add("MyLegend");
            chart1.Legends[0].LegendStyle = LegendStyle.Table;
            chart1.Legends[0].Docking = Docking.Bottom;
            chart1.Legends[0].Alignment = StringAlignment.Center;
            chart1.Legends[0].Title = "MyTitle";
            chart1.Legends[0].BorderColor = Color.Transparent;

            //Add a new chart-series
            string seriesname = "MySeriesName";
            chart1.Series.Add(seriesname);
            //set the chart-type to "Pie"
            chart1.Series[seriesname].ChartType = SeriesChartType.Pie;

            //Add some datapoints so the series. in this case you can pass the values to this method
            chart1.Series[seriesname].Points.AddXY("A", value1);
            chart1.Series[seriesname].Points.AddXY("B", value2);
            chart1.Series[seriesname].Points.AddXY("C", value3);
            chart1.Series[seriesname].Points.AddXY("D", value4);
            chart1.Series[seriesname].Points.AddXY("E", value5);
            foreach (DataPoint p in chart1.Series[seriesname].Points)
            {
                if (p.GetValueByName("Y") != 0)
                {
                    p.Label = "#PERCENT";
                    p.LegendText = "#VALX";
                }
                else
                {   
                    p["PieLabelStyle"] = "Disabled";
                }

            }
        }
    }
}