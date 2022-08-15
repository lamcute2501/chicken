namespace Persistence {

    private enum StatusOfChicken {
        Giong = 0,
        Dang_Lon = 1,
        Xuat_Chuong = 2
    }

    public class ChickenStatus {
        public int ChickenID {set;get;}
        public int Quantity {set;get;}
        public StatusOfChicken ChickenStatus {set;get;}
        public int CageID {set;get;}

        public ChickenStatus(){

        }

        public ChickenStatus(int ckID, int quantity, StatusOfChicken status, int ckStatus, int cageID){
            ChickenID = ckID;
            Quantity = quantity;
            ChickenStatus = status;
            ChickenStatus = ckStatus;
            CageID = cageID;
        }

    }

}