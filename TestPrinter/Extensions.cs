using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RioKids
{
    public static class Extensions
    {
        //Management of mouse drag and drop
        #region Menu and Mouse
        private static bool mouseDown;
        private static Point lastLocation;

        /// <summary>
        /// To enable control to be moved around with mouse
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="control"></param>
        public static void moveItselfWithMouse(this Control control)
        {
            control.MouseDown += (o, e) => { mouseDown = true; lastLocation = e.Location; };
            control.MouseMove += (o, e) =>
            {
                if (mouseDown)
                {
                    control.Location = new Point((control.Location.X - lastLocation.X) + e.X, (control.Location.Y - lastLocation.Y) + e.Y);
                    control.Update();
                }
            };
            control.MouseUp += (o, e) => { mouseDown = false; };
        }


        public static void moveOtherWithMouse<T>(this T control, Control movedObject) where T : Control
        {
            control.MouseDown += (o, e) => { mouseDown = true; lastLocation = e.Location; };
            control.MouseMove += (o, e) =>
            {
                if (mouseDown)
                {
                    movedObject.Location = new Point((movedObject.Location.X - lastLocation.X) + e.X, (movedObject.Location.Y - lastLocation.Y) + e.Y);
                    movedObject.Update();
                }
            };
            control.MouseUp += (o, e) => { mouseDown = false; };
        }

        #endregion

        #region Colors

        public static int ToString(this Color color)
        {
            return color.ToArgb();
        }

        public static Color ToColor(this int color)
        {
            return Color.FromArgb(color);
        }

        #endregion

        #region Strings
        /// <summary>
        /// Convert all persian and arabic digit to english in any string  
        /// <!-- http://stackoverflow.com/a/28905353/579381 --> 
        /// </summary>
        /// <param name="inputString">input string that maybe contain persian or arabic digit</param>
        /// <returns>a string with english digit</returns>
        public static string ConvertDigitsToLatin(this string inputString)
        {
            var sb = new StringBuilder();

            foreach (var c in inputString)
            {
                switch (c)
                {
                    case '\u06f0': //Persian digit
                    case '\u0660': //Arabic  digit
                        sb.Append('0');
                        break;
                    case '\u06f1':
                    case '\u0661':
                        sb.Append('1');
                        break;
                    case '\u06f2':
                    case '\u0662':
                        sb.Append('2');
                        break;
                    case '\u06f3':
                    case '\u0663':
                        sb.Append('3');
                        break;
                    case '\u06f4':
                    case '\u0664':
                        sb.Append('4');
                        break;
                    case '\u06f5':
                    case '\u0665':
                        sb.Append('5');
                        break;
                    case '\u06f6':
                    case '\u0666':
                        sb.Append('6');
                        break;
                    case '\u06f7':
                    case '\u0667':
                        sb.Append('7');
                        break;
                    case '\u06f8':
                    case '\u0668':
                        sb.Append('8');
                        break;
                    case '\u06f9':
                    case '\u0669':
                        sb.Append('9');
                        break;

                    default:
                        sb.Append(c);
                        break;
                }
            }

            return sb.ToString();
        }


        public static string ToPersianNumber(this string input)
        {
            if (string.IsNullOrEmpty(input)) return null;
            input = input.Replace("ي", "ی").Replace("ك", "ک");

            //۰ ۱ ۲ ۳ ۴ ۵ ۶ ۷ ۸ ۹
            return
                input
                    .Replace("0", "۰")
                    .Replace("1", "۱")
                    .Replace("2", "۲")
                    .Replace("3", "۳")
                    .Replace("4", "۴")
                    .Replace("5", "۵")
                    .Replace("6", "۶")
                    .Replace("7", "۷")
                    .Replace("8", "۸")
                    .Replace("9", "۹");
        }

        public static string ToEnglishNumber(this string input)
        {
            if (string.IsNullOrEmpty(input)) return null;
            input = input.Replace("ي", "ی").Replace("ك", "ک");

            //۰ ۱ ۲ ۳ ۴ ۵ ۶ ۷ ۸ ۹
            return input
                .Replace(",", "")
                .Replace("۰", "0")
                .Replace("۱", "1")
                .Replace("۲", "2")
                .Replace("۳", "3")
                .Replace("۴", "4")
                .Replace("۵", "5")
                .Replace("۶", "6")
                .Replace("۷", "7")
                .Replace("۸", "8")
                .Replace("۹", "9");
        }
        #endregion

        #region DateAndTime

        public static string ShortTimeFormat(this TimeSpan time)
        {
            return $"{time.Hours:00}:{time.Minutes}";
        }

        public static string FormatAsDescibedTime(this TimeSpan time, string defaultTimeString = null)
        {
            var timeElements = new List<string>();

            if (time.Days > 0)
            {
                timeElements.Add($"{time.Days} روز");
            }

            if (time.Hours > 0)
            {
                timeElements.Add($"{time.Hours} ساعت");
            }

            if (time.Minutes > 0)
            {
                timeElements.Add($"{time.Minutes} دقیقه");
            }

            StringBuilder stringBuilder = new StringBuilder();

            foreach (var item in timeElements)
            {
                if (stringBuilder.Length > 0)
                {
                    stringBuilder.Append(" و ");
                }

                stringBuilder.Append(item);
            }

            if (stringBuilder.Length > 0)
            {
                return stringBuilder.ToString();
            }
            else
            {
                return defaultTimeString;
            }
        }
        #endregion
    }
}
