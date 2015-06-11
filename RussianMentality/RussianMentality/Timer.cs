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
    public class Timer : Microsoft.Xna.Framework.DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private Vector2 position;
        private Color color;
        private float seconds = 0;
        public float Seconds
        {
            get { return seconds; }
            set { seconds = value; }
        }
        private string time;
        public string Time
        {
            get { return time; }
            set { time = value; }
        }
        private SpriteFont font;
        private bool gameover = false;
        public bool Gameover
        {
            get { return gameover; }
            set { gameover = value; }
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



        public Timer(Game game, SpriteBatch spriteBatch,
            Vector2 position,
            string time,
            SpriteFont font,
            Color color)
            : base(game)
        {
            // TODO: Construct any child components here
            this.spriteBatch = spriteBatch;
            this.position = position;
            this.time = time;
            this.font = font;
            this.color = color;
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
            //time = gameTime.ElapsedGameTime.TotalMilliseconds.ToString();
            //time = gameTime.TotalGameTime.TotalMinutes.ToString("n2");

            if (!gameover)
            {
                seconds += (float)gameTime.ElapsedGameTime.TotalSeconds;
                time = seconds.ToString("0:00");
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.DrawString(font, time, position, color);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
