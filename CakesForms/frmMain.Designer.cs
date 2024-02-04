namespace CakesForms
{
    partial class frmMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnManager = new Button();
            btnClient = new Button();
            groupBox1 = new GroupBox();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // btnManager
            // 
            btnManager.Location = new Point(24, 54);
            btnManager.Name = "btnManager";
            btnManager.Size = new Size(121, 59);
            btnManager.TabIndex = 1;
            btnManager.Text = "Менеджер";
            btnManager.UseVisualStyleBackColor = true;
            btnManager.Click += btnManager_Click;
            // 
            // btnClient
            // 
            btnClient.Location = new Point(179, 54);
            btnClient.Name = "btnClient";
            btnClient.Size = new Size(121, 59);
            btnClient.TabIndex = 2;
            btnClient.Text = "Клиент";
            btnClient.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(btnManager);
            groupBox1.Controls.Add(btnClient);
            groupBox1.Location = new Point(12, 22);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(335, 154);
            groupBox1.TabIndex = 3;
            groupBox1.TabStop = false;
            groupBox1.Text = "Ты кто?";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(359, 201);
            Controls.Add(groupBox1);
            Name = "Form1";
            Text = "Cakes";
            groupBox1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private Button btnManager;
        private Button btnClient;
        private GroupBox groupBox1;
    }
}
