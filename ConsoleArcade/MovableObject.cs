using System;
using System.IO;
using System.Media;


namespace ConsoleArcade
{
    public class MovableObject
    {

        public int row;
        public int column;
        public string symbol;

        public bool remove = false;

        public bool isTreat = false;

        public TimeSpan updateInterval;
        public DateTime lastUpdate;

        public int directionRow = 0;
        public int directionColumn = 0;

        public MovableObject(int row, int column, string symbol, TimeSpan updateInterval)
        {
            this.lastUpdate = DateTime.Now;
            this.updateInterval = updateInterval;
            this.row = row;
            this.column = column;
            this.symbol = symbol;

        }
    }

   

    public class Missile : MovableObject
    {

        public Missile(int row, int column, string symbol, TimeSpan updateInterval) : base(row, column, symbol, updateInterval)
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
