using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Assignment
{
    class ModelManager : Microsoft.Xna.Framework.DrawableGameComponent
    {
        List<BasicModel> models = new List<BasicModel>();
        Ground ground;
        public Knife knife;
        //public Vector3 KnifePosition
        //{
        //    get { return knife.CurrentPosition; }
        //}

        //public Vector3 KnifeDirection { get { return knife.Direction; } }

        //public Vector3 Knifeup { get { return knife.KnifeUp; } }
        public ModelManager(Game game) : base(game)
        {

        }

        public override void Initialize()
        {
            ground = new Ground(Game.Content.Load<Model>(@"Models/Ground/Ground"));
            models.Add(ground);
            models.Add(new SkyBox(
              Game.Content.Load<Model>(@"Models/SkyBox/skybox")));
            knife = new Knife(this.Game,Game.Content.Load<Model>(@"Models/Knife/knifemodel"),
                ((Game1)Game).GraphicsDevice,((Game1)Game).camera);
            models.Add(knife);
            base.Initialize();
        }
        protected override void LoadContent()
        {

            base.LoadContent();
        }
        public override void Update(GameTime gametime)
        {
            foreach (BasicModel model in models)
            {
                model.Update(gametime);
            }

            
            
            base.Update(gametime);
        }
        public override void Draw(GameTime gameTime)
        {
            foreach (BasicModel model in models)
            {
                model.Draw(((Game1)Game).device, ((Game1)Game).camera);
            }
            base.Draw(gameTime);
        }
    }
}
