using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 日历呦呦呦_小包子加油_
{
    public class Functionofshow
    {
        //农历数据结构的定义
        private static string[] tiangan = { "甲", "乙", "丙", "丁", "戊", "己", "庚", "辛", "壬", "癸" };
        private static string[] dizhi = { "子", "丑", "寅", "卯", "辰", "巳", "午", "未", "申", "酉", "戌", "亥" };
        private static string[] shengxiao = { "鼠", "牛", "虎", "免", "龙", "蛇", "马", "羊", "猴", "鸡", "狗", "猪" };

        private static string[] months = { "正", "二", "三", "四", "五", "六", "七", "八", "九", "十", "十一", "十二(腊)" };

        private static string[] days1 = { "初", "十", "廿", "三" };
        private static string[] days = { "一", "二", "三", "四", "五", "六", "七", "八", "九", "十" };

        //公历数据结构的定义
        private int[] noLeapYear_max_days = new int[] { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
        private int[] LeapYear_max_days = new int[] { 31, 29, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

        const int LeapYearDays = 366;
        const int noLeapYearDays = 365;

        //星期数据结构的定义
         private static string[] weeks = new string[]{"一", "二", "三", "四", "五", "六","日"};

        //范围界定数据结构的定义
        const int minGregorianYear = 1900;
        const int maxGregorianYear = 2100;

        #region 显示公历

        #region 判断输入的年份是否为闰年，工具类
        public Boolean isLeapYear(int year)
        {
            if (year % 4 == 0 && year % 100 != 0)
            {
                return true;
            }
            else if (year % 400 == 0)
            {
                return true;
            }
            return false;
        }
        #endregion

        #region 显示下一天，给出offset版本，返回Solar
        public Solar showDate(int offset,int given_year,int given_month,int given_date)
        {
            int given_offset = 0;
            if (isLeapYear(given_year))
            {
                for (int i = 1; i < given_month; i++)
                {
                    given_offset += LeapYear_max_days[i - 1];
                }
            }
            else
            {
                for (int j = 1; j < given_month; j++)
                {
                    given_offset += noLeapYear_max_days[j - 1];
                }
            }
            given_offset += given_date;

            int choose_offset;
            choose_offset = given_offset + offset;

            var solar = new Solar();
            solar.solarYear= given_year;
            solar.solarMonth= 0;
            solar.solarDay= 0;

            if (isLeapYear(given_year))
            {
                if (choose_offset > LeapYearDays)
                {
                    solar.solarYear++;
                    choose_offset -= LeapYearDays;
                }
                for (int i = 0; i < 12; i++)
                {
                    if (choose_offset <= LeapYear_max_days[i])
                    {
                        solar.solarMonth = i + 1;
                        solar.solarDay = choose_offset;
                        break;
                    }
                    choose_offset -= LeapYear_max_days[i];
                }
            }
            else
            {
                if (choose_offset > noLeapYearDays)
                {
                    solar.solarYear++;
                    choose_offset -= noLeapYearDays;
                }
                for (int j = 0; j < 12; j++)
                {
                    if (choose_offset <= noLeapYear_max_days[j])
                    {
                        solar.solarMonth = j + 1;
                        solar.solarDay = choose_offset;
                        break;
                    }
                    choose_offset -= noLeapYear_max_days[j];
                }
            }
            return solar;
        }
        #endregion

        #endregion

        #region 显示农历,要将数字年份更换成为文字类的

        //显示年份的更换
        public static string GetLunisolarYear(int lunaryear)
        {
            int tgIndex = (lunaryear - 4) % 10;
            int dzIndex = (lunaryear - 4) % 12;

                return string.Concat(tiangan[tgIndex], dizhi[dzIndex], "[", shengxiao[dzIndex], "]");
        }

        //显示月份的更换
        public static string GetLunisolarMonth(int lunarmonth)
        {
            return months[lunarmonth - 1];
        }

        //显示日期的更换
        public static string GetLunisolarDay(int lunarday)
        {
            if (lunarday != 20 && lunarday != 30)
                {
                    return string.Concat(days1[(lunarday - 1) / 10], days[(lunarday - 1) % 10]);
                }
            else
                {
                    return string.Concat(days[(lunarday - 1) / 10], days1[1]);
                }
        }
        #endregion

        #region 显示星期
        public string showWeek(int offset,int given_year,int given_month,int given_date)
        {
            int given_offset = 0;
            for (int i = 1900; i < given_year; i++)
            {
                if (isLeapYear(i))
                {
                    given_offset += LeapYearDays;
                }
                else
                {
                    given_offset += noLeapYearDays;
                }
            }

            if (isLeapYear(given_year))
            {
                for (int i = 0; i < given_month - 1; i++)
                {
                    given_offset += LeapYear_max_days[i];
                }
            }
            else
            {
                for (int j = 0; j < given_month - 1; j++)
                {
                    given_offset += noLeapYear_max_days[j];
                }
            }

            given_offset += given_date;
            int choose_offeset = given_offset + offset;
            int week_choose;
            if (choose_offeset % 7 == 0)
            {
                week_choose = 7;
                return  weeks[week_choose-1];
            }
            else if (choose_offeset % 7 > 0)
            {
                week_choose = choose_offeset % 7;
                return  weeks[week_choose-1];
            }
            else
            {
                week_choose = -1;
                return  "Error!";
            }
        }

        #endregion 

        #region 显示生肖
        public static string GetShengXiao(int lunaryear)
        {
            return shengxiao[(lunaryear - 4) % 12];
        }
        #endregion

        #region 显示输入越界提示
        public Boolean isLegal(int given_year, int given_month, int given_date)
        {
            if (given_year >= minGregorianYear && given_year <= maxGregorianYear)
            {
                if (given_month >= 1 && given_month <= 12)
                {
                    if (isLeapYear(given_year))
                    {
                        if (given_date >= 1 && given_date <= LeapYear_max_days[given_month - 1])
                        {
                            return true;
                        }
                        else if (given_date >= 1 && given_date <= noLeapYear_max_days[given_month - 1])
                        {
                            return true;
                        }
                        else
                        { }
                    }
                    else { }
                }
                else { }
            }
            return false;
        }
        #endregion
    }
}
