
namespace ConsolPL
{
    public static class DefaultClass
    {
        public static int Widch = 101;
        public static int LeftMargin = 2;
        public static int Compensate = 1;
        public static int TitleLength = 19;
        public static int InfoLength = Widch - TitleLength - Compensate;

        public static string[] appName = {
        @" _____ _     _      _               ______                    ",
        @"/  __ \ |   (_)    | |              |  ___|                   ",
        @"| /  \/ |__  _  ___| | _____ _ __   | |_ __ _ _ __ _ __ ___   ",
        @"| |   | '_ \| |/ __| |/ / _ \ '_ \  |  _/ _` | '__| '_ ` _ \  ",
        @"| \__/\ | | | | (__|   <  __/ | | | | || (_| | |  | | | | | | ",
        @" \____/_| |_|_|\___|_|\_\___|_| |_| \_| \__,_|_|  |_| |_| |_| ",
        @"                                                              "};
        
        public static string TopLine = $"╔{"".PadLeft(DefaultClass.Widch, '═')}╗";
        public static string  MidLine = $"╟{"".PadLeft(DefaultClass.Widch, '─')}╢";
        public static string  BotLine = $"╚{"".PadLeft(DefaultClass.Widch, '═')}╝";
        public static string  InfoTopLine = $"╟{"".PadLeft(DefaultClass.TitleLength, '─')}┬{"".PadLeft(DefaultClass.InfoLength, '─')}╢";
        public static string  InfoBotLine = $"╚{"".PadLeft(DefaultClass.TitleLength, '═')}╧{"".PadLeft(DefaultClass.InfoLength, '═')}╝";
    }
    public class DisplaySP
    {

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
        public static void PrintColor(string str1, string str2 , ConsoleColor color, bool newLine)
        {
            if(newLine){
                Console.ForegroundColor = color;
                Console.WriteLine(str1);
                Console.ResetColor();
                Console.WriteLine(str2);
            }
            else{
                Console.ForegroundColor = color;
                Console.Write(str1);
                Console.ResetColor();
                Console.Write(str2);
            }
        }
        public static void BorderCyanColor(string title)
        {
            PrintColor("║",ConsoleColor.Cyan,false);
            Console.Write($"{title.PadLeft( title.Length + DefaultClass.LeftMargin,' ').PadRight(DefaultClass.Widch,' ')}");
            PrintColor("║",ConsoleColor.Cyan,true);
        }
        public static void PrintInfor(string title, string infor)
        {
            PrintColor("║",ConsoleColor.Cyan,false);
            Console.Write($"{title.PadLeft(title.Length + DefaultClass.LeftMargin, ' ').PadRight(DefaultClass.TitleLength, ' ')}");
            PrintColor("│",ConsoleColor.Cyan,false);
            Console.Write($"{infor.PadLeft(infor.Length + DefaultClass.LeftMargin, ' ').PadRight(DefaultClass.InfoLength, ' ')}");
            PrintColor("║",ConsoleColor.Cyan,true);
        }
        public static void PrintCharacterAndEnd(int amount, char character, char endCharacter, bool newLine)
        {
            if(newLine){
                for(int i = 1 ; i <= amount ; i++)
                    PrintColor(character+"",ConsoleColor.Cyan,false);
                PrintColor(endCharacter+"",ConsoleColor.Cyan,true);
            }
            else{
                for(int i = 1 ; i <= amount ; i++)
                    PrintColor(character+"",ConsoleColor.Cyan,false);
                PrintColor(endCharacter+"",ConsoleColor.Cyan,false);
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
                PrintColor("╔",ConsoleColor.Cyan,false);
                for (int i = 0; i < columns.Count(); i++)
                {
                    if (i != columns.Count() - 1)
                        PrintCharacterAndEnd(space[i], '═', '╦', false);
                    else
                        PrintCharacterAndEnd(space[i], '═', '╗', true);
                }

                // hien thi ten cot
                PrintColor("║",ConsoleColor.Cyan,false);
                for (int i = 0; i < columns.Count(); i++)
                {
                    Console.Write(" " + columns[i]);
                    if (i != columns.Count() - 1) PrintCharacterAndEnd(space[i] - 1 - columns[i].Length, ' ', '║', false);
                    else PrintCharacterAndEnd(space[i] - 1 - columns[i].Length, ' ', '║', true);
                }

                // line ngan cach
                PrintColor("╟",ConsoleColor.Cyan,false);
                for (int i = 0; i < columns.Count(); i++)
                {
                    if (i != columns.Count() - 1) PrintCharacterAndEnd(space[i], '─', '╫', false);
                    else PrintCharacterAndEnd(space[i], '─', '╢', true);
                }
            }
            else
            {
                // hien thi bot line
                PrintColor("╚",ConsoleColor.Cyan,false);
                for (int i = 0; i < columns.Count(); i++)
                {
                    if (i != columns.Count() - 1)
                        PrintCharacterAndEnd(space[i], '═', '╩', false);
                    else
                        PrintCharacterAndEnd(space[i], '═', '╝', true);
                }
            }
        }

