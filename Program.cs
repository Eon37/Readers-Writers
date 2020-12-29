using System.Threading;
using System;
using System.Diagnostics;

namespace lab3
{
    public class Program
    {
        static double[,] elapsedTime = new double[5,5];
        static int numOfTests = 4;
        static double[] tests = new double[numOfTests];

        public static bool isBufferEmpty = true;
        public static bool isFinished = false;

        public static AutoResetEvent evFull = new AutoResetEvent(false);
        public static AutoResetEvent evEmpty = new AutoResetEvent(true);
        public static SemaphoreSlim semFull = new SemaphoreSlim(0,1);
        public static SemaphoreSlim semEmpty = new SemaphoreSlim(1,1);

        public static int cntFull = 0;
        public static int cntEmpty = 1;

        public static string buffer;

        static void Main(string[] args) {
             ICreateWriters[] crwr = {new CreateWriter(), new CreateLockWriter(), new CreateSignalWriter(), new CreateSemaphoreWriter(), new CreateInterlockedWriter()};
             ICreateReaders[] crr = {new CreateReader(), new CreateLockReader(), new CreateSignalReader(), new CreateSemaphoreReader(), new CreateInterlockedReader()};

             calculate(crwr, crr);            
        }

        static void calculate(ICreateWriters[] crwrs, ICreateReaders[] crrs) {
            for (int j = 500; j <= 5000; j*=10) { //length
                for (int i = 100; i <= 10_000; i*=10) { //amount
                    Console.WriteLine("\nNumOfMessages = " + i + "; Messages length = " + j);
                    for (int k = 0; k < crwrs.Length; k++) {
                        Console.WriteLine(k == 0 ? "Without synchronisation" : crwrs[k].GetType().Name.Substring(6, crwrs[k].GetType().Name.Length - 6));
                        for(int a = 1; a <=5; a++) { //writers
                            for (int b = 1; b <= 5; b++) { //readers
                                for (int c = 0; c < numOfTests; c++) {
                                    annihilate();

                                    Writer[] writers = crwrs[k].create(a, j, i);
                                    Reader[] readers = crrs[k].create(b);

                                    tests[c] = run(writers, readers);

                                }

                                elapsedTime[a-1, b-1] = Math.Round(calcAvg(tests), 3);
                            }
                        }
                        printTime();
                    }
                }
            }
        }

        static double run(Writer[] writers, Reader[] readers) {
            Thread[] wthrds = new Thread[writers.Length];
            Thread[] rthrds = new Thread[readers.Length];

            Stopwatch sw = new Stopwatch();
            sw.Start();
            for(int i = 0; i < writers.Length; i++) {
                wthrds[i] = new Thread(writers[i].work);
                wthrds[i].Start();
            }

            for(int i = 0; i < readers.Length; i++) {
                rthrds[i] = new Thread(readers[i].work);
                rthrds[i].Start();
            }

            foreach(Thread thrd in wthrds) {
                thrd.Join();
            }
            isFinished = true;
            evFull.Set();
            semFull.Release();
            
            foreach(Thread thrd in rthrds) {
                thrd.Join();
            }
            sw.Stop();

            return sw.Elapsed.TotalMilliseconds;
        }

        static double calcAvg(double[] a) {
            for (int i = 2; i < a.Length; i++) {
                a[1] += a[i];
            }
            return a[1]/(a.Length - 1);
        }

        static void annihilate() {
            isBufferEmpty = true;
            isFinished = false;

            evFull = new AutoResetEvent(false);
            evEmpty = new AutoResetEvent(true);
            semFull = new SemaphoreSlim(0,1);
            semEmpty = new SemaphoreSlim(1,1);

            cntFull = 0;
            cntEmpty = 1;
        }

        static void printTime() {
            for (int i = 0; i <= elapsedTime.GetUpperBound(0); i++) {
                for (int j = 0; j <= elapsedTime.GetUpperBound(1); j++) {
                    Console.Write(elapsedTime[i,j].ToString().PadLeft(10, ' ') + "|");
                }
                Console.WriteLine();
            }
        }
    }
}
