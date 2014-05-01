using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Realm2
{
    public class FileIO
    {
        private bool FileCompare(string file1, string file2)
        {
            int file1byte;
            int file2byte;
            FileStream fs1;
            FileStream fs2;
            if (file1 == file2)
            {
                return true;
            }
            File.SetAttributes(file1, FileAttributes.Normal);
            File.SetAttributes(file2, FileAttributes.Normal);
            fs1 = new FileStream(file1, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            fs2 = new FileStream(file2, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

            if (fs1.Length != fs2.Length)
            {
                fs1.Close();
                fs2.Close();
                return false;
            }
            do
            {
                file1byte = fs1.ReadByte();
                file2byte = fs2.ReadByte();
            }
            while ((file1byte == file2byte) && (file1byte != -1));
            fs1.Close();
            fs2.Close();
            return ((file1byte - file2byte) == 0);
        }
        private bool isConnected()
        {
            int desc;
            bool tf = Program.InternetGetConnectedState(out desc, 0);
            return tf;
        }
        public bool checkver()
        {
            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string exepath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\Realm2.exe";
            string temppath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\test.exe";
            if (isConnected())
            {
                startDownload();
                if (!FileCompare(exepath, temppath))
                {
                    return true;
                }
                else
                    return false;
            }
            else
                return false;
        }
        private void startDownload()
        {
            string temppath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\test.exe";
            WebClient client = new WebClient();
            client.DownloadFile((new Uri("https://dl.dropboxusercontent.com/u/83385592/Realm2.exe")), temppath);
        }
    }
}
