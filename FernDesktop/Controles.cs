using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FernDesktop
{
    class Controles
    {
        public static void ReadCommand(string command)
        {
            if (command.StartsWith("rightdown"))
                RightDown(command);
            else if (command.StartsWith("leftdown"))
                LeftDown(command);
            else if (command.StartsWith("rightup"))
                RightUp(command);
            else if (command.StartsWith("leftup"))
                LeftUp(command);
            else if (command.StartsWith("key"))
                PressKey(command);
            else if (command.StartsWith("move"))
                MoveMouse(command);


        }

        [DllImport("user32.dll")]
        private static extern void mouse_event(UInt32 dwFlags, UInt32 dx, UInt32 dy, UInt32 dwData, IntPtr dwExtraInfo);

        private const uint MOUSEEVENTF_LEFTDOWN = 0x02;
        private const uint MOUSEEVENTF_LEFTUP = 0x04;
        private const uint MOUSEEVENTF_RIGHTDOWN = 0x08;
        private const uint MOUSEEVENTF_RIGHTUP = 0x10;
        private const string abcdr = "abcdefghijklmnñopqrstuvwxyzç";

        private static void MoveMouse(string command)
        {
            string[] into = command.Substring(4).Split(':');
            Cursor.Position = new Point(int.Parse(into[0]), int.Parse(into[1]));
        }

        private static void RightDown(string command)
        {
            string[] into = command.Substring(9).Split(':');
            Cursor.Position = new Point(int.Parse(into[0]), int.Parse(into[1]));
            SendRightDown(Convert.ToUInt32(int.Parse(into[0])), Convert.ToUInt32(int.Parse(into[1])));
        }

        private static void LeftDown(string command)
        {
            string[] into = command.Substring(8).Split(':');
            Cursor.Position = new Point(int.Parse(into[0]), int.Parse(into[1]));
            SendLeftDown(Convert.ToUInt32(int.Parse(into[0])), Convert.ToUInt32(int.Parse(into[1])));
        }

        private static void RightUp(string command)
        {
            string[] into = command.Substring(7).Split(':');
            Cursor.Position = new Point(int.Parse(into[0]), int.Parse(into[1]));
            SendRightUp(Convert.ToUInt32(int.Parse(into[0])), Convert.ToUInt32(int.Parse(into[1])));
        }

        private static void LeftUp(string command)
        {
            string[] into = command.Substring(6).Split(':');
            Cursor.Position = new Point(int.Parse(into[0]), int.Parse(into[1]));
            SendLeftUp(Convert.ToUInt32(int.Parse(into[0])), Convert.ToUInt32(int.Parse(into[1])));
        }

        public static void SendRightDown(UInt32 posX, UInt32 posY)
        {
            mouse_event(MOUSEEVENTF_RIGHTDOWN, posX, posY, 0, new IntPtr());
        }

        public static void SendLeftDown(UInt32 posX, UInt32 posY)
        {
            mouse_event(MOUSEEVENTF_LEFTDOWN, posX, posY, 0, new IntPtr());
        }

        public static void SendRightUp(UInt32 posX, UInt32 posY)
        {
            mouse_event(MOUSEEVENTF_RIGHTUP, posX, posY, 0, new IntPtr());
        }

        public static void SendLeftUp(UInt32 posX, UInt32 posY)
        {
            mouse_event(MOUSEEVENTF_LEFTUP, posX, posY, 0, new IntPtr());
        }

        private static void PressKey(string command)
        {
            command = command.Substring(3);
            try
            {
                if (command.ToUpper() == "BACK")
                    command = "BACKSPACE";
                if (abcdr.Contains(command.ToLower()))
                    SendKeys.SendWait(command.ToLower());
                else
                    SendKeys.SendWait("{" + command.ToUpper() + "}");
                return;
            }
            catch (Exception)
            {

            }
        }
    }
}
