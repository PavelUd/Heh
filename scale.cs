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
    internal class Scale
    {
        private bool flag = false;
        public Point scaleSize;
        public Point scalePos;
        public Scale(Point pos, Point scaleSize) {
            this.scaleSize = scaleSize;
            scalePos = pos;
            var scale = new Rectangle(scalePos, this.scaleSize);
        }
        public Rectangle CreateScale () {
            var scale = new Rectangle(scalePos, this.scaleSize);
            return  scale;
        }
        public void UpdateScale(Vector2 fishPos, Stopwatch time, Point size, int speed)
        {
            var position = new Point((int)fishPos.X, (int)fishPos.Y);
            var IskeyPressed = false;
            var key = Keyboard.GetState();

            if (scalePos.X == 0)
            {
                scalePos = position;
            }

            switch (flag)
            {
                case false when time.ElapsedMilliseconds % 2 == 0:
                    scalePos.X += speed;
                    if (scalePos.X > size.X + position.X - scaleSize.X)
                    {
                        scalePos.X = size.X + position.X - scaleSize.X;
                        flag = true;
                    }
                    break;

                case true when time.ElapsedMilliseconds % 2 == 0:
                    scalePos.X -= speed;
                    if (scalePos.X < position.X)
                    {
                        scalePos.X = position.X;
                        flag = false;
                    }
                    break;
            }
        }
    }
}
