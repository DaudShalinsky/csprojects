namespace CakesForms
{
    partial class frmManager
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
            lsIngredients = new ListBox();
            btnAddNew = new Button();
            SuspendLayout();
            // 
            // lsIngredients
            // 
            lsIngredients.Dock = DockStyle.Top;
            lsIngredients.FormattingEnabled = true;
            lsIngredients.Location = new Point(0, 0);
            lsIngredients.Name = "lsIngredients";
            lsIngredients.Size = new Size(594, 344);
            lsIngredients.TabIndex = 0;
            // 
            // btnAddNew
            // 
            btnAddNew.Location = new Point(488, 350);
            btnAddNew.Name = "btnAddNew";
            btnAddNew.Size = new Size(94, 29);
            btnAddNew.TabIndex = 1;
            btnAddNew.Text = "Добавить";
            btnAddNew.UseVisualStyleBackColor = true;
            // 
            // frmManager
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(594, 385);
            Controls.Add(btnAddNew);
            Controls.Add(lsIngredients);
            Name = "frmManager";
            Text = "frmManager";
            Load += frmManager_Load;
            ResumeLayout(false);
        }

        #endregion

        private ListBox lsIngredients;
        private Button btnAddNew;
    }
}