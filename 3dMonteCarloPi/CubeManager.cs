using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Threading;
using System.Collections.Concurrent;

namespace _3dMonteCarloPi
{
    public class CubeManager : DrawableGameComponent
    {
        private Mutex cubeLock = new Mutex();
        private int count = ProjectConstants.cubeCount;
        private Cube[] cubes;
        private static CubeManager singleton = null;
        private int currentIndex;
        private static Model redModel;
        private static Model blueModel;

        public static CubeManager getCubeManager(Game game)
        {
            if(singleton == null)
            {
                singleton = new CubeManager(game);
            }
            return singleton;
        }

        private CubeManager(Game game) : base(game)
        {
            cubes = new Cube[count];
            
            blueModel = game.Content.Load<Model>("Models/CubeBlue");
            redModel = game.Content.Load<Model>("Models/CubeRed");

            for (int i = 0; i < cubes.Length; ++i)
            {
                cubes[i] = new Cube(0, 0, 0, redModel, game);
            }
            currentIndex = 0;
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public void makeCube(float x, float y, float z, bool hit)
        {
            Cube c;
            lock (cubeLock)
            {
                c = cubes[currentIndex];
                ++currentIndex;
                currentIndex %= count;
            }
            c.updateCube(x, y, z, (hit) ? redModel : blueModel);
        }

        public override void Draw(GameTime gameTime)
        {
            foreach(Cube c in cubes)
            {
                c.Draw(gameTime);
            }
            base.Draw(gameTime);
        }
    }
}
