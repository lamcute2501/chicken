namespace Persistence {

    public class ChickenStatus {
        public int ChickenID {set;get;}
        public int Quantity {set;get;}
        public string chickenStatus {set;get;}
        public int CageID {set;get;}

        public ChickenStatus(){

        }

        public ChickenStatus(int ckID, int quantity, string status, int cageID){
            ChickenID = ckID;
            Quantity = quantity;
            chickenStatus = status;
            CageID = cageID;
        }

        public ChickenStatus CreateChickenStatus(){
            int parameter;
            int myChoice;
            ChickenStatus  cs = new ChickenStatus();
            Console.WriteLine(">>>>>   Thêm Nhóm Gà Mới  <<<<<");
            do{Console.Write("ID Loại Gà: ");}
            while(!int.TryParse(Console.ReadLine() , out parameter));
            cs.ChickenID = parameter;
            do{Console.Write("Số Lượng  : ");}
            while(!int.TryParse(Console.ReadLine() , out parameter));
            cs.Quantity = parameter;
            do{Console.Write("Giai Đoạn - [1] Giống  [2] Nhỡ  [3] Xuất Chuồng : ");}
            while(!int.TryParse(Console.ReadLine(),out myChoice) || (myChoice != 1 && myChoice != 2 && myChoice != 3));
            switch(myChoice){
                case 1:
                    cs.chickenStatus = "Giống";
                    break;
                case 2:
                    cs.chickenStatus = "Nhỡ";
                    break;
                case 3:
                    cs.chickenStatus = "Xuất Chuồng";
                    break;
                default:
                    break;
            }
            do{Console.Write("ID Chuồng : ");}
            while(!int.TryParse(Console.ReadLine() , out parameter));
            cs.CageID = parameter;
            Console.WriteLine("=============================================================");
            return cs;
        }

    }

}