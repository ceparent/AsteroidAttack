using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Input;
using OpenTK;
using QuickFont;

namespace AsteroidAttack
{
    public class ConsoleLine
    {
        public string Text { get; set; }
        public Color Color { get; set; }

        public ConsoleLine(string text, Color color)
        {
            Color = color;
            Text = text;
        }
    }

    static class GameConsole
    {
        static GameConsole()
        {
            Opened = false;
            input = "";
            consoleLog = new LinkedList<ConsoleLine>();
            consoleIndex = 0;
            commandHistory = new List<string>();
        }

        public static bool Opened;


        public static void Update()
        {

        }

        public static void WriteLine(string line, Color color)
        {
            consoleLog.AddFirst(new ConsoleLine(line,color));
        }

        private static List<string> commandHistory;
        private static void SendCommand()
        {
            if (input != "")
            {
                Command(input);
                commandHistory.Add(input);
                consoleIndex = commandHistory.Count;
            }
            input = "";
        }

        private static string input;
        private static LinkedList<ConsoleLine> consoleLog;
        public static void KeyPressEvent(object sender, KeyPressEventArgs e)
        {
            //global

            //Console
            if (!Opened)
                return;
           
            char key = e.KeyChar;

            if (key == '`' || key == '#')
                return;
            else if(key == '\b' && input.Length > 0)
                input = input.Substring(0, input.Length - 1);
            else if (key >= ' ' && key <= '~')
                input += key;
            

        }

        private static int consoleIndex;
        public static void KeyDownEvent(object sender, KeyboardKeyEventArgs e)
        {
            if (!Opened)
                return;

            

            Key key = e.Key;

            switch (key)
            {
                case Key.Clear:
                    break;
                case Key.Delete:
                    break;
                case Key.Up:
                    ChangeLine(-1);
                    break;
                case Key.Down:
                    ChangeLine(1);
                    break;
                case Key.KeypadEnter:
                case Key.Enter:
                    SendCommand();
                    break;
            }

        }

        private static void ChangeLine(int diff)
        {
            if (consoleLog.Count == 0)
            {
                input = "";
                return;
            }

            consoleIndex += diff;
            if (consoleIndex >= commandHistory.Count - 1)
                consoleIndex = commandHistory.Count - 1;
            else if (consoleIndex < 0)
                consoleIndex = 0;

            input = commandHistory[consoleIndex];
        }

        static Font font = new Font("Consolas", 12.0f, FontStyle.Bold);
        static QFont qfont = new QFont(font);
        public static void Draw()
        {
            
            DrawHelper.DrawRectangle(new Rectangle(0, 0, GameOptions.Window_Width, GameOptions.Console_window_height), Color.FromArgb(200, 0, 0, 0), true, true);

            float lineHeight = 20;
            int cpt = 3;
            foreach (ConsoleLine s in consoleLog)
            {
                DrawHelper.DrawString(s.Text, qfont, s.Color, new Vector2(0, GameOptions.Console_window_height - lineHeight * cpt),false);
                cpt++;

                if (GameOptions.Console_window_height - lineHeight * cpt < 0)
                    break;
            }

            DrawHelper.DrawString("> " + input.ToString(), qfont, Color.White, new Vector2(0, GameOptions.Console_window_height - lineHeight * 1.5f),false);
        }

        public static void Command(string command)
        {
            WriteLine("> " + command, GameOptions.Console_color_clientInput);

            string[] commandArray = command.Split(' ');
            switch (commandArray[0].ToLower())
            {
                case "clear":
                    consoleLog.Clear();
                    break;
                case "echo":
                    string line = command.Substring(commandArray[0].Length + 1);
                    WriteLine(line, GameOptions.Console_color_Ok);
                    break;
                case "set":
                    ChangeOption(commandArray);
                    break;
                case "menu":
                    GameActions.OpenMenu();
                    break;
                case "start":
                    GameActions.StartGame();
                    break;
                case "quit":
                case "exit":
                    GameActions.Exit();
                    break;
                default:
                    WriteLine("Command '" + commandArray[0].ToLower() + "' is invalid.", GameOptions.Console_color_Error);
                    break;
            }

        }


        private static void ChangeOption(string[] commandline)
        {
            if (commandline.Length < 3)
            {
                WriteLine("Invalid syntax. Use 'set option value'.", GameOptions.Console_color_Warning);
                return;
            }

            string optionName = commandline[1];
            string value = commandline[2];

            GameOptionName option;
            if (!Enum.TryParse<GameOptionName>(optionName, out option))
            {
                WriteLine("'" + optionName + "' is not a valid option", GameOptions.Console_color_Error);
                return;
            }
            switch (option)
            {
                case GameOptionName.random:
                    GameOptions.Random = new Random(value.GetHashCode());
                    WriteLine("Using seed : " + value, GameOptions.Console_color_Ok);
                    break;
                case GameOptionName.console_window_height:
                    int height;
                    if (int.TryParse(value, out height))
                        GameOptions.Console_window_height = height;
                    else
                        WriteLine("Invalid " + option + " value : " + value, GameOptions.Console_color_Error);
                    break;
                case GameOptionName.console_color_clientInput:
                    break;
                case GameOptionName.console_color_Warning:
                    break;
                case GameOptionName.console_color_Error:
                    break;
                case GameOptionName.console_color_Ok:
                    break;
                case GameOptionName.mouse:
                    break;
                case GameOptionName.control_attack_delay:
                    break;
                case GameOptionName.window_Width:
                    break;
                case GameOptionName.window_Height:
                    break;
                case GameOptionName.window_state:
                    break;
                case GameOptionName.camera_Tolerance:
                    break;
                case GameOptionName.environment_min_asteroids:
                    break;
                case GameOptionName.ship_circle_flick:
                    break;
                case GameOptionName.ship_invincibility_time:
                    break;
            }



        }


    }


}
