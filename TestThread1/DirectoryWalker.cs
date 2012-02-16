using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading;

// This is a data model for the UI (or any other subscriber) to be able to view asyncronusly 

namespace DirectroryWalker.Demo
{

    public class DirectoryWalker 
    {
        const int TEN_SECONDS = 10 * 1000;
        const int FAKE_LOAD = TEN_SECONDS;
        const int MAX_LEVELS = 4;

        string _path; 
        int _levels;
        bool _inProgress;
        bool _halt;
        int _delay;

        public delegate void HandleEventView(object sender, ViewInfoEventArgs e);

        public event HandleEventView OnDiscoverDir;


        // Syncronus call
        public void DirTree(string path, int levels)  
        {
            _path = path;
            _levels = levels;
            if (_levels < 1)
            {
                Console.WriteLine("nothing to do");
                return;
            }
            
            // TODO implement level recussion and checks. 

            string[] directories = Directory.GetDirectories(_path);
            int count = directories.Length;
            foreach (string newDir in directories)
            {
                Thread.Sleep(300);

                if (OnDiscoverDir != null)
                {
                    OnDiscoverDir(this, new ViewInfoEventArgs(newDir, path, count));
                }
                Console.WriteLine("> {0}", newDir);
                count--;
            }
        }

        public void AsyncGetDirTree()
        {
            if (_levels < 1 || _path == ""){
                throw new ArgumentException("Path or levels must be set before calling this.");
            }
            if (_inProgress)
            {
                throw new SystemException("Query in progress.");
            }
            _inProgress = true;
            _halt = false;

            DirTree_r1(_path, _levels);

            //string[] directories = Directory.GetDirectories(_path);
            //int count = directories.Length; 

            //foreach (string newDir in directories)
            //{
            //    Thread.Sleep(_delay);
            //    if (OnDiscoverDir != null) 
            //    {
            //        OnDiscoverDir(this, new ViewInfoEventArgs(newDir, _path, count));
            //    }

            //    if (_halt)
            //    {
            //        break;
            //    }
                
            //    --count;
            //}

            

            _inProgress = false;
        }

        //returns true if request is in progress.
        public bool IsInProgress()
        {
            return _inProgress;
        }

        public void SetDelay(int delay)
        {
            _delay = delay;
        }

        // returns success
        public bool SetTargetPath(string path)
        {
            if (IsInProgress())
            {
                return false;
            }
            _path = path;
            return true;
        }

        // returns success
        public bool SetMaxLevel(int max_level)
        {
            if (IsInProgress())
            {
                return false;
            }

            _levels = max_level;
            return true;
        }

        public void Halt()
        {
            _halt = true;
        }


        // recussive function to decend directory tree
        public string DirTree_r1(string path, int max_depth)
        {

            if (max_depth == 0 || _halt == true)
            {
                return path;
            }
            string[] directories;
            try
            {
                directories = Directory.GetDirectories(path);
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine(path + " (access denied) ");
                return path;
            }

            foreach (string dir in directories)
            {
                this.DirTree_r1(dir, max_depth - 1);
            }

            if (OnDiscoverDir != null)
            {
                OnDiscoverDir(this, new ViewInfoEventArgs(path, path, max_depth));
            }
            Console.WriteLine(path + " depth = " + max_depth );
            return path;
   
        }

    }
}
