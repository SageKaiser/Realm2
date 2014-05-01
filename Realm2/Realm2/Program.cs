using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;

namespace Realm2
{
    public static class Program
    {
        public static Main main;

        [DllImport("wininet.dll")]
        public extern static bool InternetGetConnectedState(out int Description, int ReservedValue);

        [STAThread]
        static void Main(string[] args)
        {
            Realm2.App app = new Realm2.App();
            app.InitializeComponent();
            app.Run();
        }
    }
}
