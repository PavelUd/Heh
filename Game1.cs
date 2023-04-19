using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;

namespace My_Game
{
    class Obj
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
    }
    class Boat : Obj
    {
        public Boat(string name, Vector2 pos, ContentManager Content)
         : base(name, pos, Content) { }
        public void boatMove()
        {
            var key = Keyboard.GetState();
            if (key.IsKeyDown(Keys.A) && Position.X > 0)
            {
                Position.X -= 1;
            }
            if (key.IsKeyDown(Keys.D) && Position.X < 650)
            {
                Position.X += 1;
            }
        }
    }
    class Fish : Obj
    {
        Point point;
        Texture2D texture1;
        int flag = 0;
        public List<Texture2D> AnimationSprites(int startAnimation, int endAnimation, ContentManager Content)
        {
            var listFrames = new List<Texture2D>();
            for (var frame = startAnimation; frame < endAnimation; frame++)
            {
                listFrames.Add(Content.Load<Texture2D>(frame.ToString()));
            }
            return listFrames;
        }
        public Fish(string name, Vector2 pos, ContentManager Content, string otrNmae) : base(name, pos, Content) 
        {

            point = new Point(1, 1);
            texture1 = Content.Load <Texture2D>(otrNmae);
        }
        public void FishMove()
        {
//flag == 0 рыба идет вперед
//flag == 1 рыба ращается
//flag == 3 рыба останавливается
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
            if (Position.X == 150)
            {
                flag = 3;
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
            }
            if (flag == 1)
            {
                DrawHelper(spriteBatch, otrtexture, false);
            }
            if (flag == 3)
            {
                DrawHelper(spriteBatch, Texture, true);
            }

        }
    }

    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Fish shark;
        private Boat boat;
        private Obj _background;
        private Obj _sky;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            shark = new Fish("shark", new Vector2(180, 400), Content, "shark2");
            boat = new Boat("m_boat", new Vector2(0, 280), Content);
            _sky = new Obj("m_SAc3k1", new Vector2(0, 0), Content);
            _background = new Obj("j", new Vector2(0, 50), Content);

        }

        protected override void Update(GameTime gameTime)
        {
            boat.boatMove();
            shark.FishMove();
            base.Update(gameTime);

        }

        protected override void Draw(GameTime gameTime)
        {
            var otrListShark = shark.AnimationSprites(10, 20, Content);
            var listShark = shark.AnimationSprites(0, 10, Content);
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
            _sky.Draw(_spriteBatch);
            _background.Draw(_spriteBatch);
             boat.Draw(_spriteBatch);
            shark.DrawFish(_spriteBatch, otrListShark, listShark, 5);
            _spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}