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
using Microsoft.Xna.Framework.Storage;
using System.Text.RegularExpressions;



namespace RussianMentality
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class CollisionManager : Microsoft.Xna.Framework.GameComponent
    {
        private Gelandewagen gelandewagen;
        private Traffic traffic;
        private Background bg1, bg2, bgr;
        private Explosion explosion;
        private Timer timer;
        private bool exploded = false;
        public bool Exploded
        {
            get { return exploded; }
            set { exploded = value; }
        }
        private Gameover gameover;

        public CollisionManager(Game game,
            Gelandewagen gelandewagen,
            Traffic traffic,
            Background bg1,
            Background bg2,
            Background bgr,
            Explosion explosion,
            Timer timer,
            Gameover gameover)
            : base(game)
        {
            // TODO: Construct any child components here

            this.gelandewagen = gelandewagen;
            this.traffic = traffic;
            this.bg1 = bg1;
            this.bg2 = bg2;
            this.bgr = bgr;
            this.explosion = explosion;
            this.timer = timer;
            this.gameover = gameover;
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

            Rectangle gelandewagenRect = gelandewagen.getBounds();
            Rectangle trafficRect = traffic.getBounds();

            Rectangle trafficRect1 = new Rectangle(trafficRect.X + 2, trafficRect.Y + 28, trafficRect.Width - 6, trafficRect.Height - 28);
            Rectangle trafficRect2 = new Rectangle(trafficRect.X + 44, trafficRect.Y + 6, trafficRect.Width - 102, trafficRect.Height - 6);

            if ((gelandewagenRect.Intersects(trafficRect1) || gelandewagenRect.Intersects(trafficRect2)) && !exploded)
            {
                traffic.Speed = Vector2.Zero;
                bg1.Speed = Vector2.Zero;
                bg2.Speed = Vector2.Zero;
                bgr.Speed = Vector2.Zero;

                Vector2 gelandewagenCentre = new Vector2(gelandewagenRect.X + gelandewagenRect.Width / 2 - 32, gelandewagenRect.Y + gelandewagenRect.Height / 2 - 32);
                Vector2 trafficCentre = new Vector2(trafficRect.X + trafficRect.Width / 2 - 32, trafficRect.Y + trafficRect.Height / 2 - 32);

                explosion.Position = new Vector2((gelandewagenCentre.X + trafficCentre.X) / 2, (gelandewagenCentre.Y + trafficCentre.Y) / 2);
                explosion.start();
                exploded = true;

                timer.Gameover = true;
                gelandewagen.Crashed = true;



                if (!File.Exists("scores.txt"))
                {
                    using (FileStream fs = new FileStream("scores.txt",
                        FileMode.CreateNew)) { }
                }


                StreamReader reader = new StreamReader("scores.txt");
                string readerInput = reader.ReadToEnd().Trim();
                string[] scoresArray = Regex.Split(readerInput, "[\r\n]+");
                Array.Sort(scoresArray);
                //debug
                Console.WriteLine("/");
                for (int i = 0; i < scoresArray.Count(); i++)
                {
                    Console.WriteLine(scoresArray[i]);
                }
                //debug end
                reader.Dispose();




                StreamWriter writer = new StreamWriter("scores.txt");
                writer.WriteLine(timer.Seconds.ToString("0:00").Trim());
                writer.WriteLine(readerInput.Trim());

                writer.Dispose();




                gameover.show();
                timer.hide();

                gameover.TextScore = "Your score is " + timer.Time;

                gameover.SelectedIndex = 0;
            }

            base.Update(gameTime);
        }
    }
}
