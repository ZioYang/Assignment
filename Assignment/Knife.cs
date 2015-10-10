using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Assignment
{
    class Knife : BasicModel
    {
        
        private enum swingKnife
        {
            Ready,
            swingUp,
            swinging,
            swingBack
            
        }

        private enum StabKnife
        {
            Ready,
            backwards,
            forwards,
            finish
        }
        
        Matrix translation = Matrix.Identity;

        Matrix rotation = Matrix.CreateRotationX(-MathHelper.PiOver4);
        //Matrix rotation = Matrix.Identity;
        Vector3 currentPosition = Vector3.Zero;
        public Vector3 CurrentPosition { get { return currentPosition; } }

        public Vector3 Direction { get { return direction; } }

        public Vector3 KnifeUp { get { return knifeUp; } }
        Camera camera;
        //
        

        Vector3 direction;
        Vector3 knifeUp;
        Vector3 normal;
        float AttackPitch;
        int swingAction;
        int stabAction;
        float stabPitch;
        float stabLimit = MathHelper.PiOver4;
        float stabLength;
        float stabSpeed = 1f;
        float stabRange = 5f;
        //bool swingBack;
        //bool swinging;
        //bool swingUp;
        float attackPitchLimit = MathHelper.PiOver4 * 1.5f;
        float AttackAngleRotate = MathHelper.PiOver4 / 10;
        float Yaw;
        Game game;
        Vector3 offset;
        Vector3 nor; 
        int width;
        int height;
        //
        public Knife(Game game,Model model, GraphicsDevice device,Camera camera) : base(model)
        {
            this.camera = camera;
            this.game = game;
            width = game.Window.ClientBounds.Width;
            height = game.Window.ClientBounds.Height;
            //rotation = Matrix.CreateFromAxisAngle(nor, MathHelper.PiOver4);
            //currentYaw = 0;
            //currentPitch = 0;
            AttackPitch = 0;
            swingAction = 0;
            stabAction = 0;
            stabLength = 0;
            offset = new Vector3(1f,0,-1f);
            //swingBack = false;
            //swingUp = false;
            //swinging = false;

        }
        public override void Initialize()
        {


            direction = camera.CameraDirection;
            direction.Normalize();
            //startPosition = currentPosition;
            
            base.Initialize();
        }
        
        public void UpdateKnife(GameTime gametime)
        {

            //currentPosition = camera.CameraPosition;
            //direction = Vector3.Transform(direction, Matrix.CreateFromAxisAngle(knifeUp, camera.YawAngle));
            

            //offset = OffsetCalculate(camera.CameraDirection, camera.CameraPosition);
            knifeUp.Normalize();

            rotation *= Matrix.CreateFromAxisAngle(camera.CameraUp, camera.YawAngle);
            
            offset = Vector3.Transform(offset, Matrix.CreateRotationY(camera.YawAngle));
            
            nor = Vector3.Cross(camera.CameraDirection,camera.CameraUp);
            nor.Normalize();

            
            rotation *= Matrix.CreateFromAxisAngle(nor, -camera.PitchAngle);
            //swing Knife
            if (Mouse.GetState().LeftButton == ButtonState.Pressed  | swingAction!=0   )
            {
                
                switch (swingAction)
                {
                    case ((int)swingKnife.Ready):
                        ((Game1)game).SwingSwordEffect.Play();
                        swingAction++;
                        break;
                    case ((int)swingKnife.swingUp):
                        AttackPitch -= AttackAngleRotate;
                        rotation *= Matrix.CreateFromAxisAngle(nor, AttackAngleRotate);
                        if (AttackPitch + attackPitchLimit < 0) 
                        {
                            rotation *= Matrix.CreateFromAxisAngle(nor, -(AttackPitch+attackPitchLimit));
                            AttackPitch = -attackPitchLimit;
                            swingAction++;
                        }
                        break;
                    case ((int)swingKnife.swinging):
                        rotation *= Matrix.CreateFromAxisAngle(nor, -AttackAngleRotate*5);
                        AttackPitch += AttackAngleRotate*5;
                        if (attackPitchLimit - AttackPitch  < 0)
                        {
                            rotation *= Matrix.CreateFromAxisAngle(nor, AttackPitch-attackPitchLimit);
                            AttackPitch = attackPitchLimit;
                            swingAction++;
                        }
                        break;
                    case ((int)swingKnife.swingBack):
                        AttackPitch -= AttackAngleRotate;
                        rotation *= Matrix.CreateFromAxisAngle(nor, AttackAngleRotate);
                        if (AttackPitch <=0)
                        {
                            rotation *= Matrix.CreateFromAxisAngle(nor, AttackPitch);
                            
                            swingAction = 0;
                        }
                        break;
                }
             }
             
            if(swingAction !=0 && Mouse.GetState().RightButton == ButtonState.Pressed)
            {

            }
  
            
            normal = Vector3.Cross(camera.CameraDirection, camera.CameraUp);
            normal.Normalize();
            currentPosition = camera.CameraPosition - new Vector3(0,2f,0);
            

            //stab
            if (Mouse.GetState().RightButton == ButtonState.Pressed | stabAction != 0)
            {
                switch(stabAction)
                {
                    case ((int)StabKnife.Ready):
                        ((Game1)game).StabbingEffect.Play();
                        stabAction++;
                        break;
                    case ((int)StabKnife.backwards):
                        stabPitch += AttackAngleRotate;

                        rotation *= Matrix.CreateFromAxisAngle(nor, -AttackAngleRotate);               
                        if(stabPitch > stabLimit)
                        {
                            rotation *= Matrix.CreateFromAxisAngle(nor, stabPitch-stabLimit);
                            stabPitch = stabLimit;
                            stabAction++;
                        }
                        break;
                    case ((int)StabKnife.forwards):
                        
                        if (stabLength <= stabRange)
                        {
                            stabLength += stabSpeed;
                            offset += Vector3.Normalize(camera.CameraDirection) * stabSpeed;
                        }
                        else if(stabLength > stabRange)
                        {
                            offset -= Vector3.Normalize(camera.CameraDirection) * (stabLength-stabRange);
                            stabLength = stabRange;
                            stabAction++;
                        }
                        break;
                    
                    case ((int)StabKnife.finish):

                        if (stabLength <= stabRange && stabLength >0 )
                        {
                            stabLength -= stabSpeed/2;
                            offset -= Vector3.Normalize(camera.CameraDirection) * stabSpeed/2;
                        }
                        else if (stabLength < 0)
                        {
                            offset += Vector3.Normalize(camera.CameraDirection) * -stabLength;
                            stabLength = 0;
                        }
                        else if (stabLength == 0)
                        {
                            stabPitch -= AttackAngleRotate;
                            rotation *= Matrix.CreateFromAxisAngle(nor, AttackAngleRotate);
                            if (stabPitch <= 0)
                            {
                                rotation *= Matrix.CreateFromAxisAngle(nor, stabPitch);
                                stabPitch = 0;
                                stabAction = 0; ;
                            }
                        }
                        break;
                        
                }
            }
            if(stabAction !=0 && Mouse.GetState().LeftButton == ButtonState.Pressed)
            {

            }
            translation = Matrix.CreateTranslation(currentPosition + offset);


            base.Update(gametime);
        }

        public override void Draw(GraphicsDevice device, Camera camera)
        {

            base.Draw(device, camera);
        }

        protected override Matrix GetWorld()
        {
            return Matrix.CreateScale(30f) * rotation * translation;
        }
        
        
    }
}
