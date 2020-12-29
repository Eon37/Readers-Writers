namespace lab3 {
    public class LockReader : Reader {
        public override void work()
        {
            while (!Program.isFinished) {
                if (!Program.isBufferEmpty) {
                    lock("read") {
                        if (!Program.isBufferEmpty) {
                            msgs.Add(Program.buffer);
                            Program.isBufferEmpty = true;
                        }
                    }
                }
            }
        }
    }
}