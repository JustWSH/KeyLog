using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KeyLog
{
    
    public class GlobalHooks
    {
        private const int WH_MOUSE_LL = 14;
        private const int WM_LBUTTONDOWN = 0x0201;
        private const int WM_RBUTTONDOWN = 0x0204;
        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;
        private const int WM_KEYUP = 0x0101;


        private static LowLevelMouseKeyProc _procMouse = HookCallback;
        private static LowLevelMouseKeyProc _procKey = HookCallback;
        private static IntPtr _hookIDKey = IntPtr.Zero;
        private static IntPtr _hookIDMouse = IntPtr.Zero;
        private static readonly object _lock = new object();

        public static void StartMouse()
        {
            lock (_lock)
            {
                if (_hookIDMouse == IntPtr.Zero)
                {
                    _hookIDMouse = SetMouseHook(_procMouse);
                }
            }
        }
        public static void StopMouse()
        {
            lock (_lock)
            {
                if (_hookIDMouse != IntPtr.Zero)
                {
                    UnhookWindowsHookEx(_hookIDMouse);
                    _hookIDMouse = IntPtr.Zero;
                }
            }
        }
        public static void StartKey()
        {
            lock (_lock)
            {
                if (_hookIDKey == IntPtr.Zero)
                {
                    _hookIDKey = SetKeyBoardHook(_procKey);
                }
            }
        }
        public static void StopKey()
        {
            lock (_lock)
            {
                if (_hookIDKey != IntPtr.Zero)
                {
                    UnhookWindowsHookEx(_hookIDKey);
                    _hookIDKey = IntPtr.Zero;
                }
            }
        }


        private static IntPtr SetMouseHook(LowLevelMouseKeyProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_MOUSE_LL, proc, GetModuleHandle(curModule.ModuleName), 0);
            }
        }
        private static IntPtr SetKeyBoardHook(LowLevelMouseKeyProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_KEYBOARD_LL, proc, GetModuleHandle(curModule.ModuleName), 0);
            }
        }


        private delegate IntPtr LowLevelMouseKeyProc(int nCode, IntPtr wParam, IntPtr lParam);


        private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && (wParam == (IntPtr)WM_LBUTTONDOWN || wParam == (IntPtr)WM_RBUTTONDOWN))
            {
                Point p = new Point();
                GetCursorPos(out p);
                CaptureScreen(p);
            }
            if (nCode >= 0 && (wParam == (IntPtr)WM_KEYDOWN))
            {
                int vkCode = Marshal.ReadInt32(lParam);
                Keys key = (Keys)vkCode;
                RecordKeystroke(key);
            }
            return CallNextHookEx(_hookIDKey, nCode, wParam, lParam);
        }
        private static void RecordKeystroke(Keys key)
        {
            DateTime now = DateTime.Now;
            string keystroke = $"{now.ToString("yyyy-MM-dd HH:mm:ss.fff")} -> {key}";
            string savePath = $@"{Directory.GetCurrentDirectory()}\LogFiles\KeyBoardRecord";
            AutoCreateFoldor(savePath);
            savePath = $@"{Directory.GetCurrentDirectory()}\LogFiles\KeyBoardRecord\KeyBoardRecord_{now.ToString("yyyyMMdd")}.txt";
            File.AppendAllText(savePath, keystroke + Environment.NewLine);
        }

        private static void GetCursorPos(out Point lpPoint)
        {
            lpPoint = Control.MousePosition;
        }

        private static void CaptureScreen(Point mousePos)
        {
            Bitmap screenshot = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            using (Graphics g = Graphics.FromImage(screenshot))
            {
                g.CopyFromScreen(Screen.PrimaryScreen.Bounds.X, Screen.PrimaryScreen.Bounds.Y, 0, 0, screenshot.Size);
                //using (Pen pen = new Pen(Color.Red, 2))
                //{
                //    // Draw a red dot at the mouse position
                //    g.DrawEllipse(pen, mousePos.X - 5, mousePos.Y - 5, 10, 10);
                //}

                // Create a gradient circle
                using (GraphicsPath path = new GraphicsPath())
                {
                    path.AddEllipse(mousePos.X - 10, mousePos.Y - 10, 20, 20);
                    using (PathGradientBrush pthGrBrush = new PathGradientBrush(path))
                    {
                        Color[] colors = { Color.FromArgb(120, Color.Red), Color.FromArgb(255, Color.Red), Color.FromArgb(120, Color.Red) };
                        Blend blend = new Blend(3);
                        blend.Factors = new float[] { 0.0f, 0.5f, 1.0f };
                        blend.Positions = new float[] { 0.0f, 0.5f, 1.0f };
                        pthGrBrush.InterpolationColors = new ColorBlend(3)
                        {
                            Colors = colors,
                            Positions = blend.Positions
                        };
                        pthGrBrush.FocusScales = new PointF(0.75f, 0.75f);
                        g.FillEllipse(pthGrBrush, mousePos.X - 10, mousePos.Y - 10, 20, 20);
                    }
                }

                // Draw a circle border
                using (Pen pen = new Pen(Color.Red, 2))
                {
                    g.DrawEllipse(pen, mousePos.X - 10, mousePos.Y - 10, 20, 20);
                }

                DateTime now = DateTime.Now;
                string savePath = $@"{Directory.GetCurrentDirectory()}\LogFiles\ScreenShot\{now.ToString("yyyyMMdd")}";
                AutoCreateFoldor(savePath);
                savePath = $@"{Directory.GetCurrentDirectory()}\LogFiles\ScreenShot\{now.ToString("yyyyMMdd")}\Screenshot_{now.ToString("yyyyMMddHHmmssfff")}.png";
                screenshot.Save(savePath, System.Drawing.Imaging.ImageFormat.Png);
                g.Dispose();
                screenshot.Dispose();
            }
        }

        private static void AutoCreateFoldor(string path)
        {
            AutoCreateFoldorSub("", path);
        }

        private static void AutoCreateFoldorSub(string exist, string check)  //Auto create folder if not exist. like: AutoCreateFoldor("", @"E:\SWUtilityApp\SWUtilityApp_v3.1")
        {
            if (!check.Contains(@"\"))
            {
                string finalPath = exist + @"\" + check;
                if (!Directory.Exists(finalPath))
                {
                    Directory.CreateDirectory(finalPath);
                }
                return;
            }
            string[] folder = check.Split(@"\".ToCharArray());
            string path = exist + folder[0];
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string next = "";
            for (int i = 1; i < folder.Length; i++)
            {
                next = next + @"\" + folder[i];
            }
            next = next.Substring(1);
            AutoCreateFoldorSub(path + @"\", next);
        }


        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelMouseKeyProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);
    }

}
