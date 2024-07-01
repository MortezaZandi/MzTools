using OLTMockServer.DataStructures;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telerik.WinControls.UI;

namespace OLTMockServer.UI
{
    public class CustomGridView : RadGridView
    {
        private object data;
        private ConcurrentDictionary<GridViewRowInfo, RowHighlightInfo> tempHighlightedRows;
        private Timer tmrHighlighter;

        public CustomGridView() : base()
        {
            this.tempHighlightedRows = new ConcurrentDictionary<GridViewRowInfo, RowHighlightInfo>();
            this.tmrHighlighter = new Timer();
            this.tmrHighlighter.Interval = 1000;
            this.tmrHighlighter.Tick += TmrHighlighter_Tick;
        }

        private void TmrHighlighter_Tick(object sender, EventArgs e)
        {
            var expiredRows = this.tempHighlightedRows.Where(t => t.Value.IsExpired).Select(t => t.Value);

            foreach (var expiredRow in expiredRows)
            {
                ResetRowColor(expiredRow.Row);

                this.tempHighlightedRows.TryRemove(expiredRow.Row, out var row);
            }
        }

        public new object DataSource
        {
            get
            {
                return base.DataSource;
            }
            set
            {
                base.DataSource = value;

                this.data = value;
            }
        }

        public void ResetDataSource()
        {
            base.DataSource = null;
            this.DataSource = this.data;
        }

        public T GetSelectedRowObject<T>()
        {
            if (this.SelectedRows.Count > 0)
            {
                return (T)this.SelectedRows[0].DataBoundItem;
            }
            else
            {
                return default(T);
            }
        }

        public void ResetRowColor(GridViewRowInfo row)
        {
            foreach (GridViewCellInfo cell in row.Cells)
            {
                cell.Style.CustomizeFill = false;
                cell.Style.BackColor = this.DefaultCellBackColor;
                cell.Style.ForeColor = this.DefaultCellForeColor;
            }
        }

        public void SetRowColor(GridViewRowInfo row, Color? backColor, Color? foreColor)
        {
            foreach (GridViewCellInfo cell in row.Cells)
            {
                if (backColor.HasValue)
                {
                    cell.Style.CustomizeFill = true;
                    cell.Style.BackColor = backColor.Value;
                }
                else
                {
                    cell.Style.CustomizeFill = false;
                }

                if (foreColor.HasValue)
                {
                    cell.Style.ForeColor = foreColor.Value;
                }
            }
        }

        public void TempHighlightRow(GridViewRowInfo row, TimeSpan? hold = null, Color? bColor = null, Color? fColor = null)
        {
            if (!hold.HasValue)
                hold = TimeSpan.FromSeconds(3);

            if (!bColor.HasValue)
                bColor = this.DefaultCellBackColor;

            if (!fColor.HasValue)
                fColor = this.DefaultCellForeColor;

            if (!this.tempHighlightedRows.ContainsKey(row))
            {
                var hrinfo = new RowHighlightInfo()
                {
                    Row = row,
                    Hold = hold.Value,
                    StartTime = DateTime.Now,
                };

                this.tempHighlightedRows[row] = hrinfo;

                SetRowColor(row, bColor, fColor);

                if (!this.tmrHighlighter.Enabled)
                {
                    this.tmrHighlighter.Start();
                }
            }
        }

        private Color DefaultCellBackColor
        {
            get
            {
                return Color.White;
            }
        }

        private Color DefaultCellForeColor
        {
            get
            {
                return Color.Black;
            }
        }

        public bool HasSelection
        {
            get
            {
                return this.SelectedRows.Count > 0;
            }
        }
    }


    public class RowHighlightInfo
    {
        public GridViewRowInfo Row { get; set; }

        public TimeSpan Hold { get; set; }

        public DateTime StartTime { get; set; }

        public bool IsExpired
        {
            get
            {
                return (Hold < (DateTime.Now - StartTime));
            }
        }
    }
}
