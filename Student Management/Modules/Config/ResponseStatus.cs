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
        private int responseCodeExistUserName = 40204;
        private int responseCodeCreateUser = 40205;
        public ResponseStatus() 
        {
            //
        }

        public int ResponseCodeIncorrectUserName { get => responseCodeIncorrectUserName; set => responseCodeIncorrectUserName = value; }
        public int ResponseCodeLockedAccount { get => responseCodeLockedAccount; set => responseCodeLockedAccount = value; }
        public int ResponseCodeIncorrectPassword { get => responseCodeIncorrectPassword; set => responseCodeIncorrectPassword = value; }
        public int ResponseCodeAuth { get => responseCodeAuth; set => responseCodeAuth = value; }
        public int ResponseCodeExistUserName { get => responseCodeExistUserName; set => responseCodeExistUserName = value; }
        public int ResponseCodeCreateUser { get => responseCodeCreateUser; set => responseCodeCreateUser = value; }
    }

    
}
