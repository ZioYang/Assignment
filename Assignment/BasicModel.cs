using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Assignment
{
    class BasicModel
    {
        public Model model { get; protected set; }
        protected Matrix world = Matrix.Identity;

        public BasicModel(Model model)
        {
            this.model = model;
        }
        public virtual void Update(GameTime gametime)
        {

        }
        public virtual void Initialize()
        {

        }
        public virtual void Draw(GraphicsDevice device, Camera camera)
        {
            device.BlendState = BlendState.Opaque;
            device.DepthStencilState = DepthStencilState.Default;
            Matrix[] transforms = new Matrix[model.Bones.Count];
            model.CopyAbsoluteBoneTransformsTo(transforms);
            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    // effect.World = mesh.ParentBone.Transform*GetWorld();
                    effect.World = transforms[mesh.ParentBone.Index] * GetWorld();
                    effect.View = camera.view;
                    effect.Projection = camera.projection;
                    effect.TextureEnabled = true;
                    effect.Alpha = 1;
                }
                mesh.Draw();
            }
        }
        protected virtual Matrix GetWorld()
        {
            return world;
        }

    }
}
