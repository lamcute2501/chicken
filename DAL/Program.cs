using Persistence;
namespace DAL {
    
    public class Program {
        public static void Main(string[] agrs){
            ChickenDAL cdal = new ChickenDAL();
            Chicken c = cdal.GetChickenByName("Ga Tam Hoang1");
            if(cdal.UpdateChicken("Ga Dong Tao",1,2,"Con nay trong xong ma an cung khong ngon",1)){
                Console.WriteLine("Update success");
            }
            else{
                Console.WriteLine("Update fail!");
            }
        }

    }

}