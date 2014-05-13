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
        /// <summary>
        /// Checks if two files are the same.
        /// </summary>
        /// <param name="file1"></param>
        /// <param name="file2"></param>
        /// <returns></returns>
        private bool FileCompare(string file1, string file2)
        {
            int file1byte;
            int file2byte;
            FileStream fs1;
            FileStream fs2;

            // Determine if the same file was referenced two times.
            if (file1 == file2)
            {
                // Return true to indicate that the files are the same.
                return true;
            }

            // Open the two files.
            File.SetAttributes(file1, FileAttributes.Normal);
            File.SetAttributes(file2, FileAttributes.Normal);
            fs1 = new FileStream(file1, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            fs2 = new FileStream(file2, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

            // Check the file sizes. If they are not the same, the files 
            // are not the same.
            if (fs1.Length != fs2.Length)
            {
                // Close the file
                fs1.Close();
                fs2.Close();

                // Return false to indicate files are different
                return false;
            }

            // Read and compare a byte from each file until either a
            // non-matching set of bytes is found or until the end of
            // file1 is reached.
            do
            {
                // Read one byte from each file.
                file1byte = fs1.ReadByte();
                file2byte = fs2.ReadByte();
            }
            while ((file1byte == file2byte) && (file1byte != -1));

            // Close the files.
            fs1.Close();
            fs2.Close();

            // Return the success of the comparison. "file1byte" is 
            // equal to "file2byte" at this point only if the files are 
            // the same.
            return ((file1byte - file2byte) == 0);

        }
        /// <summary>
        /// Check if the user is connected to the internet.
        /// </summary>
        /// <returns>True if user is connected to the internet.</returns>
        private bool isConnected()
        {
            int desc;
            bool tf = Program.InternetGetConnectedState(out desc, 0);
            return tf;
        }
        /// <summary>
        /// Check if the game needs to update.
        /// </summary>
        /// <returns>True if the game needs to update.</returns>
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
        /// <summary>
        /// Start downloading the update file
        /// </summary>
        private void startDownload()
        {
            string temppath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\test.exe";
            WebClient client = new WebClient();
            client.DownloadFile((new Uri("https://dl.dropboxusercontent.com/u/83385592/Realm2.exe")), temppath);
        }
    }
}
