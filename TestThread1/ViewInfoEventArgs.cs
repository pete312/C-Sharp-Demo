using System;
using System.Collections.Generic;
using System.Text;

namespace DirectroryWalker.Demo
{
    public class ViewInfoEventArgs : EventArgs
    {
        public ViewInfoEventArgs(string directory_Name, string path, int count_Remaining)
        {
            this.dirName = directory_Name;
            this.path = path;
            this.countRemaining = count_Remaining;
        }

        public readonly string dirName;
        public readonly string path;
        public readonly int countRemaining;
    }
}
