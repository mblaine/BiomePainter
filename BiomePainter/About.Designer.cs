namespace BiomePainter
{
    partial class About
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
            this.lblAbout = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.txtReadMe = new System.Windows.Forms.RichTextBox();
            this.btnViewLicense = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblAbout
            // 
            this.lblAbout.AutoSize = true;
            this.lblAbout.Location = new System.Drawing.Point(10, 9);
            this.lblAbout.Name = "lblAbout";
            this.lblAbout.Size = new System.Drawing.Size(96, 17);
            this.lblAbout.TabIndex = 1;
            this.lblAbout.Text = "Biome Painter";
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(476, 413);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 2;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // txtReadMe
            // 
            this.txtReadMe.Location = new System.Drawing.Point(12, 29);
            this.txtReadMe.Name = "txtReadMe";
            this.txtReadMe.Size = new System.Drawing.Size(539, 378);
            this.txtReadMe.TabIndex = 3;
            this.txtReadMe.Text = "";
            // 
            // btnViewLicense
            // 
            this.btnViewLicense.Location = new System.Drawing.Point(13, 413);
            this.btnViewLicense.Name = "btnViewLicense";
            this.btnViewLicense.Size = new System.Drawing.Size(104, 23);
            this.btnViewLicense.TabIndex = 4;
            this.btnViewLicense.Text = "View License";
            this.btnViewLicense.UseVisualStyleBackColor = true;
            this.btnViewLicense.Click += new System.EventHandler(this.btnViewLicense_Click);
            // 
            // About
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(563, 444);
            this.Controls.Add(this.btnViewLicense);
            this.Controls.Add(this.txtReadMe);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.lblAbout);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "About";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "About";
            this.Load += new System.EventHandler(this.About_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblAbout;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.RichTextBox txtReadMe;
        private System.Windows.Forms.Button btnViewLicense;
    }
}