using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace Paint
{
    public class Line
    {
        public PointF start { get; set; }
        public PointF end { get; set; }
        public Pen pen { get; set; }
    }
}
