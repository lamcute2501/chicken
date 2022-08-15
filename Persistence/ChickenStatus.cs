
namespace Persistence {

    public enum StatusOfChicken {
        Giong = 0,
        Dang_Lon = 1,
        Xuat_Chuong = 2
    }

    public class ChickenStatus {
        public int ChickenID {set;get;}
        public int Quantity {set;get;}
        public StatusOfChicken chickenStatus {set;get;}
        public int CageID {set;get;}

        public ChickenStatus(){

        }

        public ChickenStatus(int ckID, int quantity, StatusOfChicken status, int cageID){
            ChickenID = ckID;
            Quantity = quantity;
            chickenStatus = status;
            CageID = cageID;
        }

    }

}