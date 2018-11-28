using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oneline
{
    public class TestOneline
    {
        Oneline one = new Oneline(4,4);

        public TestOneline()
        {
            //0 1 0 0
            //1 1 1 1
            //1 1 2 1

            one.SetStart(2,2);
            

            one.SetState(0,0,State.None);
            one.SetState(0,1,State.Avaliable);
            one.SetState(0,2,State.None);
            one.SetState(0,3,State.None);

            one.SetState(1,0, State.Avaliable);
            one.SetState(1, 1, State.Avaliable);
            one.SetState(1, 2, State.Avaliable);
            one.SetState(1, 3, State.Avaliable);

            one.SetState(2,0,State.Avaliable);
            one.SetState(2, 1, State.Avaliable);
            one.SetState(2, 2, State.Avaliable);
            one.SetState(2, 3, State.Avaliable);

            one.SetState(3, 0, State.None);
            one.SetState(3, 1, State.Avaliable);
            one.SetState(3, 2, State.Avaliable);
            one.SetState(3, 3, State.Avaliable);

            one.InitCaculate();
            one.Caculate();
        }
    }
}
