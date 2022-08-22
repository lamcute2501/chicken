using DAL;
namespace BL {

    public class UserBL {
        
        private UserDAL usdal = new UserDAL();

        public bool CheckUserName(string userName){
            return usdal.CheckUserName(userName);
        }

        public bool CheckPassword(string userName, string password){
            return usdal.CheckPassword(userName,password);
        }

    }

}