using System.Threading;

namespace lab3 {
    public class InterlockedReader : Reader {
        public override void work()
        {
            while (!Program.isFinished) {                
                if (Interlocked.CompareExchange(ref Program.cntFull, 0, 1) == 1) {
                    msgs.Add(Program.buffer);
                    Program.cntEmpty = 1;
                }
            }
        }
    }
}