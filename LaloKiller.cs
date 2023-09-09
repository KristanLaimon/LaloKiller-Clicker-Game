using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Cyan
{
    public class LaloKiller : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Texture2D targetSprite, crosshairSprite, backgroundSprite;
        SpriteFont sf;

        Vector2 targetPosition = new Vector2(300, 300);
        const int targetradio = 60;
        MouseState mState;
        bool mRelease = true;
        int score = 0;
        Random rand = new Random();
        int windowWidth;
        int windowHeight;


        public LaloKiller()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            
        }

        protected override void Initialize()
        {
            //Add your initialization logic here
            _graphics.PreferredBackBufferWidth = 800;  // anchura
            _graphics.PreferredBackBufferHeight = 600; // altura
            _graphics.ApplyChanges();
            windowWidth = _graphics.PreferredBackBufferWidth;
            windowHeight = _graphics.PreferredBackBufferHeight;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            targetSprite = Content.Load<Texture2D>("target");
            crosshairSprite = Content.Load<Texture2D>("crosshairs");
            backgroundSprite = Content.Load<Texture2D>("sky");
            sf = Content.Load<SpriteFont>("galleryFont");

            // use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            mState = Mouse.GetState();
            

            if (mState.LeftButton == ButtonState.Pressed && mRelease == true)
            {
                
                float moustTargetDistance = Vector2.Distance(targetPosition, mState.Position.ToVector2());
                
                if (moustTargetDistance < targetradio)
                {
                    score++;
                    targetPosition.X = rand.Next(50, windowWidth-50);
                    targetPosition.Y = rand.Next(55, windowHeight - 55);
                }
                mRelease = false;
            }

            if (mState.LeftButton == ButtonState.Released) mRelease = true;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // Add your drawing code here

            _spriteBatch.Begin();
            Vector2 size = new Vector2(100, 110);
            _spriteBatch.Draw(backgroundSprite, new Vector2(0,0), Color.White) ;
            _spriteBatch.DrawString(sf, $"Puntuacion: {score}", new Vector2(20,20), Color.White);
            _spriteBatch.Draw(targetSprite, new Rectangle((int)targetPosition.X - targetradio,(int)targetPosition.Y - targetradio, (int)size.X, (int)size.Y), Color.White) ;
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}