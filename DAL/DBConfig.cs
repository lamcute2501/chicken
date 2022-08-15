using MySql.Data.MySqlClient;

namespace DAL {

    public class DBConfig {

        private static MySqlConnection connection = new MySqlConnection();
        private DBConfig(){}
        public static MySqlConnection GetDefaultConnection(){
            connection.ConnectionString = "server=localhost;user id=root;password=01266211591aA!@;port=3306;database=OrderDB;";
            return connection;
        }

        public static MySqlConnection GetConnection(){
            try{
                string conString;
                using (System.IO.FileStream fileStream = System.IO.File.OpenRead("DbConfig.txt")){
                using (System.IO.StreamReader reader = new System.IO.StreamReader(fileStream)){
                    conString = reader.ReadLine();
                }
                }
                return GetConnection(conString);
            }
            catch{
                return null;
            }
        }

        public static MySqlConnection GetConnection(string connectionString){
            connection.ConnectionString = connectionString;
            return connection;
        }

    }

}    

