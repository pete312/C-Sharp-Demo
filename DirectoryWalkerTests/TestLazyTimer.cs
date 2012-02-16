using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using TestThread1;
using NUnit.Framework;
using DirectroryWalker.Demo;

namespace DirectoryWalkerDemo.Tests.LazyTimerClass
{
    
    [TestFixture]
    public class TestLazyTimer
    {
        const int ONE_SECOND = 1000;
        const int TWO_SECONDS = ONE_SECOND * 2;
        const int FIVE_SECONDS = ONE_SECOND * 5;
        const int TENTH_OF_SEC = 100;
        const int TEN_MILLISEC = 10;


        [Test]
        public void Test_Throws_Exception()
        {
            Assert.Throws<ArgumentException>(delegate { new LazyTimer(0); });
            Assert.Throws<ArgumentException>(delegate { new LazyTimer(-1); });
        }

        [Test]
        public void Check_NextRand_limits()
        {

            LazyTimer timer = new LazyTimer(TENTH_OF_SEC);
            int first = timer.NextTickInterval();
            Assert.AreNotEqual(first, timer.NextTickInterval());
            for (int i = 0; i < 2000; i++)
            {
                Assert.LessOrEqual(timer.NextTickInterval(), TENTH_OF_SEC);
                Assert.GreaterOrEqual(timer.NextTickInterval(), 0);

            }
        }


        [Test]
        public void Run_With_External_Thread()
        {

            TimerTester timed_test = new TimerTester("External thread test"); // need this for a subscriber for ticks.
            LazyTimer t1 = new LazyTimer(TENTH_OF_SEC);
            timed_test.Subscribe(t1);
            // Normally I would keep Run private. This is just for show.
            Thread worker = new Thread(new ThreadStart(t1.Run));
            worker.Start();
            Thread.Sleep(TWO_SECONDS);
            t1.Stop();
            worker.Join();
            
            
            Assert.True( timed_test.HasTirggered() );
            Assert.GreaterOrEqual(timed_test.GetTriggerCount(), 39); // we should have at least 2000 / 10 = 40

            Console.WriteLine("------------------Resetting---------------------"); // a visual

            timed_test.Reset(); // reset the test helper's count and triggered flag.

            worker = new Thread(new ThreadStart(t1.Run));
            t1.ResetTo(TEN_MILLISEC);
            worker.Start();
            Thread.Sleep(TWO_SECONDS);
            t1.Stop();
            worker.Join();

            Assert.True(timed_test.HasTirggered());

            // Was going to put a binominal 
            Assert.GreaterOrEqual(timed_test.GetTriggerCount(), (TWO_SECONDS / TEN_MILLISEC)); // we should have at least 2000 / 10 = 400 
        }

        [Test]
        public void Internal_Thread_Start_Stop_test()
        {
            LazyTimer lazybones = new LazyTimer(ONE_SECOND);
            TimerTester evaluator = new TimerTester("Internal thread test"); // need this for a subscriber for ticks.
            evaluator.Subscribe(lazybones);

            Assert.IsFalse(evaluator.HasTirggered());

            // make sure our internal thread works.
            lazybones.Start();
            Thread.Sleep(ONE_SECOND);
            lazybones.Stop();

            Assert.IsTrue(evaluator.HasTirggered());

        }

        [Test]
        public void Test_Multi_Subscribers()
        {
            LazyTimer lazybones = new LazyTimer(ONE_SECOND);
            TimerTester evaluator1 = new TimerTester("Subscriber 1");
            TimerTester evaluator2 = new TimerTester("Subscriber 2"); 
            evaluator1.Subscribe(lazybones);
            evaluator2.Subscribe(lazybones);

            Assert.IsFalse(evaluator1.HasTirggered());
            Assert.IsFalse(evaluator2.HasTirggered());

            // make sure our internal thread works.
            lazybones.Start();
            Thread.Sleep(ONE_SECOND);
            lazybones.Stop();

            Assert.IsTrue(evaluator1.HasTirggered());
            Assert.IsTrue(evaluator2.HasTirggered());
        }

        [Test]
        public void Test_Unsubscribe()
        {
            // TODO implement Unsubscribe
        }


    }
}