        #endregion

        #region Set Cursor
        
        public static void SetCurSor(int x, int y) => Console.SetCursorPosition(x, y);
        public static void WriteAt(string s, int x, int y)
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
        public static void WriteMid(string s, int BackTop, int NextLine, int LeftLine)
        {
            SetCurSor((DefaultClass.Widch-s.Length)/2+DefaultClass.Compensate, Console.CursorTop-BackTop-1);
            Console.Write(s);
            SetCurSor(LeftLine, Console.CursorTop+NextLine);
        }

        #endregion

        #region Menu Choice

        public static int Menu(string title, string[] menuItems)
        {
            PrintColor(DefaultClass.TopLine, ConsoleColor.Cyan,true);
            for (int i = 0; i < DefaultClass.appName.Length; i++)
            {
                PrintColor("║",ConsoleColor.Cyan,false);
                PrintColor($"{DefaultClass.appName[i].PadLeft((DefaultClass.Widch - DefaultClass.appName[i].Length)/2 + DefaultClass.appName[i].Length + DefaultClass.Compensate,' ').PadRight(DefaultClass.Widch,' ')}",ConsoleColor.Yellow,false);
                PrintColor("║",ConsoleColor.Cyan,true);
            }
            PrintColor(DefaultClass.MidLine, ConsoleColor.Cyan,true);
            BorderCyanColor(" ");
            WriteMid(""+title,0,1,0);
            PrintColor(DefaultClass.MidLine, ConsoleColor.Cyan,true);
            int choice = 0;
            for (int i = 0; i < menuItems.Length; i++)
            {
                BorderCyanColor($"[{i + 1}]. " + menuItems[i]);
            }
            PrintColor(DefaultClass.MidLine, ConsoleColor.Cyan,true);
            BorderCyanColor("");
            BorderCyanColor("");
            PrintColor(DefaultClass.BotLine, ConsoleColor.Cyan,true);
            WriteMid("<< Vui lòng lựa chọn >>",2, 1,0);
            if(DefaultClass.Widch % 2 == 0)
                SetCurSor(DefaultClass.Widch/2, Console.CursorTop);
            else
                SetCurSor(DefaultClass.Widch / 2 + DefaultClass.Compensate, Console.CursorTop);
            int.TryParse(Console.ReadLine(), out choice);
            if(choice <= 0 || choice > menuItems.Length)
            {
                SetCurSor(Console.CursorLeft, Console.CursorTop+1);
                PrintColor(" Lựa chọn không hợp lệ!",ConsoleColor.Red, false);
                Thread.Sleep(2000);
                return -1;
            }
            SetCurSor(Console.CursorLeft, Console.CursorTop+1);
            return choice;
        }

        public static int MenuChangeInfo(string[] infoWillChange)
        {
            int choice = 0;
            PrintColor(DefaultClass.TopLine, ConsoleColor.Cyan,true);
            WriteMid(" << Thay đổi thông tin >> ",0,1,0);
            //┌──────────────Thay đổi thông tin──────────────┐
            //│
            //│
            //│
            //│
            //└──────────────────────────────────────────────┘
            //
            for (int i = 0; i < infoWillChange.Length; i++)
            {
                BorderCyanColor($"[{i+1}]. "+ infoWillChange[i]);
            }
            PrintColor(DefaultClass.MidLine, ConsoleColor.Cyan,true);
            BorderCyanColor("");
            BorderCyanColor("");
            PrintColor(DefaultClass.BotLine, ConsoleColor.Cyan,true);
            WriteMid("<< Vui lòng lựa chọn >>",2, 1,0);

            if(DefaultClass.Widch % 2 == 0)
                SetCurSor(DefaultClass.Widch/2, Console.CursorTop);
            else
                SetCurSor(DefaultClass.Widch / 2 + DefaultClass.Compensate, Console.CursorTop);
            int.TryParse(Console.ReadLine(), out choice);

            if(choice <= 0 || choice > infoWillChange.Length)
            {
                SetCurSor(Console.CursorLeft, Console.CursorTop+1);
                PrintColor(" Lựa chọn không hợp lệ!",ConsoleColor.Red, false);
                Thread.Sleep(2000);
                return -1;
            }
            SetCurSor(Console.CursorLeft, Console.CursorTop+1);
            return choice;
        }
        
        public static string GetPassword()
        {
            var pass = string.Empty;
            ConsoleKey key;
            do
            {
                var keyInfo = Console.ReadKey(intercept: true);
                key = keyInfo.Key;
                if (key == ConsoleKey.Backspace && pass.Length > 0)
                {
                    Console.Write("\b \b");
                    pass = pass[0..^1];
                }
                else if (!char.IsControl(keyInfo.KeyChar))
                {
                    Console.Write("*");
                    pass += keyInfo.KeyChar;
                }
            } while (key != ConsoleKey.Enter);
            Console.WriteLine();
            return pass;
        }

        #endregion

    }
}
