using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace i01dew.Utility
{
    public static class Random
    {
        public static string Name()
        {
            string[] names = {
                "Mustard", "Sleepy", "Dopey", "Grumpy", "Mopey", "Sketch", "Crazy", "Saucy", "Wheezy", "Kitty",
                "Killer", "Steak", "Butcher", "Darling", "Disco", "Donut", "Cotton", "Stank", "Ratjar", "Noodle",
                "Weasel", "Snake", "Shadow", "Caboose", "King", "Sneak", "The Bear", "Ghost", "Penguin", "Coward",
                "Moogley", "Doggy Dog", "Potato", "Wilshire", "Pirate", "Wallace", "Tooth", "Goat", "Stumpy", "Stomp",
                "Winky", "Ling-ling", "Hippo", "Turtle", "Moose", "Derby", "Wiggly", "Bongo", "Skip", "Bork",
                "Lemon", "Hermit", "Ralfie", "Cheese", "Duke", "Badger", "Beagle", "Wort", "Wiggler", "Soffish",
                "Yodler", "Geronimo", "Darkman", "Snoozy", "Shaggy", "Banana", "Shiz", "Giant", "Slinky", "Pretzel",
                "Brutus", "Pinky", "Milo", "Monkey", "Bozo", "Grunty", "Bower", "Floss", "Nappy", "Gerbil",
                "Goose", "Scruffy", "Soap", "Polo", "Muffin", "Beast", "Clubby", "Surly", "Balin", "Poodle",
                "Snaz", "Jungle Jim", "Spork", "Grub", "Snook", "Spleen", "Pumpkin", "Custard",
                "Cracky", "Pickle", "Slim", "Lucky", "Pudding"
            };

            return names[UnityEngine.Random.Range(0, names.Length - 1)];
        }

        public static string String(int stringLength)
        {
            return String("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789", stringLength);
        }

        public static string String(string stringCharacters, int stringLength)
        {
            if (stringCharacters.Length < 1)
                return "";

            List<char> randomCharacters = new List<char>();
            System.Random random = new System.Random();

            for (int i = 0; i < stringLength; i++)
            {
                randomCharacters.Add(stringCharacters[random.Next(stringCharacters.Length - 1)]);
            }

            string randomString = "";

            foreach(char randomCharacter in randomCharacters)
            {
                randomString += randomCharacter;
            }

            return randomString;
        }
    }
}
