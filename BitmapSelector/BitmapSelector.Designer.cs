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
            this.SuspendLayout();
            // 
            // toolTip
            // 
            this.toolTip.UseAnimation = false;
            this.toolTip.UseFading = false;
            // 
            // BitmapSelector
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.Name = "BitmapSelector";
            this.MouseCaptureChanged += new System.EventHandler(this.BitmapSelector_MouseCaptureChanged);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.BitmapSelector_MouseDown);
            this.MouseLeave += new System.EventHandler(this.BitmapSelector_MouseLeave);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.BitmapSelector_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.BitmapSelector_MouseUp);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolTip toolTip;
    }
}
