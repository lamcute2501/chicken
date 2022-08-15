namespace Persistence {
    public enum StatusOfCage {
        Dong = 0,
        Hoat_Dong = 1,
        Bao_Tri = 2
    }

    public class Cage {
        public int CageID {set;get;}
        public string Cage_Name {set;get;}

        public int Max_Capacity {set;get;}
        public int Current_Capacity {set;get;}

        public StatusOfCage CageStatus {set;get;}

        public  Cage(){

        }

        public Cage(string name, int max_cap, int cur_cap,StatusOfCage status){
            Cage_Name = name;
            Max_Capacity = max_cap;
            Current_Capacity = cur_cap;
            CageStatus = status;
        }

    }

}