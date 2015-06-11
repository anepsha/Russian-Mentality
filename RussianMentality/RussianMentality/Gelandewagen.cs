using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace RussianMentality
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class Gelandewagen : Microsoft.Xna.Framework.DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private Texture2D tex;
        private Vector2 position;
        private Vector2 initPosition;
        private Vector2 speed;
        private bool jumped = false;
        private const int X_POS = 40;
        private const int Y_POS = 370;
        private bool crashed = true;
        public bool Crashed
        {
            get { return crashed; }
            set { crashed = value; }
        }
        public virtual void show()
        {
            this.Enabled = true;
            this.Visible = true;
        }
        public virtual void hide()
        {
            this.Enabled = false;
            this.Visible = false;
        }


        public Gelandewagen(Game game,
            SpriteBatch spriteBatch,
            Texture2D tex)
            : base(game)
        {
            // TODO: Construct any child components here
            this.spriteBatch = spriteBatch;
            this.tex = tex;
            initPosition = new Vector2(X_POS, Y_POS);
            position = initPosition;
            speed = new Vector2(0, 0);
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here

            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here

            KeyboardState ks = Keyboard.GetState();
            position += speed;

            
                if (ks.IsKeyDown(Keys.Space) && !jumped && !crashed) 
                {
                    position.Y -= 10f;
                    speed.Y = -5f;
                    jumped = true;
                }
                if (jumped)
                {
                    speed.Y += 0.10F;
                }
                if (position.Y >= Y_POS)
                {
                    jumped = false;
                }
                if (!jumped)
                {
                    speed.Y = 0f;
                }
            
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(tex, position, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        public Rectangle getBounds()
        {
            return new Rectangle((int)position.X, (int)position.Y,
                tex.Width, tex.Height);
        }
    }
}
