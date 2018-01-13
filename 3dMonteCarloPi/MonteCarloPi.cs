using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Threading;

namespace _3dMonteCarloPi
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class MonteCarloPi : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Camera camera;
        //Camera
        //Vector3 target;
        //Vector3 position;
        //Matrix projectionMatrix;
        //Matrix viewMatrix;
        //Matrix worldMatrix;

        //Model redCube;
        //Model blueCube;
        Cube[] cubes = new Cube[1000];

        public MonteCarloPi()
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
            Random rand = new Random();
            Simulation sim;
            for (int i = 0; i < ProjectConstants.threadCount; ++i)
            {
                sim = new Simulation(this, rand.Next());
                new Thread(new ThreadStart(sim.simulate)).Start();
            }
            camera = Camera.getCamera(GraphicsDevice, this);
            //setup camera
            //target = new Vector3(0.0f, 0.0f, 0.0f);
            //position = new Vector3(0.0f, 15f, -100.0f);
            //projectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45.0f), GraphicsDevice.DisplayMode.AspectRatio, 1.0f, 1000.0f);
            //viewMatrix = Matrix.CreateLookAt(position, target, Vector3.Up);
            //worldMatrix = Matrix.CreateWorld(target, Vector3.Forward, Vector3.Up);

            //blueCube = Content.Load<Model>("Models/CubeBlue");
            //redCube = Content.Load<Model>("Models/CubeRed");
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
            {
                Simulation.Stop();
                Exit();
            }

            
            camera.Update(gameTime);
            // TODO: Add your update logic here

            //if (orbit)
            //{
            //    Matrix rotationMatrix = Matrix.CreateRotationY(MathHelper.ToRadians(1.0f));
            //    position = Vector3.Transform(position, rotationMatrix);
            //}

            //viewMatrix = Matrix.CreateLookAt(position, target, Vector3.Up);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            

            GraphicsDevice.Clear(Color.TransparentBlack);

            //foreach (ModelMesh mesh in blueCube.Meshes)
            //{
            //    foreach(BasicEffect effect in mesh.Effects)
            //    {
            //        effect.AmbientLightColor = new Vector3(1.0f, 0, 0);
            //        effect.View = viewMatrix;
            //        effect.World = worldMatrix;
            //        effect.Projection = projectionMatrix;

            //    }
            //    mesh.Draw();
            //}

            // TODO: Add your drawing code here

            CubeManager.getCubeManager(this).Draw(gameTime);

            this.Window.Title = $"Pi Approximation: {Simulation.Pi}";

            base.Draw(gameTime);
        }
    }
}
