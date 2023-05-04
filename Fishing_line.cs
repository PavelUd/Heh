using Microsoft.VisualBasic.Logging;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;

namespace My_Game
{
    internal class Fishing_line
    {
        public Point startPoint;
        public Point Bait;
        public Color Color;
        public Point size;
        public float rotation;
        public bool IsTabKeyPressed;
        public Fishing_line(Point start)
        {
            rotation = 6.28f;
            startPoint = start;
            size = new Point(1, 1);
            Color = Color.White;
            IsTabKeyPressed = false;
        }
        public Rectangle Create()
        {
            // size
            Rectangle rectangle = new Rectangle(startPoint, size);
            return rectangle;
        }
        public void MoveLine(bool flag, Stopwatch stopwatch, double boattime)
        {
            if (!flag && stopwatch.ElapsedMilliseconds % 5 == 0 && size.X > 1)
            {
                size.X -= 5;
                Color = Color.Black;
            }
            if (flag && (stopwatch.ElapsedMilliseconds % 10 == 0 && size.X < 200) && (boattime > 5 || boattime == 0))
            {
                size.X += 10;
                Color = Color.Black;
            }
        }
        public void IncreaseSizeIfTabKeyPressed(Stopwatch stopwatch, double boattime)
        {
            var key = Keyboard.GetState();
            if (key.IsKeyDown(Keys.Tab))
            {
                IsTabKeyPressed = true;
            }
            if (key.IsKeyDown(Keys.F))
            {
                IsTabKeyPressed = false;
            }
            MoveLine(IsTabKeyPressed, stopwatch, boattime);
        }
        public void RotateIfSizeIsBigEnough(Stopwatch stopwatch, Vector2 boatPosition)
        {
            if (stopwatch.ElapsedMilliseconds % 2 == 0 && rotation < 7.85f && size.X > 50)
            {
                rotation += 0.01f;
                Bait = new Point((int)Math.Floor(Math.Cos(rotation) * size.X) + 150 + (int)boatPosition.X, (int)Math.Floor(Math.Sin(rotation) * size.X) + (int)boatPosition.Y);

            }
        }
    }
}
