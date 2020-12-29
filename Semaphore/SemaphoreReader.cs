using System.Threading;

namespace lab3 {
    public class SemaphoreReader : Reader {
        public override void work()
        {
            while (!Program.isFinished) {
                Program.semFull.Wait();
                if(Program.isFinished) {
                    Program.semFull.Release();
                    return;
                }
                msgs.Add(Program.buffer);
                Program.semEmpty.Release();
                
            }
        }
    }
}