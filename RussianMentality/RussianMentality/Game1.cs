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
using System.IO;
using System.Text.RegularExpressions;

namespace RussianMentality
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private Gelandewagen gelandewagen;
        private Texture2D evening;
        private CollisionManager cm;
        Traffic traffic;
        Explosion explosion;
        Timer timer;
        Gameover gameover;
        Menu menu;
        About about;
        Help help;
        HowToPlay howToPlay;
        Highscores highscores;

        Background bldg1;
        Background bldg2;
        Background road;

        Vector2 roadSpeed = new Vector2(-5F, 0);
        Vector2 bldg1Speed = new Vector2(-1.5F, 0);
        Vector2 bldg2Speed = new Vector2(-1F, 0);
        Vector2 trafficSpeed = new Vector2(-4.5F, 0);
        Vector2 trafficPosition;

        List<Texture2D> trafficTextures;

        Texture2D trafficTex;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            
            base.Initialize();
            showMenu();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            trafficTextures = new List<Texture2D>();
            trafficTextures.Add(Content.Load<Texture2D>("images/musora"));
            trafficTextures.Add(Content.Load<Texture2D>("images/lada2105"));
            trafficTextures.Add(Content.Load<Texture2D>("images/kopeidon_b"));
            trafficTextures.Add(Content.Load<Texture2D>("images/corolla"));
            trafficTextures.Add(Content.Load<Texture2D>("images/lada2108"));
            trafficTextures.Add(Content.Load<Texture2D>("images/niva"));


            Texture2D bldgTex = Content.Load<Texture2D>("images/background");
            Rectangle bldgRect = new Rectangle(0, 780, 1024, 244);

            Vector2 pos1 = new Vector2(0, graphics.PreferredBackBufferHeight
                - bldgRect.Height - 94);
            bldg1 = new Background(this, spriteBatch, bldgTex,
                bldgRect, pos1, bldg1Speed);

            Vector2 pos2 = new Vector2(0, graphics.PreferredBackBufferHeight
                - bldgRect.Height - 144);
            bldg2 = new Background(this, spriteBatch, bldgTex,
                bldgRect, pos2, bldg2Speed);


            Texture2D roadTex = Content.Load<Texture2D>("images/road");
            Rectangle roadRect = new Rectangle(0, 0, 1024, 118);

            Vector2 posRoad = new Vector2(0, graphics.PreferredBackBufferHeight
                - roadRect.Height);
            road = new Background(this, spriteBatch, roadTex,
                roadRect, posRoad, roadSpeed);


            gelandewagen = new Gelandewagen(this, spriteBatch,
                Content.Load<Texture2D>("images/gelandewagen"));


            evening = Content.Load<Texture2D>("images/evening");


            Random r = new Random();
            trafficTex = trafficTextures[r.Next(0, trafficTextures.Count)];
            Rectangle trafficRect = new Rectangle(0, 0, trafficTextures[5].Width, trafficTextures[5].Height);

            trafficPosition = new Vector2(graphics.PreferredBackBufferWidth, 382);
            traffic = new Traffic(this, spriteBatch, trafficTex,
                trafficRect, trafficPosition, Vector2.Zero);


            Texture2D tex = Content.Load<Texture2D>("images/explosion");
            Vector2 position = new Vector2(300, 200);
            int delay = 1;
            explosion = new Explosion(this, spriteBatch, tex, position, delay);


            SpriteFont spriteFont = Content.Load<SpriteFont>("SpriteFont1");
            Vector2 timerPos = new Vector2(0, 0);
            string time = "";
            timer = new Timer(this, spriteBatch, timerPos, time, spriteFont, Color.White);


            Texture2D bgTex = Content.Load<Texture2D>("images/bg");
            gameover = new Gameover(this, spriteBatch, bgTex, new Vector2(graphics.PreferredBackBufferWidth / 2 - bgTex.Width / 2,
                graphics.PreferredBackBufferHeight / 2 - bgTex.Height / 2), new Vector2(280, 130), "", spriteFont, spriteFont);
            gameover.hide();


            menu = new Menu(this, spriteBatch, spriteFont);
            menu.show();


            about = new About(this, spriteBatch, bgTex, new Vector2(graphics.PreferredBackBufferWidth / 2 - bgTex.Width / 2,
                graphics.PreferredBackBufferHeight / 2 - bgTex.Height / 2), new Vector2(280, 130), spriteFont);
            about.hide();


            help = new Help(this, spriteBatch, bgTex, new Vector2(graphics.PreferredBackBufferWidth / 2 - bgTex.Width / 2,
                graphics.PreferredBackBufferHeight / 2 - bgTex.Height / 2), new Vector2(200, 120), spriteFont);
            help.hide();


            Texture2D howToTex = Content.Load<Texture2D>("images/howtoplay");
            howToPlay = new HowToPlay(this, spriteBatch, howToTex, new Vector2(100, 30), spriteFont);
            howToPlay.hide();



            highscores = new Highscores(this, spriteBatch, bgTex, new Vector2(graphics.PreferredBackBufferWidth / 2 - bgTex.Width / 2,
                graphics.PreferredBackBufferHeight / 2 - bgTex.Height / 2), new Vector2(280, 130), "", spriteFont);
            highscores.hide();




            cm = new CollisionManager(this, gelandewagen, traffic, bldg1, bldg2, road, explosion, timer, gameover);


            this.Components.Add(timer);
            this.Components.Add(bldg2);
            this.Components.Add(bldg1);
            this.Components.Add(road);
            this.Components.Add(traffic);
            this.Components.Add(gelandewagen);
            this.Components.Add(cm);
            this.Components.Add(explosion);
            this.Components.Add(gameover);
            this.Components.Add(menu);
            this.Components.Add(about);
            this.Components.Add(help);
            this.Components.Add(howToPlay);
            this.Components.Add(highscores);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        private void showMenu()
        {
            menu.show();
            gameover.hide();
            about.hide();
            help.hide();
            howToPlay.hide();
            highscores.hide();

            traffic.Speed = Vector2.Zero;
            traffic.Position = trafficPosition;

            road.Speed = roadSpeed;
            bldg1.Speed = bldg1Speed;
            bldg2.Speed = bldg2Speed;

            timer.Gameover = true;
            timer.hide();
            timer.Seconds = 0;

            gelandewagen.Crashed = true;
            gelandewagen.hide();
        }

        private void showAbout()
        {
            menu.hide();
            gameover.hide();
            about.show();
            help.hide();
            howToPlay.hide();
            highscores.hide();

            traffic.Speed = Vector2.Zero;
            traffic.Position = trafficPosition;

            road.Speed = roadSpeed;
            bldg1.Speed = bldg1Speed;
            bldg2.Speed = bldg2Speed;

            timer.Gameover = true;
            timer.hide();
            timer.Seconds = 0;

            gelandewagen.Crashed = true;
            gelandewagen.hide();
        }

        private void showHighscores()
        {
            highscores.ScoresText = "";

            menu.hide();
            gameover.hide();
            about.hide();
            help.hide();
            howToPlay.hide();
            highscores.show();

            traffic.Speed = Vector2.Zero;
            traffic.Position = trafficPosition;

            road.Speed = roadSpeed;
            bldg1.Speed = bldg1Speed;
            bldg2.Speed = bldg2Speed;

            timer.Gameover = true;
            timer.hide();
            timer.Seconds = 0;

            gelandewagen.Crashed = true;
            gelandewagen.hide();

            if (!File.Exists("scores.txt"))
            {
                highscores.ScoresText = "No scores yet!";
            }
            else
            {
                StreamReader reader = new StreamReader("scores.txt");
                string readerInput = reader.ReadToEnd().Trim();
                string[] scoresArray = Regex.Split(readerInput, "[\r\n]+");
                Array.Sort(scoresArray);
                Array.Reverse(scoresArray);

                int top;
                if (scoresArray.Count() > 5)
                {
                    top = 5;
                }
                else
                {
                    top = scoresArray.Count();
                }

                for (int i = 0; i < top; i++)
                {
                    highscores.ScoresText += i + 1 + ". " + scoresArray[i].ToString() + "\n";
                }
                reader.Dispose();
            }
        }

        private void showHelp()
        {
            menu.hide();
            gameover.hide();
            about.hide();
            help.show();
            howToPlay.hide();
            highscores.hide();

            traffic.Speed = Vector2.Zero;
            traffic.Position = trafficPosition;

            road.Speed = roadSpeed;
            bldg1.Speed = bldg1Speed;
            bldg2.Speed = bldg2Speed;

            timer.Gameover = true;
            timer.hide();
            timer.Seconds = 0;

            gelandewagen.Crashed = true;
            gelandewagen.hide();
        }

        private void showHowTo()
        {
            menu.hide();
            gameover.hide();
            about.hide();
            help.hide();
            howToPlay.show();
            highscores.hide();

            traffic.Speed = Vector2.Zero;
            traffic.Position = trafficPosition;

            road.Speed = roadSpeed;
            bldg1.Speed = bldg1Speed;
            bldg2.Speed = bldg2Speed;

            timer.Gameover = true;
            timer.hide();
            timer.Seconds = 0;

            gelandewagen.Crashed = true;
            gelandewagen.hide();
        }

        private void startGame()
        {
            menu.hide();
            gameover.hide();
            about.hide();
            help.hide();
            howToPlay.hide();
            highscores.hide();

            traffic.Speed = trafficSpeed;
            traffic.Position = trafficPosition;

            road.Speed = roadSpeed;
            bldg1.Speed = bldg1Speed;
            bldg2.Speed = bldg2Speed;

            timer.Gameover = false;
            timer.show();
            timer.Seconds = 0;

            gelandewagen.Crashed = false;
            gelandewagen.show();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here

            if (traffic.Position.X > graphics.PreferredBackBufferWidth)
            {
                Random r = new Random();
                traffic.Tex = trafficTextures[r.Next(0, trafficTextures.Count)];
            }


            //GAMEOVER
            KeyboardState ks = Keyboard.GetState();
            if (gameover.Enabled)
            {
                if (gameover.SelectedIndex == 0 && ks.IsKeyDown(Keys.Enter))
                {
                    startGame();
                    cm.Exploded = false;
                }
                if (gameover.SelectedIndex == 1 && ks.IsKeyDown(Keys.Escape))
                {
                    showMenu();
                    cm.Exploded = false;
                    menu.SelectedIndex = 0;
                }
            }


            //MENU
            if (menu.Enabled)
            {
                if (menu.SelectedIndex == 0 && ks.IsKeyDown(Keys.Enter))
                {
                    startGame();
                    menu.SelectedIndex = -1;
                }
                if (menu.SelectedIndex == 1 && ks.IsKeyDown(Keys.Enter))
                {
                    showHighscores();
                }
                if (menu.SelectedIndex == 2 && ks.IsKeyDown(Keys.Enter))
                {
                    showHowTo();
                }
                if (menu.SelectedIndex == 3 && ks.IsKeyDown(Keys.Enter))
                {
                    showHelp();
                }
                if (menu.SelectedIndex == 4 && ks.IsKeyDown(Keys.Enter))
                {
                    showAbout();
                }
                if (menu.SelectedIndex == 5 && ks.IsKeyDown(Keys.Enter))
                {
                    this.Exit();
                }
            }


            //ABOUT

            if (about.Enabled)
            {
                if (ks.IsKeyDown(Keys.Escape))
                {
                    showMenu();
                }
            }


            //HELP

            if (help.Enabled)
            {
                if (ks.IsKeyDown(Keys.Escape))
                {
                    showMenu();
                }
            }


            //HOWTO

            if (howToPlay.Enabled)
            {
                if (ks.IsKeyDown(Keys.Escape))
                {
                    showMenu();
                }
            }



            //HIGHSCORES

            if (highscores.Enabled)
            {
                if (ks.IsKeyDown(Keys.Escape))
                {
                    showMenu();
                }
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            Rectangle r = new Rectangle(0, 0, graphics.PreferredBackBufferWidth,
               graphics.PreferredBackBufferHeight);

            spriteBatch.Begin();
            spriteBatch.Draw(evening, r, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
