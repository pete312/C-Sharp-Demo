using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace TestThread1.tests
{
    class TimerTester
    {
        private bool _triggered;
        private int _trigger_count;
        private string _name;

        public TimerTester(string name)
        {
            _name = name;
            _triggered = false;
            _trigger_count = 0;
        }

        public void Subscribe(LazyTimer timer)
        {
            timer.TickHandler += new LazyTimer.OnAdvanceTick(RecordEvent);
        }

        public void Unsubscribe()
        {
            // TODO  
        }

        public void Reset()
        {
            _trigger_count = 0;
            _triggered = false;
        }


        public bool HasTirggered() { return _triggered; }
        public int GetTriggerCount() { return _trigger_count; }

        public void RecordEvent(object sender, EventArgs e)
        {
            _triggered = true;
            ++_trigger_count;
            string ext;
            switch (_trigger_count)
            {
                case 1:
                    ext = "st";
                    break;
                case 2:
                    ext = "nd";
                    break;
                case 3:
                    ext = "rd";
                    break;
                default:
                    ext = "th";
                    break;
            }

            Console.WriteLine("{2} got a tick for the {0}{1} time", _trigger_count, ext, _name); // a visual


            
        }
    }
}
