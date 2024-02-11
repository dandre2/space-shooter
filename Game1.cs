using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Linq;

namespace Space_shooter
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Texture2D playerSprite;
        private Texture2D bullet;
        private Vector2 position = new Vector2(340, 330);
        private Vector2 bulletposition;
        private Boolean bulletChecker;
        private Rectangle bulletRectangle = new Rectangle(340, 330, 23, 88);
        private Rectangle playerRectangle = new Rectangle(340, 330, 124, 124);
        List<Enemy> enemyList = new(8);
        
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
            Debug.WriteLine("Loading content");
            for(int i = 0; i < 2; i++)
            {
                LoadEnemys(i);
            }
            
            
                Debug.WriteLine("Loading player and bullet");
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            playerSprite = Content.Load<Texture2D>("main1");
            bullet = Content.Load<Texture2D>("bullet");
        }
        private void LoadEnemys(int index) 
        {
            for (int i = 0; i < 2; i++)
            {
                Debug.WriteLine("loading enemy");
                enemyList.Add(new Enemy());
                Debug.WriteLine("enemy" + (index * 2 + i));
                enemyList[index * 2 + i].enemyTexture = Content.Load<Texture2D>("enemy" + (i +1));
                enemyList[index * 2 +i].enemyRectangle = new Rectangle(85 * (index *2 + i), 0, 64, 64);


            }
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
                bulletposition.Y -= 36;
            }
            
            if (bulletRectangle.Y <= -100) 
            {
                bulletChecker = false;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                position.Y -= 5;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                position.Y += 5;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                position.X += 5;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                position.X -= 5;
            }
            for (int i = 0; i < enemyList.Count; i++)
            {
                if (playerRectangle.Intersects(enemyList[i].enemyRectangle))
                {
                    Exit();
                }
                if (bulletRectangle.Intersects(enemyList[i].enemyRectangle))
                {
                    bulletChecker = false;
                }

           
            }
            
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            base.Update(gameTime);
             }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
            for (int i = 0; i < enemyList.Count; i++)
            {
                _spriteBatch.Draw(enemyList[i].enemyTexture, enemyList[i].enemyRectangle, Color.White);
                Console.WriteLine("drawing enemy");
            }
     
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