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
    internal class Boat : Obj
    {
        ContentManager contentManager;
        public bool flag = false;
        public double time;
        public Boat(string name, Vector2 pos, ContentManager Content)
         : base(name, pos, Content) { contentManager = Content; Position = pos; }
        public void boatMove(Fishing_line line)
        {
            var key = Keyboard.GetState();
            if (key.IsKeyDown(Keys.A) && Position.X > -10 && line.size.X < 2)
            {
                Position.X -= 1;
                if (time == 0)
                {
                    flag = false;
                }
            }
            if (key.IsKeyDown(Keys.D) && Position.X < 650 && line.size.X < 2)
            {
                Position.X += 1;
                if (time == 0)
                {
                    flag = false;
                }
            }
        }
        public void boatDraw(SpriteBatch spriteBatch, Stopwatch stopwatch, bool IsTabKeyPressed)
        {
            var listBoat = AnimationSprites(2, 9, contentManager, "boat");
            if (IsTabKeyPressed && !flag)
            {
                time += 0.1;
                if (time > 6)
                {
                    time = 0.1;
                    flag = true;
                }
                spriteBatch.Draw(listBoat[(int)Math.Floor(time)], Position, Color.White);
            }
            else { spriteBatch.Draw(listBoat[0], Position, Color.White); time = 0; }
        }
    }
}
