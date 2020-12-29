namespace lab3 {
    public class CreateInterlockedReader : ICreateReaders {
        Reader[] ICreateReaders.create(int n) {
            Reader[] r = new Reader[n];
            for(int i = 0; i < n; i++) {
                r[i] = new InterlockedReader();
            }

            return r;
        }
    }
}