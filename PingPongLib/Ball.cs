using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PingPongLib
{
    public class Ball
    {
        public PointF Location { get; set; }
        public SizeF Size { get; set; }
        public float speed { get; set; }
        public float deltaX { get; set; }
        public float deltaY { get; set; }
        public bool OutOfBounds { get; set; } = false;

        public Ball(PointF location, SizeF size, float deltaX, float deltaY, float speed = 2)
        {
            this.Location = location;
            this.Size = size;
            this.deltaX = deltaX;
            this.deltaY = deltaY;
            this.speed = speed;
        }

        public Ball(float x, float y, float width, float height, float deltaX, float deltaY, float speed = 2)
        {
            this.Location = new PointF(x, y);
            this.Size = new SizeF(width, height);
            this.deltaX = deltaX;
            this.deltaY = deltaY;
            this.speed = speed;
        }

        public void MoveBall(PointF paddleLocation, SizeF paddleSize, Size screenSize)
        {
            if (Location.X <= 0 || Location.X >= screenSize.Width - Size.Width)
                deltaX = -deltaX;
            if (Location.Y <= 0 )
                deltaY = -deltaY;
            if (Location.Y >= screenSize.Height - Size.Height)
                OutOfBounds = true;
            if (Location.X >= paddleLocation.X && Location.X <= paddleLocation.X + paddleSize.Width 
                && Location.Y >= paddleLocation.Y - paddleSize.Height * 2)
            {
                deltaY = -deltaY;
                speed += 0.2f;
            }

            Location = new PointF(Location.X + deltaX * speed, Location.Y + deltaY * speed);
        }


        public RectangleF GetRectengle()
        {
            return new RectangleF(Location, Size);
        }

        public float GetUserFriendlySpeed()
        {
            return MathF.Round(speed, 1);
        }
    }
}
