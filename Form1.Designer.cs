
namespace V2MemInjector
{
    partial class V2MemInjector
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(V2MemInjector));
            this.btnScan = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnInject = new System.Windows.Forms.Button();
            this.clistModList = new System.Windows.Forms.CheckedListBox();
            this.btnDLLFolder = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnScan
            // 
            this.btnScan.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.btnScan.Location = new System.Drawing.Point(56, 64);
            this.btnScan.Name = "btnScan";
            this.btnScan.Size = new System.Drawing.Size(179, 52);
            this.btnScan.TabIndex = 0;
            this.btnScan.Text = "Scan For Victoria 2";
            this.btnScan.UseVisualStyleBackColor = true;
            this.btnScan.Click += new System.EventHandler(this.btnScan_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.lblTitle.Location = new System.Drawing.Point(51, 23);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(237, 25);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "Victoria 2 Memory Injector";
            // 
            // btnInject
            // 
            this.btnInject.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.btnInject.Location = new System.Drawing.Point(56, 122);
            this.btnInject.Name = "btnInject";
            this.btnInject.Size = new System.Drawing.Size(179, 52);
            this.btnInject.TabIndex = 2;
            this.btnInject.Text = "Inject";
            this.btnInject.UseVisualStyleBackColor = true;
            this.btnInject.Click += new System.EventHandler(this.btnInject_Click);
            // 
            // clistModList
            // 
            this.clistModList.FormattingEnabled = true;
            this.clistModList.Location = new System.Drawing.Point(345, 64);
            this.clistModList.Name = "clistModList";
            this.clistModList.Size = new System.Drawing.Size(226, 169);
            this.clistModList.TabIndex = 3;
            // 
            // btnDLLFolder
            // 
            this.btnDLLFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.btnDLLFolder.Location = new System.Drawing.Point(56, 180);
            this.btnDLLFolder.Name = "btnDLLFolder";
            this.btnDLLFolder.Size = new System.Drawing.Size(179, 52);
            this.btnDLLFolder.TabIndex = 4;
            this.btnDLLFolder.Text = "Find DLL Folder";
            this.btnDLLFolder.UseVisualStyleBackColor = true;
            this.btnDLLFolder.Click += new System.EventHandler(this.btnDLLFolder_Click_1);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.label1.Location = new System.Drawing.Point(340, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 25);
            this.label1.TabIndex = 5;
            this.label1.Text = "Mod List";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.btnRefresh.Location = new System.Drawing.Point(462, 23);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(109, 29);
            this.btnRefresh.TabIndex = 6;
            this.btnRefresh.Text = "Refresh List";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // V2MemInjector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(629, 273);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnDLLFolder);
            this.Controls.Add(this.clistModList);
            this.Controls.Add(this.btnInject);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btnScan);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "V2MemInjector";
            this.Text = "Victoria 2 Memory Injector";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnScan;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnInject;
        private System.Windows.Forms.CheckedListBox clistModList;
        private System.Windows.Forms.Button btnDLLFolder;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnRefresh;
    }
}

