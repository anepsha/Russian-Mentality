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
    public class Gameover : Microsoft.Xna.Framework.DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private Texture2D background;
        private Vector2 bgPos;
        private Vector2 textPos;
        private string textScore;
        public string TextScore
        {
            get { return textScore; }
            set { textScore = value; }
        }
        private SpriteFont textFont;
        private SpriteFont buttonFont;
        List<string> buttonList = new List<string>();
        int selectedIndex = 0;
        public int SelectedIndex
        {
            get { return selectedIndex; }
            set { selectedIndex = value; }
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
        KeyboardState oldState = Keyboard.GetState();


        public Gameover(Game game, SpriteBatch spriteBatch,
            Texture2D background,
            Vector2 bgPos,
            Vector2 textPos,
            string textScore,
            SpriteFont textFont,
            SpriteFont buttonFont)
            : base(game)
        {
            // TODO: Construct any child components here
            this.spriteBatch = spriteBatch;
            this.background = background;
            this.textPos = textPos;
            this.bgPos = bgPos;
            this.textScore = textScore;
            this.textFont = textFont;
            this.buttonFont = buttonFont;

            buttonList.Add("Play again");
            buttonList.Add("Escape to menu");
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
            if (ks.IsKeyDown(Keys.Left) && oldState.IsKeyUp(Keys.Left))
            {
                selectedIndex--;
                if (selectedIndex == -1)
                {
                    selectedIndex = 0;
                }
            }
            if (ks.IsKeyDown(Keys.Right) && oldState.IsKeyUp(Keys.Right))
            {
                selectedIndex++;
                if (selectedIndex == buttonList.Count)
                {
                    selectedIndex = 1;
                }
                
            }
            oldState = ks;

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            Rectangle r = new Rectangle((int)bgPos.X, (int)bgPos.Y, 500, 250);
            spriteBatch.Begin();
            spriteBatch.Draw(background, r, Color.White);
            spriteBatch.DrawString(textFont, textScore, textPos, Color.White);


            Vector2 tempPos = new Vector2(250, 300);

            for (int i = 0; i < buttonList.Count; i++)
            {
                if (selectedIndex == i)
                {
                    spriteBatch.DrawString(buttonFont, buttonList[i], tempPos, Color.Red);
                    tempPos.X += (float)textFont.MeasureString(buttonList[i].ToString()).X + 30;
                }
                else
                {
                    spriteBatch.DrawString(buttonFont, buttonList[i], tempPos, Color.Gray);
                    tempPos.X += (float)textFont.MeasureString(buttonList[i].ToString()).X + 30;
                }
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
