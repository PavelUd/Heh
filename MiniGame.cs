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
    internal class MiniGame
    {
        Texture2D rectangleblock;
        public MiniGame(Texture2D rectangleBlock)
        {
            rectangleblock = rectangleBlock;
        }
        public void DrawMiniGame(SpriteBatch spriteBatch, Vector2 fishPos)
        {
            Point position = new Point((int)fishPos.X, (int)fishPos.Y - 30); // position
            Point size = new Point(140, 20); // size
            Rectangle rectangle = new Rectangle(position, size);
            Color color = new Color(225, 0, 240); // color yellow
            spriteBatch.Draw(rectangleblock, rectangle, color);
        }
    }
}
