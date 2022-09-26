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
        Vector2[] ballPos = new Vector2[4];
        Vector2 charPos = new Vector2(0,250);
        int[] ballColor = new int[4];
        Random rand = new Random();
        bool personHit;
        int direction;
        int frame;
        int totalftame;
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
            totalftame = 4;
            timePerFrame = (float)1 / framePerSec;
            totalftame = 0;

            for(int i =0; i<4; i++)
            {
                ballPos[i].X = rand.Next(graphics.GraphicsDevice.Viewport.Width - ballTexture.Width / 6);
                ballPos[i].Y = rand.Next(graphics.GraphicsDevice.Viewport.Height - ballTexture.Height);
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
                charPos.X = charPos.X - 2;
                direction = 1;
                UpdateFrame((float)gameTime.ElapsedGameTime.TotalSeconds);
            }
            if (keyboard.IsKeyDown(Keys.Right))
            {
                charPos.X = charPos.X + 2;
                direction = 2;
                UpdateFrame((float)gameTime.ElapsedGameTime.TotalSeconds);
            }
            if (keyboard.IsKeyDown(Keys.Up))
            {
                charPos.X = charPos.X - 2;
                direction = 3;
                UpdateFrame((float)gameTime.ElapsedGameTime.TotalSeconds);
            }
            if (keyboard.IsKeyDown(Keys.Down))
            {
                charPos.X = charPos.X + 2;
                direction = 0;
                UpdateFrame((float)gameTime.ElapsedGameTime.TotalSeconds);
            }

            Rectangle charRectangle = new Rectangle((int)charPos.X, (int)charPos.Y, 32, 48);

            for(int i =0; i<4; i++)
            {
                Rectangle blockRectangle = new Rectangle((int)ballPos[i].X, (int)ballPos[i].Y, 24, 24);

                if (charRectangle.Intersects(blockRectangle) == true)
                {
                    personHit = true;
                    ballPos[i].X = rand.Next(graphics.GraphicsDevice.Viewport.Width - ballTexture.Width - ballTexture.Width / 6);
                    ballPos[i].Y = rand.Next(graphics.GraphicsDevice.Viewport.Height - ballTexture.Height);
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
            
            if(personHit == true)
            {
                device.Clear(Color.Red);
            }
            else
            {
                device.Clear(Color.CornflowerBlue);
            }
            spriteBatch.Begin();
            for(int i =0; i < 4; i++)
            {
                spriteBatch.Draw(ballTexture, ballPos[i], new Rectangle(24,0,24,24), Color.White);
            }

            spriteBatch.Draw(charTexture,charPos, new Rectangle(32 * frame, 48 * direction, 32, 48), Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        void UpdateFrame(float elapsed)
        {
            frame = (frame + 1) % totalftame;
            totalElapsed -= timePerFrame;
        }
        
    }
}

