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
        }

        public List<Detail> screens = new List<Detail>();

        public Screen()
        {

            Detail standard = new Detail();
            standard.foes = new List<string>()
            {"🍎","🥑","🍅","🥕","🍏","🍍","🍇","🍓"};
            standard.cursor = "🍤";
            standard.explosion = "💥";
            standard.powerUp = "🧿";
            standard.projectile = "⚡️";
            standard.name = "Standard";
            standard.pwrUpName = "Nazaars Wisdom";
            standard.textColor = ConsoleColor.Black;
            standard.backgroundColor = ConsoleColor.White;

            Detail nature = new Detail()
            {
                foes = new List<string>()
                {
                    "🍀", "🌿", "🍃", "🍂", "🌾", "🌱", "🍁", "🌺","🌷","🌸","🌹","🌻","🌼"
                },

                cursor = "🐿",
                powerUp = "🦄",
                explosion = "🟤",
                projectile = "🥜",
                name = "Nature? Why Yes!",
                pwrUpName = "NUTS",
                backgroundColor = ConsoleColor.Black,
                textColor = ConsoleColor.DarkGreen,
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
                name = "Binary Sunset, How The Time Flies...",
                pwrUpName = "STARZ",
                backgroundColor = ConsoleColor.Black,
                textColor = ConsoleColor.Yellow,
            };


            Detail alain = new Detail();
            alain.foes = new List<string>()
            {"🍅","🌽","🐄","🐖","🐂","🐓","👩‍🌾","👨‍🌾","🤖"};
            alain.cursor = "🛸";
            alain.powerUp = "🎷";
            alain.explosion = "💖";
            alain.projectile = "🎶";
            alain.name = "Alain's Special Remix";
            alain.pwrUpName = "Saxmimum Power";
            alain.backgroundColor = ConsoleColor.Green;
            alain.textColor = ConsoleColor.Red;
            alain.LaunchSound = new List<string>()
            {
                    @"C:\Users\alain\Documents\GitHub\Nerovia\ConsoleArcade\ConsoleArcade\Sounds\Sax1.wav",
                    @"C:\Users\alain\Documents\GitHub\Nerovia\ConsoleArcade\ConsoleArcade\Sounds\Sax2.wav",
                    @"C:\Users\alain\Documents\GitHub\Nerovia\ConsoleArcade\ConsoleArcade\Sounds\Sax3.wav",
                    @"C:\Users\alain\Documents\GitHub\Nerovia\ConsoleArcade\ConsoleArcade\Sounds\Sax4.wav",
                    @"C:\Users\alain\Documents\GitHub\Nerovia\ConsoleArcade\ConsoleArcade\Sounds\Sax6.wav",
                    @"C:\Users\alain\Documents\GitHub\Nerovia\ConsoleArcade\ConsoleArcade\Sounds\Sax7.wav",
                    @"C:\Users\alain\Documents\GitHub\Nerovia\ConsoleArcade\ConsoleArcade\Sounds\Sax8.wav",
                    @"C:\Users\alain\Documents\GitHub\Nerovia\ConsoleArcade\ConsoleArcade\Sounds\Sax9.wav",
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
            };

            screens.AddRange(new List<Detail>() { standard, alain, ascii, nature, universe } );
        }

        // could be used to add custom ones with JSON
        public void resolve()
        {

        }
    }
}
