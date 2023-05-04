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

namespace My_Game
{
    internal class MiniGame
    {
        Texture2D rectangleblock;
        private Scale scale;
        private Scale lineScale;
        private Point position;
        private readonly Point size = new Point(140, 20);
        public MiniGame(Texture2D rectangleBlock)
        {
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
            position = new Point((int)fishPos.X, (int)fishPos.Y - 30);
            lineScale.UpdateScale(fishPos, time, size, 5);
            scale.UpdateScale(fishPos, time, size, 7);
        }
        
        
    }
}
