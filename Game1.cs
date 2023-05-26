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
using System.Windows.Forms;

namespace My_Game
{
    public class Game1 : Game
    {
        private Stopwatch _stopwatch = new Stopwatch();
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Fish shark;
        private Fish fish;
        private Fish qu;
        private Boat boat;
        private Obj _background;
        private Obj _sky;
        Texture2D rectangleBlock;
        Fishing_line fishing_line;

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
            boat = new Boat("boat3", new Vector2(0, 200), Content);
            _sky = new Obj("m_SAc3k1", new Vector2(0, 0), Content);
            _background = new Obj("j", new Vector2(0, 50), Content);
            rectangleBlock = new Texture2D(GraphicsDevice, 1, 1);
            Microsoft.Xna.Framework.Color xnaColorBorder = new Color(128, 128, 128);
            rectangleBlock.SetData(new[] { xnaColorBorder });
            shark = new Fish("shark", new Vector2(180, 400), Content, "shark2", new MiniGame(rectangleBlock, 4), new Point(140, 60), new Tuple<int, int>(0, 650), 1);
            fish = new Fish("fish", new Vector2(60, 300), Content, "fish6", new MiniGame(rectangleBlock, 1), new Point(60, 60), new Tuple<int, int>(60, 400), 2);
            qu = new Fish("qu", new Vector2(60, 350), Content, "qu6", new MiniGame(rectangleBlock, 2), new Point(70, 70), new Tuple<int, int>(401, 650), 3);
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
            Fish[] fishes = { qu, fish, shark };
            foreach (Fish fish in fishes)
            {
                PerformFishActions(fish, fishing_line, _stopwatch);
            }

            base.Update(gameTime);
        }

        private void PerformFishActions(Fish fish, Fishing_line fishing_line, Stopwatch stopwatch)
        {
            fish.FishMove(fishing_line.Bait);
            fish.doMiniGame(fishing_line, stopwatch);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
            _sky.Draw(_spriteBatch);
            _background.Draw(_spriteBatch);
            boat.boatDraw(_spriteBatch, _stopwatch, fishing_line.IsTabKeyPressed);

            DrawFish(shark, 0, 10, 5);
            DrawFish(fish, 0, 5, 5);
            DrawFish(qu, 0, 5, 15);

            var origin = new Vector2(0 / 2f, 0 / 2f);
            _spriteBatch.Draw(rectangleBlock, fishing_line.Create(), fishing_line.Create(), fishing_line.Color, fishing_line.rotation, origin, SpriteEffects.None, 0f);

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        private void DrawFish(Fish fish, int startFrame, int endFrame, int animationspeed)
        {
            var name = fish.Name != "shark" ? fish.Name: "";
            var list = fish.AnimationSprites(startFrame, endFrame, Content, name);
            var otrlist = fish.AnimationSprites(endFrame + 1, endFrame + (endFrame - startFrame), Content, name);
            fish.AnimationSpeed = animationspeed;
            fish.DrawFish(_spriteBatch, otrlist, list, fishing_line);
        }
        public void Start()
        {
            _stopwatch.Start();
        }
    }
}