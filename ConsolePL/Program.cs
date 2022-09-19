using BL;
using Persistence;
using System.Collections.Generic;

namespace ConsolPL {

    class Program {

        static void Main(string[] args){
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            Console.InputEncoding = System.Text.Encoding.Unicode;
            UserBL usbl = new UserBL();

            string[] loginMenu = {"Đăng Nhập","Thoát"};
            int loginChoice;
            string userName,password;
            do{
                // sửa time sleep thành biến-----------------------
                Console.Clear();
                loginChoice = DisplaySP.Menu("Đăng Nhập",loginMenu);

                if(loginChoice == 2){return;}
                if(loginChoice != 1){continue;}
                // phần dăng nhập
                Console.Write(" Tên đăng nhập: ");
                userName = Console.ReadLine()??"";
                if(usbl.CheckUserName(userName)){
                    Console.Write(" Mật khẩu: ");
                    password = DisplaySP.GetPassword();
                    if(usbl.CheckPassword(userName,password)){
                        DisplaySP.PrintColor(" Đăng nhập thành công !",ConsoleColor.Green,true);
                        Thread.Sleep(2000);
                        int mainChoice = 0,subChoice,updateChoice;
                        string[] mainMenu = {"Quản lý loại gà", "Quản lý nhóm gà","Quản lý chuồng trại","Đăng xuất"};
                        string[] chickenMenu = {"Thêm loại gà mới","Hiển thị toàn bộ loại gà","Tìm loại gà theo mã id","Tìm loại gà theo tên","Thoát"};
                        string[] chickenStatusMenu = {"Thêm nhóm gà mới","Hiển thị toàn bộ nhóm gà","Tìm nhóm gà theo mã id loại gà","Tìm nhóm gà theo giai đoạn","Thoát"};
                        string[] cageMenu = {"Thêm chuồng mới","Hiển thị toàn bộ chuồng","Tìm chuồng theo id","Tìm chuồng theo tên","Thoát"};
                        
                        ChickenBL ckbl = new ChickenBL();;
                        ChickenStatusBL csbl = new ChickenStatusBL();
                        CageBL cgbl = new CageBL();
                        object ob;
                        
                        do{ 
                            Console.Clear();
                            mainChoice = DisplaySP.Menu("Quản lý trại gà - CFMS",mainMenu);

                            switch(mainChoice){
                                case 1:
                                    do{ 
                                        Console.Clear();
                                        subChoice = DisplaySP.Menu("Quản lý loại gà",chickenMenu);
                                        List<Chicken> ckList;

                                        switch(subChoice){
                                            case 1:
                                                Chicken ck = new Chicken();
                                                if(ckbl.AddChicken((Chicken)Create(ob = ck)) != 0){
                                                    DisplaySP.PrintColor(" Thêm thành công !", ConsoleColor.Green, true);
                                                }
                                                else{
                                                    DisplaySP.PrintColor(" Thêm thất bại !", ConsoleColor.Red, true);
                                                }
                                                Thread.Sleep(2000);
                                                break;
                                            case 2:
                                                int id;
                                                string[] columns = {"ID","Tên Gà","Giá Nhập","Giá Xuất","Mô tả"};
                                                int[] space = {5,20,12,12,30};
                                                string[] change = {"Thay đổi giá nhập","Thay đổi giá xuất","Cập nhật mô tả","Xóa loại gà","Thoát"};
                                                ckList = ckbl.GetChickens(2,"");
                                                if(ckList.Count != 0){
                                                    // update menu------------------------------------
                                                    do{
                                                        Console.Clear();
                                                        ckList = ckbl.GetChickens(2,"");
                                                        DisplaySP.PrintColumns(columns,space,1);
                                                        for(int i = 0 ; i < ckList.Count ; i++){
                                                            // ckList[i].ShowChickenRow();
                                                            ShowRow(ob = ckList[i]);
                                                        }
                                                        DisplaySP.PrintColumns(columns,space,2);
                                                        do{Console.WriteLine(" Nhập ID để xem thông tin chi tiết và sửa đổi hoặc 0 để thoát: ");}
                                                        while(!int.TryParse(Console.ReadLine(), out id));
                                                        if(id != 0){
                                                            // tim ga theo id de sua doi
                                                            ck = ckbl.GetChickenById(id);
                                                            if(ck == null){
                                                                DisplaySP.PrintColor(" ID không tồn tại!",ConsoleColor.Red,true);
                                                                Thread.Sleep(2000);
                                                            }
                                                            else{    
                                                                decimal newPrice;
                                                                do{
                                                                    ck = ckbl.GetChickenById(id);
                                                                    Console.Clear();
                                                                    // ck.ShowChicken();
                                                                    ShowInfo(ob = ck);
                                                                    updateChoice = DisplaySP.MenuChangeInfo(change);
                                                                    switch(updateChoice){
                                                                        case 1: 
                                                                            do{Console.Write(" Giá nhập mới: ");}
                                                                            while(!decimal.TryParse(Console.ReadLine(),out newPrice));
                                                                            if(ckbl.UpdateChicken(ck.ChickenName,newPrice,ck.ExportPrice,ck.Decription,ck.ChickenID)){
                                                                                DisplaySP.PrintColor(" Cập nhật hoàn tất!",ConsoleColor.Green,true);
                                                                            }
                                                                            else{
                                                                                DisplaySP.PrintColor(" Cập nhật thất bại!",ConsoleColor.Red,true);
                                                                            }
                                                                            Thread.Sleep(2000);
                                                                            break;
                                                                        case 2:
                                                                            do{Console.Write(" Giá xuất mới: ");}
                                                                            while(!decimal.TryParse(Console.ReadLine(),out newPrice));
                                                                            if(ckbl.UpdateChicken(ck.ChickenName,ck.ImportPrice,newPrice,ck.Decription,ck.ChickenID)){
                                                                                DisplaySP.PrintColor(" Cập nhật hoàn tất!",ConsoleColor.Green,true);
                                                                            }
                                                                            else{
                                                                                DisplaySP.PrintColor(" Cập nhật thất bại!",ConsoleColor.Red,true);
                                                                            }
                                                                            Thread.Sleep(2000);
                                                                            break;
                                                                        case 3:
                                                                            Console.Write(" Mô tả mới: ");
                                                                            if(ckbl.UpdateChicken(ck.ChickenName,ck.ImportPrice,ck.ExportPrice,Console.ReadLine()??"",ck.ChickenID)){
                                                                                DisplaySP.PrintColor(" Cập nhật hoàn tất!",ConsoleColor.Green,true);
                                                                            }
                                                                            else{
                                                                                DisplaySP.PrintColor(" Cập nhật thất bại!",ConsoleColor.Red,true);
                                                                            }
                                                                            Thread.Sleep(2000);
                                                                            break;
                                                                        case 4:
                                                                            // thêm chức năng xóa 
                                                                            // kiểm tra có nhóm gà nào của loài gà này không
                                                                            // nếu không => xóa thẳng
                                                                            // nếu có => cảnh báo chắc chắn xóa => nếu ok xóa hết các nhóm gà + giảm lượng gà trong chuồng
                                                                            Console.WriteLine("    Bạn có chắc chắn xóa loại gà này ?\n(Mọi thông tin các nhóm gà và chuồng trại liên quan tới loại gà này đều sẽ bị xóa)\n [1] Đúng    [2] Không");
                                                                            int importanceChoice;
                                                                            do{Console.Write(" Lựa chọn: ");}
                                                                            while(!int.TryParse(Console.ReadLine(),out importanceChoice));
                                                                            if(importanceChoice == 1){
                                                                                List<ChickenStatus> chickenStatus = csbl.GetChickenStatus(3,ck.ChickenID.ToString());
                                                                                if(chickenStatus.Count != 0){
                                                                                    Console.WriteLine(" Hiện tại vẫn còn loại gà này trong các chuồng!");
                                                                                    Console.WriteLine("    Bạn vẫn chắc chắn xóa ?\n [1] Đúng   [2] Không");
                                                                                    do{Console.Write(" Lựa chọn: ");}
                                                                                    while(!int.TryParse(Console.ReadLine(),out importanceChoice));
                                                                                    if(importanceChoice == 1){
                                                                                        for(int i = 0 ; i < chickenStatus.Count ; i++){
                                                                                            Cage cage = cgbl.GetCageById(chickenStatus[i].CageID);
                                                                                            // xóa trong chuồng
                                                                                            cgbl.UpdateCage(chickenStatus[i].CageID,cage.Cage_Name,cage.Max_Capacity,cage.Current_Capacity-chickenStatus[i].Quantity,cage.CageStatus);
                                                                                            // xóa status
                                                                                            csbl.DeleteChickenStatus(chickenStatus[i].ChickenID,chickenStatus[i].CageID,chickenStatus[i].chickenStatus);
                                                                                        }
                                                                                    }
                                                                                    else{
                                                                                        Thread.Sleep(2000);
                                                                                        break;
                                                                                    }
                                                                                }
                                                                                if(ckbl.DeleteChickenById(ck.ChickenID)){
                                                                                    DisplaySP.PrintColor(" Xóa thành công!", ConsoleColor.Green,true);
                                                                                }
                                                                                else{
                                                                                    DisplaySP.PrintColor(" Xóa thất bại!",ConsoleColor.Red, true);
                                                                                }
                                                                            }
                                                                            Thread.Sleep(2000);
                                                                            updateChoice = change.Count();
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
                                                    DisplaySP.PrintColor(" Danh sách rỗng :(",ConsoleColor.Red, true);
                                                    Thread.Sleep(2000);
                                                }
                                                break;
                                            case 3:
                                                do{Console.Write(" Nhập ID: ");}
                                                while(!int.TryParse(Console.ReadLine(), out id));
                                                ck = ckbl.GetChickenById(id);
                                                if(ck != null){
                                                    // ck.ShowChicken();
                                                    ShowInfo(ob = ck);
                                                    Thread.Sleep(5000);
                                                }
                                                else{
                                                    DisplaySP.PrintColor(" Không tìm thấy !",ConsoleColor.Red,true);
                                                    Thread.Sleep(2000);
                                                }
                                                break;
                                            case 4:
                                                Console.Write(" Nhập tên: ");
                                                ckList = ckbl.GetChickens(1,Console.ReadLine()??"");
                                                if(ckList.Count != 0){
                                                    // ckList[0].ShowChicken();
                                                    ShowInfo(ob = ckList[0]);
                                                    Thread.Sleep(5000);
                                                }
                                                else{
                                                    DisplaySP.PrintColor(" Không tìm thấy !",ConsoleColor.Red,true);
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
                                        subChoice = DisplaySP.Menu("Quản lý nhóm gà",chickenStatusMenu);

                                        ChickenStatus? cs;
                                        List<ChickenStatus>? csList;
                                        Cage cage;
                                        
                                        // tạo bảng kết quả gà
                                        string[] columns = {"STT","ID Gà","Số Lượng","Giai Đoạn","ID Chuồng"};
                                        int[] space = {5,10,10,20,11};
                                        // menu update 
                                        string[] change = {"Chuyển chuồng","Đổi giai đoạn","Thêm gà vào nhóm","Thoát"};
                                        // reset change cũ
                                        string[] oldChange = change;
                                        switch(subChoice){
                                            case 1:
                                            // them cac truong hop o day ga 00 ton tai chuong 0 ton tai status khong hop li
                                                cs = new ChickenStatus();
                                                cs = (ChickenStatus)Create(ob = cs);
                                                if(ckbl.GetChickenById(cs.ChickenID) == null){
                                                    DisplaySP.PrintColor(" Loại gà này không tồn tại! Hãy thêm tại 'Quản lí loại gà'",ConsoleColor.Red, true);
                                                    Thread.Sleep(2000);
                                                    break;
                                                }
                                                cage = new Cage();
                                                cage = cgbl.GetCageById(cs.CageID);
                                                if(cage == null){
                                                    DisplaySP.PrintColor(" Chuồng này không tồn tại! Hãy thêm tại 'Quản lí chuồng trại'",ConsoleColor.Red,true);
                                                    Thread.Sleep(2000);
                                                    break;
                                                }
                                                if(string.Compare(cage.CageStatus,"Bảo Trì") == 0 || string.Compare(cage.CageStatus,"Đóng") == 0){
                                                    DisplaySP.PrintColor($" Chuồng này đang {cage.CageStatus}, không thể thêm gà!", ConsoleColor.Red,true);
                                                    Thread.Sleep(2000);
                                                    break;
                                                }
                                                if(cage.Max_Capacity < cs.Quantity || cage.Max_Capacity-cage.Current_Capacity < cs.Quantity){
                                                    DisplaySP.PrintColor($" Chuồng chỉ còn {cage.Max_Capacity-cage.Current_Capacity} chỗ trống, không đủ sức chứa {cs.Quantity} con gà", ConsoleColor.Red,true);
                                                    Thread.Sleep(2000);
                                                    break;
                                                }
                                                if(csbl.AddStatus(cs) != 0 && cgbl.UpdateCage(cage.CageID,cage.Cage_Name,cage.Max_Capacity,cage.Current_Capacity+cs.Quantity,cage.CageStatus)){
                                                    DisplaySP.PrintColor(" Thêm thành công !", ConsoleColor.Green,true);
                                                }
                                                else{
                                                    DisplaySP.PrintColor(" Thêm thất bại !", ConsoleColor.Red,true);
                                                }
                                                Thread.Sleep(2000);
                                                break;
                                            case 2:
                                                int stt;
                                                csList = csbl.GetChickenStatus(2,"");
                                                if(csList.Count != 0){
                                                    // update menu------------------------------------
                                                    do{
                                                        csList = csbl.GetChickenStatus(2,"");
                                                        Console.Clear();

                                                        DisplaySP.PrintColumns(columns,space,1);
                                                        for(int i = 0 ; i < csList.Count ; i++){
                                                            Console.Write($"║ {i+1}");
                                                            DisplaySP.PrintCharacterAndEnd(3 - i.ToString().Length,' ',' ',false);
                                                            // csList[i].ShowStatusRow();
                                                            ShowRow(ob = csList[i]);
                                                        }
                                                        DisplaySP.PrintColumns(columns,space,2);
                                                        do{Console.WriteLine(" Nhập STT để xem thông tin chi tiết và sửa đổi hoặc 0 để thoát: ");}
                                                        while(!int.TryParse(Console.ReadLine(), out stt) || stt < 0);
                                                        if(stt != 0 && stt <= csList.Count){
                                                            // giảm 1 đơn vị do list bắt đầu từ 0
                                                            stt--;
                                                            
                                                            // tim nhom ga theo id de sua doi
                                                            cs = csbl.GetChickenStatus(csList[stt].ChickenID,csList[stt].CageID,csList[stt].chickenStatus);
                                                            if(cs == null){
                                                                DisplaySP.PrintColor(" STT không hợp lệ!",ConsoleColor.Red,true);
                                                                Thread.Sleep(2000);
                                                            }
                                                            else{    
                                                                
                                                                if(string.Compare(cs.chickenStatus,"Xuất Chuồng") == 0){                  
                                                                    string[] newChange = new string[5];
                                                                    change.CopyTo(newChange,0);
                                                                    newChange[4] = newChange[3];
                                                                    newChange[3] = "Xuất chuồng";
                                                                    change = newChange;
                                                                }
                                                                else{
                                                                    change = oldChange;
                                                                }
                                                                
                                                                int quantity;
                                                                int newId;
                                                                Cage curentCage = cgbl.GetCageById(cs.CageID);
                                                                cs = csbl.GetChickenStatus(csList[stt].ChickenID,csList[stt].CageID,csList[stt].chickenStatus);
                                                                if(cs != null)
                                                                do{
                                                                    Console.Clear();
                                                                    // cs.ShowStatus();
                                                                    ShowInfo(ob = cs);
                                                                    updateChoice = DisplaySP.MenuChangeInfo(change);
                                                                    switch(updateChoice){
                                                                        case 1: 
                                                                            do{Console.Write(" ID chuồng mới: ");}
                                                                            while(!int.TryParse(Console.ReadLine(),out newId) || cs.CageID == newId);
                                                                            cage = cgbl.GetCageById(newId);
                                                                            if(cage == null){
                                                                                DisplaySP.PrintColor(" Chuồng không tồn tại !",ConsoleColor.Red,false);
                                                                                Thread.Sleep(2000);
                                                                                break;
                                                                            }
                                                                            if(string.Compare(cage.CageStatus,"Hoạt Động") != 0){
                                                                                DisplaySP.PrintColor(" Chuồng đang bảo trì hoặc đã đóng cửa!",ConsoleColor.Red,true);
                                                                                Thread.Sleep(2000);
                                                                                break;
                                                                            }
                                                                            do{
                                                                                do{Console.Write(" Số lượng gà chuyển đi: ");}
                                                                                while(!int.TryParse(Console.ReadLine(),out quantity));

                                                                                // thông báo chuồng không đủ chỗ trống
                                                                                if(quantity > (cage.Max_Capacity-cage.Current_Capacity) || quantity < 0 ){
                                                                                    Console.WriteLine(" Chuồng {0} có thể chứa thêm {1} con ! Hãy thay đổi số lượng gà!",cage.Cage_Name,cage.Max_Capacity-cage.Current_Capacity);
                                                                                }
                                                                                if( quantity > cs.Quantity){
                                                                                    Console.WriteLine(" Chuồng hiện tại chỉ còn {0} con, hãy kiểm tra và nhập lại số lượng!",cs.Quantity);
                                                                                }
                                                                            }
                                                                            while(quantity > (cage.Max_Capacity-cage.Current_Capacity));
                    
                                                                            // kiểm tra xem đã tồn tại 1 chicken status như vậy chưa chưa có sẽ tạo mới

                                                                            
                                                                            ChickenStatus? csCheck = csbl.GetChickenStatus(cs.ChickenID,cage.CageID,cs.chickenStatus);
                                                                            
                                                                            if(csCheck == null){
                                                                                csbl.AddStatus(new ChickenStatus(cs.ChickenID,quantity,cs.chickenStatus,cage.CageID));
                                                                            }
                                                                            else{
                                                                                csbl.UpdateChickenStatus(cs.ChickenID,csCheck.Quantity+quantity,cs.chickenStatus,cage.CageID,cage.CageID);
                                                                            }
                                                                            // cạp nhật số gà trong các chuồng
                                                                            cgbl.UpdateCage(cage.CageID,cage.Cage_Name,cage.Max_Capacity,cage.Current_Capacity+quantity,cage.CageStatus);
                                                                            cgbl.UpdateCage(curentCage.CageID,curentCage.Cage_Name,curentCage.Max_Capacity,curentCage.Current_Capacity - quantity,curentCage.CageStatus);
                                                                            if(csbl.UpdateChickenStatus(cs.ChickenID,cs.Quantity-quantity,cs.chickenStatus,cs.CageID,cs.CageID)){
                                                                                DisplaySP.PrintColor(" Cập nhật hoàn tất!",ConsoleColor.Green,true);
                                                                            }
                                                                            else{
                                                                                DisplaySP.PrintColor(" Cập nhật thất bại!",ConsoleColor.Red,true);
                                                                            }
                                                                            Thread.Sleep(2000);
                                                                            break;
                                                                        case 2: 
                                                                            string oldStatus = cs.chickenStatus;
                                                                            int myChoice;
                                                                            do{Console.Write("Giai Đoạn - [1] Giống  [2] Nhỡ  [3] Xuất Chuồng : ");}
                                                                            while(!int.TryParse(Console.ReadLine(),out myChoice) || (myChoice != 1 && myChoice != 2 && myChoice != 3)
                                                                            || (string.Compare(cs.chickenStatus, "Giống") == 0 && myChoice == 1)
                                                                            || (string.Compare(cs.chickenStatus, "Nhỡ") == 0 && myChoice == 2)
                                                                            || (string.Compare(cs.chickenStatus, "Xuất Chuồng") == 0 && myChoice == 3)
                                                                            );
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
                                                                            // kiểm tra đã tồn tại status với status mới chưa
                                                                            csCheck = csbl.GetChickenStatus(cs.ChickenID,cs.CageID,cs.chickenStatus);
                                                                            do{Console.Write(" Số lượng gà thay đổi giai đoạn : ");}
                                                                            while(!int.TryParse(Console.ReadLine(),out quantity) || quantity < 0  || quantity > cs.Quantity);
                                                                            if(csCheck != null){
                                                                                csbl.UpdateChickenStatus(csCheck.ChickenID,csCheck.Quantity + quantity,csCheck.chickenStatus,csCheck.CageID,csCheck.CageID);
                                                                            }
                                                                            else{
                                                                                csbl.AddStatus(new ChickenStatus(cs.ChickenID,quantity,cs.chickenStatus,cs.CageID));
                                                                            }
                                                                            if(csbl.UpdateChickenStatus(cs.ChickenID,cs.Quantity-quantity,oldStatus,cs.CageID,cs.CageID)){
                                                                                DisplaySP.PrintColor(" Cập nhật hoàn tất!",ConsoleColor.Green,true);
                                                                            }
                                                                            else{
                                                                                DisplaySP.PrintColor(" Cập nhật thất bại!",ConsoleColor.Red,true);
                                                                            }
                                                                            Thread.Sleep(2000); 
                                                                            break;
                                                                        case 3:
                                                                            //  thêm gà vào 1 chicken status
                                                                            do{Console.Write(" Số lượng gà thêm vào : ");}
                                                                            while(!int.TryParse(Console.ReadLine(),out quantity) || quantity < 0);
                                                                            cage = cgbl.GetCageById(cs.CageID);
                                                                            if(quantity > cage.Max_Capacity - cage.Current_Capacity){
                                                                                Console.WriteLine(" Chuồng hiện tại chỉ còn {0} chỗ trống , không đủ sức chứa!",cage.Max_Capacity - cage.Current_Capacity);
                                                                                Thread.Sleep(3000);
                                                                                break;
                                                                            }
                                                                            else{
                                                                                cgbl.UpdateCage(cage.CageID,cage.Cage_Name,cage.Max_Capacity,cage.Current_Capacity + quantity, cage.CageStatus);    
                                                                                if(csbl.UpdateChickenStatus(cs.ChickenID,cs.Quantity+quantity,cs.chickenStatus,cs.CageID,cs.CageID)){
                                                                                DisplaySP.PrintColor(" Cập nhật hoàn tất!",ConsoleColor.Green,true);
                                                                            }
                                                                            else{
                                                                                DisplaySP.PrintColor(" Cập nhật thất bại!",ConsoleColor.Red,true);
                                                                            }
                                                                            }
                                                                            Thread.Sleep(2000);
                                                                            break;
                                                                        case 4:
                                                                            if(updateChoice == change.Count()) break;
                                                                            do{Console.Write(" Số lượng : ");}
                                                                            while(!int.TryParse(Console.ReadLine(),out quantity) || quantity < 0 || quantity > cs.Quantity);
                                                                            cage = cgbl.GetCageById(cs.CageID);
                                                                            cgbl.UpdateCage(cage.CageID,cage.Cage_Name,cage.Max_Capacity,cage.Current_Capacity - quantity, cage.CageStatus);
                                                                            if(csbl.UpdateChickenStatus(cs.ChickenID,cs.Quantity-quantity,cs.chickenStatus,cs.CageID,cs.CageID)){
                                                                                DisplaySP.PrintColor(" Cập nhật hoàn tất!",ConsoleColor.Green,true);
                                                                            }
                                                                            else{
                                                                                DisplaySP.PrintColor(" Cập nhật thất bại!",ConsoleColor.Red,true);
                                                                            }
                                                                            Thread.Sleep(2000);
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
                                                    DisplaySP.PrintColor(" Danh sách rỗng :(",ConsoleColor.Red,true);
                                                    Thread.Sleep(2000);
                                                }
                                                break;
                                            case 3:
                                                Console.Write(" Nhập ID : ");                                   
                                                csList = csbl.GetChickenStatus(3,Console.ReadLine()??"0");
                                                if(csList.Count() != 0){
                                                    for(int i = 0 ; i < csList.Count ; i++){
                                                        // csList[i].ShowStatus();
                                                        ShowInfo(ob = csList[0]);
                                                    }
                                                    Thread.Sleep(5000*csList.Count);
                                                }
                                                else{
                                                    DisplaySP.PrintColor(" Không tìm thấy !",ConsoleColor.Red,true);
                                                    Thread.Sleep(2000);
                                                }
                                                break;
                                            case 4: 
                                                // chức năng tìm theo giai đoạn
                                                Console.Write(" Nhập giai đoạn: ");
                                                csList = csbl.GetChickenStatus(1,Console.ReadLine()??"");
                                                
                                                if(csList.Count != 0){
                                                    for(int i = 0 ; i < csList.Count ; i++){
                                                        // csList[i].ShowStatus();
                                                        ShowInfo(ob = csList[i]);
                                                    }
                                                    Thread.Sleep(5000*csList.Count);
                                                    
                                                }
                                                else{
                                                    DisplaySP.PrintColor(" Không tìm thấy !",ConsoleColor.Red,true);
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
                                        subChoice = DisplaySP.Menu("Quản lý chuồng trại",cageMenu);

                                        Cage cg;
                                        List<Cage> cgList;
                                        switch(subChoice){
                                            case 1:
                                                cg = new Cage();
                                                if(cgbl.AddCage((Cage)Create(ob = cg)) != 0){
                                                    DisplaySP.PrintColor(" Thêm thành công !",ConsoleColor.Green,true);
                                                }
                                                else{
                                                    DisplaySP.PrintColor(" Thêm thất bại !",ConsoleColor.Red,true);
                                                }
                                                Thread.Sleep(2000);
                                                break;
                                            case 2:
                                                int id;
                                                string[] columns = {"ID","Tên Chuồng","Sức Chứa Tối Đa","Lượng Gà Hiện Tại","Trạng Thái"};
                                                int[] space = {5,20,20,20,20};
                                                string[] change = {"Đổi tên chuồng","Đổi trạng thái","Mở rộng/Thu nhỏ chuồng","Thoát"};
                                                cgList = cgbl.GetCages(2,"");
                                                if(cgList.Count != 0){
                                                    // update menu------------------------------------
                                                    do{
                                                        Console.Clear();
                                                        cgList = cgbl.GetCages(2,"");
                                                        DisplaySP.PrintColumns(columns,space,1);
                                                        for(int i = 0 ; i < cgList.Count ; i++){
                                                            // cgList[i].ShowChickenRow();
                                                            ShowRow(ob = cgList[i]);
                                                        }
                                                        DisplaySP.PrintColumns(columns,space,2);
                                                        do{Console.WriteLine(" Nhập ID để xem thông tin chi tiết và sửa đổi hoặc 0 để thoát: ");}
                                                        while(!int.TryParse(Console.ReadLine(), out id));
                                                        if(id != 0){
                                                            // tim ga theo id de sua doi
                                                            cg = cgbl.GetCageById(id);
                                                            if(cg == null){
                                                                DisplaySP.PrintColor(" ID không tồn tại!",ConsoleColor.Red,true);
                                                                Thread.Sleep(2000);
                                                            }
                                                            else{    
                                                                do{
                                                                    cg = cgbl.GetCageById(id);
                                                                    Console.Clear();
                                                                    // cg.ShowCage();
                                                                    ShowInfo(ob = cg);
                                                                    updateChoice = DisplaySP.MenuChangeInfo(change);
                                                                    switch(updateChoice){
                                                                        case 1: 
                                                                            Console.Write(" Tên mới: ");
                                                                            string newName = Console.ReadLine()??cg.Cage_Name;
                                                                            List<Cage>? cageList;
                                                                            cageList = cgbl.GetCages(1,newName);
                                                                            if(cageList.Count != 0){
                                                                                DisplaySP.PrintColor(" Chuồng đã tồn tại !",ConsoleColor.Red,true);
                                                                                Thread.Sleep(2000);
                                                                                break;
                                                                            }
                                                                            if(cgbl.UpdateCage(cg.CageID,newName,cg.Max_Capacity,cg.Current_Capacity,cg.CageStatus)){
                                                                                DisplaySP.PrintColor(" Cập nhật hoàn tất!",ConsoleColor.Green,true);

                                                                            }
                                                                            else{
                                                                                DisplaySP.PrintColor(" Cập nhật thất bại!",ConsoleColor.Red,true);
                                                                            }
                                                                            Thread.Sleep(2000);
                                                                            break;
                                                                        case 2:
                                                                            int myChoice;
                                                                            do{Console.Write("Trạng Thái Mới - [1] Hoạt Động  [2] Đóng  [3] Bảo Trì : ");}
                                                                            while(!int.TryParse(Console.ReadLine(),out myChoice) || (myChoice != 1 && myChoice != 2 && myChoice != 3));
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
                                                                            if(cgbl.UpdateCage(cg.CageID,cg.Cage_Name,cg.Max_Capacity,cg.Current_Capacity,cg.CageStatus)){
                                                                                DisplaySP.PrintColor(" Cập nhật hoàn tất!",ConsoleColor.Green,true);
                                                                            }
                                                                            else{
                                                                                DisplaySP.PrintColor(" Cập nhật thất bại!",ConsoleColor.Red,true);
                                                                            }
                                                                            Thread.Sleep(2000);
                                                                            break;
                                                                        case 3:
                                                                            int quantity;
                                                                            do{Console.Write(" [1] Mở rộng  [2] Thu nhỏ: ");}
                                                                            while(!int.TryParse(Console.ReadLine(),out myChoice) || (myChoice != 1 && myChoice != 2));
                                                                            switch(myChoice){
                                                                                case 1:
                                                                                    do{Console.Write(" Số gà có thể chứa thêm: ");}
                                                                                    while(!int.TryParse(Console.ReadLine(),out quantity) || quantity < 0);
                                                                                    if(cgbl.UpdateCage(cg.CageID,cg.Cage_Name,cg.Max_Capacity+quantity,cg.Current_Capacity,cg.CageStatus)){
                                                                                        DisplaySP.PrintColor(" Cập nhật hoàn tất",ConsoleColor.Green,true);
                                                                                    }
                                                                                    else{
                                                                                        DisplaySP.PrintColor(" Cập nhật thất bại!",ConsoleColor.Red,true);
                                                                                    }
                                                                                    Thread.Sleep(2000);
                                                                                    break;
                                                                                case 2:
                                                                                    do{Console.Write(" Sức chứa giảm đi : ");}
                                                                                    while(!int.TryParse(Console.ReadLine(),out quantity) || quantity < 0);
                                                                                    if(cg.Max_Capacity - quantity < cg.Current_Capacity){
                                                                                        Console.WriteLine(" Hãy chuyển {0} con gà trước khi thu hẹp chuồng tránh quá sức chứa !",cg.Current_Capacity - (cg.Max_Capacity - quantity));
                                                                                        Thread.Sleep(2000);
                                                                                        break;
                                                                                    }
                                                                                    if(cgbl.UpdateCage(cg.CageID,cg.Cage_Name,cg.Max_Capacity-quantity,cg.Current_Capacity,cg.CageStatus)){
                                                                                        DisplaySP.PrintColor(" Cập nhật hoàn tất",ConsoleColor.Green,true);
                                                                                    }
                                                                                    else{
                                                                                        DisplaySP.PrintColor(" Cập nhật thất bại!",ConsoleColor.Red,true);
                                                                                    }
                                                                                    Thread.Sleep(2000);
                                                                                    break;
                                                                                default:
                                                                                    break;
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
                                                    DisplaySP.PrintColor(" Danh sách rỗng :(",ConsoleColor.Red,true);
                                                    Thread.Sleep(2000);
                                                }
                                                break;
                                            case 3:
                                                do{Console.Write(" Nhập ID : ");}
                                                while(!int.TryParse(Console.ReadLine(), out id));
                                                cg = cgbl.GetCageById(id);
                                                if(cg != null){
                                                    // cg.ShowCage();
                                                    ShowInfo(ob = cg);
                                                    Thread.Sleep(5000);
                                                }
                                                else{
                                                    DisplaySP.PrintColor(" Không tìm thấy !",ConsoleColor.Red,true);
                                                    Thread.Sleep(2000);
                                                }
                                                break;
                                            case 4:
                                                Console.Write(" Nhập tên: ");
                                                cgList = cgbl.GetCages(1,Console.ReadLine()??"");
                                                if(cgList.Count != 0){
                                                    // cgList[0].ShowCage();
                                                    ShowInfo(ob = cgList[0]);
                                                    Thread.Sleep(5000);
                                                }
                                                else{
                                                    DisplaySP.PrintColor(" Không tìm thấy !",ConsoleColor.Red,true);
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
                    else{
                        DisplaySP.PrintColor(" Mật khẩu không chính xác!",ConsoleColor.Red,true);
                        Thread.Sleep(2000);
                    }
                }
                else{
                    DisplaySP.PrintColor(" Tên tài khoản không tồn tại!",ConsoleColor.Red,true);
                    Thread.Sleep(2000);
                }
            }
            while(loginChoice != loginMenu.Length);
        }   

        public static object Create(object ob) {
            DisplaySP.PrintColor("───────────────────────────────────────────────────────────────────",ConsoleColor.Cyan,true);
            if(ob is Chicken){
                decimal parameter;
                Chicken ck = new Chicken();
                DisplaySP.WriteMid(" << Loại Gà Mới >> ",0,1,2);
                Console.Write("Tên gà   : ");
                ck.ChickenName = Console.ReadLine() ?? "Unset";
                DisplaySP.SetCurSor(2,Console.CursorTop);
                do{Console.Write("Giá Nhập : ");}
                while(!decimal.TryParse(Console.ReadLine() , out parameter));
                ck.ImportPrice = parameter;
                DisplaySP.SetCurSor(2,Console.CursorTop);
                do{Console.Write("Giá Xuất : ");}
                while(!decimal.TryParse(Console.ReadLine() , out parameter));
                ck.ExportPrice = parameter;
                DisplaySP.SetCurSor(2,Console.CursorTop);
                Console.Write("Mô tả    : ");
                ck.Decription = Console.ReadLine() ?? "";
                DisplaySP.SetCurSor(2,Console.CursorTop);
                ob = ck;
            }
            if(ob is Cage){
                int parameter;
                int myChoice;
                Cage cg = new Cage();
                DisplaySP.WriteMid(" << Chuồng Mới >> ",0,1,2);
                Console.Write("Tên chuồng        : ");
                cg.Cage_Name = Console.ReadLine() ?? "Unset";
                DisplaySP.SetCurSor(2,Console.CursorTop);
                Console.Write("Sức chứa gà tối đa: ");
                int.TryParse(Console.ReadLine() , out parameter);
                cg.Max_Capacity = parameter;
                DisplaySP.SetCurSor(2,Console.CursorTop);
                Console.Write("Lượng gà hiện tại : ");
                int.TryParse(Console.ReadLine() , out parameter);

                // xử lí nhập số lượng hiện tại lớn hơn tối đa
                // while(parameter > cg.Max_Capacity){
                //     Console.WriteLine(@"Anh bạn à chuồng tối đa chứa được {cg.Max_Capacity} con thôi, {parameter - cg.Max_Capacity} con nữa thì vứt đi đâu? Nhập lại hộ tôi cái!");
                //     int.TryParse(Console.ReadLine() , out parameter);
                // }

                cg.Current_Capacity = parameter;
                DisplaySP.SetCurSor(2,Console.CursorTop);
                Console.Write("Trạng thái chuồng - [1] Hoạt Động  [2] Đóng  [3] Bảo Trì :");
                int.TryParse(Console.ReadLine(),out myChoice);
                while(myChoice != 1 && myChoice != 2 && myChoice != 3){
                    DisplaySP.SetCurSor(2,Console.CursorTop);
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
                ob = cg;                
            }
            if(ob is ChickenStatus){
                int parameter;
                int myChoice;
                ChickenStatus  cs = new ChickenStatus();
                Console.WriteLine(" << Thêm Nhóm Gà Mới >> ");
                DisplaySP.SetCurSor(2,Console.CursorTop);
                do{Console.Write("ID Loại Gà: ");}
                while(!int.TryParse(Console.ReadLine() , out parameter));
                cs.ChickenID = parameter;
                DisplaySP.SetCurSor(2,Console.CursorTop);
                do{Console.Write("Số Lượng  : ");}
                while(!int.TryParse(Console.ReadLine() , out parameter));
                cs.Quantity = parameter;
                DisplaySP.SetCurSor(2,Console.CursorTop);
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
                DisplaySP.SetCurSor(2,Console.CursorTop);
                do{Console.Write("ID Chuồng : ");}
                while(!int.TryParse(Console.ReadLine() , out parameter));
                cs.CageID = parameter;
                ob = cs;
            }
            DisplaySP.PrintColor("───────────────────────────────────────────────────────────────────",ConsoleColor.Cyan,true);
            return ob;
       }
        public static void ShowInfo(object ob){
            DisplaySP.PrintColor(DefaultClass.TopLine,ConsoleColor.Cyan,true);
            DisplaySP.BorderCyanColor(" ");
            if(ob is Chicken)
                DisplaySP.WriteMid("Thông Tin Loại Gà",0,1,0);
            if(ob is Cage)
                DisplaySP.WriteMid("Thông Tin Chuồng Trại",0,1,0);
            if(ob is ChickenStatus)
                DisplaySP.WriteMid("Thông Tin Nhóm Gà",0,1,0);
            DisplaySP.PrintColor(DefaultClass.InfoTopLine,ConsoleColor.Cyan,true);

            if(ob is Chicken){
                DisplaySP.PrintInfor("ID Gà   ", ((Chicken)ob).ChickenID.ToString());
                DisplaySP.PrintInfor("Tên Gà  ", ((Chicken)ob).ChickenName);
                DisplaySP.PrintInfor("Giá Nhập", ((Chicken)ob).ImportPrice.ToString());
                DisplaySP.PrintInfor("Giá Xuất", ((Chicken)ob).ExportPrice.ToString());
                DisplaySP.PrintInfor("Mô tả   ", ((Chicken)ob).Decription);
            }

            if(ob is Cage){
                DisplaySP.PrintInfor("ID Chuồng        ", ((Cage)ob).CageID.ToString());
                DisplaySP.PrintInfor("Tên Chuồng       ", ((Cage)ob).Cage_Name);
                DisplaySP.PrintInfor("Sức Chứa Tối Đa  ", ((Cage)ob).Max_Capacity.ToString());
                DisplaySP.PrintInfor("Lượng Gà Hiện Tại", ((Cage)ob).Current_Capacity.ToString());
                DisplaySP.PrintInfor("Trạng Thái Chuồng", ((Cage)ob).CageStatus);
            }

            if(ob is ChickenStatus){
                DisplaySP.PrintInfor("ID Gà    ", ((ChickenStatus)ob).ChickenID.ToString());
                DisplaySP.PrintInfor("Số Lượng ", ((ChickenStatus)ob).Quantity.ToString());
                DisplaySP.PrintInfor("Giai Đoạn", ((ChickenStatus)ob).chickenStatus);
                DisplaySP.PrintInfor("ID Chuồng", ((ChickenStatus)ob).CageID.ToString());
            }
            DisplaySP.PrintColor(DefaultClass.InfoBotLine,ConsoleColor.Cyan,true);
            //end line
        }
        public static void ShowRow(object ob){
            if(ob is Chicken){
                DisplaySP.PrintColor("║ " , ((Chicken)ob).ChickenID.ToString(), ConsoleColor.Cyan,false);
                DisplaySP.PrintCharacterAndEnd(5 - ((Chicken)ob).ChickenID.ToString().Length - 1,' ','║',false);
                Console.Write(" " + ((Chicken)ob).ChickenName);
                DisplaySP.PrintCharacterAndEnd(20 - ((Chicken)ob).ChickenName.Length - 1,' ','║',false);
                Console.Write(" " + ((Chicken)ob).ImportPrice);
                DisplaySP.PrintCharacterAndEnd(12 - ((Chicken)ob).ImportPrice.ToString().Length - 1,' ','║',false);
                Console.Write(" " + ((Chicken)ob).ExportPrice);
                DisplaySP.PrintCharacterAndEnd(12 - ((Chicken)ob).ExportPrice.ToString().Length - 1,' ','║',false);
                string desCutDown = ((Chicken)ob).Decription;    
                if (((Chicken)ob).Decription.Length > 28){
                    desCutDown = ((Chicken)ob).Decription.Remove(25);
                    desCutDown =  string.Concat(desCutDown,"...");
                }
                Console.Write(" " + desCutDown);
                DisplaySP.PrintCharacterAndEnd(30 - desCutDown.ToString().Length - 1,' ','║',true);
            }

            if(ob is Cage ){
                DisplaySP.PrintColor("║ " , ((Cage)ob).CageID.ToString(), ConsoleColor.Cyan,false);
                DisplaySP.PrintCharacterAndEnd(5 - ((Cage)ob).CageID.ToString().Length - 1,' ','║',false);
                Console.Write(" " + ((Cage)ob).Cage_Name);
                DisplaySP.PrintCharacterAndEnd(20 - ((Cage)ob).Cage_Name.Length - 1,' ','║',false);
                Console.Write(" " + ((Cage)ob).Max_Capacity);
                DisplaySP.PrintCharacterAndEnd(20 - ((Cage)ob).Max_Capacity.ToString().Length - 1,' ','║',false);
                Console.Write(" " + ((Cage)ob).Current_Capacity);
                DisplaySP.PrintCharacterAndEnd(20 - ((Cage)ob).Current_Capacity.ToString().Length - 1,' ','║',false);
                Console.Write(" " + ((Cage)ob).CageStatus);
                DisplaySP.PrintCharacterAndEnd(20 - ((Cage)ob).CageStatus.ToString().Length - 1,' ','║',true);
            }

            if(ob is ChickenStatus){
                DisplaySP.PrintColor("║ " , ((ChickenStatus)ob).ChickenID.ToString(), ConsoleColor.Cyan,false);
                DisplaySP.PrintCharacterAndEnd(10 - ((ChickenStatus)ob).ChickenID.ToString().Length - 1,' ','║',false);
                Console.Write(" " + ((ChickenStatus)ob).Quantity);
                DisplaySP.PrintCharacterAndEnd(10 - ((ChickenStatus)ob).Quantity.ToString().Length - 1,' ','║',false);
                Console.Write(" " + ((ChickenStatus)ob).chickenStatus);
                DisplaySP.PrintCharacterAndEnd(20 - ((ChickenStatus)ob).chickenStatus.Length - 1,' ','║',false);
                Console.Write(" " + ((ChickenStatus)ob).CageID);
                DisplaySP.PrintCharacterAndEnd(11 - ((ChickenStatus)ob).CageID.ToString().Length - 1,' ','║',true);
            }

        }
    
    }
    
}

