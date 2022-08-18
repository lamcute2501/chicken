using Persistence;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
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
                cmd.Parameters.AddWithValue("@qtity", cs.Quantity);
                cmd.Parameters["@qtity"].Direction = System.Data.ParameterDirection.Input;
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
        public ChickenStatus? GetChickenStatus(int ckid,int cgid, string status){
            ChickenStatus? cs = null;
            try {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand("SearchChickenStatus",connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@chicken",ckid);
                cmd.Parameters["@chicken"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters.AddWithValue("@cage",cgid);
                cmd.Parameters["@cage"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters.AddWithValue("@status",status);
                cmd.Parameters["@status"].Direction = System.Data.ParameterDirection.Input;
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
        public List<ChickenStatus>? GetChickenStatus(int getFilter, string status){
            List<ChickenStatus>? csList = null;
            try {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand("",connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                switch(getFilter){
                    case GetFilter.Get_By_Name:
                        cmd.CommandText= "SearchChickenStatusByStatus";
                        cmd.Parameters.AddWithValue("@status",status);
                        cmd.Parameters["@status"].Direction = System.Data.ParameterDirection.Input;
                        break;
                    case GetFilter.Get_All:
                        cmd.CommandText = "GetAllChickenStatus";
                        break;
                    case GetFilter.Get_Status_By_ID:
                        cmd.CommandText = "SearchChickenStatusByID";
                        cmd.Parameters.AddWithValue("@id",Convert.ToInt32(status));
                        cmd.Parameters["@id"].Direction = System.Data.ParameterDirection.Input;
                        break;
                }
                csList = new List<ChickenStatus>();
                reader = cmd.ExecuteReader();
                while(reader.Read()){
                    csList.Add(GetChickenStatus(reader));
                }
                reader.Close();
            }
            catch {}
            finally{
                connection.Close();
            }
            return csList;
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
                cmd.Parameters.AddWithValue("@qtity",quantity);
                cmd.Parameters["@qtity"].Direction = System.Data.ParameterDirection.Input;
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

