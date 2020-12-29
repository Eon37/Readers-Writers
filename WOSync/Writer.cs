namespace lab3{
    
    public class Writer : Role {

        private string name;
        protected int n;

        public Writer(string name, int n) {
            this.name = name;
            this.n = n;
            fillArray();
        }

        private void fillArray() {
            for(int i = 1; i <= n; i++) {
                msgs.Add(name + i);
            }
        }

        public override void work() {
            int i = 0;
            while(i < n) {
                if(Program.isBufferEmpty) {
                    Program.buffer = msgs[i++];
                    Program.isBufferEmpty = false;
                }
            }
        }
    }
}