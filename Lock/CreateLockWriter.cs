namespace lab3 {
    public class CreateLockWriter : ICreateWriters {
        Writer[] ICreateWriters.create(int numOfWriters, int msgLen, int msgNum) {
            Writer[] w = new Writer[numOfWriters];
            for(char i ='A'; i < 'A' + numOfWriters; i++) {
                w[i - 'A'] = new LockWriter("".PadLeft(msgLen, i), msgNum);
            }

            return w;
        }
    }
}