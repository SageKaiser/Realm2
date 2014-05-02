using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Realm2
{
    public class Init
    {
        public void Initialize(MainWindow window)
        {
            Program.main = new Main(window);
            window.Title = "Realm 2: " + GetTitle();
            if (!Program.noUpdate)
            {
                FileIO fi = new FileIO();
                if (fi.checkver())
                {
                    DownloadNewVersionWindow dnvw = new DownloadNewVersionWindow(window);
                    dnvw.Show();
                }
            }
            window.mainText.AppendText("Hello there. It looks like you're new to Realm 2.", "Black");
            window.mainText.AppendText("What is your name?", "CadetBlue");
            Program.main.gm = Main.GameState.AskingName;
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
            int result = Program.main.rand.Next(0, titlelist.Count);
            return titlelist[result];
        }
    }
}
