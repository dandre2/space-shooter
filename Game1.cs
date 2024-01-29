using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.ComponentModel.Design;

namespace Space_shooter
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        
        private Texture2D playerSprite;
        private Texture2D playerSpritefirstenemy;
        private Texture2D bullet;
        private Vector2 position = new Vector2(340, 330);
        private Rectangle enemyRectangle = new Rectangle(340, 0, 64, 64);
        private Rectangle playerRectangle = new Rectangle(340,330,124,124);
        private Boolean bulletChecker;
        private Vector2 bulletposition;
        private Boolean FalsebulletChecker = false;
        private Rectangle bulletRectangle = new Rectangle(340, 330, 23, 88);
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            playerSprite = Content.Load<Texture2D>("main1"); 
            playerSpritefirstenemy = Content.Load<Texture2D>("enemy1");
            bullet = Content.Load<Texture2D>("bullet");
        }

        protected override void Update(GameTime gameTime)
        {
            playerRectangle= new Rectangle((int) position.X,(int)position.Y, 124, 124);
            if (position.X > this.GraphicsDevice.Viewport.Width)
                position.X = 0;
            bulletRectangle= new Rectangle((int) bulletposition.X,(int)bulletposition.Y, 23, 88);
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                if (!bulletChecker)
                {
                    bulletChecker = true;
                    bulletposition = position;
                    bulletposition.X += 50;
                }
            }
            if (bulletChecker)
            {
                bulletposition.Y -= 18;
            }
            if (bulletRectangle.Intersects(enemyRectangle))
            {
                bulletChecker = false;
            }
            if (bulletRectangle.Y <= -100) 
            {
                bulletChecker = false;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                position.Y -= 5000000000000;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                position.Y += 5000000000000000000;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                position.X += 5000000000;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                position.X -= 500000000000;
            }
            if (playerRectangle.Intersects(enemyRectangle))
            {
                Exit();
            }
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
            _spriteBatch.Draw(playerSpritefirstenemy, enemyRectangle, Color.White);
            
            if (bulletChecker)
            {   
                _spriteBatch.Draw(bullet,bulletposition, Color.White);
            }

            _spriteBatch.Draw(playerSprite, position , Color.White);
            _spriteBatch.End();




            base.Draw(gameTime);
        }
    }
}