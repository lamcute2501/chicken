
namespace ConsolPL
{
    public class DisplaySP
    {
        // public static Int32 Longs = 60;
        #region Print Color
        public static void PrintColor(string str, ConsoleColor color, bool newLine)
        {
            if(newLine){
                Console.ForegroundColor = color;
                Console.WriteLine(str);
                Console.ResetColor();
            }
            else{
                Console.ForegroundColor = color;
                Console.Write(str);
                Console.ResetColor();
            }
        }
        public static void PrintCyanColor(string str, bool newLine)
        {
            if(newLine){
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(str);
                Console.ResetColor();
            }
            else{
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write(str);
                Console.ResetColor();
            }
        }
        public static void PrintCyanColor(string str1, string str2)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(str1);
            Console.ResetColor();
            Console.Write(str2);
        }
        public static void BorderColor(string title, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write("║");
            Console.ResetColor();
            Console.Write($"{title,-63}");
            Console.ForegroundColor = color;
            Console.Write("║\n");
            Console.ResetColor();
        }
        public static void BorderCyanColor(string title)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("║");
            Console.ResetColor();
            Console.Write($"{"",1}"+$"{title,-62}");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("║\n");
            Console.ResetColor();
        }
        public static void BorderLogo(string title)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("║");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"{title,-63}");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("║\n");
            Console.ResetColor();
        }
        
        #endregion

        #region Set Cursor
        
        public static void SetCurSor(int x, int y) => Console.SetCursorPosition(x, y);
        public static void WhileAt(string s, int x, int y)
        {
            try
            {
                Console.SetCursorPosition(x, y);
                Console.Write(s);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        #endregion

        public static int Menu(string title, string[] menuItems)
        {
                        string[] Logo = {
"    ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀ ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣠⣶⣾⠃⠀⣀⠀⠀⠀⠀⠀⠀",
"    ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀ ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢰⣤⣤⣿⡿⠿⠾⣿⡿⠋⠀⠀⠀⠀⠀",
"    ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀ ⠀⠀⢀⣀⣀⡀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢸⣿⠟⢁⣴⠶⣦⣄⡀⠀⠀⠀⠀⠀⠀",
"    ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀ ⠀⠀⠘⣿⣿⣿⡿⠿⣷⣶⣶⣤⣤⣀⡀⠈⢁⣴⣿⣧⣀⣼⡟⢁⣴⣶⣄⡀⠀⠀",
"    ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀ ⠀⠀⠀⣤⣤⣤⣤⣤⣤⣬⣽⣿⣿⣿⡿⠀⣾⣿⣿⣿⣿⣿⣇⠘⢿⣯⡉⠁⠀⠀",
"    ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀ ⠀⠀⠀⢿⣿⣿⣿⣿⣿⠿⢿⣿⣿⣿⣇⣈⣉⣉⡀⠹⣿⡿⢿⣷⠀⠀⠀⠀⠀⠀",
"    ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀ ⠀⠀⠀⠘⣿⡿⠛⠉⣁⣴⣾⣿⣿⣿⣿⣿⣿⣿⡟⢀⣉⣠⣄⠙⠀⠀⠀⠀⠀⠀",
"    ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀ ⠀⠀⠀⠘⢿⣷⣦⣤⣄⠀⢸⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡆⠀⠀⠀⠀⠀⠀",
"    ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀ ⠀⠀⠀⠀⠀⠈⠻⣿⣏⣀⠀⠛⠛⠻⠿⠿⠿⠛⠋⣠⣿⣿⣿⡇⠀⠀⠀⠀⠀⠀",
"    ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀ ⠀⠀⠀⠀⠀⠀⠀⠈⠛⢿⣿⣿⣷⣶⣶⣶⣶⣾⣿⣿⣿⣿⡿⠃⠀⠀⠀⠀⠀⠀",
"    ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀ ⠀⠀⠀⠀⣦⣀⣴⠾⠃⠀⠈⠙⠻⠿⠿⣿⣿⣿⡿⠿⠟⠋⠀⠀⠀⠀⠀⠀⠀⠀",
"    ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀ ⠀⠀⠀⠐⠛⣿⣅⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⢤⣄⣀⣠⣶⠞⠃⠀⠀⠀⠀",
"    ⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀ ⠀⠀⠀⠀⠸⡟⠻⠆⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⣿⠻⠟⠉⠉⠉⠀⠀⠀⠀"
            };
            
            string[] appName = {
@"  _____ _     _      _               ______                    ",
@" /  __ \ |   (_)    | |              |  ___|                   ",
@" | /  \/ |__  _  ___| | _____ _ __   | |_ __ _ _ __ _ __ ___   ",
@" | |   | '_ \| |/ __| |/ / _ \ '_ \  |  _/ _` | '__| '_ ` _ \  ",
@" | \__/\ | | | | (__|   <  __/ | | | | || (_| | |  | | | | | | ",
@"  \____/_| |_|_|\___|_|\_\___|_| |_| \_| \__,_|_|  |_| |_| |_| ",
@"                                                               "};

            string topLine = "╔═══════════════════════════════════════════════════════════════╗";
            string line = "╟───────────────────────────────────────────────────────────────╢";
            string endLine = "╚═══════════════════════════════════════════════════════════════╝";
            PrintCyanColor(topLine,true);
            for (int i = 0; i < appName.Length; i++)
                BorderLogo(appName[i]);
            PrintCyanColor(line,true);
            int choice = 0;
            for (int i = 0; i < menuItems.Length; i++)
            {
                
                BorderCyanColor($" [{i + 1}]. " + menuItems[i]);
                // PrintCharacterAndEnd(57 - menuItems[i].Length, ' ', '║', true);
            }
            PrintCyanColor(line,true);
            BorderCyanColor(" "+title);
            // PrintCharacterAndEnd(62 - title.Length, ' ', '║', true);
            PrintCyanColor(line,true);
            BorderCyanColor("");
            BorderCyanColor("");
            PrintCyanColor(endLine,true);
            // do
            // {
                WhileAt("<< Vui lòng lựa chọn >>", 21, Console.CursorTop - 3); //23
                SetCurSor(Console.CursorLeft - 12, Console.CursorTop+1);
                // SetCurSor(Console.CursorLeft - 1, Console.CursorTop);
                    int.TryParse(Console.ReadLine(), out choice);
                    if(choice <= 0 || choice > menuItems.Length)
                    {
                        SetCurSor(Console.CursorLeft, Console.CursorTop+1);
                        PrintColor(" Lựa chọn không hợp lệ!",ConsoleColor.Red, false);
                        Thread.Sleep(2000);
                        return -1;
                    }
                    SetCurSor(Console.CursorLeft, Console.CursorTop+1);

                    // SetCurSor(Console.CursorLeft - 12, Console.CursorTop+6);
            // }
            // while (choice > menuItems.Length || choice <= 0);
            return choice;
        }

        public static void PrintCharacterAndEnd(int amount, char character, char endCharacter, bool newLine)
        {
            if(newLine){
                for(int i = 1 ; i <= amount ; i++)
                    PrintCyanColor(character+"",false);
                PrintCyanColor(endCharacter+"",true);
            }
            else{
                for(int i = 1 ; i <= amount ; i++)
                    PrintCyanColor(character+"",false);
                PrintCyanColor(endCharacter+"",false);
                // Console.Write(endCharacter);
            }
        }

        public static void PrintColumns(string[] columns, int[] space, int filter)
        {
            //╔════╦═════════════════════════════════════════════╗
            //║ ID ║                                             ║             
            //╟────╫─────────────────────────────────────────────╢
            //║    ║                                             ║
            //╚════╩═════════════════════════════════════════════╝





            if (filter == 1)
            {
                // hien thi top line
                PrintCyanColor("╔",false);
                for (int i = 0; i < columns.Count(); i++)
                {
                    if (i != columns.Count() - 1)
                        PrintCharacterAndEnd(space[i], '═', '╦', false);
                    else
                        PrintCharacterAndEnd(space[i], '═', '╗', true);
                }

                // hien thi ten cot
                PrintCyanColor("║",false);
                for (int i = 0; i < columns.Count(); i++)
                {
                    Console.Write(" " + columns[i]);
                    if (i != columns.Count() - 1) PrintCharacterAndEnd(space[i] - 1 - columns[i].Length, ' ', '║', false);
                    else PrintCharacterAndEnd(space[i] - 1 - columns[i].Length, ' ', '║', true);
                }

                // line ngan cach
                PrintCyanColor("╟",false);
                for (int i = 0; i < columns.Count(); i++)
                {
                    if (i != columns.Count() - 1) PrintCharacterAndEnd(space[i], '─', '╫', false);
                    else PrintCharacterAndEnd(space[i], '─', '╢', true);
                }
            }
            else
            {
                // hien thi bot line
                PrintCyanColor("╚",false);
                for (int i = 0; i < columns.Count(); i++)
                {
                    if (i != columns.Count() - 1)
                        PrintCharacterAndEnd(space[i], '═', '╩', false);
                    else
                        PrintCharacterAndEnd(space[i], '═', '╝', true);
                }
            }

        }

        public static int MenuChangeInfo(string[] infoWillChange)
        {
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
            PrintCharacterAndEnd(46, ' ', '│', true);
            for (int i = 0; i < infoWillChange.Length; i++)
            {
                Console.Write($"        │ [{i + 1}]. " + infoWillChange[i]);
                PrintCharacterAndEnd(40 - infoWillChange[i].Length, ' ', '│', true);
            }
            Console.Write("        │");
            PrintCharacterAndEnd(46, ' ', '│', true);
            Console.WriteLine("        └──────────────────────────────────────────────┘");
            do
            {
                Console.Write(" Lựa chọn: ");
                try
                {
                    int.TryParse(Console.ReadLine(), out choice);
                }
                catch
                {
                    Console.WriteLine(" Lựa chọn không hợp lệ!");
                    continue;
                }
            }
            while (choice > infoWillChange.Length || choice <= 0);
            return choice;
        }
        public static string GetPassword(){
        var pass = string.Empty;
        ConsoleKey key;
        do{
            var keyInfo = Console.ReadKey(intercept: true);
            key = keyInfo.Key;
            if (key == ConsoleKey.Backspace && pass.Length > 0){
                Console.Write("\b \b");
                pass = pass[0..^1];
            }
            else if (!char.IsControl(keyInfo.KeyChar)){
                Console.Write("*");
                pass += keyInfo.KeyChar;
            }
        } while (key != ConsoleKey.Enter);
        Console.WriteLine();
        return pass;
    }
    }

}
