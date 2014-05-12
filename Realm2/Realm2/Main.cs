using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realm2
{
    public enum GameState
    {
        Main,
        GettingPlayerInfo,
        SunPalace,
        Frozen,
        Dead
    }
    public class Main
    {
        public Random rand;
        public GameState gm;
        public Player player;
        public Map map;
        public List<Book> mainBookList;
        public List<Item> mainItemList;
        public List<PlayerClass> mainClassList;
        public List<Race> mainRaceList;
        public MainWindow mainWindow;

        private Enemy currentEnemy;
        private BackpackWindow bw;
        private ClassRaceChoiceWindow cw;
        private Place currentplace;

        public Main(MainWindow _mainWindow, BackpackWindow bpwindow, ClassRaceChoiceWindow crcwindow)
        {
            mainWindow = _mainWindow;
            bw = bpwindow;
            cw = crcwindow;
            player = new Player();
            rand = new Random();
            map = new Map(7);
            mainClassList = new List<PlayerClass>() { new Knight(), new Lancer(), new Brawler(), new Mage(), new Ranger(), new Rogue(), new BladeDancer(), new Assassin(), new DreadKnight(), new Jester() };
            mainRaceList = new List<Race>() { new Human(), new Elf(), new Dwarf(), new Orc(), new Lycanthrope(), new Halfdragon(), new Revenant(), new Djinn(), new Vampire(), new Demon() };
        }
        public void HandleInput(string input)
        {
            switch (gm)
            {
                case GameState.GettingPlayerInfo:
                    player.name = mainWindow.inputText.Text;
                    write("Welcome, ", "Black");
                    write(player.name, "MediumOrchid", true);
                    write(" to Realm 2.", "Black", true);
                    cw.Show();
                    mainWindow.IsEnabled = false;
                    break;
                case GameState.Main:
                    mainWindow.placeName.Text = "\r\n" + currentplace.name;
                    write(currentplace.desc, "Black");
                    writeStats();
                    break;
                case GameState.Dead:
                    Environment.Exit(0);
                    break;
                case GameState.Frozen:
                    break;
            }
            if (player.position != null)
                currentplace = map.getPlace(new Tuple<int, int>(player.position.x, player.position.y));
        }
        /// <summary>
        /// Write out the player's stats
        /// </summary>
        public void writeStats()
        {
            mainWindow.statText.Document.Blocks.Clear();
            mainWindow.statText.AppendText(player.name + "(" + player.pRace + ")", "Black", true);
            mainWindow.statText.AppendText("Level " + player.level + " " + player.pClass, "Black");
            mainWindow.statText.AppendText("HP: " + player.hp + "/" + player.maxhp, "Black");
            mainWindow.statText.AppendText("Mana: " + player.mana + "/" + player.maxmana, "Black");
            mainWindow.statText.AppendText("Attack: " + player.atk, "Black");
            mainWindow.statText.AppendText("Defense: " + player.def, "Black");
            mainWindow.statText.AppendText("Speed: " + player.spd, "Black");
            mainWindow.statText.AppendText("Intelligence: " + player.intl, "Black");
        }
        /// <summary>
        /// Write a string to the main text box
        /// </summary>
        /// <param name="input">The string to be written to the box</param>
        /// <param name="color">A color defined by the Color class</param>
        public void write(string input, string color)
        {
            mainWindow.mainText.AppendText(input, color);
        }
        /// <summary>
        /// Write a string to the main text box
        /// </summary>
        /// <param name="input">The string to be written to the box</param>
        /// <param name="color">A color defined by the Color class</param>
        /// <param name="sameLine">If true, the function will not write to the same line as the previous</param>
        public void write(string input, string color, bool sameLine)
        {
            mainWindow.mainText.AppendText(input, color, sameLine);
        }
        public void EnterCombat()
        {
            CombatWindow cw = new CombatWindow(currentEnemy);
            cw.Show();
        }
    }
}
