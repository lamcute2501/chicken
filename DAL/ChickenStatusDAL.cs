using MySql.Data.MySqlClient;
using Persistence;
namespace DAL {

    public class ChickenStatusDAL {
        private MySqlConnection connection = DbConfig.GetConnection();
        private MySqlDataReader reader;

        public ChickenStatusDAL() {}

        public int AddStatus(ChickenStatus  cs){
            int result = 0;
            
            if(connection.State == System.Data.ConnectionState.Closed){
                connection.Open();
            }
            MySqlCommand cmd = new MySqlCommand("AddStatus",connection);
            try {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", cs.ChickenID);
                cmd.Parameters["@id"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters.AddWithValue("@quantity", cs.Quantity);
                cmd.Parameters["@quantity"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters.AddWithValue("@status", cs.chickenStatus);
                cmd.Parameters["@status"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters.AddWithValue("@cage", cs.CageID);
                cmd.Parameters["@cage"].Direction = System.Data.ParameterDirection.Input;
                cmd.ExecuteNonQuery();
                result = 1;
            }
            catch(Exception e) {
                Console.WriteLine(e.Message);
            }
            finally {
                connection.Close();
            }
            return result;
        }
        
        public ChickenStatus GetChickenStatusByChickenId(int id){
            ChickenStatus cs = null;
            try {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand("SearchChickenStatusByID",connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id",id);
                cmd.Parameters["@id"].Direction = System.Data.ParameterDirection.Input;
                reader = cmd.ExecuteReader();
                if(reader.Read()){
                    cs = GetChickenStatus(reader);
                }
                reader.Close();
            }
            catch(Exception e) {
                Console.WriteLine(e.Message);
            }
            finally{
                connection.Close();
            }
            return cs;
        }

        public ChickenStatus GetChickenStatusByStatus(string status){
            ChickenStatus cs = null;
            try {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand("SearchChickenStatusByStatus",connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@status",status);
                cmd.Parameters["@status"].Direction = System.Data.ParameterDirection.Input;
                reader = cmd.ExecuteReader();
                if(reader.Read()){
                    cs = GetChickenStatus(reader);
                }
                reader.Close();
            }
            catch {}
            finally{
                connection.Close();
            }
            return cs;
        }

        public bool UpdateChickenStatus(int id, int quantity, string status, int oldCage, int newCage){
            bool result = false;
            try {
                connection.Open();
                MySqlTransaction transaction = connection.BeginTransaction();
                MySqlCommand cmd = new MySqlCommand("UpdateChickenStatus",connection);
                cmd.Transaction = transaction;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id",id);
                cmd.Parameters["@id"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters.AddWithValue("@quantity",quantity);
                cmd.Parameters["@quantity"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters.AddWithValue("@status",status);
                cmd.Parameters["@status"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters.AddWithValue("@oldCage",oldCage);
                cmd.Parameters["@oldCage"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters.AddWithValue("@newCage", newCage);
                cmd.Parameters["@newCage"].Direction = System.Data.ParameterDirection.Input;
                try{
                    cmd.ExecuteNonQuery();
                    transaction.Commit();
                    result = true;
                }
                catch(Exception e){
                    Console.WriteLine(e.Message);
                    try{
                        transaction.Rollback();
                    }
                    catch {}
                }
            }
            catch(Exception e) {
                Console.WriteLine(e.Message);
            }
            finally{
                connection.Close();
            }
            return result;
        }

        internal ChickenStatus GetChickenStatus(MySqlDataReader reader){
            ChickenStatus cs = new ChickenStatus();
            cs.ChickenID = reader.GetInt32("chicken_id");
            cs.Quantity = reader.GetInt32("quantity");
            cs.chickenStatus = reader.GetString("chicken_status");
            cs.CageID = reader.GetInt32("cage_id");
            return cs;
        }
    }

}

