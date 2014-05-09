﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realm2
{
    public class Main
    {
        public enum GameState
        {
            Main,
            GettingPlayerInfo,
            Dead,
            End
        }
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
            switch(gm)
            {
                case GameState.GettingPlayerInfo:
                    player.name = mainWindow.inputText.Text;
                    write("Welcome, ", "Black", true);
                    write(player.name, "CadetBlue", true);
                    write(" to Realm 2.", "Black");
                    cw.Show();
                    break;
            }
        }
        public void write(string input, string color)
        {
            mainWindow.mainText.AppendText(input, color);
        }
        public void write(string input, string color, bool sameLine)
        {
            mainWindow.mainText.AppendText(input, color, sameLine);
        }
    }
}
