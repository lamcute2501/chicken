using MySql.Data.MySqlClient;
using Persistence;
namespace DAL {

    public class CageDAL{
        private MySqlConnection connection = DbConfig.GetConnection();
        private MySqlDataReader reader;

        public CageDAL() {}

        public int? AddCage(Cage  cage){
            int? result = null;
            
            if(connection.State == System.Data.ConnectionState.Closed){
                connection.Open();
            }
            MySqlCommand cmd = new MySqlCommand("AddCage",connection);
            try {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@name", cage.Cage_Name);
                cmd.Parameters["@name"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters.AddWithValue("@max_cap", cage.Max_Capacity);
                cmd.Parameters["@max_cap"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters.AddWithValue("@cur_cap", cage.Current_Capacity);
                cmd.Parameters["@cur_cap"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters.AddWithValue("@status", cage.CageStatus);
                cmd.Parameters["@status"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters.AddWithValue("@id", MySqlDbType.Int32);
                cmd.Parameters["@id"].Direction = System.Data.ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                result = (int)cmd.Parameters["@id"].Value;
            }
            catch(Exception e) {
                Console.WriteLine(e.Message);
            }
            finally {
                connection.Close();
            }
            return result;
        }
        
        public Cage GetCageById(int id){
            Cage cg = null;
            try {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand("SearchCageByID",connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id",id);
                cmd.Parameters["@id"].Direction = System.Data.ParameterDirection.Input;
                reader = cmd.ExecuteReader();
                if(reader.Read()){
                    cg = GetCage(reader);
                }
                reader.Close();
            }
            catch(Exception e) {
                Console.WriteLine(e.Message);
            }
            finally{
                connection.Close();
            }
            return cg;
        }

        public Cage GetCageByName(string name){
            Cage cg = null;
            try {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand("SearchCageByName",connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@name",name);
                cmd.Parameters["@name"].Direction = System.Data.ParameterDirection.Input;
                reader = cmd.ExecuteReader();
                if(reader.Read()){
                    cg = GetCage(reader);
                }
                reader.Close();
            }
            catch {}
            finally{
                connection.Close();
            }
            return cg;
        }

        public bool UpdateCage(int id,string name, int max_cap, int cur_cap, string status){
            bool result = false;
            try {
                connection.Open();
                MySqlTransaction transaction = connection.BeginTransaction();
                MySqlCommand cmd = new MySqlCommand("UpdateCage",connection);
                cmd.Transaction = transaction;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id",id);
                cmd.Parameters["@id"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters.AddWithValue("@name",name);
                cmd.Parameters["@name"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters.AddWithValue("@max_cap",max_cap);
                cmd.Parameters["@max_cap"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters.AddWithValue("@cur_cap",cur_cap);
                cmd.Parameters["@cur_cap"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters.AddWithValue("@status", status);
                cmd.Parameters["@status"].Direction = System.Data.ParameterDirection.Input;
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

        internal Cage GetCage(MySqlDataReader reader){
            Cage cg = new Cage();
            cg.CageID = reader.GetInt32("cage_id");
            cg.Cage_Name = reader.GetString("cage_name");
            cg.Current_Capacity = reader.GetInt32("current_capacity");
            cg.Max_Capacity = reader.GetInt32("max_capacity");
            cg.CageStatus = reader.GetString("cage_status");
            return cg;
        }

    }

}