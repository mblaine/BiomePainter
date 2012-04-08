/*
 * InputBox: An implementation functionally similar to Microsoft.VisualBasic.Interaction.Inputbox. Created by Matthew Blaine, Mar 28, 2012.
 * 
 * LICENSE:
 * 
 * The following code is placed in the public domain. For more information, please see: http://creativecommons.org/publicdomain/zero/1.0/
 * 
 * SOURCE:
 * 
 * This file may be found at: https://github.com/mblaine/public/blob/master/InputBox.cs
 * 
 * USAGE:
 * 
 * String result = InputBox.Show(String Prompt, [String Title = "",] [String DefaultResponse = "",] [int XPos = -1,] [int YPos = -1]);
 */

using System;
using System.Drawing;
using System.Windows.Forms;

public class InputBox : Form
{
    private System.ComponentModel.IContainer components = null;
    
    private System.Windows.Forms.Label lblPrompt;
    private System.Windows.Forms.Button btnOk;
    private System.Windows.Forms.Button btnCancel;
    private System.Windows.Forms.TextBox txtOutput;

    private String Output = "";

    public InputBox()
    {
        InitializeComponent();
        this.AcceptButton = btnOk;
        this.CancelButton = btnCancel;
        this.lblPrompt.MaximumSize = new Size(btnOk.Left - btnOk.Margin.Left - lblPrompt.Left, 0);
    }

    private void InitializeComponent()
    {
        this.lblPrompt = new System.Windows.Forms.Label();
        this.btnOk = new System.Windows.Forms.Button();
        this.btnCancel = new System.Windows.Forms.Button();
        this.txtOutput = new System.Windows.Forms.TextBox();
        this.SuspendLayout();
        // 
        // lblPrompt
        // 
        this.lblPrompt.AutoSize = true;
        this.lblPrompt.Location = new System.Drawing.Point(9, 10);
        this.lblPrompt.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
        this.lblPrompt.Name = "lblPrompt";
        this.lblPrompt.Size = new System.Drawing.Size(0, 13);
        this.lblPrompt.TabIndex = 0;
        // 
        // btnOk
        // 
        this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
        this.btnOk.Location = new System.Drawing.Point(283, 10);
        this.btnOk.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
        this.btnOk.Name = "btnOk";
        this.btnOk.Size = new System.Drawing.Size(56, 19);
        this.btnOk.TabIndex = 1;
        this.btnOk.Text = "&OK";
        this.btnOk.UseVisualStyleBackColor = true;
        this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
        // 
        // btnCancel
        // 
        this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
        this.btnCancel.Location = new System.Drawing.Point(283, 34);
        this.btnCancel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
        this.btnCancel.Name = "btnCancel";
        this.btnCancel.Size = new System.Drawing.Size(56, 19);
        this.btnCancel.TabIndex = 2;
        this.btnCancel.Text = "&Cancel";
        this.btnCancel.UseVisualStyleBackColor = true;
        this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
        // 
        // txtOutput
        // 
        this.txtOutput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
        | System.Windows.Forms.AnchorStyles.Right)));
        this.txtOutput.Location = new System.Drawing.Point(9, 83);
        this.txtOutput.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
        this.txtOutput.Name = "txtOutput";
        this.txtOutput.Size = new System.Drawing.Size(331, 20);
        this.txtOutput.TabIndex = 3;
        // 
        // InputBox
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(348, 110);
        this.Controls.Add(this.txtOutput);
        this.Controls.Add(this.btnCancel);
        this.Controls.Add(this.btnOk);
        this.Controls.Add(this.lblPrompt);
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
        this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.Name = "InputBox";
        this.ShowIcon = false;
        this.Text = "InputBox";
        this.Activated += new System.EventHandler(this.InputBox_Activated);
        this.ResumeLayout(false);
        this.PerformLayout();
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
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

        if (Prompt != null)
            form.lblPrompt.Text = Prompt;

        if (Title != null && Title.Length > 0)
        {
            form.Text = Title;
        }
        else if (parent != null)
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
