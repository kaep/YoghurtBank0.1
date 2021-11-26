using System;
using System.Collections.Generic;

namespace YoghurtBank.Data.Model {

    public class Supervisor : User {

        public ICollection<Idea> ideas { get; set; }

        public void PostIdea(Idea idea)
        {
            throw new NotImplementedException();
        }

        public void UpdateIdea()
        {
            throw new NotImplementedException();
        }
    }

}


