using System;
using System.IO;
using System.Media;


namespace ConsoleArcade
{
    public class BaseGameObject
    {

        public int row;
        public int column;
        public string symbol;

        public bool remove = false;

        public TimeSpan updateInterval;
        public DateTime lastUpdate;

        public TimeSpan lifeTime;
        public DateTime spawnedAt;

        public int directionRow = 0;
        public int directionColumn = 0;

        public BaseGameObject(int row, int column, string symbol, int updateInterval, int lifeTime = 0)
        {
            this.lastUpdate = DateTime.Now;
            this.spawnedAt = DateTime.Now;
            this.updateInterval = new TimeSpan(updateInterval);
            this.lifeTime = new TimeSpan(lifeTime);
            this.row = row;
            this.column = column;
            this.symbol = symbol;

        }
    }

    public class Vanity : BaseGameObject
    {
        public Vanity(int row, int column, string symbol, int lifeTime, int updateInterval = 0) : base(row, column, symbol, updateInterval, lifeTime)
        {



        }
    }

    public class Threat : BaseGameObject
    {
        public Threat(int row, int column, string symbol, int updateInterval, int lifeTime = 0) : base(row, column, symbol, updateInterval, lifeTime)
        {

            

        }
    }

    public class PowerUp : BaseGameObject
    {
        public PowerUp(int row, int column, string symbol, int updateInterval, int lifeTime = 0) : base(row, column, symbol, updateInterval, lifeTime)
        {



        }
    }

    public class Missile : BaseGameObject
    {

        public Missile(int row, int column, string symbol, int updateInterval, int lifeTime = 0) : base(row, column, symbol, updateInterval, lifeTime)
        {
        }

        public void Launch(int directionRow = -1, int directionColumn = 0)
        {
            this.directionRow = directionRow;
            this.directionColumn = directionColumn;

            if(Program.currentDetail.LaunchSound != null)
            {
                try
                {

                    using (SoundPlayer saxPlayer = new SoundPlayer(Program.currentDetail.LaunchSound.PickRandom()))
                    {
                        saxPlayer?.Play();
                    }
                }
                catch { }
                
            }
        }
    }
}
