using MySql.Data.MySqlClient;
using Persistence;
namespace DAL {

    public class ChickenDAL {
        private string query;
        private MySqlConnection connection = DbConfig.GetConnection();
        private MySqlDataReader reader;
        
        public ChickenDAL(){}
        
        public int? AddChicken(Chicken ckicken){
            int? result = null;
            
            if(connection.State == System.Data.ConnectionState.Closed){
                connection.Open();
            }
            MySqlCommand cmd = new MySqlCommand("AddChicken",connection);
            try {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@chicken_name", ckicken.ChickenName);
                cmd.Parameters["@chicken_name"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters.AddWithValue("@im_price", ckicken.ImportPrice);
                cmd.Parameters["@im_price"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters.AddWithValue("@ex_price", ckicken.ExportPrice);
                cmd.Parameters["@ex_price"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters.AddWithValue("@description", ckicken.Decription);
                cmd.Parameters["@desc"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters.AddWithValue("@id", MySqlDbType.Int32);
                cmd.Parameters["@desc"].Direction = System.Data.ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                result = (int)cmd.Parameters["@id"].Value;
            }
            catch {}
            finally {
                connection.Close();
            }
            return result;
        }
        
        public Chicken GetChickenById(int id){
            Chicken ck = null;
            try {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand("SearchChickenByID",connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id",id);
                cmd.Parameters["@id"].Direction = System.Data.ParameterDirection.Input;
                reader = cmd.ExecuteReader();
                if(reader.Read()){
                    ck = GetChicken(reader);
                }
                reader.Close();
            }
            catch {}
            finally{
                connection.Close();
            }
            return ck;
        }

        public Chicken GetChickenByName(string name){
            Chicken ck = null;
            try {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand("SearchChickenByName",connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@name",name);
                cmd.Parameters["@name"].Direction = System.Data.ParameterDirection.Input;
                reader = cmd.ExecuteReader();
                if(reader.Read()){
                    ck = GetChicken(reader);
                }
                reader.Close();
            }
            catch {}
            finally{
                connection.Close();
            }
            return ck;
        }

        public bool UpdateChicken(string name, decimal im_price, decimal ex_price, string desc){
            bool result = false;

            return result;
        }

        internal Chicken GetChicken(MySqlDataReader reader){
            Chicken ck = new Chicken();
            ck.ChickenID = reader.GetInt32("chicken_id");
            ck.ChickenName = reader.GetString("chicken_name");
            ck.ImportPrice = reader.GetDecimal("import_price");
            ck.ExportPrice = reader.GetDecimal("export_price");
            ck.Decription = reader.GetString("decription");
            return ck;
        }
    }

}