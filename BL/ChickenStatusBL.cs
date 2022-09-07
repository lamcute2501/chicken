using DAL;
using Persistence;
using System.Collections.Generic;
namespace BL {
    public class ChickenStatusBL{
        private ChickenStatusDAL csdal = new ChickenStatusDAL();

        public int AddStatus(ChickenStatus cs){
            return csdal.AddStatus(cs);
        }

        public ChickenStatus GetChickenStatus(int ckid, int cgid,string status){
            return csdal.GetChickenStatus(ckid,cgid,status);
        }

        public bool DeleteChickenStatus(int ck_id,int cg_id,string status){
            return csdal.DeleteChickenStatus(ck_id,cg_id,status);
        }

        public List<ChickenStatus> GetChickenStatus(int getFilter,string status){
            return csdal.GetChickenStatus(getFilter, status);
        }

        public bool UpdateChickenStatus(int id, int quantity, string status, int oldCage, int newCage){
            return csdal.UpdateChickenStatus(id, quantity, status, oldCage, newCage);
        }
    }

}