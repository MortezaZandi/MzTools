using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caroom1
{
    public static class Extensions
    {
        //public static RectangleF Inflate(this RectangleF rect, float size)
        //{
        //    return rect.Inflate(size);
        //}

        public static RectangleF Inflate(this PointF p, float size)
        {
            var r = new RectangleF(p.X, p.Y, 1, 1);
            r.Inflate(size, size);
            return r;
        }

    }
}
