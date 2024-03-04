using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Space_shooter
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Texture2D playerSprite;
        private Texture2D bullet;
        private Vector2 position = new Vector2(575, 666);
        private Vector2 bulletposition;
        private Boolean bulletChecker;
        private Rectangle bulletRectangle = new Rectangle(340, 330, 23, 88);
        private Rectangle playerRectangle = new Rectangle(340, 330, 124, 124);
        List<Enemy> enemyList = new(32);
        private SpriteFont file;
        private int score = 0;
        private int frames = 0;
        private Boolean xDirection = false;
        private Boolean yDirection = false;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.IsFullScreen = false;
            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 960;
            _graphics.ApplyChanges();
            base.Initialize();

        }

        protected override void LoadContent()
        {
            file = Content.Load<SpriteFont>("file");
            Debug.WriteLine("Loading content");
            for(int i = 0; i <4; i++)
            {
                Load2Enemys(i, 0, i);
            }
            for (int i = 4; i < 8; i++)
            {
                Load2Enemys(i, 150, i-4);
            }



            Debug.WriteLine("Loading player and bullet");
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            playerSprite = Content.Load<Texture2D>("main1");
            bullet = Content.Load<Texture2D>("bullet");
        }
        private void Load2Enemys(int index, int offsetY, int offsetX) 
        {
            for (int i = 0; i < 2; i++)
            {
                Debug.WriteLine("loading enemy");
                enemyList.Add(new Enemy());
           
                enemyList[index * 2 + i].enemyTexture = Content.Load<Texture2D>("enemy" + (i +1));
                enemyList[index * 2 + i].enemyRectangle = new Rectangle(100 *( offsetX * 2 + i)+ 400, 50+offsetY, 64, 64);


            }
        }
        private void MoveEnemys(int xSpeed, int ySpeed)
        {
            for (int i = 0; i < enemyList.Count; i++)
            {
                if (enemyList[i] != null)
                {
                    enemyList[i].enemyRectangle.X += xSpeed;
                    enemyList[i].enemyRectangle.Y += ySpeed;
                }
            }
        }

        protected override void Update(GameTime gameTime)
        {
            frames += 1;
            if(frames % 2==0)
            {
                if (xDirection)
                {
                    MoveEnemys(10, 0);
                }
                else
                {
                    MoveEnemys(-10,0);
                }
            }
            if(frames == 70)
            {
                frames = 0;
                xDirection = !xDirection;
            }
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
                bulletposition.Y -= 60;
            }
            
            if (bulletRectangle.Y <= -100) 
            {
                bulletChecker = false;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                position.Y -= 15;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                position.Y += 15;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                position.X += 15;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                position.X -= 15;
            }
            
            for (int i = 0; i < enemyList.Count; i++)
            {
                if (enemyList[i] != null)
                {
                    if (playerRectangle.Intersects(enemyList[i].enemyRectangle))
                    {
                        Exit();
                    }
                    if (bulletRectangle.Intersects(enemyList[i].enemyRectangle))
                    {
                        bulletChecker = false;
                        enemyList[i] = null;
                        for (int j = 0; j< enemyList.Count+1; j+=2) 
                        {
                            if (i == j)
                            {
                                score += 10;
                            }
                            if (i+1==j) 
                            {
                                score += 20;
                            }
                        }
                    }

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
                if (enemyList[i] != null)
                {
                    _spriteBatch.Draw(enemyList[i].enemyTexture, enemyList[i].enemyRectangle, Color.White);
                    Console.WriteLine("drawing enemy");
                }
            }
     
            if (bulletChecker)
            {   
                _spriteBatch.Draw(bullet,bulletposition, Color.White);
            }
            _spriteBatch.Draw(playerSprite, position , Color.White);




            _spriteBatch.DrawString(file, "Score: " + score, new Vector2(900, 800), Color.Black);


            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}