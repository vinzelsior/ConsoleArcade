using System;
using System.Collections.Generic;
using System.IO;
using System.Media;
using System.Threading;

/* 
 * This is the ConsoleArcade, the worst game made in just about a day.
 * On windows, it can make sounds, but can't display emojies, but will flicker like theres no tomorrow.
 * On Mac, it works pretty well, apart from the lacking sound.
 * 
 * Today's Motto:
 * 
 * WHO NEEDS COMMENTS?
 * Bonus: WE LOVE STATIC FIELDS!
 */


namespace ConsoleArcade
{
    class Program
    {
        // row = line
        public static int maxRows = 10;
        // the spaces
        public static int maxColumns = 40;

        public static int score = 0;
        public static int ammo = 5;

        public static Screen.Detail currentDetail;
        public static string filler;

        public static int chargeIncrements = 0;
        public static int maxCharges = maxColumns;
        public static int maxChargeTime = 4_500_000;

        static void DrawSeveralInLine(List<MovableObject> objects, List<VanityObject> vanities)
        {

            string newLine = "";
           
            for (int i = 0; i < maxColumns; i++)
            {
                MovableObject obj = objects.Find(o => o.column == i);

                VanityObject vans = vanities.Find(o => o.column == i);

                if (obj != null)
                {
                    newLine += obj.symbol;
                    continue;
                }
                if (vans != null)
                {
                    newLine += vans.symbol;
                    continue;
                }
                else
                {
                    newLine += filler;
                }
            }

            Console.WriteLine(newLine);
        }

        static List<VanityObject> GenerateVanitiesFromString(string text)
        {

            List<VanityObject> vs = new List<VanityObject>();

            int clm = (maxColumns - text.Length) / 2;
            int rw = maxRows / 2;

            for (int i = 0; i < text.Length; i++)
            {
                VanityObject v = new VanityObject(rw, clm, text[i].ToString(), new TimeSpan(20_000_000));

                vs.Add(v);

                clm++;
            }

            return vs;
        }

        static void DrawScore()
        {

            string scr = $"Score: {score}\t\tLevel: {Spawner.level}";

            string buffer = "";

            for (int i = 0; i < maxColumns * filler.Length; i++)
            {
                buffer += "_";
            }
            
            Console.WriteLine(buffer);

            Console.WriteLine();

            Console.WriteLine(scr);
        }

        static void DrawAmmo()
        {

            string amm = $"{currentDetail.pwrUpName}: {ammo}";

            Console.WriteLine(amm);
            Console.WriteLine();
        }

        static void DrawCharge()
        {
            string chrg = "";

            int incrementMod = chargeIncrements;

            if (currentDetail.charge.Length > 2)
            {
                incrementMod = incrementMod / 2;
            }
            
            for (int i = 0; i < incrementMod; i++)
            {
                chrg += currentDetail.charge;
            }
            Console.WriteLine("");
            Console.WriteLine(chrg);
        }

