using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Graph2D
{
    public partial class GraphControl : UserControl
    {
        private GraphDataCollection<long> dataCollection = new GraphDataCollection<long>();

        private GraphPrecisionTypes precisionType;
        public GraphPrecisionTypes PrecisionType
        {
            get => this.precisionType;
            set
            {
                if (value != this.precisionType)
                {
                    this.precisionType = value;
                    OnGraphDataChanged();
                }
            }
        }


        private int sampleCount;
        public int SampleCount
        {
            get => this.sampleCount;
            set
            {
                if (value != this.sampleCount)
                {
                    this.sampleCount = value;
                    OnGraphDataChanged();
                }
            }
        }

        private int levelCount;
        public int LevelCount
        {
            get => this.levelCount;
            set
            {
                if (value != this.levelCount)
                {
                    this.levelCount = value;
                    OnGraphDataChanged();
                }
            }
        }

        private int maxSampleValue;
        public int MaxSampleValue
        {
            get => this.maxSampleValue;
            set
            {
                if (value != this.maxSampleValue)
                {
                    this.maxSampleValue = value;
                    OnGraphDataChanged();
                }
            }
        }

        public void AddData(long value)
        {
            this.dataCollection.Add(value);

            OnGraphDataChanged();
        }

        public GraphControl()
        {
            InitializeComponent();
            precisionType = GraphPrecisionTypes.Second;
            sampleCount = 10;
            levelCount = 10;
            pnlCanvas.Paint += PnlCanvas_Paint;
        }

        private void PnlCanvas_Paint(object sender, PaintEventArgs e)
        {
            var data = TakeDataFromCollection();

            var levels = ComputeLevels(data);

            var drawAreaHeight = pnlCanvas.Height - (pnlCanvas.Padding.Top + pnlCanvas.Padding.Bottom);

            drawAreaHeight = (drawAreaHeight / levels.Count) * (levels.Count - 2);

            var drawAreaTop = pnlCanvas.Height / 2 - drawAreaHeight / 2;

            var drawArea =
                new Rectangle(
                    pnlCanvas.Padding.Left,
                    drawAreaTop,
                    pnlCanvas.Width - (pnlCanvas.Padding.Left + pnlCanvas.Padding.Right),
                    drawAreaHeight);

            DrawLevels(levels, drawArea, e.Graphics);

            DrawSamples(data, drawArea, e.Graphics);
        }

        private void DrawSamples(GraphDataCollection<long> samples, Rectangle drawArea, Graphics g)
        {
            var max = samples.GetMaxValue()?.Value ?? 0f;
            var points = new List<PointF>();

            if (max > 0)
            {
                var index = 0;
                var spaceBetweenEachSample = drawArea.Width / (sampleCount - 1);
                for (int i = sampleCount - samples.Count; i < sampleCount; i++)
                {
                    var sample = samples.Get(index++);
                    var x = spaceBetweenEachSample * i;
                    var y = drawArea.Height - (sample.Value / max * drawArea.Height);
                    points.Add(new PointF(x, y));
                    g.DrawEllipse(Pens.Black, x - 2, y - 2, 4, 4);
                }

                if (points.Count > 1)
                {
                    g.DrawLines(Pens.Gray, points.ToArray());
                }
            }
        }

        private void OnGraphDataChanged()
        {
            var rnd = new Random();
            if (this.IsHandleCreated && !this.IsDisposed)
            {
                if (DesignMode && dataCollection.Count == 0)
                {
                    for (int i = 0; i < sampleCount; i++)
                    {
                        dataCollection.Add(rnd.Next(0, 100));
                    }
                }
                this.pnlCanvas.Invalidate();
            }
        }

        private void DrawLevels(List<long> levels, Rectangle drawArea, Graphics g)
        {
            for (int i = 0; i < levels.Count; i++)
            {
                var spaceBetweenEachLevel = drawArea.Height / (levels.Count - 1);
                var x = drawArea.Left;
                var y = drawArea.Bottom - (i * spaceBetweenEachLevel);
                var text = levels[i].ToString();
                var sz = g.MeasureString(text, Font);
                g.DrawString(text, this.Font, Brushes.Black, x, y - sz.Height / 2);

                g.DrawLine(Pens.LightGray, drawArea.Left + sz.Width + 5, y, drawArea.Right, y);
            }
        }

        private List<long> ComputeLevels(GraphDataCollection<long> data)
        {
            var min = 0f; //data.GetMinValue()?.Value ?? 0f;
            var max = (float)(data.GetMaxValue()?.Value ?? levelCount);

            var threshold = Math.Floor(max / (levelCount - 1));

            var levels = new List<long>();

            for (int i = 0; i < levelCount; i++)
            {
                var l = i * threshold;
                levels.Add((long)l);
            }

            return levels;
        }

        private GraphDataCollection<long> TakeDataFromCollection()
        {
            switch (precisionType)
            {
                case GraphPrecisionTypes.Second:
                    return dataCollection.GetLastSecondData().TakeLast(sampleCount);

                case GraphPrecisionTypes.Minute:
                    return dataCollection.GetLastMinuteData().TakeLast(sampleCount);

                case GraphPrecisionTypes.Hour:
                    return dataCollection.GetLastHourData().TakeLast(sampleCount);

                case GraphPrecisionTypes.Day:
                    return dataCollection.GetLastDayData().TakeLast(sampleCount);

                case GraphPrecisionTypes.None:
                default:
                    throw new ArgumentException("Precition type was not set.");
            }
        }
    }

    public enum GraphPrecisionTypes
    {
        None,
        Second,
        Minute,
        Hour,
        Day,
    }
}
