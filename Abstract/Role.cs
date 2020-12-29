using System.Collections.Generic;

namespace lab3
{
    public abstract class Role {
        protected List<string> msgs = new List<string>(); 

        protected Role() {
        }     

        public abstract void work();
    }
}
