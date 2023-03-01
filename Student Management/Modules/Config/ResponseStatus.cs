using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_Management.Modules.Config
{
    public class ResponseStatus
    {
        private int responseCodeIncorrectUserName = 40201;
        private int responseCodeLockedAccount = 40203;
        private int responseCodeIncorrectPassword = 40202;
        private int responseCodeAuth = 40200;
        private int responseCodeUserIsUndefiend = 40210;
        private int responseCodeUserIsAdmin = 40211;
        private int responseCodeUserIsNotAdmin = 40212;

        public ResponseStatus() 
        {
            //
        }

        public int ResponseCodeIncorrectUserName { get => responseCodeIncorrectUserName; set => responseCodeIncorrectUserName = value; }
        public int ResponseCodeLockedAccount { get => responseCodeLockedAccount; set => responseCodeLockedAccount = value; }
        public int ResponseCodeIncorrectPassword { get => responseCodeIncorrectPassword; set => responseCodeIncorrectPassword = value; }
        public int ResponseCodeAuth { get => responseCodeAuth; set => responseCodeAuth = value; }
        public int ResponseCodeUserIsUndefiend { get => responseCodeUserIsUndefiend; set => responseCodeUserIsUndefiend = value; }
        public int ResponseCodeUserIsAdmin { get => responseCodeUserIsAdmin; set => responseCodeUserIsAdmin = value; }
        public int ResponseCodeUserIsNotAdmin { get => responseCodeUserIsNotAdmin; set => responseCodeUserIsNotAdmin = value; }
    }
}
