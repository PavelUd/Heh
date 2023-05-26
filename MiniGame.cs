using Microsoft.VisualBasic.Logging;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.DirectoryServices.ActiveDirectory;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace My_Game
{
    internal class MiniGame
    {
        Texture2D rectangleblock;
        private Scale scale;
        private Scale lineScale;
        private Point position;
        private int Level;
        private int flag = 0;
        public int Flag
        {
            get { return flag; }
            set { flag = value < 0 || value > 2 ? throw new ArgumentOutOfRangeException("miniGmae flag must be between 0 and 2") : value; }
        }
        private readonly Point size = new Point(140, 20);
        public MiniGame(Texture2D rectangleBlock, int level)
        {
            Level = level;
            rectangleblock = rectangleBlock;
        }
        public void LoadScales(Vector2 fishPos)
        {

            position = new Point(0, 0);
            Point scaleSize = new Point(20, 20);
            Point scaleLineSize = new Point(2, 20);
            lineScale = new Scale(position, scaleLineSize);
            scale = new Scale(position, scaleSize);
        }
        public void DrawMiniGame(SpriteBatch spriteBatch)
        {
 
            Color color1 = Color.Red;
            Rectangle rectangle = new Rectangle(position, size);
            Color color = new Color(240, 240, 240); // color grey
            spriteBatch.Draw(rectangleblock, rectangle, color);
            spriteBatch.Draw(rectangleblock, scale.CreateScale(), color1);
            spriteBatch.Draw(rectangleblock, new Rectangle(lineScale.scalePos, lineScale.scaleSize), Color.Black);
        }
        public void UpdateScale(Vector2 fishPos, Stopwatch time)
        {
            var key = Keyboard.GetState();
            if (key.IsKeyDown(Keys.Space))
            {
                flag = 1;
                if (scale.scalePos.X < lineScale.scalePos.X && lineScale.scalePos.X < scale.scalePos.X + 20) {
                    flag = 2;
                }
            }
            position = new Point((int)fishPos.X, (int)fishPos.Y - 30);
            if (flag == 0) 
            {
                lineScale.UpdateScale(position.ToVector2(), time, size, Level + 1);
                scale.UpdateScale(position.ToVector2(), time, size, Level);
            }
        }
        
        
    }
}
