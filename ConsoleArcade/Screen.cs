using System;
using System.Collections.Generic;

namespace ConsoleArcade
{
    public class Screen
    {

        public class Detail
        {
            public List<string> foes;
            public string cursor;
            public string projectile;
            public string powerUp;
            public string explosion;
            public string name;
            public string pwrUpName;
            public List<string> LaunchSound;
            public ConsoleColor textColor;
            public ConsoleColor backgroundColor;
            public string filler;
            public string charge;
        }

        public List<Detail> screens = new List<Detail>();

        public Screen()
        {

            Detail standard = new Detail()
            {
                foes = new List<string>()
                {
                    "🍎", "🥑", "🍅", "🥕", "🍏", "🍍", "🍇", "🍓", "🥭"
                },

                cursor = "🍤",
                explosion = "💥",
                powerUp = "🧿",
                projectile = "⚡️",
                name = "Shrimp!",
                pwrUpName = "Nazaars Wisdom",
                textColor = ConsoleColor.Black,
                backgroundColor = ConsoleColor.White,
                filler = "  ",
                charge = " 🍭 "
            };

            Detail nature = new Detail()
            {
                foes = new List<string>()
                {
                    "🍀", "🌿", "🍃", "🌾", "🌱", "🌺","🌷","🌸","🌹","🌻","🌼"
                },

                cursor = "🐿",
                powerUp = "🦄",
                explosion = "🍂",
                projectile = "🥜",
                name = "Nature? Why Yes!",
                pwrUpName = "NUTS",
                backgroundColor = ConsoleColor.Black,
                textColor = ConsoleColor.DarkGreen,
                filler = "  ",
                charge = " 🦆 "
            };

            Detail universe = new Detail()
            {
                foes = new List<string>()
                {
                    "🌝", "🌛", "🌜", "🌚", "🌕", "🌖", "🌗", "🌘","🌑","🌒","🌓","🌔","🌙","🌎","🌍","🌏","🪐"
                },

                cursor = "🌞",
                powerUp = "⭐️",
                explosion = "🌟",
                projectile = "✨",
                name = "Binary Sunset",
                pwrUpName = "STARZ",
                backgroundColor = ConsoleColor.Black,
                textColor = ConsoleColor.Yellow,
                filler = "⬛️",
                charge = " 🌠 "
            };


            Detail alain = new Detail()
            {
                foes = new List<string>()
                {
                    "🍅","🌽","🐄","🐖","🐂","🐓","👩‍🌾","👨‍🌾","🤖"
                },
                cursor = "🛸",
                powerUp = "🎷",
                explosion = "💖",
                projectile = "🎶",
                name = "Alain's Special Remix",
                pwrUpName = "Saxmimum Power",
                backgroundColor = ConsoleColor.Green,
                textColor = ConsoleColor.Red,
                filler = "  ",
                charge = " 🐽 ",
            LaunchSound = new List<string>()
                {
                    @"C:\Users\alain\Documents\GitHub\Nerovia\ConsoleArcade\ConsoleArcade\Sounds\Sax1.wav",
                    @"C:\Users\alain\Documents\GitHub\Nerovia\ConsoleArcade\ConsoleArcade\Sounds\Sax2.wav",
                    @"C:\Users\alain\Documents\GitHub\Nerovia\ConsoleArcade\ConsoleArcade\Sounds\Sax3.wav",
                    @"C:\Users\alain\Documents\GitHub\Nerovia\ConsoleArcade\ConsoleArcade\Sounds\Sax4.wav",
                    @"C:\Users\alain\Documents\GitHub\Nerovia\ConsoleArcade\ConsoleArcade\Sounds\Sax6.wav",
                    @"C:\Users\alain\Documents\GitHub\Nerovia\ConsoleArcade\ConsoleArcade\Sounds\Sax7.wav",
                    @"C:\Users\alain\Documents\GitHub\Nerovia\ConsoleArcade\ConsoleArcade\Sounds\Sax8.wav",
                    @"C:\Users\alain\Documents\GitHub\Nerovia\ConsoleArcade\ConsoleArcade\Sounds\Sax9.wav",
                },
            };
            

            Detail ascii = new Detail()
            {
                foes = new List<string>()
                {
                    "o", "0", "Q", "°", "O", "ö", "Ö"
                },

                cursor = "^",
                powerUp = "@",
                explosion = "X",
                projectile = "!",
                name = "Ascii for Windows",
                pwrUpName = "Crashes",
                LaunchSound = alain.LaunchSound,
                backgroundColor = ConsoleColor.Black,
                textColor = ConsoleColor.White,
                filler = " ",
                charge = "+",
            };

            Detail cats = new Detail()
            {
                foes = new List<string>()
                {
                    "😼", "😽", "🙀", "😿", "😾", "😸", "😺", "😹"
                },
                cursor = "🤷‍♀️",
                projectile = "🧶",
                explosion = "😻",
                powerUp = "📦",
                name = "Jellicle Cats",
                pwrUpName = "Yarn",
                backgroundColor = ConsoleColor.Black,
                textColor = ConsoleColor.DarkYellow,
                filler = "  ",
                charge = " 🐱 ",
            };

            Detail shapes = new Detail()
            {
                foes = new List<string>()
                {
                    "🟥", "🟧", "🟨", "🟩", "🟦", "🟪"
                },
                cursor = "↔️",
                projectile = "🔺",
                explosion = "🌀",
                powerUp = "♦️",
                name = "Shapes and More",
                pwrUpName = "Cölor? No More!",
                backgroundColor = ConsoleColor.White,
                textColor = ConsoleColor.Gray,
                filler = "  ",
                charge = " 🔷 ",
            };

            screens.AddRange(new List<Detail>() { standard, alain, ascii, cats, nature, universe, shapes } );
        }

        // could be used to add custom ones with JSON
        public void resolve()
        {

        }
    }
}
