using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using NUnit.Framework;

namespace TestThread1.tests 
{
    [TestFixture]
    class Walk 
    {
        [TestCase]
        public void Recussion()
        {
            Console.WriteLine( WalkDown("c:\\users") );
        }

        public string WalkDown(string path)
        {
            string[] directories ;
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
                this.WalkDown(dir);
            }
            Console.WriteLine(path);
            return path;

        }
    }
}
