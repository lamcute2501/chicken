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
                Console.Clear();
                loginChoice = Menu("Đăng Nhập",loginMenu);

                if(loginChoice == 2){
                    return;
                }

                // phần dăng nhập
                Console.Write(" Tên đăng nhập: ");
                userName = Console.ReadLine()??"";
                if(usbl.CheckUserName(userName)){
                    Console.Write(" Mật khẩu: ");
                    password = Console.ReadLine()??"";
                    if(usbl.CheckPassword(userName,password)){
                        Console.WriteLine(" Đăng nhập thành công !");
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
                            mainChoice = Menu("Quản lý trại gà - CFMS",mainMenu);

                            switch(mainChoice){
                                case 1:
                                    do{ 
                                        Console.Clear();
                                        subChoice = Menu("Quản lý loại gà",chickenMenu);

                                        
                                        List<Chicken> ckList;

                                        switch(subChoice){
                                            case 1:
                                                Chicken ck = new Chicken();
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
                                                if(ckList.Count != 0){
                                                    // update menu------------------------------------
                                                    do{
                                                        Console.Clear();
                                                        ckList = ckbl.GetChickens(2,"");
                                                        PrintColumns(columns,space,1);
                                                        for(int i = 0 ; i < ckList.Count ; i++){
                                                            // ckList[i].ShowChickenRow();
                                                            ShowRow(ob = ckList[i]);
                                                        }
                                                        PrintColumns(columns,space,2);
                                                        do{Console.WriteLine(" Nhập ID để xem thông tin chi tiết và sửa đổi hoặc 0 để thoát: ");}
                                                        while(!int.TryParse(Console.ReadLine(), out id));
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
                                                                    // ck.ShowChicken();
                                                                    ShowInfo(ob = ck);
                                                                    updateChoice = MenuChangeInfo(change);
                                                                    switch(updateChoice){
                                                                        case 1: 
                                                                            do{Console.Write(" Giá nhập mới: ");}
                                                                            while(!decimal.TryParse(Console.ReadLine(),out newPrice));
                                                                            if(ckbl.UpdateChicken(ck.ChickenName,newPrice,ck.ExportPrice,ck.Decription,ck.ChickenID)){
                                                                                Console.WriteLine(" Cập nhật hoàn tất!");

                                                                            }
                                                                            else{
                                                                                Console.WriteLine(" Cập nhật thất bại!");
                                                                            }
                                                                            Thread.Sleep(2000);
                                                                            break;
                                                                        case 2:
                                                                            do{Console.Write(" Giá xuất mới: ");}
                                                                            while(!decimal.TryParse(Console.ReadLine(),out newPrice));
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
                                                do{Console.Write(" Nhập ID: ");}
                                                while(!int.TryParse(Console.ReadLine(), out id));
                                                ck = ckbl.GetChickenById(id);
                                                if(ck != null){
                                                    // ck.ShowChicken();
                                                    ShowInfo(ob = ck);
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
                                                if(ckList.Count != 0){
                                                    // ckList[0].ShowChicken();
                                                    ShowInfo(ob = ckList[0]);
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
                                                    Console.WriteLine(" Chuồng này đang {0}, không thể thêm gà!",cage.CageStatus);
                                                    Thread.Sleep(2000);
                                                    break;
                                                }
                                                if(cage.Max_Capacity < cs.Quantity || cage.Max_Capacity-cage.Current_Capacity < cs.Quantity){
                                                    Console.WriteLine(" Chuồng chỉ còn {0} chỗ trống, không đủ sức chứa {1} con gà",cage.Max_Capacity-cage.Current_Capacity,cs.Quantity);
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
                                                csList = csbl.GetChickenStatus(2,"");
                                                if(csList.Count != 0){
                                                    // update menu------------------------------------
                                                    do{
                                                        csList = csbl.GetChickenStatus(2,"");
                                                        Console.Clear();

                                                        PrintColumns(columns,space,1);
                                                        for(int i = 0 ; i < csList.Count ; i++){
                                                            Console.Write($"║ {i+1}");
                                                            PrintCharacterAndEnd(3 - i.ToString().Length,' ',' ',false);
                                                            // csList[i].ShowStatusRow();
                                                            ShowRow(ob = csList[i]);
                                                        }
                                                        PrintColumns(columns,space,2);
                                                        do{Console.WriteLine(" Nhập STT để xem thông tin chi tiết và sửa đổi hoặc 0 để thoát: ");}
                                                        while(!int.TryParse(Console.ReadLine(), out stt) || stt < 0);
                                                        if(stt != 0 && stt <= csList.Count){
                                                            // giảm 1 đơn vị do list bắt đầu từ 0
                                                            stt--;
                                                            
                                                            // tim nhom ga theo id de sua doi
                                                            cs = csbl.GetChickenStatus(csList[stt].ChickenID,csList[stt].CageID,csList[stt].chickenStatus);
                                                            if(cs == null){
                                                                Console.WriteLine(" STT không hợp lệ!");
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

                                                                do{
                                                                    cs = csbl.GetChickenStatus(csList[stt].ChickenID,csList[stt].CageID,csList[stt].chickenStatus);
                                                                    Console.Clear();
                                                                    // cs.ShowStatus();
                                                                    ShowInfo(ob = cs);
                                                                    updateChoice = MenuChangeInfo(change);
                                                                    switch(updateChoice){
                                                                        case 1: 
                                                                            do{Console.Write(" ID chuồng mới: ");}
                                                                            while(!int.TryParse(Console.ReadLine(),out newId) || cs.CageID == newId);
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
                                                                                Console.WriteLine(" Cập nhật hoàn tất!");
                                                                            }
                                                                            else{
                                                                                Console.WriteLine(" Cập nhật thất bại!");
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
                                                                                Console.WriteLine(" Cập nhật hoàn tất!");
                                                                            }
                                                                            else{
                                                                                Console.WriteLine(" Cập nhật thất bại!");
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
                                                                                    Console.WriteLine(" Cập nhật thành công!");
                                                                                }
                                                                                else{
                                                                                    Console.WriteLine(" Cập nhật thất bại!");
                                                                                }
                                                                            }
                                                                            Thread.Sleep(2000);
                                                                            break;
                                                                        case 4:
                                                                            do{Console.Write(" Số lượng : ");}
                                                                            while(!int.TryParse(Console.ReadLine(),out quantity) || quantity < 0 || quantity > cs.Quantity);
                                                                            cage = cgbl.GetCageById(cs.CageID);
                                                                            cgbl.UpdateCage(cage.CageID,cage.Cage_Name,cage.Max_Capacity,cage.Current_Capacity - quantity, cage.CageStatus);
                                                                            if(csbl.UpdateChickenStatus(cs.ChickenID,cs.Quantity-quantity,cs.chickenStatus,cs.CageID,cs.CageID)){
                                                                                Console.WriteLine(" Cập nhật hoàn tất!");
                                                                            }
                                                                            else{
                                                                                Console.WriteLine(" Cập nhật thất bại!");
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
                                                    Console.WriteLine(" Danh sách rỗng :(");
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
                                                    Console.WriteLine(" Không tìm thấy !");
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

                                        Cage cg;
                                        List<Cage> cgList;
                                        switch(subChoice){
                                            case 1:
                                                cg = new Cage();
                                                if(cgbl.AddCage(cg.CreateCage()) != 0){
                                                    Console.WriteLine(" Thêm thành công !");
                                                }
                                                else{
                                                    Console.WriteLine(" Thêm thất bại !");
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
                                                        PrintColumns(columns,space,1);
                                                        for(int i = 0 ; i < cgList.Count ; i++){
                                                            // cgList[i].ShowChickenRow();
                                                            ShowRow(ob = cgList[i]);
                                                        }
                                                        PrintColumns(columns,space,2);
                                                        do{Console.WriteLine(" Nhập ID để xem thông tin chi tiết và sửa đổi hoặc 0 để thoát: ");}
                                                        while(!int.TryParse(Console.ReadLine(), out id));
                                                        if(id != 0){
                                                            // tim ga theo id de sua doi
                                                            cg = cgbl.GetCageById(id);
                                                            if(cg == null){
                                                                Console.WriteLine(" ID không tồn tại!");
                                                                Thread.Sleep(2000);
                                                            }
                                                            else{    
                                                                do{
                                                                    cg = cgbl.GetCageById(id);
                                                                    Console.Clear();
                                                                    // cg.ShowCage();
                                                                    ShowInfo(ob = cg);
                                                                    updateChoice = MenuChangeInfo(change);
                                                                    switch(updateChoice){
                                                                        case 1: 
                                                                            Console.Write(" Tên mới: ");
                                                                            string newName = Console.ReadLine()??cg.Cage_Name;
                                                                            List<Cage>? cageList;
                                                                            cageList = cgbl.GetCages(1,newName);
                                                                            if(cageList.Count != 0){
                                                                                Console.WriteLine(" Chuồng đã tồn tại !");
                                                                                Thread.Sleep(2000);
                                                                                break;
                                                                            }
                                                                            if(cgbl.UpdateCage(cg.CageID,newName,cg.Max_Capacity,cg.Current_Capacity,cg.CageStatus)){
                                                                                Console.WriteLine(" Cập nhật hoàn tất!");

                                                                            }
                                                                            else{
                                                                                Console.WriteLine(" Cập nhật thất bại!");
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
                                                                                Console.WriteLine(" Cập nhật hoàn tất!");
                                                                            }
                                                                            else{
                                                                                Console.WriteLine(" Cập nhật thất bại!");
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
                                                                                        Console.WriteLine(" Cập nhật hoàn tất");
                                                                                    }
                                                                                    else{
                                                                                        Console.WriteLine(" Cập nhật thất bại!");
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
                                                                                        Console.WriteLine(" Cập nhật hoàn tất");
                                                                                    }
                                                                                    else{
                                                                                        Console.WriteLine(" Cập nhật thất bại!");
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
                                                    Console.WriteLine(" Danh sách rỗng :(");
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
                                                    Console.WriteLine(" Không tìm thấy !");
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
                    else{
                        Console.WriteLine(" Mật khẩu không chính xác!");
                        Thread.Sleep(2000);
                    }
                }
                else{
                    Console.WriteLine(" Tên tài khoản không tồn tại!");
                    Thread.Sleep(2000);
                }

            }
            while(loginChoice != loginMenu.Length);
            

            
        }   

        private static int Menu(string title ,string[] menuItems ){
            string appName = @"║  _____ _     _      _               ______                    ║
║ /  __ \ |   (_)    | |              |  ___|                   ║
║ | /  \/ |__  _  ___| | _____ _ __   | |_ __ _ _ __ _ __ ___   ║
║ | |   | '_ \| |/ __| |/ / _ \ '_ \  |  _/ _` | '__| '_ ` _ \  ║
║ | \__/\ | | | | (__|   <  __/ | | | | || (_| | |  | | | | | | ║
║  \____/_| |_|_|\___|_|\_\___|_| |_| \_| \__,_|_|  |_| |_| |_| ║
║                                                               ║";
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
                Console.WriteLine(endCharacter);
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
       
        public static void ShowInfo(object ob){
            if(ob is Chicken){
                Console.WriteLine("┌─────────────────────────Thông Tin Loại Gà─────────────────────────┐");
                Console.WriteLine("     ID Gà   : " + ((Chicken)ob).ChickenID);
                Console.WriteLine("     Tên Gà  : " + ((Chicken)ob).ChickenName);
                Console.WriteLine("     Giá Nhập: " + ((Chicken)ob).ImportPrice);
                Console.WriteLine("     Giá Xuất: " + ((Chicken)ob).ExportPrice);
                Console.WriteLine("     Mô tả   : " + ((Chicken)ob).Decription);
            }
            

            if(ob is Cage){
                Console.WriteLine("┌───────────────────────Thông Tin Chuồng Trại───────────────────────┐");
                Console.WriteLine("    ID Chuồng         : " +((Cage)ob).CageID);
                Console.WriteLine("    Tên Chuồng        : " +((Cage)ob).Cage_Name);
                Console.WriteLine("    Sức Chứa Tối Đa   : " +((Cage)ob).Max_Capacity);
                Console.WriteLine("    Lượng Gà Hiện Tại : " +((Cage)ob).Current_Capacity);
                Console.WriteLine("    Trạng Thái Chuồng : " +((Cage)ob).CageStatus);
            }

            if(ob is ChickenStatus){
                Console.WriteLine("┌─────────────────────────Thông Tin Nhóm Gà─────────────────────────┐");
                Console.WriteLine("     ID Gà     : " + ((ChickenStatus)ob).ChickenID);
                Console.WriteLine("     Số Lượng  : " + ((ChickenStatus)ob).Quantity);
                Console.WriteLine("     Giai Đoạn : " + ((ChickenStatus)ob).chickenStatus);
                Console.WriteLine("     ID Chuồng : " + ((ChickenStatus)ob).CageID);
            }

            //end line
                Console.WriteLine("└───────────────────────────────────────────────────────────────────┘");
        }

        public static void ShowRow(object ob){
            if(ob is Chicken){
                Console.Write("║ " + ((Chicken)ob).ChickenID);
                PrintCharacterAndEnd(5 - ((Chicken)ob).ChickenID.ToString().Length - 1,' ','║',false);
                Console.Write(" " + ((Chicken)ob).ChickenName);
                PrintCharacterAndEnd(20 - ((Chicken)ob).ChickenName.Length - 1,' ','║',false);
                Console.Write(" " + ((Chicken)ob).ImportPrice);
                PrintCharacterAndEnd(12 - ((Chicken)ob).ImportPrice.ToString().Length - 1,' ','║',false);
                Console.Write(" " + ((Chicken)ob).ExportPrice);
                PrintCharacterAndEnd(12 - ((Chicken)ob).ExportPrice.ToString().Length - 1,' ','║',false);
                string desCutDown = ((Chicken)ob).Decription;    
                if (((Chicken)ob).Decription.Length > 28){
                    desCutDown = ((Chicken)ob).Decription.Remove(25);
                    desCutDown =  string.Concat(desCutDown,"...");
                }
                Console.Write(" " + desCutDown);
                PrintCharacterAndEnd(30 - desCutDown.ToString().Length - 1,' ','║',true);
            }

            if(ob is Cage ){
                Console.Write("║ " + ((Cage)ob).CageID);
                PrintCharacterAndEnd(5 - ((Cage)ob).CageID.ToString().Length - 1,' ','║',false);
                Console.Write(" " + ((Cage)ob).Cage_Name);
                PrintCharacterAndEnd(20 - ((Cage)ob).Cage_Name.Length - 1,' ','║',false);
                Console.Write(" " + ((Cage)ob).Max_Capacity);
                PrintCharacterAndEnd(20 - ((Cage)ob).Max_Capacity.ToString().Length - 1,' ','║',false);
                Console.Write(" " + ((Cage)ob).Current_Capacity);
                PrintCharacterAndEnd(20 - ((Cage)ob).Current_Capacity.ToString().Length - 1,' ','║',false);
                Console.Write(" " + ((Cage)ob).CageStatus);
                PrintCharacterAndEnd(20 - ((Cage)ob).CageStatus.ToString().Length - 1,' ','║',true);
            }

            if(ob is ChickenStatus){
                Console.Write("║ " + ((ChickenStatus)ob).ChickenID);
                PrintCharacterAndEnd(10 - ((ChickenStatus)ob).ChickenID.ToString().Length - 1,' ','║',false);
                Console.Write(" " + ((ChickenStatus)ob).Quantity);
                PrintCharacterAndEnd(10 - ((ChickenStatus)ob).Quantity.ToString().Length - 1,' ','║',false);
                Console.Write(" " + ((ChickenStatus)ob).chickenStatus);
                PrintCharacterAndEnd(20 - ((ChickenStatus)ob).chickenStatus.Length - 1,' ','║',false);
                Console.Write(" " + ((ChickenStatus)ob).CageID);
                PrintCharacterAndEnd(11 - ((ChickenStatus)ob).CageID.ToString().Length - 1,' ','║',true);
            }

        }
    
    }
    
}

