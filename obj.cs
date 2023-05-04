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
    internal class Obj
    {
        public Texture2D Texture;
        public Vector2 Position;
        public Obj(string name, Vector2 pos, ContentManager Content)
        {
            Texture = Content.Load<Texture2D>(name);
            Position = pos;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Color.White);

        }
        public List<Texture2D> AnimationSprites(int startAnimation, int endAnimation, ContentManager Content, string name)
        {
            var listFrames = new List<Texture2D>();
            for (var frame = startAnimation; frame < endAnimation; frame++)
            {
                var texture = name != "" ? name + frame.ToString() : frame.ToString();
                listFrames.Add(Content.Load<Texture2D>(texture));
            }
                return listFrames;
        }
    }
}
