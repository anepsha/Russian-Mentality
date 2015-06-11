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
    public class Menu : Microsoft.Xna.Framework.DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private SpriteFont textFont;
        KeyboardState oldState = Keyboard.GetState();
        List<string> menuList = new List<string>();
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

        public Menu(Game game, SpriteBatch spriteBatch,
            SpriteFont textFont)
            : base(game)
        {
            // TODO: Construct any child components here
            this.spriteBatch = spriteBatch;
            this.textFont = textFont;

            menuList.Add("Play");
            menuList.Add("Highscores");
            menuList.Add("How to Play");
            menuList.Add("Help");
            menuList.Add("About");
            menuList.Add("Exit");
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
            if (ks.IsKeyDown(Keys.Down) && oldState.IsKeyUp(Keys.Down))
            {
                selectedIndex++;
                if (selectedIndex == menuList.Count)
                {
                    selectedIndex = 0;
                }
            }
            if (ks.IsKeyDown(Keys.Up) && oldState.IsKeyUp(Keys.Up))
            {
                selectedIndex--;
                if (selectedIndex == -1)
                {
                    selectedIndex = menuList.Count - 1;
                }

            }
            oldState = ks;

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            


            Vector2 tempPos = new Vector2(350, 100);
            Color tempColor = Color.Gray;

            for (int i = 0; i < menuList.Count; i++)
            {
                if (selectedIndex == i)
                    tempColor = Color.Red;
                else
                    tempColor = Color.Gray;

                tempPos.Y += textFont.LineSpacing + 5;
                if (i > 0)
                    tempPos.X += ((float)textFont.MeasureString(menuList[i - 1].ToString()).X -
                        (float)textFont.MeasureString(menuList[i].ToString()).X) / 2;

                spriteBatch.DrawString(textFont, menuList[i], tempPos, tempColor);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
