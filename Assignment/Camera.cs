using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Assignment
{
    public class Camera : Microsoft.Xna.Framework.GameComponent
    {
        public Matrix view { get; protected set; }
        public Matrix projection { get; protected set; }
        public Vector3 cameraPosition { get; set; }
        Vector3 cameraDirection;

        public Vector3 CameraPosition { get { return cameraPosition; } }

        public Vector3 CameraDirection { get { return cameraDirection; } }
        Vector3 cameraUp;
        public Vector3 CameraUp { get { return cameraUp; } }
        Vector3 moveDirection;

        public Vector3 MoveDirection { get { return moveDirection; } }
        float speed = 3;
        bool jumptrigger = false;
        bool checkspacekey = true;
        float jumpspeed = 5.00f;
        float jumpaccelate = 0.25f;
        Vector3 newpos;
        Vector3 startPosition;
        MouseState preMouseState;
        //float totalYaw = MathHelper.Pi * 2;
        float currentYaw = 0;

        public float YawAngle { get { return yawAngle; } }

        public float PitchAngle { get { return pitchAngle; } }

        float totalPitch = MathHelper.PiOver4;
        float currentPitch = 0;
        float borderlimit = 5f;
        
        float yawAngle;
        float pitchAngle;
        int width;
        int height;
        


        //float currentpitchradius = 0;
        public Camera(Game game, Vector3 pos, Vector3 target, Vector3 up) : base(game)
        {

            yawAngle = 0;
            pitchAngle = 0;
            //width = this.Game.Window.ClientBounds.Width;
            //height = this.Game.Window.ClientBounds.Height;

            cameraPosition = pos;
            cameraDirection = target - pos;
            cameraDirection.Normalize();
            cameraUp = up;
            cameraUp.Normalize();
            CreateLookAt();
            projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4,
                (float)game.Window.ClientBounds.Width / (float)game.Window.ClientBounds.Height,
                1, 3000);

            width = this.Game.GraphicsDevice.Viewport.Width;
            height = this.Game.GraphicsDevice.Viewport.Height;
        }
        public override void Initialize()
        {
            Mouse.SetPosition(Game.Window.ClientBounds.Width / 2, Game.Window.ClientBounds.Height / 2);
            preMouseState = Mouse.GetState();

            startPosition = cameraPosition;

            base.Initialize();
        }
        public override void Update(GameTime gameTime)
        {
            
            view = Matrix.CreateLookAt(cameraPosition, cameraDirection, Vector3.Up);
            moveDirection = new Vector3(cameraDirection.X, 0, cameraDirection.Z);
            moveDirection.Normalize();
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                cameraPosition += moveDirection * speed;
                if(!jumptrigger)
                ((Game1)Game).FootStep1.Play();
            }
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                cameraPosition -= moveDirection * speed;
                if (!jumptrigger)
                    ((Game1)Game).FootStep2.Play();
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                cameraPosition += Vector3.Cross(cameraUp, cameraDirection) * speed;
                if (!jumptrigger)
                    ((Game1)Game).FootStep3.Play();
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                cameraPosition -= Vector3.Cross(cameraUp, cameraDirection) * speed;
                if (!jumptrigger)
                    ((Game1)Game).FootStep4.Play();
            }

            //jump function:

            if (checkspacekey)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Space))
                {
                    jumptrigger = true;
                    checkspacekey = false;
                    ((Game1)Game).JumpEffect.Play();
                }
            }
            if (jumptrigger && !checkspacekey)
            {
                cameraPosition += new Vector3(0, jumpspeed, 0);
                jumpspeed -= jumpaccelate;

                newpos = cameraPosition;
                if (jumpspeed <= -5.0f)
                {
                    checkspacekey = true;
                    ((Game1)Game).LandingEffect.Play();
                    jumptrigger = false;
                    jumpspeed = 5f;
                    cameraPosition = new Vector3(newpos.X, 50, newpos.Z);
                }
            }

            // float YawAngle = (-MathHelper.PiOver4 / 150) *(Mouse.GetState().X - preMouseState.X);
            // cameraDirection = Vector3.Transform(cameraDirection, Matrix.CreateFromAxisAngle(cameraUp,YawAngle));
            //float PitchAngle = (-MathHelper.PiOver4 / 150) * (Mouse.GetState().Y - preMouseState.Y);
            //   Vector3 normalisecross = Vector3.Cross(cameraUp, cameraDirection);
            //normalisecross.Normalize();
            //  cameraDirection = Vector3.Transform(cameraDirection,Matrix.CreateFromAxisAngle(normalisecross, PitchAngle));

            // Yaw Rotation:
            
            yawAngle = (-MathHelper.PiOver4 / 200) *
                ((Mouse.GetState().X - preMouseState.X) / 2);
            pitchAngle = (MathHelper.PiOver4 / 200) *
                    ((Mouse.GetState().Y - preMouseState.Y) / 2);
            //MouseOutWindow(Mouse.GetState());
            Mouse.SetPosition(width / 2, height / 2);

            //MouseOutWindow(Mouse.GetState());
            // float yawAngle = (-MathHelper.PiOver4 / 150) *
            //         (Mouse.GetState().X - preMouseState.X);

            //if (Math.Abs(currentYaw + yawAngle) < totalYaw)
            //{
            cameraDirection = Vector3.Transform(cameraDirection,
                Matrix.CreateFromAxisAngle(cameraUp, yawAngle));
            currentYaw += yawAngle;
            //}

            // float pitchAngle = (MathHelper.PiOver4 / 150) *
            //     (Mouse.GetState().Y - preMouseState.Y);

            if (Math.Abs(currentPitch + pitchAngle) < totalPitch)
            {
                
                Vector3 nor = Vector3.Cross(cameraUp, cameraDirection);
                nor.Normalize();

                //cameraDirection = Vector3.Transform(cameraDirection,
                //    Matrix.CreateFromAxisAngle(
                //        Vector3.Cross(cameraUp, cameraDirection),
                //        pitchAngle));
                cameraDirection = Vector3.Transform(cameraDirection,
                    Matrix.CreateFromAxisAngle(
                        nor,
                        pitchAngle));

                currentPitch += pitchAngle;
                preMouseState = Mouse.GetState();
            }
            else
            {
                pitchAngle = 0;
            }
            // Reset prevMouseState
            


            CreateLookAt();

            base.Update(gameTime);

        }
        private void CreateLookAt()
        {
            view = Matrix.CreateLookAt(cameraPosition,
                cameraPosition + cameraDirection, cameraUp);
        }

        private bool MouseOutWindow(MouseState mousestate)
        {
            bool outwindow = false;
            if (mousestate.X < borderlimit)
            {
                //outWindowLeft = true;
                Mouse.SetPosition(0, (int)mousestate.Y);
                outwindow = true;
            }
            else if (mousestate.X > width - borderlimit)
            {
                //outWindowRight = true;
                Mouse.SetPosition(width, (int)mousestate.Y);
                outwindow = true;
            }

            else if (mousestate.Y < borderlimit)
            {
                //outWindowUp = true;
                Mouse.SetPosition((int)mousestate.X, 0);
                outwindow = true;
            }

            else if (mousestate.Y > height - borderlimit)
            {
                //outWindowDown = true;
                Mouse.SetPosition((int)mousestate.X, height);
                outwindow = true;
            }
            return outwindow;
        }
    }
}

