using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

public class LineNumberedRichTextBox : RichTextBox
{

    public LineNumberedRichTextBox()
    {
        this.HandleCreated += LineNumberedRichTextBox_HandleCreated;
        this.TextChanged += LineNumberedRichTextBox_TextChanged;
        SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
    }

    private void LineNumberedRichTextBox_TextChanged(object sender, EventArgs e)
    {
        UpdateLineNumbers();
    }

    private void LineNumberedRichTextBox_HandleCreated(object sender, EventArgs e)
    {
        this.SetInnerMargins(50, 0, 0, 0);
    }

    public void Format()
    {
    }

    private void UpdateLineNumbers()
    {
        using (var g = this.CreateGraphics())
        {
            // Fill the line number background
            g.FillRectangle(new SolidBrush(BackColor), 0, 0, 50, Height);

            // Get the first visible line
            int firstVisibleLine = GetLineFromCharIndex(GetCharIndexFromPosition(new Point(0, 0)));

            // Get total visible lines
            int lastVisibleLine = GetLineFromCharIndex(GetCharIndexFromPosition(new Point(0, ClientSize.Height)));

            // Draw line numbers
            for (int i = firstVisibleLine; i <= lastVisibleLine + 1; i++)
            {
                if (i >= Lines.Length) break;

                int lineY = GetPositionFromCharIndex(GetFirstCharIndexFromLine(i)).Y;
                g.DrawString(
                    (i + 1).ToString(),
                    this.Font,
                    new SolidBrush(Color.DarkGray),
                    new Point(1, lineY)
                );
            }
        }
    }

    private void LineNumberedRichTextBox_MouseDown(object sender, MouseEventArgs e)
    {
        // If clicked in the line number area, move caret to the beginning of that line
        if (e.X <= 50)
        {
            int charIndex = GetCharIndexFromPosition(new Point(50 + 10, e.Y));
            int lineIndex = GetLineFromCharIndex(charIndex);

            if (lineIndex >= 0 && lineIndex < Lines.Length)
            {
                int lineStartIndex = GetFirstCharIndexFromLine(lineIndex);
                SelectionStart = lineStartIndex;
                SelectionLength = 0;
            }

            // Prevent default handling
            return;
        }
    }

}