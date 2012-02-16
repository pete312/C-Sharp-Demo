using System;
using System.Collections.Generic;
using System.Text;

namespace DirectroryWalker.Demo
{
    public class LazyTimerEventArgs : EventArgs
    {
        public  LazyTimerEventArgs(int tick_interval, int elapsed_ticks)
        {
            tickInterval = tick_interval;
            elapsedTicks = elapsed_ticks;
        }

        public readonly int tickInterval;
        public readonly int elapsedTicks;
    }
}
