using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Rex_Jump_Final_Project
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Texture2D dinoTexture, introTexture,gameTexture;
        Rectangle dinoRect, introRect,gameRect;
        Vector2 dinoSpeed;
        KeyboardState KeyboardState;
        SpriteFont introFont,gameFont;
        Screen screen;
        
        bool musicPlaying;

        enum Screen
        {
            Intro,
            Game,
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
            dinoRect = new Rectangle(40, 370, 150, 50);
            dinoSpeed = new Vector2(3, 3);
            introRect = new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight + 20);
            gameRect = new Rectangle(0, 0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight + 20);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            dinoTexture = Content.Load<Texture2D>("Dino final project");
            introFont = Content.Load<SpriteFont>("IntroFont");
            gameFont = Content.Load<SpriteFont>("File");
            introTexture = Content.Load<Texture2D>("dinoIntro");
            gameTexture = Content.Load<Texture2D>("GameBackground");
        }


        protected override void Update(GameTime gameTime)
        {
            KeyboardState = Keyboard.GetState();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (screen == Screen.Intro)
            {
                if (musicPlaying == false)
                {
                    //introMusic.Play();
                   // musicPlaying = true;
                }
                if (KeyboardState.IsKeyDown(Keys.Enter))
                {
                    screen = Screen.Game;
                    //.Play();
                    //.Play();
                }
            }



            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
            _spriteBatch.Begin();
            if (screen == Screen.Intro)
            {
                _spriteBatch.Draw(introTexture, introRect, Color.White);
                _spriteBatch.DrawString(introFont, "Click Enter to begin", new Vector2(200, 400), Color.Black);
                _spriteBatch.DrawString(introFont, "Rex Jump", new Vector2(100, 30), Color.Black);
            }
            else if (screen == Screen.Game)
            {
                _spriteBatch.Draw(gameTexture, gameRect, Color.White);
            }


                _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}