        static void Main(string[] args)
        {
            Console.CursorVisible = false;

        // from goto
        start:

            TimeSpan chargeTime = new TimeSpan(maxCharges/maxChargeTime);
            DateTime chargeBegan = DateTime.Now;

            bool chargeReady = false;
            
            Screen screen = new Screen();

            bool chargePreparing = false;


            Console.WriteLine("------------------------------------------");
            Console.WriteLine("Console Arcade - Project by Cedric Zwahlen");
            Console.WriteLine("------------------------------------------");
            Console.WriteLine("");
            Console.WriteLine("Press '⌘+⇧+1' to Resize the Window.");
            Console.WriteLine("");
            Console.WriteLine("Please Choose a Look by Pressing the Associated Number.");

            int cntr = 0;

            foreach (Screen.Detail s in screen.screens)
            {
                Console.WriteLine("");
                Console.WriteLine($"{cntr}:\t{s.name}  {s.cursor}");
                cntr++;
            }

            ConsoleKey key = Console.ReadKey(true).Key;

            try {

                int index = int.Parse(key.ToString().Replace("D", ""));

                currentDetail = screen.screens[index];
            } catch 
            {
                currentDetail = screen.screens[0];
            }

            Console.BackgroundColor = currentDetail.backgroundColor;
            Console.ForegroundColor = currentDetail.textColor;

            Spawner.symbols = currentDetail.foes;
            filler = currentDetail.filler;


            Console.Clear();

            // One million ticks = 1 sec / ticks not used in cursor
            MovableObject cursor = new MovableObject(maxRows, maxRows / 2, currentDetail.cursor, new TimeSpan(50_000_000));

            cursor.lastUpdate = DateTime.Now;

            List<MovableObject> movableObjects = new List<MovableObject>();

            List<VanityObject> vanityObjects = new List<VanityObject>();

            bool gameOver = false;

            bool started = false;
            
            do
            {

                if (!started)
                {
                    started = true;
                    vanityObjects.AddRange(GenerateVanitiesFromString("Start!"));
                }

                // updates the game

                while (Console.KeyAvailable == false && gameOver == false)
                {
                    Spawner.SpawnAutomatically(movableObjects);
                    
                    Console.SetCursorPosition(0, 0);

                    // updates the position of missiles first
                    List<MovableObject> mssls = movableObjects.FindAll(m => m is Missile);

                    foreach (MovableObject obj in mssls)
                    {

                        if (DateTime.Now > obj.lastUpdate + obj.updateInterval)
                        {
                            obj.lastUpdate = DateTime.Now;

                            obj.column = obj.column + obj.directionColumn;
                            obj.row = obj.row + obj.directionRow;
                        }
                    }

                    foreach (VanityObject obj in vanityObjects)
                    {
                        if (DateTime.Now > obj.spawnedAt + obj.lifeTime)
                        {
                            obj.remove = true;
                        }
                    }


                    // mark objects to be removed
                    for (int i = 0; i < movableObjects.Count; i++)
                    {
                        if (movableObjects[i].row > maxRows || movableObjects[i].row < 0)
                        {
                            //movableObjects.RemoveAt(i);
                            movableObjects[i].remove = true;
                        }

                        MovableObject collisionWithCursor = movableObjects.Find(o => o.column == cursor.column && o.row == cursor.row);

                        // if a collision with the cursor occurred
                        if (collisionWithCursor != null)
                        {

                            if (collisionWithCursor.isTreat)
                            {
                                ammo += 5;
                                collisionWithCursor.remove = true;
                                break;
                            }
                            else
                            {
                                gameOver = true;
                                break;
                            }
                        }

                        MovableObject mvblObj = movableObjects.Find(o => o.column == movableObjects[i].column && o.row == movableObjects[i].row);

                        if (mvblObj != movableObjects[i] && mvblObj != null)
                        {
                            mvblObj.remove = true;
                            movableObjects[i].remove = true;

                            if (mvblObj is Missile || movableObjects[i] is Missile)
                            {
                                score++;
                            }
                            
                            vanityObjects.Add(new VanityObject(mvblObj.row, mvblObj.column, currentDetail.explosion, new TimeSpan(2_000_000)));
                        }
                    }

                    // remove all marked items
                    List<MovableObject> ToRemoveMov = movableObjects.FindAll(m => m.remove == true);

                    foreach (MovableObject obj in ToRemoveMov)
                    {
                        movableObjects.Remove(obj);
                    }

                    List<VanityObject> ToRemoveVan = vanityObjects.FindAll(m => m.remove == true);

                    foreach (VanityObject obj in ToRemoveVan)
                    {
                        vanityObjects.Remove(obj);
                    }

                    // updates the position of non-missiles after
                    List<MovableObject> nonmssls = movableObjects.FindAll(m => !(m is Missile));

                    foreach (MovableObject obj in nonmssls)
                    {
                        if (DateTime.Now > obj.lastUpdate + obj.updateInterval)
                        {

                            obj.lastUpdate = DateTime.Now;

                            obj.column = obj.column + obj.directionColumn;
                            obj.row = obj.row + obj.directionRow;
                        }
                    }

                    DrawAmmo();

                    // draws the lines again
                    for (int i = 0; i <= maxRows; i++)
                    {
                        List<MovableObject> objectsInLine = movableObjects.FindAll(m => m.row == i);

                        List<VanityObject> vanitiesInLine = vanityObjects.FindAll(m => m.row == i);

                        if (i == cursor.row)
                        {
                            objectsInLine.Add(cursor);
                        }
                        
                        DrawSeveralInLine(objectsInLine, vanitiesInLine);
                    }

                    DrawCharge();
                    DrawScore();

                    Thread.Sleep(10);
                }


                if (gameOver)
                { break; }


                // reacts to user input
                key = Console.ReadKey(true).Key;

                if (key == ConsoleKey.UpArrow)
                {
                    // charge Vince's Grace (secret?)
                    if (!chargePreparing)
                    {
                        chargeBegan = DateTime.Now;
                        chargePreparing = true;
                    }

                    if (DateTime.Now > chargeBegan + chargeTime)
                    {
                        chargeIncrements++;
                        chargePreparing = false;
                        chargeBegan = DateTime.Now + new TimeSpan(1_000_000_000);
                    }

                    if (chargeIncrements >= maxCharges)
                    {
                        chargeReady = true;
                        chargePreparing = false;
                        chargeIncrements = maxCharges;
                    }
                }
                else
                {
                    chargePreparing = false;
                    chargeBegan = DateTime.Now + new TimeSpan(1_000_000_000);
                    chargeIncrements = chargeReady ? chargeIncrements : 0;
                }


                if (key == ConsoleKey.RightArrow)
                {
                    cursor.column = cursor.column >= maxColumns - 1 ? maxColumns - 1 : cursor.column + 1;
                }

                if (key == ConsoleKey.LeftArrow)
                {
                    cursor.column = cursor.column <= 0 ? 0 : cursor.column - 1;
                }

                if (key == ConsoleKey.Spacebar && ammo > 0)
                {
                        ammo -= 1;

                        Missile missile = new Missile(cursor.row - 1, cursor.column, currentDetail.projectile, new TimeSpan(1_000_000));
                        movableObjects.Add(missile);
                        missile.Launch();
                    
                }

                if (key == ConsoleKey.V && chargeReady)
                {


                    chargeReady = false;
                    chargeIncrements = 0;

                    Missile piece1 = new Missile(cursor.row - 1, cursor.column, currentDetail.projectile, new TimeSpan(1_000_000));
                    Missile piece2 = new Missile(cursor.row - 1, cursor.column + 1, currentDetail.projectile, new TimeSpan(1_000_000));
                    Missile piece3 = new Missile(cursor.row - 1, cursor.column - 1, currentDetail.projectile, new TimeSpan(1_000_000));

                    Missile piece4 = new Missile(cursor.row - 2, cursor.column, currentDetail.projectile, new TimeSpan(800_000));
                    Missile piece5 = new Missile(cursor.row - 1, cursor.column + 2, currentDetail.projectile, new TimeSpan(1_200_000));
                    Missile piece6 = new Missile(cursor.row - 1, cursor.column - 2, currentDetail.projectile, new TimeSpan(1_200_000));

                    Missile piece7 = new Missile(cursor.row - 1, cursor.column + 3, currentDetail.projectile, new TimeSpan(1_400_000));
                    Missile piece8 = new Missile(cursor.row - 1, cursor.column - 3, currentDetail.projectile, new TimeSpan(1_400_000));

                    movableObjects.AddRange(new List<MovableObject>() { piece1, piece2, piece3, piece4, piece5, piece6, piece8, piece7 });

                    piece1.Launch();
                    piece2.Launch();
                    piece3.Launch();

                    piece4.Launch();
                    piece5.Launch(-1, 1);
                    piece6.Launch(-1, -1);

                    piece7.Launch(0, 1);
                    piece8.Launch(0, -1);
                }


            } while (key != ConsoleKey.Escape);

            Console.Clear();

            Console.WriteLine();
            Console.WriteLine($"Game Over! - Your Score: {score}");
            Console.WriteLine();

            Console.ReadKey(true);

            Spawner.reset();

            ammo = 5;
            score = 0;

            movableObjects = new List<MovableObject>();

            goto start;
        }
    }
}
