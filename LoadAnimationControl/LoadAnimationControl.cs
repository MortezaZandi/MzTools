using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;

namespace howto_point_segment_distance
{
    public class LoadAnimationControl : GraphicControl
    {
        private Timer tmrUpdate;
        private bool showDetails;
        private bool leftIsBigger = true;
        private BallPressAnimationRectangle animation;
        private Color ballColor = Color.FromArgb(8, 84, 161);
        private Brush ballBrush = new SolidBrush(Color.FromArgb(8, 84, 161));

        public LoadAnimationControl() : base()
        {
            animation = new BallPressAnimationRectangle(this.DisplayRectangle, 3);
            tmrUpdate = new Timer()
            {
                Interval = 10,
                Enabled = false,
            };

            tmrUpdate.Tick += TmrUpdate_Tick;
        }

        public int BallCount
        {
            get
            {
                return animation.BallCount;
            }
            set
            {

                animation.BallCount = value;
                ApplyChanges();
            }
        }

        public Color BallColor
        {
            get
            {
                return this.ballColor;
            }
            set
            {
                this.ballColor = value;
                this.ballBrush = new SolidBrush(this.ballColor);
                ApplyChanges();
            }
        }

        private void ApplyChanges()
        {
            try
            {
                RebuildUI();
                RefreshUI();
            }
            catch (Exception)
            {
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            animation.Update(new PointF(0, 0), new PointF(Width, 0), new PointF(0, Height), new PointF(Width, Height));
        }

        private void TmrUpdate_Tick(object sender, EventArgs e)
        {
            bool changed = false;
            int resizeAmount = 1;
            if (leftIsBigger)
            {
                changed = animation.BiggerRight(resizeAmount);
                changed |= animation.SmallerLeft(resizeAmount);

                if (!changed)
                {
                    leftIsBigger = false;
                }
            }
            else
            {
                changed = animation.BiggerLeft(resizeAmount);
                changed |= animation.SmallerRight(resizeAmount);
                if (!changed)
                {
                    leftIsBigger = true;
                }
            }

            if (changed)
            {
                RebuildUI();
            }
            else
            {
                RefreshUI();
            }
        }

        public bool ShowDetails
        {
            get { return this.showDetails; }
            set
            {
                this.showDetails = value;
                ApplyChanges();
            }
        }

        protected override void DrawGraphics(Graphics gr, Rectangle rectangle)
        {
            foreach (var ball in animation.CalculateBallRects())
            {
                gr.FillEllipse(this.ballBrush, ball);
            }
            if (ShowDetails)
            {
                foreach (var p in animation.TopLine.getPoints(animation.BallCount * 2))
                {
                    gr.FillEllipse(Brushes.Red, p.X - 2, p.Y - 2, 4, 4);
                }

                foreach (var p in animation.BottomLine.getPoints(animation.BallCount * 2))
                {
                    gr.FillEllipse(Brushes.Red, p.X - 2, p.Y - 2, 4, 4);
                }

                gr.DrawLine(Pens.Black, this.animation.TopLine.Start, this.animation.TopLine.End);
                gr.DrawLine(Pens.Black, this.animation.BottomLine.Start, this.animation.BottomLine.End);
            }

            //e.Graphics.DrawRectangle(Pens.Red, animation.Bounds.X, animation.Bounds.Y, animation.Bounds.Width, animation.Bounds.Height);
        }

        public void Play()
        {
            tmrUpdate.Start();
        }

        public void Pause()
        {
            tmrUpdate.Stop();
        }
    }

    public class BallPressAnimationRectangle : AnimationRectangle
    {
        private int ballCount;

        public BallPressAnimationRectangle(Rectangle displayRectangle, int ballCount) : base(displayRectangle)
        {
            this.ballCount = ballCount;
        }

        public int BallCount { get => ballCount; set => ballCount = value; }

        public RectangleF[] CalculateBallRects()
        {
            var result = new List<RectangleF>();
            var topPoints = TopLine.getPoints(ballCount * 2 + 1);
            var bottomPoints = BottomLine.getPoints(ballCount * 2 + 1);
            var minPointCount = Math.Min(topPoints.Length, bottomPoints.Length);
            for (int i = 1; i < minPointCount - 1; i += 2)
            {
                var middleTopPoint = topPoints[i];
                var middleBottomPoint = bottomPoints[i];

                float h = middleBottomPoint.Y - middleTopPoint.Y;
                float w = h;
                float x = middleTopPoint.X - w / 2;
                float y = middleTopPoint.Y;
                var rect = new RectangleF(x, y, w, h);

                result.Add(rect);
            }

            return result.ToArray();
        }

