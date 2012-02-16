using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

// the timer will will clip or crawl in a random fashion to mimic a load.


namespace DirectroryWalker.Demo
{
    public class LazyTimer
    {

        private Thread _worker;
        private Random _rand;
       
        private int _tick_interval;
        private int _elapsed_ticks;
        private int _delay_ms; // this is how slugish each tick sould be
        private bool _stop;

        public delegate void OnAdvanceTick(Object timer, LazyTimerEventArgs e ); // setup my signature for the return to subscriber.

        public event OnAdvanceTick TickHandler;  // make my placeholder for subscriber references.

        public LazyTimer(int max_delay)
        {
            Init(max_delay, 0);
        }

        public LazyTimer(int max_delay, int seed)
        {
            Init(max_delay, seed);
        }

        private void Init(int max_delay, int seed)
        {
            
            if (max_delay < 1)
            {
                throw new ArgumentException("max delay < 1 millisecond."); 
            }

            _stop = false;
            _elapsed_ticks = 0;
            _delay_ms = max_delay;
            _rand = seed == 0 ? new Random(): new Random(seed);
            _tick_interval = NextTickInterval();   

        }

        public int NextTickInterval()
        {
            return (int)(_rand.Next() % _delay_ms); 
        }


        // This is public for threading demonstrations only.
        // I would make this private and use the internal thread call in the start method to make this calss simpler to use.

        public void Run()  
            
        {
            _tick_interval = this.NextTickInterval();

            while ( true )
            {
                Thread.Sleep(_tick_interval);

                if (TickHandler != null)
                {
                    TickHandler(this, new LazyTimerEventArgs( _tick_interval, _elapsed_ticks));
                }

                _tick_interval = NextTickInterval();

                if (_stop)
                {
                    break;
                }
            }
        }

        public void ResetTo(int max_delay)
        {
            Init(max_delay, 0);    
        }

        public void ResetTo(int max_delay, int seed)
        {
            Init(max_delay, seed);
        }

        public void Start()
        {
            _worker = new Thread(new ThreadStart(this.Run));
            _worker.Start();
        }

        public void Stop()
        {
            _stop = true;   // halt the thread.
            if (_worker != null){
                _worker.Join(); // wait for worker to end.
                _worker = null;
            }
        }


    }
}
