using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace _3dMonteCarloPi
{
    public class Camera : GameComponent
    {
        private static Camera singleton = null;

        private Vector3 position;
        private Vector3 target;
        private Matrix projection;
        private Matrix view;
        private bool orbit;
        private bool spaceUpSinceLastPress = true;
        
        public static Camera getCamera(GraphicsDevice device, Game game)
        {
            if(singleton == null)
            {
                singleton = new Camera(device, game);
            }
            return singleton;
        }

        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Space) && spaceUpSinceLastPress)
            {
                orbit = !orbit;
                spaceUpSinceLastPress = false;
            }
            else if (Keyboard.GetState().IsKeyUp(Keys.Space))
            {
                spaceUpSinceLastPress = true;
            }

            if (orbit)
            {
                float radians = MathHelper.ToRadians((float)(36.0 / 1000.0 * gameTime.ElapsedGameTime.TotalMilliseconds));
                Matrix rotationMatrix = Matrix.CreateRotationY(radians);
                position = Vector3.Transform(position, rotationMatrix);
            }
            view = Matrix.CreateLookAt(position, target, Vector3.Up);

            base.Update(gameTime);
        }

        public Matrix Projection
        {
            get => projection;
        }

        public Matrix View
        {
            get => view;
        }

        private Camera(GraphicsDevice device, Game game) : base(game)
        {
            position = new Vector3(0, 0, 700);
            target = new Vector3(0, 0, 0);
            projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(70.0f), device.DisplayMode.AspectRatio, 1.0f, 1000.0f);
            view = Matrix.CreateLookAt(position, target, Vector3.Up);
        }  
    }
}