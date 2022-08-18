using BL;
using Persistence;
using System.Collections.Generic;

namespace ConsolPL {

    class Program {

        

        static void Main(string[] args){
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            Console.InputEncoding = System.Text.Encoding.Unicode;

            int mainChoice = 0,subChoice,updateChoice;
            string[] mainMenu = {"Quản lý loại gà", "Quản lý nhóm gà","Quản lý chuồng trại","Thoát"};
            string[] chickenMenu = {"Thêm loại gà mới","Hiển thị toàn bộ loại gà","Tìm loại gà theo mã id","Tìm loại gà theo tên","Thoát"};
            string[] chickenStatusMenu = {"Thêm nhóm gà mới","Hiển thị toàn bộ nhóm gà","Xuất chuồng","Tìm nhóm gà theo mã id loại gà","Tìm nhóm gà theo giai đoạn","Thoát"};
            string[] cageMenu = {"Thêm chuồng mới","Hiển thị toàn bộ chuồng","Tìm chuồng theo id","Tìm chuồng theo tên","Thoát"};
            
            ChickenBL ckbl = new ChickenBL();;
            ChickenStatusBL csbl = new ChickenStatusBL();
            CageBL cgbl = new CageBL();
            
            do{ 
                Console.Clear();
                mainChoice = Menu("Quản lý trại gà - CFMS",mainMenu);

                switch(mainChoice){
                    case 1:
                        do{ 
                            Console.Clear();
                            subChoice = Menu("Quản lý loại gà",chickenMenu);

                            Chicken? ck = null;
                            List<Chicken>? ckList  = null;

                            switch(subChoice){
                                case 1:
                                    ck = new Chicken();
                                    if(ckbl.AddChicken(ck.CreateChicken()) != 0){
                                        Console.WriteLine(" Thêm thành công !");
                                    }
                                    else{
                                        Console.WriteLine(" Thêm thất bại !");
                                    }
                                    Thread.Sleep(2000);
                                    break;
                                case 2:
                                    int id;
                                    string[] columns = {"ID","Tên Gà","Giá Nhập","Giá Xuất","Mô tả"};
                                    int[] space = {5,20,12,12,30};
                                    string[] change = {"Thay đổi giá nhập","Thay đổi giá xuất","Cập nhật mô tả","Thoát"};
                                    ckList = ckbl.GetChickens(2,"");
                                    if(ckList != null){
                                        // update menu------------------------------------
                                        do{
                                            Console.Clear();

                                            PrintColumns(columns,space,1);
                                            for(int i = 0 ; i < ckList.Count ; i++){
                                                ckList[i].ShowChickenRow();
                                            }
                                            PrintColumns(columns,space,2);
                                            Console.WriteLine(" Nhập ID để xem thông tin chi tiết và sửa đổi hoặc 0 để thoát: ");
                                            int.TryParse(Console.ReadLine(), out id);
                                            if(id != 0){
                                                // tim ga theo id de sua doi
                                                ck = ckbl.GetChickenById(id);
                                                if(ck == null){
                                                    Console.WriteLine(" ID không tồn tại!");
                                                    Thread.Sleep(2000);
                                                }
                                                else{    
                                                    decimal newPrice;
                                                    do{
                                                        ck = ckbl.GetChickenById(id);
                                                        Console.Clear();
                                                        ck.ShowChicken();
                                                        updateChoice = MenuChangeInfo(change);
                                                        switch(updateChoice){
                                                            case 1: 
                                                                Console.Write(" Giá nhập mới: ");
                                                                decimal.TryParse(Console.ReadLine(),out newPrice);
                                                                if(ckbl.UpdateChicken(ck.ChickenName,newPrice,ck.ExportPrice,ck.Decription,ck.ChickenID)){
                                                                    Console.WriteLine(" Cập nhật hoàn tất!");

                                                                }
                                                                else{
                                                                    Console.WriteLine(" Cập nhật thất bại!");
                                                                }
                                                                Thread.Sleep(2000);
                                                                break;
                                                            case 2:
                                                                Console.Write(" Giá xuất mới: ");
                                                                decimal.TryParse(Console.ReadLine(),out newPrice);
                                                                if(ckbl.UpdateChicken(ck.ChickenName,ck.ImportPrice,newPrice,ck.Decription,ck.ChickenID)){
                                                                    Console.WriteLine(" Cập nhật hoàn tất!");
                                                                }
                                                                else{
                                                                    Console.WriteLine(" Cập nhật thất bại!");
                                                                }
                                                                Thread.Sleep(2000);
                                                                break;
                                                            case 3:
                                                                Console.Write(" Mô tả mới: ");
                                                                if(ckbl.UpdateChicken(ck.ChickenName,ck.ImportPrice,ck.ExportPrice,Console.ReadLine()??"",ck.ChickenID)){
                                                                    Console.WriteLine(" Cập nhật hoàn tất!");
                                                                }
                                                                else{
                                                                    Console.WriteLine(" Cập nhật thất bại!");
                                                                }
                                                                break;
                                                        }
                                                    }
                                                    while(updateChoice != change.Count());
                                                }
                                            }
                                        }
                                        while(id != 0);
                                    }
                                    else{
                                        Console.WriteLine(" Danh sách rỗng :(");
                                        Thread.Sleep(2000);
                                    }
                                    break;
                                case 3:
                                    Console.Write(" Nhập ID: ");
                                    int.TryParse(Console.ReadLine(), out id);
                                    ck = ckbl.GetChickenById(id);
                                    if(ck != null){
                                        ck.ShowChicken();
                                        Thread.Sleep(5000);
                                    }
                                    else{
                                        Console.WriteLine(" Không tìm thấy !");
                                        Thread.Sleep(2000);
                                    }
                                    break;
                                case 4:
                                    Console.Write(" Nhập tên: ");
                                    ckList = ckbl.GetChickens(1,Console.ReadLine()??"");
                                    if(ckList != null){
                                        ckList[0].ShowChicken();
                                        Thread.Sleep(5000);
                                    }
                                    else{
                                        Console.WriteLine(" Không tìm thấy !");
                                        Thread.Sleep(2000);
                                    }
                                    break;
                            }
                        }
                        while(subChoice != chickenMenu.Length);
                        break;
                    case 2:
                        do{ 
                            Console.Clear();
                            subChoice = Menu("Quản lý nhóm gà",chickenStatusMenu);

                            ChickenStatus? cs;
                            List<ChickenStatus>? csList = null;
                            Cage? cage;

                            switch(subChoice){
                                case 1:
                                // them cac truong hop o day ga 00 ton tai chuong 0 ton tai status khong hop li
                                    cs = new ChickenStatus();
                                    cs = cs.CreateChickenStatus();
                                    if(ckbl.GetChickenById(cs.ChickenID) == null){
                                        Console.WriteLine(" Loại gà này không tồn tại! Hãy thêm tại 'Quản lí loại gà'");
                                        Thread.Sleep(2000);
                                        break;
                                    }
                                    cage = new Cage();
                                    cage = cgbl.GetCageById(cs.CageID);
                                    if(cage == null){
                                        Console.WriteLine(" Chuồng này không tồn tại! Hãy thêm tại 'Quản lí chuồng trại'");
                                        Thread.Sleep(2000);
                                        break;
                                    }
                                    if(string.Compare(cage.CageStatus,"Bảo Trì") == 0 || string.Compare(cage.CageStatus,"Đóng") == 0){
                                        Console.WriteLine(" Chuồng này đang {0}}, không thể thêm gà!",cage.CageStatus);
                                        Thread.Sleep(2000);
                                        break;
                                    }
                                    if(csbl.AddStatus(cs) != 0 && cgbl.UpdateCage(cage.CageID,cage.Cage_Name,cage.Max_Capacity,cage.Current_Capacity+cs.Quantity,cage.CageStatus)){
                                        Console.WriteLine(" Thêm thành công !");
                                    }
                                    else{
                                        Console.WriteLine(" Thêm thất bại !");
                                    }
                                    Thread.Sleep(2000);
                                    break;
                                case 2:
                                    int stt;
                                    string[] columns = {"STT","ID Gà","Số Lượng","Giai Đoạn","ID Chuồng"};
                                    int[] space = {5,10,10,20,11};
                                    string[] change = {"Chuyển chuồng","Đổi giai đoạn","Thoát"};
                                    csList = csbl.GetChickenStatus(2,"");
                                    if(csList != null){
                                        // update menu------------------------------------
                                        do{
                                            csList = csbl.GetChickenStatus(2,"");
                                            Console.Clear();

                                            PrintColumns(columns,space,1);
                                            for(int i = 0 ; i < csList.Count ; i++){
                                                Console.Write($"║ {i+1}");
                                                PrintCharacterAndEnd(3 - i.ToString().Length,' ',' ',false);
                                                csList[i].ShowStatusRow();
                                            }
                                            PrintColumns(columns,space,2);
                                            do{Console.WriteLine(" Nhập STT để xem thông tin chi tiết và sửa đổi hoặc 0 để thoát: ");}
                                            while(!int.TryParse(Console.ReadLine(), out stt) || stt < 0);
                                    // tại đây----------------------------------------------------------------
                                            if(stt != 0 && stt <= csList.Count){
                                                stt--;
                                                // tim nhom ga theo id de sua doi
                                                cs = csbl.GetChickenStatus(csList[stt].ChickenID,csList[stt].CageID,csList[stt].chickenStatus);
                                                if(cs == null){
                                                    Console.WriteLine(" STT không hợp lệ!");
                                                    Thread.Sleep(2000);
                                                }
                                                else{    
                                                    int quantity;
                                                    int newId;
                                                    Cage curentCage = cgbl.GetCageById(cs.CageID);

                                                    do{
                                                        cs = csbl.GetChickenStatus(csList[stt].ChickenID,csList[stt].CageID,csList[stt].chickenStatus);
                                                        Console.Clear();
                                                        cs.ShowStatus();
                                                        updateChoice = MenuChangeInfo(change);
                                                        switch(updateChoice){
                                                            case 1: 
                                                                Console.Write(" ID chuồng mới: ");
                                                                int.TryParse(Console.ReadLine(),out newId);
                                                                cage = cgbl.GetCageById(newId);
                                                                if(cage == null){
                                                                    Console.Write(" Chuồng không tồn tại !");
                                                                    Thread.Sleep(2000);
                                                                    break;
                                                                }
                                                                if(string.Compare(cage.CageStatus,"Hoạt Động") != 0){
                                                                    Console.WriteLine(" Chuồng đang bảo trì hoặc đã đóng cửa!");
                                                                    Thread.Sleep(2000);
                                                                    break;
                                                                }
                                                                do{
                                                                    Console.Write(" Số lượng gà chuyển đi: ");
                                                                    int.TryParse(Console.ReadLine(),out quantity);

                                                                    // thông báo chuồng không đủ chỗ trống
                                                                    if(quantity > (cage.Max_Capacity-cage.Current_Capacity) || quantity <= 0 || quantity > cs.Quantity){
                                                                        Console.WriteLine(" Chuồng {0} có thể chứa thêm {1} con ! Hãy thay đổi số lượng gà!",cage.Cage_Name,cage.Max_Capacity-cage.Current_Capacity);
                                                                    }
                                                                }
                                                                while(quantity > (cage.Max_Capacity-cage.Current_Capacity));
        
                                                                // kiểm tra xem đã tồn tại 1 chicken status như vậy chưa chưa có sẽ tạo mới

                                                                
                                                                ChickenStatus? csCheck = csbl.GetChickenStatus(cs.ChickenID,cage.CageID,cs.chickenStatus);
                                                                
                                                                if(csCheck == null){
                                                                    Console.WriteLine("cs khong ton tai " + csbl.AddStatus(new ChickenStatus(cs.ChickenID,quantity,cs.chickenStatus,cage.CageID)));
                                                                }
                                                                else{
                                                                    Console.WriteLine("cs ton tai " + csbl.UpdateChickenStatus(cs.ChickenID,csCheck.Quantity+quantity,cs.chickenStatus,cage.CageID,cage.CageID));
                                                                }
                                                                // cạp nhật số gà trong các chuồng
                                                                Console.WriteLine(" " + cgbl.UpdateCage(cage.CageID,cage.Cage_Name,cage.Max_Capacity,cage.Current_Capacity+quantity,cage.CageStatus));
                                                                Console.WriteLine(" " + cgbl.UpdateCage(curentCage.CageID,curentCage.Cage_Name,curentCage.Max_Capacity,curentCage.Current_Capacity - quantity,curentCage.CageStatus));
                                                                if(csbl.UpdateChickenStatus(cs.ChickenID,cs.Quantity-quantity,cs.chickenStatus,cs.CageID,cs.CageID)){
                                                                    Console.WriteLine(" Cập nhật hoàn tất!");

                                                                }
                                                                else{
                                                                    Console.WriteLine(" Cập nhật thất bại!");
                                                                }
                                                                Thread.Sleep(2000);
                                                                break;
                                                            case 2:
                                                                // Console.Write(" Giá xuất mới: ");
                                                                // decimal.TryParse(Console.ReadLine(),out newPrice);
                                                                // if(ckbl.UpdateChicken(ck.ChickenName,ck.ImportPrice,newPrice,ck.Decription,ck.ChickenID)){
                                                                //     Console.WriteLine(" Cập nhật hoàn tất!");
                                                                // }
                                                                // else{
                                                                //     Console.WriteLine(" Cập nhật thất bại!");
                                                                // }
                                                                // Thread.Sleep(2000);
                                                                break;
                                                        }
                                                    }
                                                    while(updateChoice != change.Count());
                                                }
                                                //resset
                                                stt++;
                                            }
                                        }
                                        while(stt != 0);
                                    }
                                    else{
                                        Console.WriteLine(" Danh sách rỗng :(");
                                        Thread.Sleep(2000);
                                    }
                                    break;
                                case 3:
                                    Console.Write(" Nhập ID : ");                                   
                                    csList = csbl.GetChickenStatus(3,Console.ReadLine()??"0");
                                    if(csList != null){
                                        for(int i = 0 ; i < csList.Count ; i++){
                                            csList[i].ShowStatus();
                                        }
                                        Thread.Sleep(5000*csList.Count);
                                    }
                                    else{
                                        Console.WriteLine(" Không tìm thấy !");
                                        Thread.Sleep(2000);
                                    }
                                    break;
                                case 4:
                                    Console.Write(" Nhập giai đoạn: ");
                                    csList = csbl.GetChickenStatus(1,Console.ReadLine()??"");
                                    if(csList != null){
                                        for(int i = 0 ; i < csList.Count ; i++){
                                            csList[i].ShowStatus();
                                        }
                                        Thread.Sleep(5000*csList.Count);
                                    }
                                    else{
                                        Console.WriteLine(" Không tìm thấy !");
                                        Thread.Sleep(2000);
                                    }
                                    break;
                            }
                        }
                        while(subChoice != chickenStatusMenu.Length);
                        break;
                    case 3:
                        do{ 
                            Console.Clear();
                            subChoice = Menu("Quản lý chuồng trại",cageMenu);

                            Cage cg = new Cage();
                            List<Cage> cgList = new List<Cage>();
                            switch(subChoice){
                                case 1:
                                    if(cgbl.AddCage(cg.CreateCage()) != 0){
                                        Console.WriteLine(" Thêm thành công !");
                                    }
                                    else{
                                        Console.WriteLine(" Thêm thất bại !");
                                    }
                                    Thread.Sleep(2000);
                                    break;
                                case 2:
                                    break;
                                case 3:
                                    int id;
                                    Console.Write(" Nhập ID : ");
                                    int.TryParse(Console.ReadLine(), out id);
                                    cg = cgbl.GetCageById(id);
                                    if(cg != null){
                                        cg.ShowCage();
                                        Thread.Sleep(5000);
                                    }
                                    else{
                                        Console.WriteLine(" Không tìm thấy !");
                                        Thread.Sleep(2000);
                                    }
                                    break;
                                case 4:
                                    Console.Write(" Nhập tên: ");
                                    cgList = cgbl.GetCages(1,Console.ReadLine()??"");
                                    if(cgList != null){
                                        cgList[0].ShowCage();
                                        Thread.Sleep(5000);
                                    }
                                    else{
                                        Console.WriteLine(" Không tìm thấy !");
                                        Thread.Sleep(2000);
                                    }
                                    break;
                            }
                        }
                        while(subChoice != cageMenu.Length);
                        break;
                }

            }
            while(mainChoice != mainMenu.Length);

        }   

