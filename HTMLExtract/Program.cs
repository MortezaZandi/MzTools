using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using HtmlAgilityPack;

namespace HTMLExtract
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (Environment.GetCommandLineArgs().Length > 1)
            {
                try
                {
                    Console.WriteLine("start");
                    string fl = Environment.GetCommandLineArgs()[1];
                    Console.WriteLine($"file: {fl}");
                    var data = File.ReadAllText(fl);
                    Console.WriteLine($"data:");
                    Console.WriteLine(data);
                    var hd = new HtmlDocument();
                    hd.LoadHtml(data);
                    data = ExtractTextFromHtml(hd);
                    Console.WriteLine();
                    Console.WriteLine("data Text:");
                    Console.WriteLine(data);
                    File.WriteAllText(fl, data);
                    Console.WriteLine("Data write to file");
                    Console.WriteLine("finish");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error:");
                    Console.WriteLine(ex.Message);
                    File.WriteAllText("HTMLExtract.Error.txt", ex.ToString());
                }
            }
            else
            {
                Console.WriteLine("Unexpected parameters:");
                Console.WriteLine("Args:");
                foreach (var item in Environment.GetCommandLineArgs())
                {
                    Console.WriteLine(item);
                }
            }
        }

        static string ExtractTextFromHtml(HtmlDocument document)
        {
            // Select the body node and get its inner text
            var bodyNode = document.DocumentNode.SelectSingleNode("//body");

            if (bodyNode != null)
            {
                return bodyNode.InnerText.Trim(); // Return trimmed inner text
            }

            return string.Empty; // Return empty if body not found
        }
    }
}
