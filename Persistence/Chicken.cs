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

    }
    
}