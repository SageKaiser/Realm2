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

        public static bool noUpdate = false, bypassConfirmation = false;

        [DllImport("wininet.dll")]
        public extern static bool InternetGetConnectedState(out int Description, int ReservedValue);

        [STAThread]
        static void Main(string[] args)
        {
            if (args.Contains("-noupdate"))
                noUpdate = true;
            if (args.Contains("-bypassconfirm"))
                bypassConfirmation = true;
            Realm2.App app = new Realm2.App();
            app.InitializeComponent();
            app.Run();
        }
    }
}
