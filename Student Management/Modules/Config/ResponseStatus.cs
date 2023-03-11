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
        private int responseCodeMatriculeExists = 40221;
        private int responseCodeNameExists = 40222;
        private int responseCodePasswordExists = 40223;
        private int responseCodeCreateUserSuccefly = 40224;
        private int responseCodeDeleteFormerAuthentificate = 40311;
        private int responseCodeDeleteFormer = 40310;
        private int responseCodeFormerIsUndefiened = 40312; 
        private int responseCodeFormerIsUpdatedSuccessfly = 40319;

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
        public int ResponseCodeMatriculeExists { get => responseCodeMatriculeExists; set => responseCodeMatriculeExists = value; }
        public int ResponseCodeNameExists { get => responseCodeNameExists; set => responseCodeNameExists = value; }
        public int ResponseCodePasswordExists { get => responseCodePasswordExists; set => responseCodePasswordExists = value; }
        public int ResponseCodeCreateUserSuccefly { get => responseCodeCreateUserSuccefly; set => responseCodeCreateUserSuccefly = value; }
        public int ResponseCodeDeleteFormerAuthentificate { get => responseCodeDeleteFormerAuthentificate; set => responseCodeDeleteFormerAuthentificate = value; }
        public int ResponseCodeDeleteFormer { get => responseCodeDeleteFormer; set => responseCodeDeleteFormer = value; }
        public int ResponseCodeFormerIsUndefiened { get => responseCodeFormerIsUndefiened; set => responseCodeFormerIsUndefiened = value; }
        public int ResponseCodeFormerIsUpdatedSuccessfly { get => responseCodeFormerIsUpdatedSuccessfly; set => responseCodeFormerIsUpdatedSuccessfly = value; }
    }
}
