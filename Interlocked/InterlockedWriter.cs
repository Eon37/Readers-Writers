using System.Threading;
using System;

namespace lab3 {
    public class InterlockedWriter : Writer {
        public InterlockedWriter(string name, int n) : base(name, n) {}

        public override void work()
        {
            int i = 0;
            while(i < n) {
                if(Interlocked.CompareExchange(ref Program.cntEmpty, 0, 1) == 1) {
                    Program.buffer = msgs[i++];
                    Program.cntFull = 1;
                }
            }
        }
    }
}