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
        public static int maxColumns = 30;

        public static int score = 0;
        public static int ammo = 5;

        public static Screen.Detail currentDetail;
        public static string filler;

        public static int chargeIncrements = 0;
        public static int maxCharges = maxColumns;
        public static int maxChargeTime = 4_500_000;

        static void DrawSeveralInLine(List<BaseGameObject> objects)
        {

            string newLine = "";
           
            for (int i = 0; i < maxColumns; i++)
            {
                BaseGameObject obj = objects.Find(o => o.column == i);

                if (obj != null)
                {
                    newLine += obj.symbol;
                }
                else
                {
                    newLine += filler;
                }
            }

            Console.WriteLine(newLine);
        }

        static List<Vanity> GenerateVanitiesFromString(string text)
        {

            List<Vanity> vs = new List<Vanity>();

            int clm = (maxColumns - text.Length) / 2;
            int rw = maxRows / 2;

            for (int i = 0; i < text.Length; i++)
            {
                Vanity v = new Vanity(rw, clm, text[i].ToString(), 20_000_000);

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
                incrementMod /= 2;
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

            Console.WriteLine("---------------------------------------------");
            Console.WriteLine("Console Arcade - Project by Vince and Nerovia");
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine();
            Console.WriteLine("Press '⌘+⇧+1' to Resize the Window.");
            Console.WriteLine();
            Console.WriteLine("Please Choose a Look by Pressing the Associated Number.");

            int cntr = 0;

            foreach (Screen.Detail s in screen.screens)
            {
                Console.WriteLine();
                Console.WriteLine($"{cntr}:\t{s.name}  {s.cursor}");
                cntr++;
            }

            Console.WriteLine();
            Console.WriteLine("Controls:\n - Move with ← →\n - Shoot with Spacebar\n - Charge Vince's Grace with ↑, and release it V");

            ConsoleKey key;

            while (true)
            {
                key = Console.ReadKey(true).Key;

                int number;
                if (int.TryParse(key.ToString().Remove(0, 1), out number))
                {
                    if (number < screen.screens.Count)
                    {
                        currentDetail = screen.screens[number];
                        break;
                    }
                }
            }

            Console.BackgroundColor = currentDetail.backgroundColor;
            Console.ForegroundColor = currentDetail.textColor;

            Spawner.symbols = currentDetail.foes;
            filler = currentDetail.filler;


            Console.Clear();

            // One million ticks = 1 sec / ticks not used in cursor
            BaseGameObject cursor = new BaseGameObject(maxRows, maxRows / 2, currentDetail.cursor, 50_000_000)
            {
                lastUpdate = DateTime.Now
            };

            List<BaseGameObject> baseGameObjects = new List<BaseGameObject>();

           //List<VanityObject> vanityObjects = new List<VanityObject>();

            // could be a statemachine

            bool gameOver = false;

            bool started = false;
            
            do
            {

                if (!started)
                {
                    started = true;
                    baseGameObjects.AddRange(GenerateVanitiesFromString("Start!"));
                }

                // updates the game

                while (Console.KeyAvailable == false && gameOver == false)
                {
                    Spawner.SpawnAutomatically(baseGameObjects);
                    
                    Console.SetCursorPosition(0, 0);

                    // updates the position of gamecontent
                    //List<BaseGameObject> mssls = ;

                    //List<BaseGameObject> vnts = ;

                    foreach (BaseGameObject obj in baseGameObjects)
                    {
                        if (DateTime.Now > obj.lastUpdate + obj.updateInterval)
                        {
                            obj.lastUpdate = DateTime.Now;

                            obj.column += obj.directionColumn;
                            obj.row += obj.directionRow;
                        }
                    }
                    /*
                    foreach (Missile obj in baseGameObjects.FindAll(m => m is Missile))
                    {

                        if (DateTime.Now > obj.lastUpdate + obj.updateInterval)
                        {
                            obj.lastUpdate = DateTime.Now;

                            obj.column = obj.column + obj.directionColumn;
                            obj.row = obj.row + obj.directionRow;
                        }
                    }
                    
                    foreach (Vanity obj in baseGameObjects.FindAll(m => m is Vanity))
                    {
                        
                    }
                    */

                    // mark objects to be removed
                    for (int i = 0; i < baseGameObjects.Count; i++)
                    {

                        // automatically mark as removable if the objects went out of bounds
                        if (baseGameObjects[i].row > maxRows || baseGameObjects[i].row < 0)
                        {
                            //movableObjects.RemoveAt(i);
                            baseGameObjects[i].remove = true;
                        }

                        // remove vanity objects after they expire
                        if (baseGameObjects[i] is Vanity)
                        {
                            if (DateTime.Now > baseGameObjects[i].spawnedAt + baseGameObjects[i].lifeTime)
                            {
                                baseGameObjects[i].remove = true;
                            }
                        }

                        // check if a collision with the cursor occurred

                        BaseGameObject collisionWithCursor = baseGameObjects.Find(o => o.column == cursor.column && o.row == cursor.row);

                        
                        if (collisionWithCursor != null)
                        {

                            if (collisionWithCursor is PowerUp)
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

                        // check if a collision with two different objects occurred

                        BaseGameObject mvblObj = baseGameObjects.Find(o => o.column == baseGameObjects[i].column && o.row == baseGameObjects[i].row && !(o is Vanity));

                        if (mvblObj != baseGameObjects[i] && mvblObj != null && !(baseGameObjects[i] is Vanity))
                        {
                            mvblObj.remove = true;
                            baseGameObjects[i].remove = true;

                            // if a missile was part of the collision, increase the score
                            if (mvblObj is Missile || baseGameObjects[i] is Missile)
                            {
                                score++;
                            }

                            // add an explosion effect
                            baseGameObjects.Add(new Vanity(mvblObj.row, mvblObj.column, currentDetail.explosion, 2_000_000));
                        }
                    }

                    // remove all marked items
                    //List<BaseGameObject> ToRemoveMov = ;

                    foreach (BaseGameObject obj in baseGameObjects.FindAll(m => m.remove == true))
                    {
                        baseGameObjects.Remove(obj);
                    }

                    
                    /*
                    // updates the position of non-missiles after
                    List<BaseGameObject> nonmssls = baseGameObjects.FindAll(m => !(m is Missile));

                    foreach (BaseGameObject obj in nonmssls)
                    {
                        if (DateTime.Now > obj.lastUpdate + obj.updateInterval)
                        {

                            obj.lastUpdate = DateTime.Now;

                            obj.column = obj.column + obj.directionColumn;
                            obj.row = obj.row + obj.directionRow;
                        }
                    }
                    */
                    DrawAmmo();

                    // draws the lines again
                    for (int i = 0; i <= maxRows; i++)
                    {
                        List<BaseGameObject> objectsInLine = baseGameObjects.FindAll(m => m.row == i);

                        if (i == cursor.row)
                        {
                            objectsInLine.Add(cursor);
                        }
                        
                        DrawSeveralInLine(objectsInLine);
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

                        Missile missile = new Missile(cursor.row - 1, cursor.column, currentDetail.projectile, 1_000_000);
                        baseGameObjects.Add(missile);
                        missile.Launch();
                    
                }

                if (key == ConsoleKey.V && chargeReady)
                {


                    chargeReady = false;
                    chargeIncrements = 0;

                    Missile piece1 = new Missile(cursor.row - 1, cursor.column, currentDetail.projectile, 1_000_000);
                    Missile piece2 = new Missile(cursor.row - 1, cursor.column + 1, currentDetail.projectile, 1_000_000);
                    Missile piece3 = new Missile(cursor.row - 1, cursor.column - 1, currentDetail.projectile, 1_000_000);

                    Missile piece4 = new Missile(cursor.row - 2, cursor.column, currentDetail.projectile, 800_000);
                    Missile piece5 = new Missile(cursor.row - 1, cursor.column + 2, currentDetail.projectile, 1_200_000);
                    Missile piece6 = new Missile(cursor.row - 1, cursor.column - 2, currentDetail.projectile, 1_200_000);

                    Missile piece7 = new Missile(cursor.row - 1, cursor.column + 3, currentDetail.projectile, 1_400_000);
                    Missile piece8 = new Missile(cursor.row - 1, cursor.column - 3, currentDetail.projectile, 1_400_000);

                    baseGameObjects.AddRange(new List<BaseGameObject>() { piece1, piece2, piece3, piece4, piece5, piece6, piece8, piece7 });

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

            Spawner.Reset();

            ammo = 5;
            score = 0;

            baseGameObjects = new List<BaseGameObject>();

            goto start;
        }
    }
}
