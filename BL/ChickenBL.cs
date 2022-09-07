using DAL;
using Persistence;
using System.Collections.Generic;
namespace BL {

    public class ChickenBL {
        private ChickenDAL ckdal = new ChickenDAL();

        public int AddChicken(Chicken ck){
            return ckdal.AddChicken(ck) ?? 0;
        }

        public Chicken GetChickenById(int id){
            return ckdal.GetChickenById(id);
        }

        public bool DeleteChickenById(int id){
            return ckdal.DeleteChickenById(id);
        }

        public List<Chicken> GetChickens(int getFilter,string name){
            return ckdal.GetChickens(getFilter,name);
        }

        public bool UpdateChicken(string name, decimal im_price, decimal ex_price, string desc, int id){
            return ckdal.UpdateChicken( name,  im_price,  ex_price,  desc, id);
        }

    }

}