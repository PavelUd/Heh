using Microsoft.VisualBasic.Logging;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Mail;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;

namespace My_Game
{
    internal class Fish : Obj
    {
        public string Name;
        private Point Size;
        public MiniGame minigame;
        private Tuple<int, int> distance;
        private int animationSpeed;
        public int AnimationSpeed { 
            get { return animationSpeed; }
            set
            {
                animationSpeed = value > 0 ? value 
                    : throw new ArgumentOutOfRangeException("animationSpeed must be value > 0"); ;
            } 
        }

        public Tuple<int, int> Distance
        {
            get { return distance; }
            set
            {
                distance = (value.Item1 <= -1 && value.Item2 <= -1)
                    ? new Tuple<int, int>(0, 350)
                    : new Tuple<int, int>((value.Item1 < value.Item2) 
                    ? value.Item1 : value.Item2, value.Item2);
            }
        }
        Texture2D texture1;
        private int flag = 0;
        private double speed;
        public double Speed
        {
            get { return speed; }
            set { speed = value > -1 ? value 
                    : throw new ArgumentOutOfRangeException("Speed must be value >= 0"); ; }
        }
        public int Flag
        {
            get { return flag; }
            set
            {
                flag = (value >= 0 && value <= 5) ? value 
                    : throw new ArgumentOutOfRangeException("Flag must be a value between 0 and 5."); ;
            }
        }
        public Fish(string name, Vector2 pos, ContentManager Content, string otrName, MiniGame minigame, Point size, Tuple<int, int> dis, double  speed) : base(name, pos, Content)
        {
            Speed = speed;
            Name = name;
            Distance = dis;
            Size = size.X >= 0 && size.Y >= 0 ? size : new Point(1, 1);
            texture1 = Content.Load<Texture2D>(otrName);
            this.minigame = minigame;
            minigame.LoadScales(Position);
        }
        private void MoveEnd(Point bait)
        {
            if (flag == 4)
            {
                Position.Y = bait.Y + 20;
                Position.X = bait.X + 50;
            }
        }
        private bool isDrawEnd(Fishing_line fishing_line)
        {
            var sharkend = false;
            if (fishing_line.IsTabKeyPressed == 0 && flag == 4)
            {
                flag = 5;
                sharkend = true;
            }
            return sharkend; 
        }
        public void doMiniGame(Fishing_line fishing_line, Stopwatch stopwatch)
        {
            switch (flag)
            {
                case 3 when fishing_line.IsTabKeyPressed != 3:
                    minigame.UpdateScale(Position, stopwatch);
                    break;

                case 1 when minigame.Flag == 1:
                    flag = 0;
                    minigame.Flag = 0;
                    fishing_line.IsTabKeyPressed = 3;
                    break;

                case not 3 when minigame.Flag == 2:
                    flag = 4;
                    fishing_line.IsTabKeyPressed = 3;
                    break;
            }

            MoveEnd(fishing_line.Bait);
        }
        public void FishMove(Point bait)
        {
            switch (flag)
            {
                case 0:
                    if (Position.X < distance.Item2 + 1)
                        Position.X += (float)speed;
                    if (Position.X >= distance.Item2)
                        flag = 1;
                    break;
                case 1:
                    Position.X -= (float)speed;
                    if (Position.X < distance.Item1 + speed)
                        flag = 0;
                    if (Position.X == bait.X && Position.Y == bait.Y)
                        flag = 3;
                    break;
                case 3:
                    if (flag == 1)
                    {
                        if (Position.X == bait.X && Position.Y == bait.Y)
                            flag = 3;
                    }
                    else if (flag == 0)
                    {
                        if (Position.X + Size.X - 10 == bait.X && Position.Y == bait.Y)
                            flag = 3;
                    }
                    break;
                default:
                    break;
            }
        }
        private void DrawHelper(SpriteBatch spriteBatch, Texture2D texture, bool isCatch, float rec = 0)
        {
            var origin = new Vector2(0, 0);
            if (!isCatch)
            {
                spriteBatch.Draw(texture, new Rectangle((int)Position.X, (int)Position.Y, Size.X, Size.Y), Color.White);
            }
            if (isCatch)
            {
                spriteBatch.Draw(texture, new Rectangle((int)Position.X, (int)Position.Y, Size.X, Size.Y), null, Color.White, rec, origin, SpriteEffects.None, 0f);
            }
        }
        public void DrawFish(SpriteBatch spriteBatch, List<Texture2D> list = null, List<Texture2D> otrlist = null, Fishing_line fishing_line = null)
        {
            if (isDrawEnd(fishing_line))
            {
                minigame.Flag = 0;
                return;
            }

            var texture = list != null ? list[(int)Math.Floor(Position.X / animationSpeed) % list.Count] : texture1;
            var otrtexture = otrlist != null ? otrlist[(int)Math.Floor(Position.X / animationSpeed) % otrlist.Count] : Texture;

            switch (flag)
            {
                case 0:
                    DrawHelper(spriteBatch, texture, false);
                    Texture = texture;
                    break;
                case 1:
                    DrawHelper(spriteBatch, otrtexture, false);
                    Texture = otrtexture;
                    break;
                case 3:
                    minigame.DrawMiniGame(spriteBatch);
                    DrawHelper(spriteBatch, Texture, false);
                    break;
                case 4:
                    var rec = (float)(3.14 / 2);
                    DrawHelper(spriteBatch, otrtexture, true, rec);
                    break;
            }
        }
    }
}
