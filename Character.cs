using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Rex_Jump_Final_Project
{
    public class Character
    {
        Texture2D texture;
        Vector2 position;
        Vector2 velocity;
        Rectangle bounds;
        bool hasJumped;

        public Character(Texture2D newTexture, Vector2 newPosition)
        {
            texture = newTexture;
            position = newPosition;
            bounds = new Rectangle((int)Math.Round(position.X), (int)Math.Round(position.Y), 150, 100);
            hasJumped = true;

        }
        public void update(GameTime gameTime)
        {
            position += velocity;//velocity changes so does screen

            // Start Jump
            if (Keyboard.GetState().IsKeyDown(Keys.Space) && hasJumped == false)
            {
                position.Y -= 15f;//shoot up
                velocity.Y = -5f;
                hasJumped = true;
            }

            if (hasJumped == true)
            {
                float i = 1;
                velocity.Y += 0.15f * i;//fall animation
            }

            if (position.Y + texture.Height >= 450)
                hasJumped = false;

            if (hasJumped == false)
                velocity.Y = 0f;


            bounds.Y = (int)Math.Round(position.Y);

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, bounds, Color.White);
        }
    }
}
