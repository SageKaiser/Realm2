using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Realm2
{
    public class Init
    {
        public void Initialize(MainWindow window, BackpackWindow bpwindow, ClassRaceChoiceWindow crcwindow, StatWindow swindow)
        {
            Program.main = new Main(window, bpwindow, crcwindow, swindow);
            bpwindow.itemBox.ItemsSource = Program.main.player.backpack;
            window.Title = "Realm 2: " + GetTitle();
            if (!Program.noUpdate)
            {
                //if the game needs an update show the update dialog
                FileIO fi = new FileIO();
                if (fi.checkver())
                {
                    DownloadNewVersionWindow dnvw = new DownloadNewVersionWindow(window);
                    dnvw.Show();
                }
            }
            string temppath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\test.exe";
            if (File.Exists(temppath))
                File.Delete(temppath);
            Program.main.write("Hello there. It looks like you're new to Realm 2.", "Black", true);
            Program.main.write("What is your name?", "SteelBlue");
            Program.main.player.backpack.Add(new Stick());
            Program.main.gm = GameState.GettingPlayerInfo;
        }
        private string GetTitle()
        {
            List<string> titlelist = new List<string>()
            {
                "Not what was expected",
                "Seriously, this was supposed to have graphics",
                "Oh well",
                "99.99% meme free",
                "Special thanks to Sweet Bro",
                "Dog you know I love the big game",
                "They don't know 'bout my wooden sword",
                "Trifling with johnathans since 2014",
                "Obama 2016",
                "muh freedoms"
            };
            int result = Program.random.Next(0, titlelist.Count);
            return titlelist[result];
        }
    }
}
