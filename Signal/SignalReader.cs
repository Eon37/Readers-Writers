using System.Threading;

namespace lab3 {
    public class SignalReader : Reader {
        public override void work()
        {

            while (!Program.isFinished) {
                Program.evFull.WaitOne();
                if(Program.isFinished) {
                    Program.evFull.Set();
                    return;
                }
                msgs.Add(Program.buffer);
                Program.evEmpty.Set();
                
            }
        }
    }
}