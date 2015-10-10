using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace Assignment
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        ModelManager modelManager;
        SpriteFont font;
        public GraphicsDevice device { get; protected set; }
        public Camera camera { get; protected set; }
        //private
        int width;
        int height;
        SoundEffect footstep_1;
        SoundEffect footstep_2;
        SoundEffect footstep_3;
        SoundEffect footstep_4;
        SoundEffect musicfx;
        SoundEffect jumping;
        SoundEffect landing;
        SoundEffect Ghost_death_1;
        SoundEffect Ghost_death_2;
        SoundEffect Ghost_death_3;
        SoundEffect Ghost_death_4;
        SoundEffect stabbing;
        SoundEffect swingsword;

        SoundEffectInstance jumpeffect;
        SoundEffectInstance landingeffect;
        SoundEffectInstance musicef;
        SoundEffectInstance footstep_1_effect;
        SoundEffectInstance footstep_2_effect;
        SoundEffectInstance footstep_3_effect;
        SoundEffectInstance footstep_4_effect;

        SoundEffectInstance Ghost_death_1_effect;
        SoundEffectInstance Ghost_death_2_effect;
        SoundEffectInstance Ghost_death_3_effect;
        SoundEffectInstance Ghost_death_4_effect;

        SoundEffectInstance stabbingEffect;
        SoundEffectInstance swingswordEffect;
        public SoundEffectInstance FootStep1 { get { return footstep_1_effect; } }

        public SoundEffectInstance FootStep2 { get { return footstep_2_effect; } }

        public SoundEffectInstance FootStep3 { get { return footstep_3_effect; } }

        public SoundEffectInstance FootStep4 { get { return footstep_4_effect; } }

        public SoundEffectInstance JumpEffect { get { return jumpeffect; } }

        public SoundEffectInstance LandingEffect { get { return landingeffect; } }

        public SoundEffectInstance Ghost_Death_1_Effect { get { return Ghost_death_1_effect; } }
        public SoundEffectInstance Ghost_Death_2_Effect { get { return Ghost_death_2_effect; } }
        public SoundEffectInstance Ghost_Death_3_Effect { get { return Ghost_death_3_effect; } }
        public SoundEffectInstance Ghost_Death_4_Effect { get { return Ghost_death_4_effect; } }

        public SoundEffectInstance StabbingEffect { get { return stabbingEffect; } }
        public SoundEffectInstance SwingSwordEffect { get { return swingswordEffect; } }
        public int Width
        { get { return width; } }
        public int Height
        { get { return height; } }
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
            width = this.Window.ClientBounds.Width;
            height = this.Window.ClientBounds.Height;
            camera = new Camera(this, new Vector3(0, 50, 10), new Vector3(0, 50, 0), Vector3.Up);
            Components.Add(camera);
            modelManager = new ModelManager(this);
            Components.Add(modelManager);

            footstep_1 = Content.Load<SoundEffect>(@"Sound/woodpanel1");
            footstep_1_effect = footstep_1.CreateInstance();
            footstep_1_effect.IsLooped = false;

            footstep_2 = Content.Load<SoundEffect>(@"Sound/woodpanel2");
            footstep_2_effect = footstep_2.CreateInstance();
            footstep_2_effect.IsLooped = false;

            footstep_3 = Content.Load<SoundEffect>(@"Sound/woodpanel3");
            footstep_3_effect = footstep_3.CreateInstance();
            footstep_3_effect.IsLooped = false;

            footstep_4 = Content.Load<SoundEffect>(@"Sound/woodpanel4");
            footstep_4_effect = footstep_4.CreateInstance();
            footstep_4_effect.IsLooped = false;

            jumping = Content.Load<SoundEffect>(@"Sound/jumping_wood");
            jumpeffect = jumping.CreateInstance();
            jumpeffect.IsLooped = false;

            landing = Content.Load<SoundEffect>(@"Sound/landing_2");
            landingeffect = landing.CreateInstance();
            landingeffect.IsLooped = false;

            Ghost_death_1 = Content.Load<SoundEffect>(@"Sound/Ghost_death_1");
            Ghost_death_1_effect = Ghost_death_1.CreateInstance();
            Ghost_death_1_effect.IsLooped = false;

            Ghost_death_2 = Content.Load<SoundEffect>(@"Sound/Ghost_death_2");
            Ghost_death_2_effect = Ghost_death_2.CreateInstance();
            Ghost_death_2_effect.IsLooped = false;

            Ghost_death_3 = Content.Load<SoundEffect>(@"Sound/Ghost_death_3");
            Ghost_death_3_effect = Ghost_death_3.CreateInstance();
            Ghost_death_3_effect.IsLooped = false;

            Ghost_death_4 = Content.Load<SoundEffect>(@"Sound/Ghost_death_4");
            Ghost_death_4_effect = Ghost_death_4.CreateInstance();
            Ghost_death_4_effect.IsLooped = false;

            stabbing = Content.Load<SoundEffect>(@"Sound/stabbing");
            stabbingEffect = stabbing.CreateInstance();
            stabbingEffect.IsLooped = false;

            swingsword = Content.Load<SoundEffect>(@"Sound/swingsword");
            swingswordEffect = swingsword.CreateInstance();
            swingswordEffect.IsLooped = false;

            musicfx = Content.Load<SoundEffect>(@"Music/wakeupstanley");
            musicef = musicfx.CreateInstance();
            musicef.IsLooped = true;
            musicef.Play();

            //this.IsMouseVisible = true;
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            device = graphics.GraphicsDevice;
            font = Content.Load<SpriteFont>(@"Font/Arial");
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            

            base.Update(gameTime);
            modelManager.knife.UpdateKnife(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            // TODO: Add your drawing code here
            //string message1 = "knifePosition:" 
            //    + (int)modelManager.knife.CurrentPosition.X+"."
           //     + (int)modelManager.knife.CurrentPosition.Y+"."
           //     + (int)modelManager.knife.CurrentPosition.Z;
            //string message2 = "KnifeDirection:"
            //    + (int)modelManager.knife.Direction.X + "."
            //    + (int)modelManager.knife.Direction.Y + "."
            //    + (int)modelManager.knife.Direction.Z;
            base.Draw(gameTime);
            //spriteBatch.Begin();
           // spriteBatch.DrawString(font, message1, new Vector2(50, 20),
            //    Color.Black, 0, Vector2.Zero, 1f, SpriteEffects.None, 0.5f);
            //spriteBatch.DrawString(font, message2, new Vector2(50, 60),
            //    Color.Black, 0, Vector2.Zero, 1f, SpriteEffects.None, 0.5f);
           // spriteBatch.End();

        }
    }
}
