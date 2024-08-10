namespace SqlVersionControlSystem
{
    partial class Form1
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
            richTextBoxSol = new RichTextBox();
            richTextBoxSağ = new RichTextBox();
            btnKarsılastırma = new Button();
            cmbSpname = new ComboBox();
            cmbEskiSp = new ComboBox();
            cmbYeniSp = new ComboBox();
            SuspendLayout();
            // 
            // richTextBoxSol
            // 
            richTextBoxSol.Location = new Point(12, 148);
            richTextBoxSol.Name = "richTextBoxSol";
            richTextBoxSol.Size = new Size(868, 647);
            richTextBoxSol.TabIndex = 0;
            richTextBoxSol.Text = "";
            // 
            // richTextBoxSağ
            // 
            richTextBoxSağ.Location = new Point(886, 148);
            richTextBoxSağ.Name = "richTextBoxSağ";
            richTextBoxSağ.Size = new Size(868, 647);
            richTextBoxSağ.TabIndex = 1;
            richTextBoxSağ.Text = "";
            // 
            // btnKarsılastırma
            // 
            btnKarsılastırma.Location = new Point(847, 115);
            btnKarsılastırma.Name = "btnKarsılastırma";
            btnKarsılastırma.Size = new Size(75, 24);
            btnKarsılastırma.TabIndex = 3;
            btnKarsılastırma.Text = "Karşilaştırma";
            btnKarsılastırma.UseVisualStyleBackColor = true;
            btnKarsılastırma.Click += btnKarsılastırma_Click;
            // 
            // cmbSpname
            // 
            cmbSpname.FormattingEnabled = true;
            cmbSpname.Location = new Point(585, 36);
            cmbSpname.Name = "cmbSpname";
            cmbSpname.Size = new Size(613, 23);
            cmbSpname.TabIndex = 4;
            cmbSpname.SelectedIndexChanged += cmbSpname_SelectedIndexChanged;
            // 
            // cmbEskiSp
            // 
            cmbEskiSp.FormattingEnabled = true;
            cmbEskiSp.Location = new Point(267, 79);
            cmbEskiSp.Name = "cmbEskiSp";
            cmbEskiSp.Size = new Size(613, 23);
            cmbEskiSp.TabIndex = 5;
            cmbEskiSp.SelectedIndexChanged += cmbEskiSp_SelectedIndexChanged;
            // 
            // cmbYeniSp
            // 
            cmbYeniSp.FormattingEnabled = true;
            cmbYeniSp.Location = new Point(886, 79);
            cmbYeniSp.Name = "cmbYeniSp";
            cmbYeniSp.Size = new Size(613, 23);
            cmbYeniSp.TabIndex = 6;
            cmbYeniSp.SelectedIndexChanged += cmbYeniSp_SelectedIndexChanged;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1774, 827);
            Controls.Add(cmbYeniSp);
            Controls.Add(cmbEskiSp);
            Controls.Add(cmbSpname);
            Controls.Add(btnKarsılastırma);
            Controls.Add(richTextBoxSağ);
            Controls.Add(richTextBoxSol);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
        }

        #endregion

        private RichTextBox richTextBoxSol;
        private RichTextBox richTextBoxSağ;
        private Button btnKarsılastırma;
        private ComboBox cmbSpname;
        private ComboBox cmbEskiSp;
        private ComboBox cmbYeniSp;
    }
}
