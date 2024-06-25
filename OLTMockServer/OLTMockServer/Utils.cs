using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telerik.WinControls;

namespace OLTMockServer
{
    public static class Utils
    {
        private static string codes = "0123456789abcdefghijklmnopqrstuvwxyz0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxyz";

        public static string GenerateCode(int len)
        {
            Random rnd = new Random();

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < len; i++)
            {
                var locRandom = rnd.Next(0, codes.Length - 1);
                sb.Append(codes[locRandom]);
            }

            return sb.ToString();
        }

        private static List<Tuple<string, string>> names;
        public static Tuple<string, string> GenerateName()
        {
            if (names == null)
            {
                var nameFileLines = OLTMockServer.Properties.Resources.Names.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                names = new List<Tuple<string, string>>();
                foreach (var item in nameFileLines)
                {
                    var elements = item.Split('@');
                    names.Add(new Tuple<string, string>(elements[0], elements[1]));
                }
            }

            Random rnd = new Random();

            var rnIndex = rnd.Next(0, names.Count - 1);

            return names[rnIndex];
        }

        public static void ShowError(string message)
        {
            RadMessageBox.ThemeName = "Windows7";
            RadMessageBox.Show(null, message, "OLT-MockServer", MessageBoxButtons.OK, RadMessageIcon.Error);
        }

        internal static void ShowInfo(string message)
        {
            RadMessageBox.ThemeName = "Windows7";
            RadMessageBox.Show(null, message, "OLT-MockServer", MessageBoxButtons.OK, RadMessageIcon.Info);
        }

        internal static DialogResult AskQuestion(string message)
        {
            RadMessageBox.ThemeName = "Windows7";
            return RadMessageBox.Show(null, message, "OLT-MockServer", MessageBoxButtons.YesNoCancel, RadMessageIcon.Question);
        }

        public static TDest SwapObjects<TSource, TDest>(TSource source, TDest dest, params string[] skipProps)
        {
            var sourceProps = source.GetType().GetProperties();
            var destProps = dest.GetType().GetProperties();

            foreach (var pSource in sourceProps)
            {
                if (!skipProps.Contains(pSource.Name) && pSource.SetMethod != null)
                {
                    foreach (var pDest in destProps)
                    {
                        if (pDest.Name == pSource.Name && pDest.PropertyType == pSource.PropertyType)
                        {
                            pDest.SetValue(dest, pSource.GetValue(source, null));
                            break;
                        }
                    }
                }
            }

            return dest;
        }
    }
}
