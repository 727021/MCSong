namespace MCSong.Gui
{
    partial class UpdateWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UpdateWindow));
            this.grpVersions = new System.Windows.Forms.GroupBox();
            this.txtCurrentVersion = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtLatestVersion = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmdUpdate = new System.Windows.Forms.Button();
            this.listRevisions = new System.Windows.Forms.ListBox();
            this.grpReadOnly = new System.Windows.Forms.GroupBox();
            this.txtCountdownRO = new System.Windows.Forms.TextBox();
            this.chkAutoUpdateRO = new System.Windows.Forms.CheckBox();
            this.btnEdit = new System.Windows.Forms.Button();
            this.chkNotifyRO = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.grpVersions.SuspendLayout();
            this.grpReadOnly.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpVersions
            // 
            this.grpVersions.Controls.Add(this.txtCurrentVersion);
            this.grpVersions.Controls.Add(this.label4);
            this.grpVersions.Controls.Add(this.txtLatestVersion);
            this.grpVersions.Controls.Add(this.label3);
            this.grpVersions.Controls.Add(this.cmdUpdate);
            this.grpVersions.Controls.Add(this.listRevisions);
            this.grpVersions.Location = new System.Drawing.Point(3, 3);
            this.grpVersions.Name = "grpVersions";
            this.grpVersions.Size = new System.Drawing.Size(209, 177);
            this.grpVersions.TabIndex = 2;
            this.grpVersions.TabStop = false;
            // 
            // txtCurrentVersion
            // 
            this.txtCurrentVersion.Location = new System.Drawing.Point(111, 97);
            this.txtCurrentVersion.Name = "txtCurrentVersion";
            this.txtCurrentVersion.ReadOnly = true;
            this.txtCurrentVersion.Size = new System.Drawing.Size(74, 20);
            this.txtCurrentVersion.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(108, 81);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Current Version:";
            // 
            // txtLatestVersion
            // 
            this.txtLatestVersion.Location = new System.Drawing.Point(111, 47);
            this.txtLatestVersion.Name = "txtLatestVersion";
            this.txtLatestVersion.ReadOnly = true;
            this.txtLatestVersion.Size = new System.Drawing.Size(74, 20);
            this.txtLatestVersion.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(108, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Latest Version:";
            // 
            // cmdUpdate
            // 
            this.cmdUpdate.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdUpdate.Location = new System.Drawing.Point(9, 148);
            this.cmdUpdate.Name = "cmdUpdate";
            this.cmdUpdate.Size = new System.Drawing.Size(82, 23);
            this.cmdUpdate.TabIndex = 4;
            this.cmdUpdate.Text = "Update";
            this.cmdUpdate.UseVisualStyleBackColor = true;
            // 
            // listRevisions
            // 
            this.listRevisions.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listRevisions.FormattingEnabled = true;
            this.listRevisions.Location = new System.Drawing.Point(9, 19);
            this.listRevisions.Name = "listRevisions";
            this.listRevisions.Size = new System.Drawing.Size(82, 121);
            this.listRevisions.TabIndex = 3;
            // 
            // grpReadOnly
            // 
            this.grpReadOnly.Controls.Add(this.txtCountdownRO);
            this.grpReadOnly.Controls.Add(this.chkAutoUpdateRO);
            this.grpReadOnly.Controls.Add(this.btnEdit);
            this.grpReadOnly.Controls.Add(this.chkNotifyRO);
            this.grpReadOnly.Controls.Add(this.label2);
            this.grpReadOnly.Location = new System.Drawing.Point(3, 186);
            this.grpReadOnly.Name = "grpReadOnly";
            this.grpReadOnly.Size = new System.Drawing.Size(209, 120);
            this.grpReadOnly.TabIndex = 5;
            this.grpReadOnly.TabStop = false;
            this.grpReadOnly.Text = "Updater Properties";
            // 
            // txtCountdownRO
            // 
            this.txtCountdownRO.Location = new System.Drawing.Point(164, 60);
            this.txtCountdownRO.Name = "txtCountdownRO";
            this.txtCountdownRO.ReadOnly = true;
            this.txtCountdownRO.Size = new System.Drawing.Size(42, 20);
            this.txtCountdownRO.TabIndex = 4;
            // 
            // chkAutoUpdateRO
            // 
            this.chkAutoUpdateRO.AutoCheck = false;
            this.chkAutoUpdateRO.AutoSize = true;
            this.chkAutoUpdateRO.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkAutoUpdateRO.Location = new System.Drawing.Point(31, 19);
            this.chkAutoUpdateRO.Name = "chkAutoUpdateRO";
            this.chkAutoUpdateRO.Size = new System.Drawing.Size(133, 17);
            this.chkAutoUpdateRO.TabIndex = 1;
            this.chkAutoUpdateRO.Text = "Auto update to newest";
            this.chkAutoUpdateRO.UseVisualStyleBackColor = true;
            // 
            // btnEdit
            // 
            this.btnEdit.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEdit.Location = new System.Drawing.Point(72, 89);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(59, 23);
            this.btnEdit.TabIndex = 2;
            this.btnEdit.Text = "Edit";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // chkNotifyRO
            // 
            this.chkNotifyRO.AutoCheck = false;
            this.chkNotifyRO.AutoSize = true;
            this.chkNotifyRO.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkNotifyRO.Location = new System.Drawing.Point(31, 38);
            this.chkNotifyRO.Name = "chkNotifyRO";
            this.chkNotifyRO.Size = new System.Drawing.Size(139, 17);
            this.chkNotifyRO.TabIndex = 2;
            this.chkNotifyRO.Text = "Notify in-game of restart";
            this.chkNotifyRO.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(5, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(158, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Time (in seconds) to countdown:";
            // 
            // UpdateWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(214, 309);
            this.Controls.Add(this.grpReadOnly);
            this.Controls.Add(this.grpVersions);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UpdateWindow";
            this.Text = "MCSong Updater";
            this.Load += new System.EventHandler(this.UpdateWindow_Load);
            this.Disposed += new System.EventHandler(this.UpdateWindow_Unload);
            this.grpVersions.ResumeLayout(false);
            this.grpVersions.PerformLayout();
            this.grpReadOnly.ResumeLayout(false);
            this.grpReadOnly.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox grpVersions;
        private System.Windows.Forms.Button cmdUpdate;
        private System.Windows.Forms.ListBox listRevisions;
        private System.Windows.Forms.GroupBox grpReadOnly;
        private System.Windows.Forms.TextBox txtCountdownRO;
        private System.Windows.Forms.CheckBox chkAutoUpdateRO;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.CheckBox chkNotifyRO;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCurrentVersion;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtLatestVersion;
        private System.Windows.Forms.Label label3;
    }
}