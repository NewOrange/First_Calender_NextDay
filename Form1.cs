using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 日历呦呦呦_小包子加油_
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.textBox_now.Text = DateTime.Now.ToLongDateString();
        }
        public int given_year;
        public int given_month;
        public int given_day;
        Functionofshow fos = new Functionofshow();

        private void button1_Click(object sender, EventArgs e)
        {
            given_year = int.Parse(this.textBox_year.Text);
            given_month = int.Parse(this.textBox_month.Text);
            given_day = int.Parse(this.textBox_day.Text);
           if( fos.isLegal(given_year,given_month,given_day))
           {
               var tsolar = new Solar();
               tsolar = fos.showDate(1, given_year, given_month, given_day);
               this.textBox_solar.Text = tsolar.solarYear + "年" + tsolar.solarMonth + "月" + tsolar.solarDay + "日";
               var tlunar = new Lunar();
               tlunar = LunarSolarConverter.SolarToLunar(tsolar);
               this.textBox_lunar.Text = Functionofshow.GetLunisolarYear(tlunar.lunarYear) + "年" + Functionofshow.GetLunisolarMonth(tlunar.lunarMonth) + "月" + Functionofshow.GetLunisolarDay(tlunar.lunarDay);
               this.textBox_week.Text = fos.showWeek(1, given_year, given_month, given_day);
           }
           else
           {

           }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.textBox_year.Clear();
            this.textBox_month.Clear();
            this.textBox_day.Clear();
            this.textBox_lunar.Clear();
            this.textBox_solar.Clear();
            this.textBox_lunar.Clear();
            this.textBox_week.Clear();
        }  
     
    }
}
