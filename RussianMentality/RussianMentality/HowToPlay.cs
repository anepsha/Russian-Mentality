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
    public class HowToPlay : Microsoft.Xna.Framework.DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private Texture2D background;
        private Vector2 bgPos;
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



        public HowToPlay(Game game, SpriteBatch spriteBatch,
            Texture2D background,
            Vector2 bgPos,
            SpriteFont textFont)
            : base(game)
        {
            // TODO: Construct any child components here

            this.spriteBatch = spriteBatch;
            this.background = background;
            this.bgPos = bgPos;
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
            Rectangle r = new Rectangle((int)bgPos.X, (int)bgPos.Y, 600, 395);
            spriteBatch.Begin();
            spriteBatch.Draw(background, r, Color.White);
            spriteBatch.DrawString(textFont, button, new Vector2(280, 365), Color.Red);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
