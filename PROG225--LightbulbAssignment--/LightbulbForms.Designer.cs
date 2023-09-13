namespace PROG225__LightbulbAssignment__
{
    partial class LightbulbForms
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
            btnCreateLightbulb = new Button();
            btnAllOn = new Button();
            btnAllOff = new Button();
            SuspendLayout();
            // 
            // btnCreateLightbulb
            // 
            btnCreateLightbulb.Location = new Point(160, 12);
            btnCreateLightbulb.Name = "btnCreateLightbulb";
            btnCreateLightbulb.Size = new Size(200, 23);
            btnCreateLightbulb.TabIndex = 2;
            btnCreateLightbulb.Text = "Create Lightbulb";
            btnCreateLightbulb.UseVisualStyleBackColor = true;
            btnCreateLightbulb.Click += btnCreateLightbulb_Click;
            // 
            // btnAllOn
            // 
            btnAllOn.Location = new Point(366, 12);
            btnAllOn.Name = "btnAllOn";
            btnAllOn.Size = new Size(75, 23);
            btnAllOn.TabIndex = 3;
            btnAllOn.Text = "All On";
            btnAllOn.UseVisualStyleBackColor = true;
            btnAllOn.Click += btnAllOn_Click;
            // 
            // btnAllOff
            // 
            btnAllOff.Location = new Point(447, 12);
            btnAllOff.Name = "btnAllOff";
            btnAllOff.Size = new Size(75, 23);
            btnAllOff.TabIndex = 4;
            btnAllOff.Text = "All Off";
            btnAllOff.UseVisualStyleBackColor = true;
            btnAllOff.Click += btnAllOff_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(804, 450);
            Controls.Add(btnAllOff);
            Controls.Add(btnAllOn);
            Controls.Add(btnCreateLightbulb);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
        }

        #endregion
        private Button btnCreateLightbulb;
        private Button btnAllOn;
        private Button btnAllOff;
    }
}