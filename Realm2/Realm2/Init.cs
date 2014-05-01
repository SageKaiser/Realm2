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
            FileIO fi = new FileIO();
            if (fi.checkver())
            {
                DownloadNewVersionWindow dnvw = new DownloadNewVersionWindow(window);
                dnvw.Show();
            }
        }
    }
}
