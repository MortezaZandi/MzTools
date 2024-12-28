using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

public class CodeInfo
{
    public string Name;
    public string Description;
    public string Pattern;
    public Color CodeColor;
    public Color ParamColor;
    private Regex regex;
    public Regex Regex
    {
        get
        {
            if (regex == null)
            {
                regex = new Regex(Pattern, RegexOptions.IgnoreCase)
                {
                };
            }
            return regex;
        }
    }
}

public class LineInfo
{
    public string lineText;
    public CodeInfo CodeInfo;
    public string CodeName;
    public string[] Parameters;
    public bool IsEmpty;

    public Match MatchResult { get; internal set; }

    public string Format()
    {
        switch (CodeName.ToUpper())
        {
            case "GOTO":
            case "EFON":
            case "EFFD":
                return $"\t{CodeName.ToUpper()} {string.Join(", ", Parameters)}";

            case "EFOF":
                return $"\t{CodeName.ToUpper()}";

            default:
                return lineText;
        }
    }
}

public class CodeFormatter
{
    private Dictionary<string, CodeInfo> CodeDefinitions = new Dictionary<string, CodeInfo>();
    private void Init()
    {
        CodeDefinitions["GOTO"] = new CodeInfo() { Name = "GOTO", CodeColor = Color.Blue, ParamColor = Color.Red, Description = "Go to specific position", Pattern = @"(GOTO)\s+([\d\s,\.]+)" };
        CodeDefinitions["EFON"] = new CodeInfo() { Name = "EFON", CodeColor = Color.Blue, ParamColor = Color.Red, Description = "Turn on end effector", Pattern = @"\b(EFON)\s*([\d\s,\.]+)" };
        CodeDefinitions["EFOF"] = new CodeInfo() { Name = "EFOF", CodeColor = Color.Blue, ParamColor = Color.Red, Description = "Turn off end effector", Pattern = @"\b(EFOF)" };
        CodeDefinitions["EFFD"] = new CodeInfo() { Name = "EFFD", CodeColor = Color.Blue, ParamColor = Color.Red, Description = "Feed end effector", Pattern = @"\b(EFFD)\s*([\d\s,\.]+)" };
        CodeDefinitions["EFRT"] = new CodeInfo() { Name = "EFRT", CodeColor = Color.Blue, ParamColor = Color.Red, Description = "Retract end effector", Pattern = @"\b(EFRT)" };
        CodeDefinitions["//"] = new CodeInfo() { Name = "//", CodeColor = Color.DarkSeaGreen, ParamColor = Color.Green, Description = "Comment", Pattern = @"(\/\/)(.+)" };
    }

    private readonly RichTextBox richTextBox;

    public CodeFormatter(RichTextBox richTextBox)
    {
        this.richTextBox = richTextBox;
        Init();
    }

    public void Format()
    {
        string[] lines = richTextBox.Lines;

        var pos = 0;

        richTextBox.Text = string.Empty;

        foreach (string line in lines)
        {
            var lineInfo = GetLineInfo(line);

            if (!lineInfo.IsEmpty)
            {
                var formatted = $"{lineInfo.Format()}{Environment.NewLine}";
                richTextBox.Text += formatted;

                pos += formatted.Length;
            }
            else
            {
                richTextBox.Text += Environment.NewLine;
                pos++;
            }
        }
    }


    private LineInfo GetLineInfo(string line)
    {
        LineInfo lineInfo = new LineInfo();
        if (!string.IsNullOrWhiteSpace(line))
        {
            foreach (var code in CodeDefinitions)
            {
                var matchResult = code.Value.Regex.Match(line);
                if (matchResult.Success)
                {
                    lineInfo.CodeInfo = code.Value;
                    lineInfo.CodeName = matchResult.Groups[1].Value;
                    lineInfo.lineText = line;
                    lineInfo.MatchResult = matchResult;

                    if (matchResult.Groups.Count > 2)
                    {
                        lineInfo.Parameters = matchResult.Groups[2].Value.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).Select(p => p.Trim()).ToArray();
                    }

                    break;
                }
            }

            if (lineInfo.IsEmpty)//failed to parse the code
            {
                lineInfo.lineText = line;
            }
        }
        else
        {
            lineInfo.IsEmpty = true;
        }

        return lineInfo;
    }

    //private void ApplyColors()
    //{
    //    //string[] lines = richTextBox.Lines;
    //    //int position = 0;

    //    //foreach (string line in lines)
    //    //{
    //    //    string trimmedLine = line.Trim();
    //    //    if (!string.IsNullOrWhiteSpace(trimmedLine))
    //    //    {
    //    //        if (trimmedLine.StartsWith("//"))
    //    //        {
    //    //            // Color the entire comment line
    //    //            richTextBox.SelectionStart = position;
    //    //            richTextBox.SelectionLength = line.Length;
    //    //            richTextBox.SelectionColor = Color.Green;
    //    //        }
    //    //        else
    //    //        {
    //    //            // Color the command
    //    //            string[] commandAndParams = trimmedLine.Split(new[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);
    //    //            string command = commandAndParams[0].Trim().ToUpper();
    //    //            if (commandDefinitions.TryGetValue(command, out var definition))
    //    //            {
    //    //                // Color the command blue
    //    //                richTextBox.SelectionStart = position + line.IndexOf(command);
    //    //                richTextBox.SelectionLength = command.Length;
    //    //                richTextBox.SelectionColor = definition.color;

    //    //                // Color numbers in parameters red
    //    //                if (commandAndParams.Length > 1)
    //    //                {
    //    //                    string parameters = commandAndParams[1];
    //    //                    int paramStart = position + line.IndexOf(parameters);

    //    //                    // Find and color all numbers in parameters
    //    //                    foreach (Match match in Regex.Matches(parameters, @"\b\d+(\.\d+)?\b"))
    //    //                    {
    //    //                        richTextBox.SelectionStart = paramStart + match.Index;
    //    //                        richTextBox.SelectionLength = match.Length;
    //    //                        richTextBox.SelectionColor = Color.Blue;
    //    //                    }
    //    //                }
    //    //            }
    //    //        }
    //    //    }
    //    //    position += line.Length + 1;
    //    //}
    //}
}
