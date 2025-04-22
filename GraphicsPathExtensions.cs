using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace FinalProject
{
    public static class GraphicsPathExtensions
    {
        public static void AddRoundedRectangle(this GraphicsPath path, Rectangle rect, int cornerRadius)
        {
            // If the corner radius is less than or equal to zero, 
            // just add a normal rectangle
            if (cornerRadius <= 0)
            {
                path.AddRectangle(rect);
                return;
            }

            // Make sure the corner radius isn't too large
            int diameter = cornerRadius * 2;
            Size size = new Size(diameter, diameter);
            Rectangle arc = new Rectangle(rect.Location, size);

            // Top left corner
            path.AddArc(arc, 180, 90);

            // Top right corner
            arc.X = rect.Right - diameter;
            path.AddArc(arc, 270, 90);

            // Bottom right corner
            arc.Y = rect.Bottom - diameter;
            path.AddArc(arc, 0, 90);

            // Bottom left corner
            arc.X = rect.Left;
            path.AddArc(arc, 90, 90);

            // Close the path
            path.CloseFigure();
        }
    }
}
