using MySql.Data.MySqlClient;

namespace DAL {

    public class UserDAL {

        private MySqlConnection connection = DbConfig.GetConnection();
        private MySqlDataReader reader;
        string query;
        public UserDAL() {}

        public bool CheckUserName(string userName){
            bool result = false;
            try{
                connection.Open();
                query = @"select user_id from user where user_name = '" + userName + "';";
                MySqlCommand cmd = new MySqlCommand(query,connection);
                reader = cmd.ExecuteReader();
                if(reader.Read()){
                    result = true;
                }
                reader.Close();
            }
            catch(Exception e){
                Console.WriteLine(e.Message);
            }
            finally{
                connection.Close();
            }

            return result;
        }

        public bool CheckPassword(string userName,string password){
            bool result = false;
            try{
                connection.Open();
                query = @"select user_id from user where user_name = '" + userName + "' and user_password = '" + password + "';";
                MySqlCommand cmd = new MySqlCommand(query,connection);
                reader = cmd.ExecuteReader();
                if(reader.Read()){
                    result = true;
                }
                reader.Close();
            }
            catch{}
            finally{
                connection.Close();
            }

            return result;
        }

    }

}