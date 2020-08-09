using System;
using System.Collections.Generic;

namespace ConsoleArcade
{
    public class Spawner
    {

        private static Random rand = new Random();
        private static TimeSpan downTime = new TimeSpan(0);
        private static DateTime lastSpawn = DateTime.Now;

        private static int totalSpawned = 0;
        private static int difficulty = 0;

        public static int level = 1;

        public static List<string> symbols = Program.currentDetail.foes;

        public static void SpawnAutomatically(List<MovableObject> into)
        {
            if (DateTime.Now > lastSpawn + downTime)
            {
                lastSpawn = DateTime.Now;

                int val = (20_000_000 - (level * 1_000_000) - (difficulty * 200_000)) / (int)(Program.maxColumns / 5);

                downTime = new TimeSpan(val);

                level = level > 7 ? 8 : level;

                for (int i = 0; i < level; i++)
                {
                    // spawn rare
                    if (rand.Next(0, 20) == 1)
                    {
                        // haha lol
                        into.Add(spawnTreat());
                    }
                    else
                    {
                        // easy to confuse
                        into.Add(spawnThreat());
                    }

                    totalSpawned += 1;
                }



                int rowModifier = (int)(Program.maxColumns / 2);

                if ((int)(totalSpawned / rowModifier) * 1 > difficulty)
                {
                    difficulty = (int)(totalSpawned / rowModifier);
                }

                
                if ((int)(difficulty / 5) > level)
                {
                    difficulty = 0;
                    level++;
                }

            }
        }

        private static MovableObject spawnThreat()
        {
            MovableObject threat = new MovableObject(0, rand.Next(0, Program.maxColumns - 1), symbols[rand.Next(symbols.Count)], new TimeSpan(3_500_000 - (level * 200_000)));

            threat.directionRow = 1;

            return threat;
        }

        private static MovableObject spawnTreat()
        {
            MovableObject treat = new MovableObject(0, rand.Next(0, Program.maxColumns - 1), Program.currentDetail.powerUp, new TimeSpan(3_500_000 - (level * 200_000)));

            treat.directionRow = 1;
            treat.isTreat = true;

            return treat;
        }

        public static void reset()
        {
            totalSpawned = 0;
            difficulty = 0;

            level = 1;
        }
    }
}
