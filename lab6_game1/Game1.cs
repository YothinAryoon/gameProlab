using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
namespace lab6_game1
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D ballTexture;
        Texture2D charTexture;
        Vector2[] ballPosition = new Vector2[4];
        Vector2 charPosition = new Vector2(0, 250);
        int[] ballColor = new int[4];
        Random rand = new Random();

        bool personHit;

        int direction = 0;

        int frame;
        int totalFrame;
        int framePerSec;
        float timePerFrame;
        float totalElapsed;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
        protected override void Initialize()
        {
            base.Initialize();
        }
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            charTexture = Content.Load<Texture2D>("Char01");
            ballTexture = Content.Load<Texture2D>("ball");
            frame = 0;
            totalFrame = 4;
            framePerSec = 8;
            timePerFrame = (float)1 / framePerSec;
            totalElapsed = 0;

            for(int i = 0; i < 4; i++)
            {
                ballPosition[i].X = rand.Next(graphics.GraphicsDevice.Viewport.Width - ballTexture.Width / 6);
                ballPosition[i].Y = rand.Next(graphics.GraphicsDevice.Viewport.Height - ballTexture.Height);
                ballColor[i] = rand.Next(6);
            }
        }
        protected override void UnloadContent()
        {

        }
        protected override void Update(GameTime gameTime)
        {
            GraphicsDevice device = graphics.GraphicsDevice;
            
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            KeyboardState keyboard = Keyboard.GetState();

            if (keyboard.IsKeyDown(Keys.Left))
            {
                charPosition.X = charPosition.X - 2;
                direction = 1;
                UpdateFrame((float)gameTime.ElapsedGameTime.TotalSeconds);
            }
            if (keyboard.IsKeyDown(Keys.Right))
            {
                charPosition.X = charPosition.X + 2;
                direction = 2;
                UpdateFrame((float)gameTime.ElapsedGameTime.TotalSeconds);
            }
            if (keyboard.IsKeyDown(Keys.Up))
            {
                charPosition.Y = charPosition.Y - 2;
                direction = 3;
                UpdateFrame((float)gameTime.ElapsedGameTime.TotalSeconds);
            }
            if (keyboard.IsKeyDown(Keys.Down))
            {
                charPosition.Y = charPosition.Y + 2;
                direction = 0;
                UpdateFrame((float)gameTime.ElapsedGameTime.TotalSeconds);
            }

            Rectangle charRectangle = new Rectangle((int)charPosition.X, (int)charPosition.Y, 32, 48);

            for(int i =0; i<4; i++)
            {
                Rectangle blockRectangle = new Rectangle((int)ballPosition[i].X, (int)ballPosition[i].Y, 24, 24);

                if (charRectangle.Intersects(blockRectangle) == true)
                {
                    personHit = true;
                    ballPosition[i].X = rand.Next(graphics.GraphicsDevice.Viewport.Width - ballTexture.Width / 6);
                    ballPosition[i].Y = rand.Next(graphics.GraphicsDevice.Viewport.Height - ballTexture.Height);
                    ballColor[i] = rand.Next(6);
                    break;
                }
                else if (charRectangle.Intersects(blockRectangle) == false)
                {
                    personHit = false;
                }
            }
            

            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice device = graphics.GraphicsDevice;
            if (personHit == true)
            {
                device.Clear(Color.Red);
            }
            else
            {
                device.Clear(Color.CornflowerBlue);
            }
            spriteBatch.Begin();
            for(int i = 0; i < 4; i++)
            {
                spriteBatch.Draw(ballTexture, ballPosition[i], new Rectangle(24, 0, 24, 24), Color.White);
            }
            
            spriteBatch.Draw(charTexture, charPosition, new Rectangle(32*frame, 48*direction, 32, 48), Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        void UpdateFrame(float elapsed)
        {
            frame = (frame + 1) % totalFrame;
            totalElapsed -= timePerFrame;
        }
    }
}

