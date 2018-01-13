using System;
using Microsoft.Xna.Framework;
using System.Threading;


namespace _3dMonteCarloPi
{
    public class Simulation
    {
        // 6 * hits / trials
        private static Mutex counterLock = new Mutex();
        private Random rand;
        private static long hits = 0;
        private static long trials = 0;
        private static bool keepGoing = true;
        private CubeManager manager;


        public static double Pi
        {
            get
            {
                lock (counterLock)
                {
                    return (6.0 * (double)hits / (double)trials);
                }
            }
        }

        public Simulation(Game game, int seed)
        {
            rand = new Random(seed);
            manager = CubeManager.getCubeManager(game);
        }

        public static void Stop()
        {
            keepGoing = false;
        }

        public void simulate()
        {
            int x;
            int y;
            int z;
            long trialNum;
            int radius = ProjectConstants.radius;
            int diameter = radius * 2;
            int rSquared = radius * radius;
            bool hit = false;
            while (keepGoing)
            {
                hit = false;
                x = rand.Next(diameter);
                y = rand.Next(diameter);
                z = rand.Next(diameter);
                x -= radius;
                y -= radius;
                z -= radius;
                if (((x*x) + (y*y) + (z*z)) <= rSquared)
                {
                    hit = true;
                }

                lock (counterLock)
                {
                    trialNum = trials++;
                    if (hit)
                    {
                        ++hits;
                    }
                }

                if(trialNum %100 == 0)
                {
                    manager.makeCube(x, y, z, hit);
                }
            }
        }
    }
}
