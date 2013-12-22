using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Input;
using OpenTK;

namespace AsteroidAttack
{
    public enum GameOptionName
    {
        //random
        random,

        //Console
        console_window_height,
        console_color_clientInput,
        console_color_Warning,
        console_color_Error,
        console_color_Ok,

        //Inputs
        mouse,
        control_attack_delay,

        //Viewport
        window_Width,
        window_Height,
        window_state,

        //Camera
        camera_Tolerance,

        //Environment
        environment_min_asteroids,

        //Spaceships
        ship_circle_flick,
        ship_invincibility_time
    }

    static class GameOptions
    {
        //random
        public static Random Random = new Random();

        //Console
        public static int Console_window_height = 400;
        public static Color Console_color_clientInput = Color.Yellow;
        public static Color Console_color_Warning = Color.DarkOrange;
        public static Color Console_color_Error = Color.DarkRed;
        public static Color Console_color_Ok = Color.White;

        //Inputs
        public static MouseDevice Mouse;
        public static int Control_attack_delay = 250;

        //Viewport
        public static int Window_Width = 1400;
        public static int Window_Height = 900;
        public static  WindowState Window_state = WindowState.Normal; 
        
        //Camera
        public static int Camera_Tolerance = 120;

        //Environment
        public static int Environment_min_asteroids = 25;

        //Spaceships
        public static int Ship_circle_flick = 200;
        public static int Ship_invincibility_time = 1500;

        
        

    }
}
