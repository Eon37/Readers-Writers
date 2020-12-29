using System.Threading;

namespace lab3 {
    public class SignalWriter : Writer {
        public SignalWriter(string name, int n) : base(name, n) {}

        public override void work()
        {
            int i = 0;
            while(i < n) {
                Program.evEmpty.WaitOne();
                Program.buffer = msgs[i++];
                Program.evFull.Set();
            }
        }
    }
}