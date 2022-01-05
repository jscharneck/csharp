namespace Copy_Files
{
    partial class Form1
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
            this.button1 = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.lblCopyStart = new System.Windows.Forms.Label();
            this.lblCopyEnd = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.button2 = new System.Windows.Forms.Button();
            this.txtBxFolderSelect = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(985, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(178, 127);
            this.button1.TabIndex = 0;
            this.button1.Text = "COPY FILES";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // listBox1
            // 
            this.listBox1.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 16;
            this.listBox1.Location = new System.Drawing.Point(12, 145);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(1151, 484);
            this.listBox1.TabIndex = 1;
            // 
            // lblCopyStart
            // 
            this.lblCopyStart.AutoSize = true;
            this.lblCopyStart.Location = new System.Drawing.Point(12, 82);
            this.lblCopyStart.Name = "lblCopyStart";
            this.lblCopyStart.Size = new System.Drawing.Size(95, 13);
            this.lblCopyStart.TabIndex = 2;
            this.lblCopyStart.Text = "Copying started at:";
            // 
            // lblCopyEnd
            // 
            this.lblCopyEnd.AutoSize = true;
            this.lblCopyEnd.Location = new System.Drawing.Point(12, 110);
            this.lblCopyEnd.Name = "lblCopyEnd";
            this.lblCopyEnd.Size = new System.Drawing.Size(96, 13);
            this.lblCopyEnd.TabIndex = 3;
            this.lblCopyEnd.Text = "Copying ended  at:";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(494, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(155, 20);
            this.button2.TabIndex = 4;
            this.button2.Text = "Select folder";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // txtBxFolderSelect
            // 
            this.txtBxFolderSelect.Location = new System.Drawing.Point(12, 12);
            this.txtBxFolderSelect.Name = "txtBxFolderSelect";
            this.txtBxFolderSelect.Size = new System.Drawing.Size(476, 20);
            this.txtBxFolderSelect.TabIndex = 5;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1175, 638);
            this.Controls.Add(this.txtBxFolderSelect);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.lblCopyEnd);
            this.Controls.Add(this.lblCopyStart);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label lblCopyStart;
        private System.Windows.Forms.Label lblCopyEnd;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox txtBxFolderSelect;
    }
}