        internal bool IsValid()
        {
            return TopLine.IsValid() && BottomLine.IsValid();
        }
    }

    public class AnimationRectangle : IRegularRectangle
    {
        public AnimationRectangle(Rectangle displayRectangle)
        {
            Update(
                displayRectangle.Location,
                new PointF(displayRectangle.Right, displayRectangle.Top),
                new PointF(displayRectangle.Left, displayRectangle.Bottom),
                new PointF(displayRectangle.Right, displayRectangle.Bottom));
        }

        public RectangleF Bounds { get; set; }

        public override void Update(PointF pointF1, PointF pointF2, PointF pointF3, PointF pointF4)
        {
            base.Update(pointF1, pointF2, pointF3, pointF4);
            Bounds = CreateRectangleFromPoints(pointF1, pointF2, pointF3, pointF4);
        }

        public RectangleF CreateRectangleFromPoints(PointF pointF1, PointF pointF2, PointF pointF3, PointF pointF4)
        {
            var smallestX = Math.Min(Math.Min(pointF1.X, pointF2.X), Math.Min(pointF3.X, pointF4.X));
            var biggestX = Math.Max(Math.Max(pointF1.X, pointF2.X), Math.Max(pointF3.X, pointF4.X));
            var smallestY = Math.Min(Math.Min(pointF1.Y, pointF2.Y), Math.Min(pointF3.Y, pointF4.Y));
            var biggestY = Math.Max(Math.Max(pointF1.Y, pointF2.Y), Math.Max(pointF3.Y, pointF4.Y));
            return new RectangleF(smallestX, smallestY, biggestX - smallestX, biggestY - smallestY);
        }

        public bool SmallerLeft(float amount = 1)
        {
            var leftHeight = BottomLine.Start.Y - TopLine.Start.Y;

            if ((leftHeight - amount) >= 0)
            {
                var newLeftHeight = leftHeight - amount;
                var diff = leftHeight - newLeftHeight;
                var diff2 = diff / 2;

                var newLeftTopPoint = new PointF(TopLine.Start.X, TopLine.Start.Y + diff2);
                TopLine.Update(newLeftTopPoint, TopLine.End);

                var newLeftBottomPoint = new PointF(BottomLine.Start.X, BottomLine.Start.Y - diff2);
                BottomLine.Update(newLeftBottomPoint, BottomLine.End);

                return true;
            }

            return false;
        }

        public bool BiggerLeft(float amount = 1)
        {
            var leftHeight = BottomLine.Start.Y - TopLine.Start.Y;

            if ((leftHeight + amount) <= Bounds.Height)
            {
                var newLeftHeight = leftHeight + amount;
                var diff = newLeftHeight - leftHeight;
                var diff2 = diff / 2;

                var newLeftTopPoint = new PointF(TopLine.Start.X, TopLine.Start.Y - diff2);
                TopLine.Update(newLeftTopPoint, TopLine.End);

                var newLeftBottomPoint = new PointF(BottomLine.Start.X, BottomLine.Start.Y + diff2);
                BottomLine.Update(newLeftBottomPoint, BottomLine.End);

                return true;
            }

            return false;
        }

        public bool SmallerRight(float amount = 1)
        {
            var rightHeight = BottomLine.End.Y - TopLine.End.Y;

            if ((rightHeight - amount) >= 0)
            {
                var newRightHeight = rightHeight - amount;
                var diff = rightHeight - newRightHeight;
                var diff2 = diff / 2;

                var newRightTopPoint = new PointF(TopLine.End.X, TopLine.End.Y + diff2);
                TopLine.Update(TopLine.Start, newRightTopPoint);

                var newRightBottomPoint = new PointF(BottomLine.End.X, BottomLine.End.Y - diff2);
                BottomLine.Update(BottomLine.Start, newRightBottomPoint);

                return true;
            }

            return false;
        }

