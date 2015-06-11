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
    public class About : Microsoft.Xna.Framework.DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private Texture2D background;
        private Vector2 bgPos;
        private Vector2 textPos;
        private string aboutText;

        public string AboutText
        {
            get { return aboutText; }
            set { aboutText = value; }
        }
        private SpriteFont textFont;
        string button = "Escape to menu";

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



        public About(Game game, SpriteBatch spriteBatch,
            Texture2D background,
            Vector2 bgPos,
            Vector2 textPos,
            SpriteFont textFont)
            : base(game)
        {
            // TODO: Construct any child components here

            this.spriteBatch = spriteBatch;
            this.background = background;
            this.textPos = textPos;
            this.bgPos = bgPos;
            this.aboutText = "by\nPhilippe Kornilov\nAnton Nepsha\n(c) 2014";
            this.textFont = textFont;

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

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            Rectangle r = new Rectangle((int)bgPos.X, (int)bgPos.Y, 500, 250);
            spriteBatch.Begin();
            spriteBatch.Draw(background, r, Color.White);
            spriteBatch.DrawString(textFont, aboutText, textPos, Color.White);
            spriteBatch.DrawString(textFont, button, new Vector2(320, 280), Color.Red);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
