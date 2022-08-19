using System.Text;
namespace Persistence {

    public class Cage {
        public int CageID {set;get;}
        public string Cage_Name {set;get;}

        public int Max_Capacity {set;get;}
        public int Current_Capacity {set;get;}

        public string CageStatus {set;get;}

        public  Cage(){

        }

        public Cage(string name, int max_cap, int cur_cap,string status){
            Cage_Name = name;
            Max_Capacity = max_cap;
            Current_Capacity = cur_cap;
            CageStatus = status;
        }

        public Cage CreateCage(){
            int parameter;
            int myChoice;
            Cage cg = new Cage();
            Console.WriteLine(">>>>>   Chuồng Mới  <<<<<");
            Console.Write("Tên chuồng        : ");
            cg.Cage_Name = Console.ReadLine() ?? "Unset";
            Console.Write("Sức chứa gà tối đa: ");
            int.TryParse(Console.ReadLine() , out parameter);
            cg.Max_Capacity = parameter;
            Console.Write("Lượng gà hiện tại : ");
            int.TryParse(Console.ReadLine() , out parameter);

            // xử lí nhập số lượng hiện tại lớn hơn tối đa
            // while(parameter > cg.Max_Capacity){
            //     Console.WriteLine(@"Anh bạn à chuồng tối đa chứa được {cg.Max_Capacity} con thôi, {parameter - cg.Max_Capacity} con nữa thì vứt đi đâu? Nhập lại hộ tôi cái!");
            //     int.TryParse(Console.ReadLine() , out parameter);
            // }

            cg.Current_Capacity = parameter;
            Console.Write("Trạng thái chuồng - [1] Hoạt Động  [2] Đóng  [3] Bảo Trì :");
            int.TryParse(Console.ReadLine(),out myChoice);
            while(myChoice != 1 && myChoice != 2 && myChoice != 3){
                 Console.WriteLine("Lựa chọn không hợp lệ! Mời nhập lại : ");
                int.TryParse(Console.ReadLine(),out myChoice);
            }
            switch(myChoice){
                case 1:
                    cg.CageStatus = "Hoạt Động";
                    break;
                case 2:
                    cg.CageStatus = "Đóng";
                    break;
                case 3:
                    cg.CageStatus = "Bảo Trì";
                    break;
                default:
                    break;
            }
            
            // xử lí nhập trạng thái không phù hợp (chuồng đang đóng hoặc bảo trì sẽ không chứa gà)
            // if(String.Compare(cg.CageStatus,"Hoạt Động") != 0 && cg.Current_Capacity > 0){

            // }
            
            Console.WriteLine("=============================================================");
            return cg;
        }

        public void ShowCage(){
            Console.WriteLine("┌───────────────────────Thông Tin Chuồng Trại───────────────────────┐");
            Console.WriteLine("    ID Chuồng         : " + CageID);
            Console.WriteLine("    Tên Chuồng        : " + Cage_Name);
            Console.WriteLine("    Sức Chứa Tối Đa   : " + Max_Capacity);
            Console.WriteLine("    Lượng Gà Hiện Tại : " + Current_Capacity);
            Console.WriteLine("    Trạng Thái Chuồng : " + CageStatus);
            Console.WriteLine("└───────────────────────────────────────────────────────────────────┘");
        }

        public void ShowChickenRow(){
            Console.Write("║ " + CageID);
            for(int i = 1 ; i <= 5 - CageID.ToString().Length - 1; i++) Console.Write(" ");
            Console.Write("║ " + Cage_Name);
            for(int i = 1 ; i <= 20 - Cage_Name.Length - 1; i++) Console.Write(" ");
            Console.Write("║ " + Max_Capacity);
            for(int i = 1 ; i <= 20 - Max_Capacity.ToString().Length - 1; i++) Console.Write(" ");
            Console.Write("║ " + Current_Capacity);
            for(int i = 1 ; i <= 20 - Current_Capacity.ToString().Length - 1; i++) Console.Write(" ");         
            Console.Write("║ " + CageStatus);
            for(int i = 1 ; i <= 20 - CageStatus.ToString().Length - 1; i++) Console.Write(" ");
            Console.WriteLine("║");
        }

    }

}