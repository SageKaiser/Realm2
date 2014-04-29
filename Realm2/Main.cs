using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Windows.Forms;
using System.IO;

namespace Realm2
{
    public static class Main
    {
        public enum GameState
        {
            Main,
            Battle,
            AwaitingInput,
            Backpack,
            AskingName,
            AskingClass,
            AskingRace,
            AskingPassword,
            Dead,
            AskingSkip,
        }
        public static int loop_number = 0, slimecounter = 0, goblincounter = 0, banditcounter = 0, drakecounter = 0, wkingcounter = 0, slibcounter = 0, forrestcounter = 0, libcounter = 0, centrallibcounter = 0, ramsaycounter = 0, magiccounter = 0, nlibcounter = 0, townfolkcounter = 0, nomadcounter = 0, minecounter = 0, frozencounter = 0, noobcounter = 0, gbooks = 0, intlbuff = 0, defbuff = 0, atkbuff = 0, spdbuff = 0;

        public static bool raven_dead = false, is_theif = false, wkingdead = false, is_typing = false, devmode = false, hasmap = false, achievements_disabled = false;

        public static GameState gm;

        public static Achievement ach = new Achievement();
        public static Player.GamePlayer Player = new Realm2.Player.GamePlayer();
        public static Map globals = new Map();

        public static Random rand = new Random();

        public static string version = "Version Number - 1.8.4.3";

        public static Form1 form;

        public static Dictionary<string, bool> achieve = new Dictionary<string, bool>();

        public static List<Item> MainItemList = new List<Item> {
new cardboard_armor(), new cardboard_shield(), new cardboard_sword(), new iron_band(), new iron_buckler(), new iron_lance(), new iron_mail(), new iron_rapier(), new wood_armor(), new wood_plank(), new wood_staff(), new fmBP(), new sonictee(), new slwscreen(), new plastic_ring(), new m_amulet(), new m_robes(), new m_staff(), new m_tome(), new bt_battleaxe(), new bt_greatsword(), new bt_longsword(), new bt_plate(), new blood_amulet(), new swifites(), new ice_amulet(), new ice_dagger(), new ice_shield(), new p_mail(), new p_shield(), new p_shortsword(), new goldcloth_cloak(), new a_amulet(), new a_mail(), new a_staff(), new tome(), new ds_amulet(), new ds_kite(), new ds_kris(), new ds_scale(), new sb_saber(), new sb_chain(), new sb_gauntlet(), new sb_shield()
        };

