using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Reflection.PortableExecutable;

namespace Rex_Jump_Final_Project
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Texture2D dinoTexture, introTexture, gameTexture,rockTexture,controlTexture,endTexture;
        Rectangle introRect,gameRect,rockRect,controlRect,endRect;
        Vector2 rockSpeed;
        KeyboardState KeyboardState, prevKeyboardState, kstate;
        SpriteFont introFont,gameFont,controlFont,endFont,timerFont;
        Screen screen;
        SoundEffect introMusic;
        float seconds, startTime;

        Character dino;
        bool musicPlaying;
       
        enum Screen
        {
            Intro,
            Controls,
            Game,
            End,
        }
        
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            screen = Screen.Intro;
            this.Window.Title = "MonoGame Final Project";
            _graphics.PreferredBackBufferHeight = 500;
            _graphics.PreferredBackBufferWidth = 800;
            //dinoRect = new Rectangle(20, 315, 150, 50);
            rockRect = new Rectangle(500, 315, 100, 50);
            rockSpeed = new Vector2(5, 5);
            introRect = new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight + 20);
            controlRect = new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight + 20);
            gameRect = new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight + 20);
            endRect = new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight + 20);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            dinoTexture = Content.Load<Texture2D>("Dino final project");
            rockTexture = Content.Load<Texture2D>("Newrock");
            introFont = Content.Load<SpriteFont>("IntroFont");
            gameFont = Content.Load<SpriteFont>("File");
            controlFont = Content.Load<SpriteFont>("Controls");
            endFont = Content.Load<SpriteFont>("endFont");
            introTexture = Content.Load<Texture2D>("dinoIntro");
            gameTexture = Content.Load<Texture2D>("GameBackground");
            controlTexture = Content.Load<Texture2D>("controlScreen");
            rockSpeed = new Vector2(-5, -5);
            dino = new Character(Content.Load<Texture2D>("Dino final project"), new Vector2(100, 270));
            //rock = new Character(Content.Load<Texture2D>("Newrock"), new Vector2(100, 270));
            introMusic = Content.Load<SoundEffect>("introMusic");
            endTexture= Content.Load<Texture2D>("endScreen");
            timerFont = Content.Load<SpriteFont>("timerFont");

        }


        protected override void Update(GameTime gameTime)
        {
            seconds = (float)gameTime.TotalGameTime.TotalSeconds - startTime;
            dino.update(gameTime);
            prevKeyboardState = KeyboardState;
            KeyboardState = Keyboard.GetState();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            //seconds = (float)gameTime.TotalGameTime.TotalSeconds - startTime;
            //if (seconds > 10) // Takes a timestamp every 10 seconds.
                //startTime = (float)gameTime.TotalGameTime.TotalSeconds;



            if (screen == Screen.Intro)
            {
                if (musicPlaying == false)
                {
                    introMusic.Play();
                     musicPlaying = true;
                }
                if (KeyboardState.IsKeyDown(Keys.Enter))
                {
                    screen = Screen.Controls;
                    rockRect.X += (int)rockSpeed.X;
                    introMusic.Play();
                    

                }
               
                
            }
            else if (screen == Screen.Controls)
            {
                if (KeyboardState.IsKeyDown(Keys.Enter) && prevKeyboardState.IsKeyUp(Keys.Enter))
                {
                    screen = Screen.Game;
                    introMusic.Dispose();
                }
            }
            else if (screen == Screen.Game)
            {
                dino.update(gameTime);
                rockRect.X += (int)rockSpeed.X;//run right

                if (dino.Intercects(rockRect))
                {
                    screen = Screen.End;
                }
                    

                if (KeyboardState.IsKeyDown(Keys.Escape))
                {
                    Exit();
                }

            }
            if (rockRect.Right < 0)
            {
                rockRect.X = _graphics.PreferredBackBufferWidth;
            }
            if (KeyboardState.IsKeyDown(Keys.R))
            {
                screen = Screen.Game;
                seconds = 1;

            }
            



        }
        
        


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
            _spriteBatch.Begin();
            if (screen == Screen.Intro)
            {
                _spriteBatch.Draw(introTexture, introRect, Color.White);
                _spriteBatch.DrawString(introFont, "Click Enter to veiw controls", new Vector2(200, 400), Color.Black);
                _spriteBatch.DrawString(introFont, "Rex Jump", new Vector2(100, 30), Color.Black);
            }
            else if (screen == Screen.Controls)
            {
                _spriteBatch.Draw(controlTexture, controlRect, Color.White);
                _spriteBatch.DrawString(controlFont, "Controls", new Vector2(350, 50), Color.Black);
                _spriteBatch.DrawString(controlFont, "Key\r\nSpace\r\nDown Arrow\r\nEscape\r\nF12\r\nF8\r\n", new Vector2(200, 100), Color.Black);
                _spriteBatch.DrawString(controlFont, "Action\r\nJump/Start Game\r\nDuck\r\nExit Game\r\nToggle Window Size\r\nClear High Score", new Vector2(400, 100), Color.Black);
                _spriteBatch.DrawString(controlFont, "Click enter to play", new Vector2(500, 400), Color.Black);
            }
            else if (screen == Screen.Game)
            {
                _spriteBatch.Draw(gameTexture, gameRect, Color.White);
                //_spriteBatch.Draw(dinoTexture, dinoRect, Color.White);
                dino.Draw(_spriteBatch);
                _spriteBatch.Draw(rockTexture, rockRect, Color.White);
                _spriteBatch.DrawString(timerFont, (0 + seconds).ToString("00:00"), new Vector2(100, 100), Color.Black);
                if (dino.Intercects(rockRect))
                {
                    _spriteBatch.Draw(rockTexture, rockRect, Color.White);
                }
            }
            else if (screen == Screen.End)
            {
                _spriteBatch.Draw(endTexture, endRect, Color.White);
                _spriteBatch.DrawString(endFont, "You died ", new Vector2(350, 50), Color.White);
                _spriteBatch.DrawString(endFont, "Press R to respawn", new Vector2(100, 100), Color.White);
                _spriteBatch.DrawString(endFont, "You lasted" + (seconds), new Vector2(400, 100), Color.White);
            }

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}