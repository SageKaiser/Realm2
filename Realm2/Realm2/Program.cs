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
        //make a public static instance of Main
        public static Main main;

        //noUpdate: doesn't check for an update; bypassConfirmation: doesn't ask if the user is sure
        public static bool noUpdate = false, bypassConfirmation = false;

        //random for everyone to use
        public static readonly Random random = new Random();

        //get required DLL for checking if the internet is connected
        [DllImport("wininet.dll")]
        public extern static bool InternetGetConnectedState(out int Description, int ReservedValue);

        static void Main(string[] args)
        {
            //checking for command-line arguments
            if (args.Contains("-noupdate"))
                noUpdate = true;
            if (args.Contains("-bypassconfirm"))
                bypassConfirmation = true;

            //make a new instance of the App
            Realm2.App app = new Realm2.App();
            //initialize it
            app.InitializeComponent();
            //run it
            app.Run();
        }
    }
}
