using System;

namespace lab3{
    public class Reader : Role {

        public override void work() {
            while (!Program.isFinished) {
                if (!Program.isBufferEmpty) {
                    msgs.Add(Program.buffer);
                    Program.isBufferEmpty = true;
                }
            }
        }

        public void printMessages() {
            foreach(string s in msgs) {
                Console.WriteLine(s);
            }
        }
    }
}