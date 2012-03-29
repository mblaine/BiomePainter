using System;
using System.Drawing;
using System.Windows.Forms;

namespace InputBox
{
    public partial class InputBox : Form
    {
        public String Output = "";

        public InputBox()
        {
            InitializeComponent();
            this.AcceptButton = btnOk;  
            this.CancelButton = btnCancel;
            this.lblPrompt.MaximumSize = new Size(btnOk.Left - btnOk.Margin.Left - lblPrompt.Left, 0);
        }

        private void InputBox_Activated(object sender, EventArgs e)
        {
            txtOutput.Focus();
            txtOutput.SelectAll();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Output = txtOutput.Text;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        public static String Show(String Prompt, String Title = "", String DefaultResponse = "", int XPos = -1, int YPos = -1)
        {
            InputBox form = new InputBox();

            Form parent = null;
            if (Application.OpenForms.Count > 0)
                parent = Application.OpenForms[0];
            
            if(Prompt !=  null)
                form.lblPrompt.Text = Prompt;
            
            if (Title != null && Title.Length > 0)
            {
                form.Text = Title;
            }
            else if(parent != null)
            {
                form.Text = parent.Text;
            }

            if (DefaultResponse != null && DefaultResponse.Length > 0)
                form.txtOutput.Text = DefaultResponse;

            if (XPos >= 0 && YPos >= 0)
            {
                form.StartPosition = FormStartPosition.Manual;
                form.DesktopLocation = new Point(XPos, YPos);
            }
            else if (parent != null)
                form.StartPosition = FormStartPosition.CenterParent;
            else
                form.StartPosition = FormStartPosition.CenterScreen;

            form.ShowDialog(parent);

            String ret = form.Output;
            form.Dispose();
            return ret;
        }
    }
}
