namespace BitmapSelector
{
    partial class BitmapSelector
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.scrollHorizontal = new System.Windows.Forms.HScrollBar();
            this.scrollVertical = new System.Windows.Forms.VScrollBar();
            this.SuspendLayout();
            // 
            // toolTip
            // 
            this.toolTip.UseAnimation = false;
            this.toolTip.UseFading = false;
            // 
            // scrollHorizontal
            // 
            this.scrollHorizontal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.scrollHorizontal.Location = new System.Drawing.Point(0, 129);
            this.scrollHorizontal.Name = "scrollHorizontal";
            this.scrollHorizontal.Size = new System.Drawing.Size(129, 21);
            this.scrollHorizontal.TabIndex = 0;
            this.scrollHorizontal.Visible = false;
            this.scrollHorizontal.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrollHorizontal_Scroll);
            // 
            // scrollVertical
            // 
            this.scrollVertical.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.scrollVertical.Location = new System.Drawing.Point(129, 0);
            this.scrollVertical.Name = "scrollVertical";
            this.scrollVertical.Size = new System.Drawing.Size(21, 129);
            this.scrollVertical.TabIndex = 1;
            this.scrollVertical.Visible = false;
            this.scrollVertical.Scroll += new System.Windows.Forms.ScrollEventHandler(this.scrollVertical_Scroll);
            // 
            // BitmapSelector
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.Controls.Add(this.scrollVertical);
            this.Controls.Add(this.scrollHorizontal);
            this.Name = "BitmapSelector";
            this.MouseCaptureChanged += new System.EventHandler(this.BitmapSelector_MouseCaptureChanged);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BitmapSelector_MouseDown);
            this.MouseLeave += new System.EventHandler(this.BitmapSelector_MouseLeave);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.BitmapSelector_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.BitmapSelector_MouseUp);
            this.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.BitmapSelector_MouseWheel);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.HScrollBar scrollHorizontal;
        private System.Windows.Forms.VScrollBar scrollVertical;
    }
}
