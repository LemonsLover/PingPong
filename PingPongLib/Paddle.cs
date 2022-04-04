using System;
using System.Drawing;

namespace PingPongLib
{
    public class Paddle
    {
        public PointF Location { get; set; }
        public SizeF Size { get; set; }

        public Paddle(PointF location, SizeF size)
        {
            this.Location = location;
            this.Size = size;
        }

        public Paddle(float x, float y, float width, float height)
        {
            this.Location = new PointF(x, y);
            this.Size = new SizeF(width, height);
        }

        public RectangleF getRectengle()
        {
            return new RectangleF(Location, Size);
        }

        public void MoveX(float DeltaX)
        {
            this.Location = new PointF(this.Location.X + DeltaX, this.Location.Y);
        }
    }
}
