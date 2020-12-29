using System.Threading;

namespace lab3 {
    public class SemaphoreWriter : Writer {
        public SemaphoreWriter(string name, int n) : base(name, n) {}

        public override void work()
        {
            int i = 0;
            while(i < n) {
                Program.semEmpty.Wait();
                Program.buffer = msgs[i++];
                Program.semFull.Release();
            }
        }
    }
}