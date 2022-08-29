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
            //     Console.WriteLine(@"chuồng tối đa chứa được {cg.Max_Capacity} con, {parameter - cg.Max_Capacity}");
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

    }

}