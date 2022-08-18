using Persistence;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
namespace DAL {

    public static class GetFilter {
        public const int Get_By_Name = 1;
        public const int Get_All = 2;
        public const int Get_Status_By_ID = 3;
    }

    public class ChickenDAL {
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
                cmd.Parameters.AddWithValue("@name", ckicken.ChickenName);
                cmd.Parameters["@name"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters.AddWithValue("@im_price", ckicken.ImportPrice);
                cmd.Parameters["@im_price"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters.AddWithValue("@ex_price", ckicken.ExportPrice);
                cmd.Parameters["@ex_price"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters.AddWithValue("@description", ckicken.Decription);
                cmd.Parameters["@description"].Direction = System.Data.ParameterDirection.Input;
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
        
        public Chicken? GetChickenById(int id){
            Chicken? ck = null;
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
            catch(Exception e) {
                Console.WriteLine(e.Message);
            }
            finally{
                connection.Close();
            }
            return ck;
        }

        public List<Chicken>? GetChickens(int chikenFilter,string name){
            List<Chicken>? ckList = null;
            try {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand("",connection);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                switch(chikenFilter){
                    case GetFilter.Get_By_Name:
                        cmd.CommandText = "SearchChickenByName";
                        cmd.Parameters.AddWithValue("@name",name);
                        cmd.Parameters["@name"].Direction = System.Data.ParameterDirection.Input;
                        break;
                    case GetFilter.Get_All:
                        cmd.CommandText = "GetAllChicken";
                        break;
                }
                ckList = new List<Chicken>();
                reader = cmd.ExecuteReader();
                while(reader.Read()){
                    ckList.Add(GetChicken(reader));
                }
                reader.Close();
            }
            catch(Exception e) {
                Console.WriteLine(e.Message);
            }
            finally{
                connection.Close();
            }
            return ckList;
        }

        public bool UpdateChicken(string name, decimal im_price, decimal ex_price, string desc, int id){
            bool result = false;
            try {
                connection.Open();
                MySqlTransaction transaction = connection.BeginTransaction();
                MySqlCommand cmd = new MySqlCommand("UpdateChickenInfo",connection);
                cmd.Transaction = transaction;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@name",name);
                cmd.Parameters["@name"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters.AddWithValue("@im_price",im_price);
                cmd.Parameters["@im_price"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters.AddWithValue("@ex_price",ex_price);
                cmd.Parameters["@ex_price"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters.AddWithValue("@des", desc);
                cmd.Parameters["@des"].Direction = System.Data.ParameterDirection.Input;
                cmd.Parameters.AddWithValue("@id",id);
                cmd.Parameters["@id"].Direction = System.Data.ParameterDirection.Input;
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

        internal Chicken GetChicken(MySqlDataReader reader){
            Chicken ck = new Chicken();
            ck.ChickenID = reader.GetInt32("chicken_id");
            ck.ChickenName = reader.GetString("chicken_name");
            ck.ImportPrice = reader.GetDecimal("import_price");
            ck.ExportPrice = reader.GetDecimal("export_price");
            ck.Decription = reader.GetString("description");
            return ck;
        }
    }

}