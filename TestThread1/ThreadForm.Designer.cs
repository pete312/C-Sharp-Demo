namespace DirectroryWalker.Demo
{
    partial class ThreadForm
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
            this.startButton = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.stopButton = new System.Windows.Forms.Button();
            this.Output = new System.Windows.Forms.TextBox();
            this.entryBaseDir = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.group1 = new System.Windows.Forms.GroupBox();
            this.group1.SuspendLayout();
            this.SuspendLayout();
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(351, 14);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(75, 23);
            this.startButton.TabIndex = 0;
            this.startButton.Text = "Start Walk";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(22, 288);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(569, 23);
            this.progressBar1.TabIndex = 1;
            // 
            // stopButton
            // 
            this.stopButton.Location = new System.Drawing.Point(432, 14);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(75, 23);
            this.stopButton.TabIndex = 2;
            this.stopButton.Text = "Stop";
            this.stopButton.UseVisualStyleBackColor = true;
            this.stopButton.Click += new System.EventHandler(this.stopButton_Click);
            // 
            // Output
            // 
            this.Output.Location = new System.Drawing.Point(22, 22);
            this.Output.Multiline = true;
            this.Output.Name = "Output";
            this.Output.Size = new System.Drawing.Size(569, 231);
            this.Output.TabIndex = 3;
            // 
            // entryBaseDir
            // 
            this.entryBaseDir.Location = new System.Drawing.Point(59, 16);
            this.entryBaseDir.Name = "entryBaseDir";
            this.entryBaseDir.Size = new System.Drawing.Size(277, 20);
            this.entryBaseDir.TabIndex = 4;
            this.entryBaseDir.KeyUp += new System.Windows.Forms.KeyEventHandler(this.entryBaseDir_KeyUp);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Base Dir";
            // 
            // group1
            // 
            this.group1.Controls.Add(this.label1);
            this.group1.Controls.Add(this.entryBaseDir);
            this.group1.Controls.Add(this.stopButton);
            this.group1.Controls.Add(this.startButton);
            this.group1.Location = new System.Drawing.Point(55, 317);
            this.group1.Name = "group1";
            this.group1.Size = new System.Drawing.Size(523, 47);
            this.group1.TabIndex = 6;
            this.group1.TabStop = false;
            this.group1.Text = "Target Directory";
            // 
            // ThreadForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(616, 376);
            this.Controls.Add(this.group1);
            this.Controls.Add(this.Output);
            this.Controls.Add(this.progressBar1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "ThreadForm";
            this.Text = "ThreadForm";
            this.group1.ResumeLayout(false);
            this.group1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button stopButton;
        private System.Windows.Forms.TextBox Output;
        private System.Windows.Forms.TextBox entryBaseDir;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox group1;
    }
}

