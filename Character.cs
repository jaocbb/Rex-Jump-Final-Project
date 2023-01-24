using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
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
        Rectangle hitRect;
        bool hasJumped;
        float groundPose;

        public Character(Texture2D newTexture, Vector2 newPosition)
        {
            texture = newTexture;
            position = newPosition;
            bounds = new Rectangle((int)Math.Round(position.X), (int)Math.Round(position.Y), 150, 100);
            hitRect = new Rectangle(bounds.X + 60, bounds.Y + 30, 65, 60); 
            hasJumped = false;
            groundPose = 0;

        }
        public void update(GameTime gameTime)
        {
            position += velocity;//velocity changes so does screen
            
            if (!hasJumped)
            {
                groundPose = position.Y;
            }
            // Start Jump
            if (Keyboard.GetState().IsKeyDown(Keys.Space) && hasJumped == false)
            {
                position.Y -= 120f;//shoot up
                velocity.Y = -5f;
                hasJumped = true;
            }

            if (hasJumped == true && position.Y != groundPose)
            {
                float i = 1;
                velocity.Y += 0.15f * i;//fall animation
                position.Y += 0.2f;
            }


            if (position.Y >= groundPose)
            {
                hasJumped = false;
                velocity.Y = 0f;
                position.Y = groundPose;

            }


            if (!hasJumped)
            {
                velocity.Y = 0f;
            }
                



            bounds.Y = (int)Math.Round(position.Y);
            hitRect.Y = bounds.Y + 40;

        }

        public bool Intercects(Rectangle obstacle)
        {
            return hitRect.Intersects(obstacle);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, bounds, Color.White);
        }

        public Rectangle Bounds
        {
            get { return bounds; }
            set { bounds = value; }
        }

    }
}
