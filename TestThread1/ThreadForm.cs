using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace TestThread1
{
    public partial class ThreadForm : Form
    {
        DirectoryWalker _dirWalker;

        Thread _dataModel;

        delegate void SetTextCallback(string text);
        delegate void SetProgressCallback(int total, int current);

        public ThreadForm()
        {
            InitializeComponent();
        }

        ~ThreadForm()
        {
            Console.WriteLine("halting.");
            if (_dirWalker != null && _dirWalker.IsInProgress())
            {
                _dirWalker.Halt();
                _dataModel.Join();
            }
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            Start_Directory_Walk();
        }

        private bool IsBaseDirValid()
        {
            return Directory.Exists(entryBaseDir.Text);
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            if (_dirWalker != null)
            {
                if (_dirWalker.IsInProgress())
                {
                    _dirWalker.Halt();
                }
                progressBar1.Style = ProgressBarStyle.Continuous;
            }
            Console.WriteLine("Im stopping");
        }

        private void Start_Directory_Walk()
        {
            Console.WriteLine("Im starting a dir walk request");

            if (_dirWalker != null)
            {
                if (_dirWalker.IsInProgress())
                {
                    //Console.WriteLine("operation in progress");
                    //TODO display MsgBox operation in progress
                    MessageBox.Show("Request in progress");
                    return;
                }
                else
                {
                    // the last request is finished. So reclaim this thread.
                    //_dataModel.Join();
                }
            }

            _dirWalker = new DirectoryWalker();


            // Setup subscribers - this could be wrapped into a _dirWalker.Subscribe method.
            _dirWalker.OnDiscoverDir += new DirectoryWalker.HandleEventView(UpdateDirEvent);
            _dirWalker.OnDiscoverDir += new DirectoryWalker.HandleEventView(UpdateProgressBar);

            // prep the obj


            if (!IsBaseDirValid())
            {
                MessageBox.Show("No such directory " + entryBaseDir.Text);
                return;
            }

            // we know entryBaseDir.Text is valid.
            _dirWalker.SetTargetPath(entryBaseDir.Text);
            _dirWalker.SetMaxLevel(10);

            progressBar1.Style = ProgressBarStyle.Marquee;

            // start the data model in a thread.
            _dataModel = new Thread(new ThreadStart(_dirWalker.AsyncGetDirTree));
            _dataModel.Start();
        }

        private void UpdateProgressBar(object sender, ViewInfoEventArgs e) 
        {
            int remain = e.countRemaining;
            Console.WriteLine("progress update" + e.countRemaining);
        }

        public void UpdateDirEvent(object sender, ViewInfoEventArgs e)
        {
            this.SetText(e.path + "/" + e.dirName);
        }


        // This SetText solution came from microsoft site. I found this out when I got an error from 
        // VS2005 about external threads updating the Form thread. 
        // However I found this to be unsafe too.
        private void SetText(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.Output.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.Output.AppendText(text + "\n");
            }
        }


        private void SetProgress(int total, int current)
        {
            if (this.progressBar1.InvokeRequired)
            {
                SetProgressCallback delg = new SetProgressCallback(SetProgress);
                //this.Invole(delg, new );
            }
        }


        private void entryBaseDir_KeyUp(object sender, KeyEventArgs e)
        {
            Console.WriteLine(e.KeyCode);
            if (e.KeyCode == System.Windows.Forms.Keys.Return)
            {
                Start_Directory_Walk();
            }
        }


    }
}