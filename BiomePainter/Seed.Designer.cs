namespace BiomePainter
{
    partial class Seed
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
            this.label1 = new System.Windows.Forms.Label();
            this.lblOriginalSeed = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtNewSeed = new System.Windows.Forms.TextBox();
            this.btnTemporary = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Original Seed:";
            // 
            // lblOriginalSeed
            // 
            this.lblOriginalSeed.AutoSize = true;
            this.lblOriginalSeed.Location = new System.Drawing.Point(116, 40);
            this.lblOriginalSeed.Name = "lblOriginalSeed";
            this.lblOriginalSeed.Size = new System.Drawing.Size(16, 17);
            this.lblOriginalSeed.TabIndex = 1;
            this.lblOriginalSeed.Text = "0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "New Seed:";
            // 
            // txtNewSeed
            // 
            this.txtNewSeed.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNewSeed.Location = new System.Drawing.Point(119, 6);
            this.txtNewSeed.Name = "txtNewSeed";
            this.txtNewSeed.Size = new System.Drawing.Size(216, 22);
            this.txtNewSeed.TabIndex = 3;
            // 
            // btnTemporary
            // 
            this.btnTemporary.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTemporary.Location = new System.Drawing.Point(341, 5);
            this.btnTemporary.Name = "btnTemporary";
            this.btnTemporary.Size = new System.Drawing.Size(225, 25);
            this.btnTemporary.TabIndex = 4;
            this.btnTemporary.Text = "Temporarily Use New Seed";
            this.btnTemporary.UseVisualStyleBackColor = true;
            this.btnTemporary.Click += new System.EventHandler(this.btnTemporary_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(341, 36);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(225, 25);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "Change and Save to level.dat";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(341, 67);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(225, 25);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // Seed
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(578, 97);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnTemporary);
            this.Controls.Add(this.txtNewSeed);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblOriginalSeed);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = global::BiomePainter.Properties.Resources.icon;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Seed";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Seed";
            this.Activated += new System.EventHandler(this.Seed_Activated);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblOriginalSeed;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtNewSeed;
        private System.Windows.Forms.Button btnTemporary;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
    }
}