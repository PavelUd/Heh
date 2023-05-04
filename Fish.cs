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
    internal class Fish : Obj
    {
        Point point;
        Texture2D texture1;
        public int flag = 0;
        public Fish(string name, Vector2 pos, ContentManager Content, string otrNmae) : base(name, pos, Content)
        {

            point = new Point(1, 1);
            texture1 = Content.Load<Texture2D>(otrNmae);
        }
        public void FishMove(Point bait)
        {
            //IsTabKeyPressed == 0 рыба идет вперед
            //IsTabKeyPressed == 1 рыба возвращается
            //IsTabKeyPressed == 3 рыба останавливается
            if (Position.X < 651 && flag == 0)
                Position.X += 1;
            if (Position.X == 650)
            {
                flag = 1;
            }
            if (flag == 1)
            {
                Position.X -= 1;
                if (Position.X == 1) { flag = 0; }
            }
            if (flag == 1)
            {
                if (Position.X == bait.X && Position.Y == bait.Y)
                {
                    flag = 3;
                }
            }
            else if (flag == 0) {
                if (Position.X + 120 == bait.X && Position.Y == bait.Y)
                {
                    flag = 3;
                }
            }
        }
        public void DrawHelper(SpriteBatch spriteBatch, Texture2D texture, bool isCatch)
        {
            var origin = new Vector2(0, 0);
            if (!isCatch)
            {
                spriteBatch.Draw(texture, new Rectangle((int)Position.X, (int)Position.Y, 140, 60), Color.White);
            }
            if (isCatch)
            {
                spriteBatch.Draw(texture, new Rectangle((int)Position.X, (int)Position.Y, 140, 60), null, Color.White, 0f, origin, SpriteEffects.None, 0f);
            }
        }
        public void DrawFish(SpriteBatch spriteBatch, List<Texture2D> list = null, List<Texture2D> otrlist = null, int animationSpeed = 1)
        {
            var texture = list != null ? list[(int)Math.Floor(Position.X / animationSpeed) % list.Count] : texture1;
            var otrtexture = otrlist != null ? otrlist[(int)Math.Floor(Position.X / animationSpeed) % otrlist.Count] : Texture;
            if (flag == 0)
            {
                DrawHelper(spriteBatch, texture, false);
                Texture = texture;
            }
            if (flag == 1)
            {
                DrawHelper(spriteBatch, otrtexture, false);
                Texture = otrtexture;
            }
            if (flag == 3)
            {
                DrawHelper(spriteBatch, Texture, true);
            }

        }
    }
}
