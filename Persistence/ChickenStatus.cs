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

        public void ShowStatus(){
            Console.WriteLine("┌───────────────────────Thông Tin Nhóm Gà───────────────────────┐");
            Console.WriteLine("     ID Gà     : " + ChickenID);
            Console.WriteLine("     Số Lượng  : " + Quantity);
            Console.WriteLine("     Giai Đoạn : " + chickenStatus);
            Console.WriteLine("     ID Chuồng : " + CageID);
            Console.WriteLine("└───────────────────────────────────────────────────────────────┘");
        }

        public void ShowStatusRow(){
            Console.Write("║ " + ChickenID);
            for(int i = 1 ; i <= 10 - ChickenID.ToString().Length - 1; i++) Console.Write(" ");
            Console.Write("║ " + Quantity);
            for(int i = 1 ; i <= 10 - Quantity.ToString().Length - 1; i++) Console.Write(" ");
            Console.Write("║ " + chickenStatus);
            for(int i = 1 ; i <= 20 - chickenStatus.Length - 1; i++) Console.Write(" ");
            Console.Write("║ " + CageID);
            for(int i = 1 ; i <= 11 - CageID.ToString().Length - 1; i++) Console.Write(" ");
            Console.WriteLine("║");
        }
    }

}