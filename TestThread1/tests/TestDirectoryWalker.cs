using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.IO;
using NUnit.Framework;

namespace TestThread1.tests
{
    [TestFixture]
    class TestDirectoryWalker
    {
        const int TWO_SECONDS = 2000;
        bool _triggered;
        int _triggered_count;
        List<string> _directory_list;
        string _path;
       
        //[SetUpFixture]
        //public void Init()
        //{
        //    _directory_list.Clear();
        //    _triggered_count = 0;
        //    _triggered = false;
        //    _path = "c:/";
        //}
        
        [TestCase]
        public void Check_thread()
        {

            _directory_list = new List<string>();
            _triggered_count = 0;
            _triggered = false;
            _path = "c:/";

            DirectoryWalker dw = new DirectoryWalker();
            Assert.NotNull(dw); 

            // add Subscriber 
            dw.OnDiscoverDir += new DirectoryWalker.HandleEventView(this.DirWalkEventHandler);

            // set running condition
            dw.SetMaxLevel(2);
            dw.SetTargetPath("c:/users");

            Thread worker = new Thread(new ThreadStart(dw.AsyncGetDirTree));
            worker.Start();
            Thread.Sleep(100);

            
            Console.WriteLine("join thread");

            worker.Abort();
            //worker.Join();
            
        }


        [TestCase]
        public void Content_Check()
        {
            _directory_list = new List<string>();
            _triggered_count = 0;
            _triggered = false;
            string test_path = "c:/users";

            DirectoryWalker dw = new DirectoryWalker();
            Assert.NotNull(dw);

            // add Subscriber 
            dw.OnDiscoverDir += new DirectoryWalker.HandleEventView(this.DirWalkEventHandler);
            // set running condition
            dw.SetMaxLevel(2);
            dw.SetTargetPath(test_path);
            dw.SetDelay(0);

            
            string[] directory_sample = Directory.GetDirectories(test_path); 

            Thread worker = new Thread(new ThreadStart(dw.AsyncGetDirTree));
            worker.Start();
            Thread.Sleep(100);

            foreach ( string dir in directory_sample )
            {
               Assert.True( _directory_list.Contains(dir) );
            }

            Assert.AreEqual(directory_sample.Length, _directory_list.Count); 

        }


        [TestCase]
        public void Test_Walk()
        {
            string test_path = "c:\\users";

            DirectoryWalker dw = new DirectoryWalker();
            dw.OnDiscoverDir += new DirectoryWalker.HandleEventView(this.DirWalkEventHandler);
            Thread worker = new Thread(new ThreadStart(dw.AsyncGetDirTree));
            dw.SetMaxLevel(2);
            dw.SetTargetPath(test_path);
            
            worker.Start();

            //Thread.Sleep(5000);
            //Console.WriteLine ( dw.DirTree_r1("c:\\users", 5) );
            
        }

        public void DirWalkEventHandler(object sender, ViewInfoEventArgs e)
        {
            _triggered = true;
            ++_triggered_count;
            Console.WriteLine(" .. {0}", e.dirName);
            _directory_list.Add(e.dirName);
           
            Console.WriteLine(" Dir {0} contains {1}", e.path, e.dirName);
        }
    }
}
