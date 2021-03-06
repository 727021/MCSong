﻿namespace MCSong.Gui
{
    partial class HackControlDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HackControlDialog));
            this.chkFlying = new System.Windows.Forms.CheckBox();
            this.chkNoClip = new System.Windows.Forms.CheckBox();
            this.chkSpeeding = new System.Windows.Forms.CheckBox();
            this.chkSpawnControl = new System.Windows.Forms.CheckBox();
            this.chkThirdPerson = new System.Windows.Forms.CheckBox();
            this.numJumpHeight = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.btnDiscard = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnDefaults = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numJumpHeight)).BeginInit();
            this.SuspendLayout();
            // 
            // chkFlying
            // 
            this.chkFlying.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkFlying.Location = new System.Drawing.Point(16, 15);
            this.chkFlying.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkFlying.Name = "chkFlying";
            this.chkFlying.Size = new System.Drawing.Size(157, 30);
            this.chkFlying.TabIndex = 0;
            this.chkFlying.Text = "Flying";
            this.chkFlying.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkFlying.UseVisualStyleBackColor = true;
            // 
            // chkNoClip
            // 
            this.chkNoClip.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkNoClip.Location = new System.Drawing.Point(16, 52);
            this.chkNoClip.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkNoClip.Name = "chkNoClip";
            this.chkNoClip.Size = new System.Drawing.Size(157, 30);
            this.chkNoClip.TabIndex = 1;
            this.chkNoClip.Text = "NoClip";
            this.chkNoClip.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkNoClip.UseVisualStyleBackColor = true;
            // 
            // chkSpeeding
            // 
            this.chkSpeeding.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkSpeeding.Location = new System.Drawing.Point(16, 89);
            this.chkSpeeding.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkSpeeding.Name = "chkSpeeding";
            this.chkSpeeding.Size = new System.Drawing.Size(157, 30);
            this.chkSpeeding.TabIndex = 2;
            this.chkSpeeding.Text = "Speeding";
            this.chkSpeeding.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkSpeeding.UseVisualStyleBackColor = true;
            // 
            // chkSpawnControl
            // 
            this.chkSpawnControl.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkSpawnControl.Location = new System.Drawing.Point(16, 126);
            this.chkSpawnControl.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkSpawnControl.Name = "chkSpawnControl";
            this.chkSpawnControl.Size = new System.Drawing.Size(157, 30);
            this.chkSpawnControl.TabIndex = 3;
            this.chkSpawnControl.Text = "Spawn Control";
            this.chkSpawnControl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkSpawnControl.UseVisualStyleBackColor = true;
            // 
            // chkThirdPerson
            // 
            this.chkThirdPerson.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkThirdPerson.Location = new System.Drawing.Point(16, 162);
            this.chkThirdPerson.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkThirdPerson.Name = "chkThirdPerson";
            this.chkThirdPerson.Size = new System.Drawing.Size(157, 30);
            this.chkThirdPerson.TabIndex = 4;
            this.chkThirdPerson.Text = "Third Person View";
            this.chkThirdPerson.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkThirdPerson.UseVisualStyleBackColor = true;
            // 
            // numJumpHeight
            // 
            this.numJumpHeight.DecimalPlaces = 1;
            this.numJumpHeight.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.numJumpHeight.Location = new System.Drawing.Point(115, 199);
            this.numJumpHeight.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.numJumpHeight.Maximum = new decimal(new int[] {
            1023,
            0,
            0,
            0});
            this.numJumpHeight.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.numJumpHeight.Name = "numJumpHeight";
            this.numJumpHeight.Size = new System.Drawing.Size(59, 22);
            this.numJumpHeight.TabIndex = 5;
            this.numJumpHeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numJumpHeight.Value = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 202);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 17);
            this.label1.TabIndex = 6;
            this.label1.Text = "Jump Height:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(173, 22);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(132, 17);
            this.label2.TabIndex = 7;
            this.label2.Text = "Allow players to fly?";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(173, 126);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(157, 34);
            this.label3.TabIndex = 8;
            this.label3.Text = "Allow using ENTER and\r\nR for respawns?";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(173, 96);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(127, 17);
            this.label4.TabIndex = 9;
            this.label4.Text = "Allow fast running?";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(173, 170);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(159, 17);
            this.label5.TabIndex = 10;
            this.label5.Text = "Allow third person view?";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(173, 194);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(171, 34);
            this.label6.TabIndex = 11;
            this.label6.Text = "Max jump height (blocks).\r\nNegatives = client default.";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(173, 52);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(143, 34);
            this.label7.TabIndex = 12;
            this.label7.Text = "Allow walking through\r\nblocks?";
            // 
            // btnDiscard
            // 
            this.btnDiscard.Location = new System.Drawing.Point(124, 236);
            this.btnDiscard.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnDiscard.Name = "btnDiscard";
            this.btnDiscard.Size = new System.Drawing.Size(100, 28);
            this.btnDiscard.TabIndex = 13;
            this.btnDiscard.Text = "Discard";
            this.btnDiscard.UseVisualStyleBackColor = true;
            this.btnDiscard.Click += new System.EventHandler(this.btnDiscard_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(16, 236);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 28);
            this.btnSave.TabIndex = 14;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnDefaults
            // 
            this.btnDefaults.Location = new System.Drawing.Point(232, 236);
            this.btnDefaults.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnDefaults.Name = "btnDefaults";
            this.btnDefaults.Size = new System.Drawing.Size(100, 28);
            this.btnDefaults.TabIndex = 15;
            this.btnDefaults.Text = "Defaults";
            this.btnDefaults.UseVisualStyleBackColor = true;
            this.btnDefaults.Click += new System.EventHandler(this.btnDefaults_Click);
            // 
            // HackControlDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(345, 279);
            this.Controls.Add(this.btnDefaults);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnDiscard);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.numJumpHeight);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chkThirdPerson);
            this.Controls.Add(this.chkSpawnControl);
            this.Controls.Add(this.chkSpeeding);
            this.Controls.Add(this.chkNoClip);
            this.Controls.Add(this.chkFlying);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "HackControlDialog";
            this.ShowInTaskbar = false;
            this.Text = "Hack Control";
            this.Load += new System.EventHandler(this.HackControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numJumpHeight)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkFlying;
        private System.Windows.Forms.CheckBox chkNoClip;
        private System.Windows.Forms.CheckBox chkSpeeding;
        private System.Windows.Forms.CheckBox chkSpawnControl;
        private System.Windows.Forms.CheckBox chkThirdPerson;
        private System.Windows.Forms.NumericUpDown numJumpHeight;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnDiscard;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnDefaults;
    }
}