        private static int Menu(string title ,string[] menuItems ){
            string appName = @"║  _____ _     _      _               ______                    ║
║ /  __ \ |   (_)    | |              |  ___|                   ║
║ | /  \/ |__  _  ___| | _____ _ __   | |_ __ _ _ __ _ __ ___   ║
║ | |   | '_ \| |/ __| |/ / _ \ '_ \  |  _/ _` | '__| '_ ` _ \  ║
║ | \__/\ | | | | (__|   <  __/ | | | | || (_| | |  | | | | | | ║
║  \____/_| |_|_|\___|_|\_\___|_| |_| \_| \__,_|_|  |_| |_| |_| ║";
            string topLine = "╔═══════════════════════════════════════════════════════════════╗";
            string line = "╟───────────────────────────────────────────────────────────────╢";
            string endLine = "╚═══════════════════════════════════════════════════════════════╝";
            int choice = 0;
            Console.WriteLine(topLine);
            Console.WriteLine(appName);
            Console.WriteLine(line);
            for(int i = 0 ; i < menuItems.Length ; i++){
                Console.Write($"║ [{i+1}]. " + menuItems[i]);
                PrintCharacterAndEnd(57-menuItems[i].Length,' ','║',true);
            }
            Console.WriteLine(line);
            Console.Write("║ " + title);
            PrintCharacterAndEnd(62-title.Length,' ','║',true);
            Console.WriteLine(endLine);
            do{
                Console.Write(" Lựa chọn: ");
                try{
                    int.TryParse(Console.ReadLine(), out choice);
                }
                catch{
                    Console.WriteLine(" Lựa chọn không hợp lệ!");
                    continue;
                }
            }
            while(choice > menuItems.Length || choice <= 0);
            return choice;
        }

        private static void PrintCharacterAndEnd(int amount, char character,char endCharacter,bool newLine){
            if(newLine){
                for(int i = 1 ; i <= amount ; i++)
                    Console.Write(character);
                Console.Write(endCharacter + "\n");
            }
            else{
                for(int i = 1 ; i <= amount ; i++)
                    Console.Write(character);
                Console.Write(endCharacter);
            }
        }

        private static void  PrintColumns (string[] columns,int[] space, int filter){
            //╔════╦═════════════════════════════════════════════╗
            //║ ID ║                                             ║             
            //╟────╫─────────────────────────────────────────────╢
            //║    ║                                             ║
            //╚════╩═════════════════════════════════════════════╝
            
           

            

            if(filter == 1){
                 // hien thi top line
                Console.Write("╔");
                for(int i = 0; i < columns.Count() ; i++){
                    if(i != columns.Count() - 1)
                        PrintCharacterAndEnd(space[i],'═','╦',false);
                    else    
                        PrintCharacterAndEnd(space[i],'═','╗',true);
                }
                
                // hien thi ten cot
                Console.Write("║");
                for(int i = 0 ; i < columns.Count(); i++){
                    Console.Write(" " + columns[i]);
                    if(i != columns.Count() - 1) PrintCharacterAndEnd(space[i] - 1 - columns[i].Length,' ','║',false);
                    else PrintCharacterAndEnd(space[i] - 1 - columns[i].Length,' ','║',true);
                }

                // line ngan cach
                Console.Write("╟");
                for(int i = 0 ; i < columns.Count(); i++){
                    if(i != columns.Count() - 1) PrintCharacterAndEnd(space[i],'─','╫',false);
                    else PrintCharacterAndEnd(space[i],'─','╢',true);
                }
            }
            else{
                // hien thi bot line
                Console.Write("╚");
                for(int i = 0; i < columns.Count() ; i++){
                    if(i != columns.Count() - 1)
                        PrintCharacterAndEnd(space[i],'═','╩',false);
                    else    
                        PrintCharacterAndEnd(space[i],'═','╝',true);
                }
            }

        }

        private static int MenuChangeInfo(string[] infoWillChange){
            int choice = 0;
            Console.WriteLine("        ┌──────────────Thay đổi thông tin──────────────┐");
            //┌──────────────Thay đổi thông tin──────────────┐
            //│
            //│
            //│
            //│
            //└──────────────────────────────────────────────┘
            //
            Console.Write("        │");
            PrintCharacterAndEnd(46,' ','│',true);
            for(int i = 0 ; i < infoWillChange.Length ; i++){
                Console.Write($"        │ [{i+1}]. " + infoWillChange[i]);
                PrintCharacterAndEnd(40-infoWillChange[i].Length,' ','│',true);
            }
            Console.Write("        │");
            PrintCharacterAndEnd(46,' ','│',true);
            Console.WriteLine("        └──────────────────────────────────────────────┘");
            do{
                Console.Write(" Lựa chọn: ");
                try{
                    int.TryParse(Console.ReadLine(), out choice);
                }
                catch{
                    Console.WriteLine(" Lựa chọn không hợp lệ!");
                    continue;
                }
            }
            while(choice > infoWillChange.Length || choice <= 0);
            return choice;
        }
    }

}

