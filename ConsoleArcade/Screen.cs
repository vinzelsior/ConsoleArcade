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

            Detail alain = new Detail();
            alain.foes = new List<string>()
            {"🍅","🌽","🐄","🐖","🐂","🐓","👩‍🌾","👨‍🌾"};
            alain.cursor = "🛸";
            alain.powerUp = "🎷";
            alain.explosion = "💖";
            alain.projectile = "🎶";
            alain.name = "Alain's Special Remix";
            alain.pwrUpName = "Saxmimum Power";
            alain.LaunchSound = new List<string>()
            {
                    @"\Users\cedriczwahlen\Documents\GitHub\ConsoleArcade\ConsoleArcade\ConsoleArcade\Sounds\Sax1.wav",
                    @"\Users\cedriczwahlen\Documents\GitHub\ConsoleArcade\ConsoleArcade\ConsoleArcade\Sounds\Sax2.wav",
            };

            screens.AddRange(new List<Detail>() { standard, alain } );
        }

        // could be used to add custom ones with JSON
        public void resolve()
        {

        }
    }
}
