namespace KeyLog
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.contextMenuStripNotifyIcon = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openMainFormToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.checkBoxScreenShot = new System.Windows.Forms.CheckBox();
            this.checkBoxKeyBoard = new System.Windows.Forms.CheckBox();
            this.buttonOpenLog = new System.Windows.Forms.Button();
            this.contextMenuStripNotifyIcon.SuspendLayout();
            this.SuspendLayout();
            // 
            // contextMenuStripNotifyIcon
            // 
            this.contextMenuStripNotifyIcon.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openMainFormToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.contextMenuStripNotifyIcon.Name = "contextMenuStripNotifyIcon";
            this.contextMenuStripNotifyIcon.Size = new System.Drawing.Size(176, 48);
            // 
            // openMainFormToolStripMenuItem
            // 
            this.openMainFormToolStripMenuItem.Name = "openMainFormToolStripMenuItem";
            this.openMainFormToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.openMainFormToolStripMenuItem.Text = "Open Main Form";
            this.openMainFormToolStripMenuItem.Click += new System.EventHandler(this.openMainFormToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // notifyIcon
            // 
            this.notifyIcon.ContextMenuStrip = this.contextMenuStripNotifyIcon;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "KeyLog";
            this.notifyIcon.Visible = true;
            this.notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseDoubleClick);
            // 
            // checkBoxScreenShot
            // 
            this.checkBoxScreenShot.AutoSize = true;
            this.checkBoxScreenShot.Location = new System.Drawing.Point(32, 78);
            this.checkBoxScreenShot.Name = "checkBoxScreenShot";
            this.checkBoxScreenShot.Size = new System.Drawing.Size(458, 20);
            this.checkBoxScreenShot.TabIndex = 2;
            this.checkBoxScreenShot.Text = "Take a screenshot every time the user clicks the mouse";
            this.checkBoxScreenShot.UseVisualStyleBackColor = true;
            this.checkBoxScreenShot.CheckedChanged += new System.EventHandler(this.checkBoxScreenShot_CheckedChanged);
            // 
            // checkBoxKeyBoard
            // 
            this.checkBoxKeyBoard.AutoSize = true;
            this.checkBoxKeyBoard.Location = new System.Drawing.Point(32, 120);
            this.checkBoxKeyBoard.Name = "checkBoxKeyBoard";
            this.checkBoxKeyBoard.Size = new System.Drawing.Size(330, 20);
            this.checkBoxKeyBoard.TabIndex = 3;
            this.checkBoxKeyBoard.Text = "Record user keyboard input information";
            this.checkBoxKeyBoard.UseVisualStyleBackColor = true;
            this.checkBoxKeyBoard.CheckedChanged += new System.EventHandler(this.checkBoxKeyBoard_CheckedChanged);
            // 
            // buttonOpenLog
            // 
            this.buttonOpenLog.Location = new System.Drawing.Point(32, 24);
            this.buttonOpenLog.Name = "buttonOpenLog";
            this.buttonOpenLog.Size = new System.Drawing.Size(523, 28);
            this.buttonOpenLog.TabIndex = 4;
            this.buttonOpenLog.Text = "Open Log Folder";
            this.buttonOpenLog.UseVisualStyleBackColor = true;
            this.buttonOpenLog.Click += new System.EventHandler(this.buttonOpenLog_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(594, 169);
            this.Controls.Add(this.buttonOpenLog);
            this.Controls.Add(this.checkBoxKeyBoard);
            this.Controls.Add(this.checkBoxScreenShot);
            this.Font = new System.Drawing.Font("宋体", 12F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainForm";
            this.Text = "KeyLog_v1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.contextMenuStripNotifyIcon.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStripNotifyIcon;
        private System.Windows.Forms.ToolStripMenuItem openMainFormToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.CheckBox checkBoxScreenShot;
        private System.Windows.Forms.CheckBox checkBoxKeyBoard;
        private System.Windows.Forms.Button buttonOpenLog;
    }
}

