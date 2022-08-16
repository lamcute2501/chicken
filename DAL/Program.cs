using Persistence;
namespace DAL {
    
    public class Program {
        public static void Main(string[] agrs){
           
            ChickenStatusDAL cdal = new ChickenStatusDAL();

            // cdal.AddStatus(new ChickenStatus(1,100,"Giống",1));

            if(cdal.UpdateChickenStatus(1,90,"Giống",1,2)) Console.WriteLine("success");  

        }

    }

}