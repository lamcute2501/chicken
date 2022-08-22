namespace Persistence {
    public class Chicken {
        public int ChickenID {set;get;}
        public string ChickenName {set;get;}

        public decimal ImportPrice {set;get;}
        public decimal ExportPrice {set;get;}
        public string Decription {get;set;}

        public Chicken(){
            
        }

        public Chicken(string name, decimal im_price, decimal ex_price, string desc){
            ChickenName = name;
            ImportPrice = im_price;
            ExportPrice = ex_price;
            Decription = desc;
        }

        public Chicken CreateChicken(){
            decimal parameter;
            Chicken ck = new Chicken();
            Console.WriteLine(">>>>>   Loại Gà Mới  <<<<<");
            Console.Write("Tên gà   : ");
            ck.ChickenName = Console.ReadLine() ?? "Unset";
            do{Console.Write("Giá Nhập : ");}
            while(!decimal.TryParse(Console.ReadLine() , out parameter));
            ck.ImportPrice = parameter;
            do{Console.Write("Giá Xuất : ");}
            while(!decimal.TryParse(Console.ReadLine() , out parameter));
            ck.ExportPrice = parameter;
            Console.Write("Mô tả    : ");
            ck.Decription = Console.ReadLine() ?? "";
            Console.WriteLine("=============================================================");
            return ck;
        }

        public void ShowChicken(){
            char[] desNor =  Decription.ToCharArray();
            // if(Decription.Length > 44)
            //     desNor[Decription.LastIndexOf(' ',0,44)] = '\n';

            Console.WriteLine("┌───────────────────────Thông Tin Loại Gà───────────────────────┐");
            Console.WriteLine("     ID Gà   : " + ChickenID);
            Console.WriteLine("     Tên Gà  : " + ChickenName);
            Console.WriteLine("     Giá Nhập: " + ImportPrice);
            Console.WriteLine("     Giá Xuất: " + ExportPrice);
            Console.WriteLine("     Mô tả   : " + Decription);
            Console.WriteLine("└───────────────────────────────────────────────────────────────┘");
        }

        public void ShowChickenRow(){
            Console.Write("║ " + ChickenID);
            for(int i = 1 ; i <= 5 - ChickenID.ToString().Length - 1; i++) Console.Write(" ");
            Console.Write("║ " + ChickenName);
            for(int i = 1 ; i <= 20 - ChickenName.Length - 1; i++) Console.Write(" ");
            Console.Write("║ " + ImportPrice);
            for(int i = 1 ; i <= 12 - ImportPrice.ToString().Length - 1; i++) Console.Write(" ");
            Console.Write("║ " + ExportPrice);
            for(int i = 1 ; i <= 12 - ExportPrice.ToString().Length - 1; i++) Console.Write(" ");
            string desCutDown = Decription;    
            if(Decription.Length > 28){
                desCutDown = Decription.Remove(25);
                desCutDown =  string.Concat(desCutDown,"...");
            }
            Console.Write("║ " + desCutDown);
            for(int i = 1 ; i <= 30 - desCutDown.ToString().Length - 1; i++) Console.Write(" ");
            Console.WriteLine("║");
        }

    }
    
}