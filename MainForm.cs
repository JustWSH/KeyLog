using KeyLog.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KeyLog
{
    public partial class MainForm : Form
    {
        bool isAvoidClose = true;
        public MainForm()
        {
            InitializeComponent();
            MainFormLoad();
        }
        private void MainFormLoad()
        {
            //string language = Properties.Settings.Default.language;
            //if (language == "zh")
            //{
            //    radioButtonZH.Checked = true;
            //    Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("zh");
            //}
            //else if(language == "en")
            //{
            //    Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("en");
            //    radioButtonEN.Checked = true;
            //}
            //ApplyLanguage();
            if(Properties.Settings.Default.screenshot)
            {
                checkBoxScreenShot.Checked = true;
                GlobalHooks.StartMouse();
            }
            else
            {
                checkBoxScreenShot.Checked = false;
            }
            if (Properties.Settings.Default.keyboard)
            {
                checkBoxKeyBoard.Checked = true;
                GlobalHooks.StartKey();
            }
            else
            {
                checkBoxKeyBoard.Checked = false;
            }


        }


        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isAvoidClose)
            {
                e.Cancel = true;
                this.Hide();
            }
        }

        private void openMainFormToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GlobalHooks.StopMouse();
            GlobalHooks.StopKey();
            isAvoidClose = false;
            this.Close();
        }

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
        }

        private void ApplyLanguage()
        {
            ApplyResouceControl(this.Controls);
            ApplyResouceNotifyIcon();
        }

        private void ApplyResouceControl(Control.ControlCollection controls)
        {
            ComponentResourceManager resource = new ComponentResourceManager(typeof(MainForm));
            foreach (Control control in controls)
            {
                resource.ApplyResources(control, control.Name);
                control.AutoSize = true;
                if (control is GroupBox groupBox)
                {
                    ApplyResouceControl(groupBox.Controls);
                }
            }
        }
        private void ApplyResouceNotifyIcon()
        {
            ComponentResourceManager resource = new ComponentResourceManager(typeof(MainForm));
            //resource.ApplyResources(notifyIcon, "notifyIcon");
            resource.ApplyResources(exitToolStripMenuItem, "exitToolStripMenuItem");
            resource.ApplyResources(openMainFormToolStripMenuItem, "openMainFormToolStripMenuItem");
        }

        private void AutoCreateFoldor(string path)
        {
            AutoCreateFoldorSub("", path);
        }

        private void AutoCreateFoldorSub(string exist, string check)  //Auto create folder if not exist. like: AutoCreateFoldor("", @"E:\SWUtilityApp\SWUtilityApp_v3.1")
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

        private void buttonOpenLog_Click(object sender, EventArgs e)
        {
            AutoCreateFoldor($@"{Directory.GetCurrentDirectory()}\LogFiles\ScreenShot");
            Process.Start("explorer.exe", $@"{Directory.GetCurrentDirectory()}\LogFiles");
        }

        private void checkBoxScreenShot_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxScreenShot.Checked)
            {
                Properties.Settings.Default.screenshot = true;
                GlobalHooks.StartMouse();
            }
            else
            {
                Properties.Settings.Default.screenshot = false;
                GlobalHooks.StopMouse();
            }
            Properties.Settings.Default.Save();
        }

        private void checkBoxKeyBoard_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxKeyBoard.Checked)
            {
                Properties.Settings.Default.keyboard = true;
                GlobalHooks.StartKey();
            }
            else
            {
                Properties.Settings.Default.keyboard = false;
                GlobalHooks.StopKey();
            }
            Properties.Settings.Default.Save();
        }


        //end class
    }
}
