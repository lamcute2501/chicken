using DAL;
using Persistence;
using System.Collections.Generic;
namespace BL {

    public class CageBL {

        private CageDAL cgdal = new CageDAL();

        public int AddCage(Cage cg){
            return cgdal.AddCage(cg) ?? 0;
        }

        public Cage GetCageById(int id){
            return cgdal.GetCageById(id);
        }

        public List<Cage> GetCages(int getFilter,string name){
            return cgdal.GetCages(getFilter,name);
        }

        public bool UpdateCage(int id,string name, int max_cap, int cur_cap, string status){
            return cgdal.UpdateCage( id, name,  max_cap,  cur_cap,  status);
        }

    }

}