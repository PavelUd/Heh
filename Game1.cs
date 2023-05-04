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
    public class Game1 : Game
    {
        private Stopwatch _stopwatch = new Stopwatch();
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Fish shark;
        private Boat boat;
        private Obj _background;
        private Obj _sky;
        Texture2D rectangleBlock;
        Fishing_line fishing_line;
        MiniGame minigame;

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
            boat = new Boat("boat3", new Vector2(0, 200), Content);
            _sky = new Obj("m_SAc3k1", new Vector2(0, 0), Content);
            _background = new Obj("j", new Vector2(0, 50), Content);
            rectangleBlock = new Texture2D(GraphicsDevice, 1, 1);
            Microsoft.Xna.Framework.Color xnaColorBorder = new Color(128, 128, 128);
            minigame = new MiniGame(rectangleBlock);
            rectangleBlock.SetData(new[] { xnaColorBorder });
            fishing_line = new Fishing_line(new Point(156, 240));
        }

        protected override void Update(GameTime gameTime)
        {

            Start();
            var key = Keyboard.GetState();
            boat.boatMove(fishing_line);
            if (fishing_line.startPoint.X != 156 + boat.Position.X)
            {
                fishing_line = new Fishing_line(new Point(156 + (int)boat.Position.X, 240));
                Start();
            }
            fishing_line.IncreaseSizeIfTabKeyPressed(_stopwatch, boat.time);
            fishing_line.RotateIfSizeIsBigEnough(_stopwatch, boat.Position);
            shark.FishMove(fishing_line.Bait);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            var otrListShark = shark.AnimationSprites(10, 20, Content, "");
            var listShark = shark.AnimationSprites(0, 10, Content, "");
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
            _sky.Draw(_spriteBatch);
            _background.Draw(_spriteBatch);
             boat.boatDraw(_spriteBatch,_stopwatch, fishing_line.IsTabKeyPressed);
            shark.DrawFish(_spriteBatch, otrListShark, listShark, 5);
            var origin = new Vector2(0/ 2f, 0 / 2f);
            _spriteBatch.Draw(rectangleBlock, fishing_line.Create(), fishing_line.Create(), fishing_line.Color, fishing_line.rotation, origin, SpriteEffects.None, 0f);
            if (shark.flag == 3)
            {
                minigame.DrawMiniGame(_spriteBatch, shark.Position);
            }
            _spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
        public void Start()
        {
            _stopwatch.Start();
        }
    }
}