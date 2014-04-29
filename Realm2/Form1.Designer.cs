namespace Realm2
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
            this.mainText = new System.Windows.Forms.Label();
            this.PlaceHeading = new System.Windows.Forms.Label();
            this.inputText = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // mainText
            // 
            this.mainText.Location = new System.Drawing.Point(13, 40);
            this.mainText.Name = "mainText";
            this.mainText.Size = new System.Drawing.Size(599, 329);
            this.mainText.TabIndex = 0;
            // 
            // PlaceHeading
            // 
            this.PlaceHeading.AutoSize = true;
            this.PlaceHeading.Location = new System.Drawing.Point(285, 13);
            this.PlaceHeading.Name = "PlaceHeading";
            this.PlaceHeading.Size = new System.Drawing.Size(0, 13);
            this.PlaceHeading.TabIndex = 1;
            // 
            // inputText
            // 
            this.inputText.Location = new System.Drawing.Point(13, 385);
            this.inputText.Multiline = true;
            this.inputText.Name = "inputText";
            this.inputText.Size = new System.Drawing.Size(599, 44);
            this.inputText.TabIndex = 2;
            this.inputText.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyUp);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 441);
            this.Controls.Add(this.inputText);
            this.Controls.Add(this.PlaceHeading);
            this.Controls.Add(this.mainText);
            this.Name = "Form1";
            this.Text = "Realm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label mainText;
        public System.Windows.Forms.Label PlaceHeading;
        public System.Windows.Forms.TextBox inputText;
    }
}