namespace lab3 {
    public class LockWriter : Writer {
        public LockWriter(string name, int n) : base(name, n) {}

        public override void work()
        {
            int i = 0;
            while(i < n) {

                lock("write") {

                    if(Program.isBufferEmpty) {
                        Program.buffer = msgs[i++];
                        Program.isBufferEmpty = false;
                    }
                    
                }
            }
        }
    }
}