        public bool BiggerRight(float amount = 1)
        {
            var rightHeight = BottomLine.End.Y - TopLine.End.Y;

            if ((rightHeight + amount) <= Bounds.Height)
            {
                var newrightHeight = rightHeight + amount;
                var diff = newrightHeight - rightHeight;
                var diff2 = diff / 2;

                var newrightTopPoint = new PointF(TopLine.End.X, TopLine.End.Y - diff2);
                TopLine.Update(TopLine.Start, newrightTopPoint);

                var newrightBottomPoint = new PointF(BottomLine.End.X, BottomLine.End.Y + diff2);
                BottomLine.Update(BottomLine.Start, newrightBottomPoint);

                return true;
            }

            return false;
        }

        public PointF[] GetPoints()
        {
            var list = new List<PointF>();
            list.Add(TopLine.Start);
            list.Add(TopLine.End);
            list.Add(BottomLine.Start);
            list.Add(BottomLine.End);
            return list.ToArray();
        }
    }

    public class IRegularRectangle
    {
        public Line TopLine { get; set; }
        public Line BottomLine { get; set; }

        public IRegularRectangle()
        {
            TopLine = new Line();
            BottomLine = new Line();
        }

        public void AddPoint(PointF point)
        {
            if (!TopLine.IsValid())
            {
                TopLine.AddPoint(point);
            }
            else if (!BottomLine.IsValid())
            {
                BottomLine.AddPoint(point);
            }
        }

        public void ClearPoints()
        {
            TopLine.Clear();
            BottomLine.Clear();
        }

        public virtual void Update(PointF pointF1, PointF pointF2, PointF pointF3, PointF pointF4)
        {
            TopLine.Update(pointF1, pointF2);
            BottomLine.Update(pointF3, pointF4);
        }
    }

    public class Line
    {
        public Line(PointF start, PointF end)
        {
            Start = start;
            End = end;
        }

        public Line()
        {
            Start = PointF.Empty; End = PointF.Empty;
        }

        public void Update(PointF start, PointF end)
        {
            Start = start;
            End = end;
        }

        public PointF Start { get; set; }
        public PointF End { get; set; }

        public PointF GetPointOnLine(int x, int y)
        {
            PointF pt = new PointF(x, y);
            PointF p1 = Start;
            PointF p2 = End;
            PointF closest = new PointF();

            float dx = p2.X - p1.X;
            float dy = p2.Y - p1.Y;
            if ((dx == 0) && (dy == 0))
            {
                // It's a point not a line segment.
                closest = p1;
                dx = pt.X - p1.X;
                dy = pt.Y - p1.Y;
                return Start;
            }

            // Calculate the t that minimizes the distance.
            float t = ((pt.X - p1.X) * dx + (pt.Y - p1.Y) * dy) / (dx * dx + dy * dy);

            // See if this represents one of the segment's
            // end points or a point in the middle.
            if (t < 0)
            {
                closest = new PointF(p1.X, p1.Y);
                dx = pt.X - p1.X;
                dy = pt.Y - p1.Y;
            }
            else if (t > 1)
            {
                closest = new PointF(p2.X, p2.Y);
                dx = pt.X - p2.X;
                dy = pt.Y - p2.Y;
            }
            else
            {
                closest = new PointF(p1.X + t * dx, p1.Y + t * dy);
                dx = pt.X - closest.X;
                dy = pt.Y - closest.Y;
            }

            return closest;
        }

        internal bool IsValid()
        {
            return (Start != PointF.Empty && End != PointF.Empty);
        }

        internal void AddPoint(PointF location)
        {
            if (Start == PointF.Empty)
            {
                Start = location;
            }
            else if (End == PointF.Empty)
            {
                End = location;
            }
        }

        public void Clear()
        {
            Start = End = PointF.Empty;
        }

        public PointF[] getPoints(int quantity)
        {
            var p1 = Start;
            var p2 = End;

            var points = new PointF[quantity];
            var ydiff = p2.Y - p1.Y;
            var xdiff = p2.X - p1.X;
            double slope = (double)(p2.Y - p1.Y) / (p2.X - p1.X);
            double x, y;

            --quantity;

            for (double i = 0; i < quantity; i++)
            {
                y = slope == 0 ? 0 : ydiff * (i / quantity);
                x = slope == 0 ? xdiff * (i / quantity) : y / slope;
                points[(int)i] = new PointF((float)Math.Round(x) + p1.X, (float)Math.Round(y) + p1.Y);
            }

            points[quantity] = p2;
            return points;
        }
    }
}

