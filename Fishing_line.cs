﻿using Microsoft.VisualBasic.Logging;
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
        private int isTabKeyPressed;
        public int IsTabKeyPressed
        {
            get { return isTabKeyPressed; }
            set { isTabKeyPressed = value >= 0 && value <= 4 ? value : throw new ArgumentOutOfRangeException("Flag must be a value between 0 and 4."); ; }
        }
        public Fishing_line(Point start)
        {
            rotation = 6.28f;
            startPoint = start;
            size = new Point(1, 1);
            Color = Color.White;
            IsTabKeyPressed = 0;
        }
        public Rectangle Create()
        {
            // size
            Rectangle rectangle = new Rectangle(startPoint, size);
            return rectangle;
        }
        public void MoveLine(int flag, Stopwatch stopwatch, double boattime)
        {
            var key = Keyboard.GetState();
            if ((key.IsKeyDown(Keys.F) || flag == 3 || flag == 4) && stopwatch.ElapsedMilliseconds % 5 == 0 && size.X > 1)
            {
                size.X -= 5;
                Bait.Y -= 5;
                Color = Color.Black;
            }
            if (flag == 1 && (stopwatch.ElapsedMilliseconds % 10 == 0 && size.X < 200) && (boattime > 5 || boattime == 0))
            {
                size.X += 10;
                Color = Color.Black;
            }
        }
        public void IncreaseSizeIfTabKeyPressed(Stopwatch stopwatch, double boattime)
        {
            var key = Keyboard.GetState();
            if (key.IsKeyDown(Keys.Tab) && IsTabKeyPressed == 0)
            {
                isTabKeyPressed = 1;
            }
            if (key.IsKeyDown(Keys.F))
            {
                isTabKeyPressed = 2;
            }
            if ((isTabKeyPressed == 2 || isTabKeyPressed == 3) && size.X < 4)
            {
                isTabKeyPressed = 0;
            }
            MoveLine(isTabKeyPressed, stopwatch, boattime);
        }
        public void RotateIfSizeIsBigEnough(Stopwatch stopwatch, Vector2 boatPosition)
        {
            if (stopwatch.ElapsedMilliseconds % 2 == 0 && rotation < 7.85f && size.X > 50)
            {
                rotation += 0.01f;
                Bait = new Point((int)Math.Floor(Math.Cos(rotation) * size.X) + 150 + (int)boatPosition.X, (int)Math.Floor(Math.Sin(rotation) * size.X) + (int)boatPosition.Y);

            }
            if (size.X < 3)
            {
                rotation = 6.28f;
            }
        }
    }
}
