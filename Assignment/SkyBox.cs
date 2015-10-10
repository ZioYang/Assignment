using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Assignment
{
    class SkyBox : BasicModel
    {
        Matrix skybox;
        public SkyBox(Model model) : base(model)
        {

        }
        public override void Update(GameTime gametime)
        {

        }
        public override void Draw(GraphicsDevice device, Camera camera)
        {
            device.SamplerStates[0] = SamplerState.LinearClamp;
            skybox = Matrix.CreateScale(1000f) * Matrix.CreateTranslation(camera.cameraPosition.X, 0, camera.cameraPosition.Z);
            //skybox = Matrix.CreateScale(300f) * Matrix.CreateTranslation((camera.cameraPosition) - new Vector3(0, 150, 0));
            base.Draw(device, camera);
        }
        protected override Matrix GetWorld()
        {
            return skybox;
        }
    }
}