        public static void Tutorial()
        {
            List<string> racelist = new List<string> { "human", "elf", "rockman", "giant", "zephyr", "shade" }, classlist = new List<string> { "warrior", "paladin", "mage", "thief" }, secret = new List<string>();
            if (achieve["100slimes"])
                secret.Add("Slime");
            if (achieve["100goblins"])
                secret.Add("Goblin");
            if (achieve["100bandits"])
                secret.Add("Bandit");
            if (achieve["100drakes"])
                secret.Add("Drake");
            is_typing = true;
            Interface.type("Welcome, ");
            Interface.typeOnSameLine(Player.name, ConsoleColor.White);
            Interface.typeOnSameLine(", to Realm.");
            if (achieve["finalboss"])
                Player.backpack.Add(new nightbringer());
            if (!devmode)
            {
                Interface.type("Skip the tutorial?");
                if (Interface.readkey().KeyChar == 'n')
                {
                    Interface.type("To do anything in Realm, simply press one of the listed commands.");
                    Interface.type("If at any time, you wish to you view you stats, press 'v'");
                    Interface.type("Make sure to visit every library! They offer many valuable abilities as well as experience.");
                    Interface.type("When in combat, select an availible move. All damage is randomized. Mana is refilled after each fight.");
                    Interface.type("While in the backpack, simply select a number corresponding to an item. You may swap this item in or out. Make sure to equip an item once you pick it up!");
                    Interface.type("At any specified time, you may press x, then y. This will cause you to commit suicide.");
                    Interface.type("At any specified time, you may press #. Doing so will save the game.");
                }
                Interface.type("In Realm, every player selects a race. Each race gives its own bonuses. You may choose from Human, Elf, Rockman, Giant, Zephyr, or Shade.");
                if (achieve["100slimes"] || achieve["100goblins"] || achieve["100bandits"] || achieve["100drakes"])
                {
                    Interface.type("Secret Races: ", ConsoleColor.Yellow);
                    for (int i = 0; i < secret.Count; i++)
                    {
                        if (i > 1)
                            Interface.typeOnSameLine(", ", ConsoleColor.Yellow);
                        Interface.type(secret[i], ConsoleColor.Yellow);
                    }
                    foreach (string s in secret)
                    {
                        racelist.Add(s);
                    }
                }
                Interface.type("Please enter a race. ");
                is_typing = false;
                string race = Interface.readinput();
                while (!racelist.Contains(race))
                {
                    Interface.type("Invalid. Please try again. ");
                    race = Interface.readinput();
                }
                Player.race = race;
                Interface.type("You have selected " + Interface.ToUpperFirstLetter(Player.race) + ".", ConsoleColor.Magenta);
                Interface.type("Each player also has a class. You may choose from Warrior, Paladin, Mage, or Thief.");
                Interface.type("Please enter a class. ");
                string pclass = Interface.readinput();
                while (!classlist.Contains(pclass))
                {
                    Interface.type("Invalid. Please try again. ");
                    pclass = Interface.readinput();
                }
                Player.pclass = pclass;
                Interface.type("You have selected " + Interface.ToUpperFirstLetter(Player.pclass) + ".", ConsoleColor.Magenta);
                Interface.type("You are now ready to play Realm. Good luck!");
                Interface.type("Press any key to continue.", ConsoleColor.White);
                Interface.readkey();
                Map.PlayerPosition.x = 0;
                Map.PlayerPosition.y = 6;
                if (Player.race == "giant")
                    Player.hp = 15;
            }
            MainLoop();
        }
        public static void HandleInput(string input)
        {
            switch (gm)
            {
                case GameState.Main:
                    Place currPlace = Map.map[Map.PlayerPosition.x, Map.PlayerPosition.y];
                    form.PlaceHeading.Text = currPlace.name;
                    Interface.type(currPlace.desc);

                    Enemy enemy = new Enemy();
                    if (loop_number >= 1)
                    {
                        if (Combat.CheckBattle() && currPlace.getEnemyList() != null && !devmode)
                        {
                            enemy = currPlace.getEnemyList();
                            Combat.BattleLoop(enemy);
                        }
                    }
                    break;
                case GameState.AwaitingInput:
                    
                    break;
                case GameState.Battle:
                    break;
                case GameState.AskingName:
                    break;
                case GameState.AskingRace:
                    break;
                case GameState.AskingClass:
                    break;
                case GameState.AskingPassword:
                    break;
            }
            Tuple<int, int> xy = Map.CoordinatesOf(typeof(Nomad));
            Map.map[xy.Item1, xy.Item2] = new Place();
            Tuple<int, int> nextNomad = Map.getRandomBlankTile();
            Map.map[nextNomad.Item1, nextNomad.Item2] = new Nomad();

            foreach (Place p in Map.map)
                if (p.GetType() == typeof(Place))
                    p.is_npc_active = false;
            Tuple<int, int> randomTile = Map.getRandomBlankTile();
            if (rand.NextDouble() <= .1)
                Map.map[randomTile.Item1, randomTile.Item2].is_npc_active = true;
        }
        private void parse(string src)
        {
            List<string> Verbs = new List<string>() { "go", "fight", "interact" };

            List<string> go = new List<string>() { "t", "go", "travel", "move", "run" };
            List<string> fight = new List<string>() { "fight", "kill", "attack" };
            List<string> interact = new List<string>() { "talk", "interact", "visit" };
            List<string> read = new List<string>() { "read" };
            List<string> north = new List<string>() { "north", "n", "up" };
            List<string> east = new List<string>() { "east", "e", "right" };
            List<string> south = new List<string>() { "south", "s", "down" };
            List<string> west = new List<string>() { "west", "w", "left" };

            List<string> search = new List<string>() { "search", "look", "find" };

            string[] words = src.Split();

            
        }
    }
}
