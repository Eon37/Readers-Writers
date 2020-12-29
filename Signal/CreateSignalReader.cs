namespace lab3 {
    public class CreateSignalReader : ICreateReaders {
        Reader[] ICreateReaders.create(int n) {
            Reader[] r = new Reader[n];
            for(int i = 0; i < n; i++) {
                r[i] = new SignalReader();
            }

            return r;
        }
    }
}