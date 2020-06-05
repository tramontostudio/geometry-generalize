using MapWinGIS;

namespace Generalizer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.originalButton = new System.Windows.Forms.Button();
            this.generalizedButton = new System.Windows.Forms.Button();
            this.viewLabel = new System.Windows.Forms.Label();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.trackBarLabel = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.chooseFileButton = new System.Windows.Forms.Button();
            this.bothButton = new System.Windows.Forms.Button();
            this.axMap1 = new AxMapWinGIS.AxMap();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axMap1)).BeginInit();
            this.SuspendLayout();
            // 
            // originalButton
            // 
            this.originalButton.Location = new System.Drawing.Point(15, 42);
            this.originalButton.Name = "originalButton";
            this.originalButton.Size = new System.Drawing.Size(140, 29);
            this.originalButton.TabIndex = 1;
            this.originalButton.Text = "Original";
            this.originalButton.UseVisualStyleBackColor = true;
            this.originalButton.Click += new System.EventHandler(this.OriginalButton_Click);
            // 
            // generalizedButton
            // 
            this.generalizedButton.Location = new System.Drawing.Point(15, 77);
            this.generalizedButton.Name = "generalizedButton";
            this.generalizedButton.Size = new System.Drawing.Size(140, 29);
            this.generalizedButton.TabIndex = 2;
            this.generalizedButton.Text = "Generalized";
            this.generalizedButton.UseVisualStyleBackColor = true;
            this.generalizedButton.Click += new System.EventHandler(this.GeneralizedButton_Click);
            // 
            // viewLabel
            // 
            this.viewLabel.AutoSize = true;
            this.viewLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.viewLabel.Location = new System.Drawing.Point(646, 9);
            this.viewLabel.Name = "viewLabel";
            this.viewLabel.Size = new System.Drawing.Size(75, 24);
            this.viewLabel.TabIndex = 3;
            this.viewLabel.Text = "Original";
            this.viewLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(33, 165);
            this.trackBar1.Maximum = 20;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.trackBar1.Size = new System.Drawing.Size(122, 45);
            this.trackBar1.TabIndex = 4;
            this.trackBar1.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.trackBar1.Scroll += new System.EventHandler(this.TrackBar1_Scroll);
            // 
            // trackBarLabel
            // 
            this.trackBarLabel.AutoSize = true;
            this.trackBarLabel.Location = new System.Drawing.Point(12, 177);
            this.trackBarLabel.Name = "trackBarLabel";
            this.trackBarLabel.Size = new System.Drawing.Size(13, 13);
            this.trackBarLabel.TabIndex = 5;
            this.trackBarLabel.Text = "0";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // chooseFileButton
            // 
            this.chooseFileButton.Location = new System.Drawing.Point(15, 245);
            this.chooseFileButton.Name = "chooseFileButton";
            this.chooseFileButton.Size = new System.Drawing.Size(140, 29);
            this.chooseFileButton.TabIndex = 6;
            this.chooseFileButton.Text = "Choose File";
            this.chooseFileButton.UseVisualStyleBackColor = true;
            this.chooseFileButton.Click += new System.EventHandler(this.ChooseFileButton_Click);
            // 
            // bothButton
            // 
            this.bothButton.Location = new System.Drawing.Point(15, 112);
            this.bothButton.Name = "bothButton";
            this.bothButton.Size = new System.Drawing.Size(140, 29);
            this.bothButton.TabIndex = 7;
            this.bothButton.Text = "Both";
            this.bothButton.UseVisualStyleBackColor = true;
            this.bothButton.Click += new System.EventHandler(this.BothButton_Click);
            // 
            // axMap1
            // 
            this.axMap1.Enabled = true;
            this.axMap1.Location = new System.Drawing.Point(177, 42);
            this.axMap1.Name = "axMap1";
            this.axMap1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axMap1.OcxState")));
            this.axMap1.Size = new System.Drawing.Size(995, 396);
            this.axMap1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 450);
            this.Controls.Add(this.bothButton);
            this.Controls.Add(this.chooseFileButton);
            this.Controls.Add(this.trackBarLabel);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.viewLabel);
            this.Controls.Add(this.generalizedButton);
            this.Controls.Add(this.originalButton);
            this.Controls.Add(this.axMap1);
            this.Name = "Form1";
            this.Text = "Geometry Generalize";
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axMap1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private AxMapWinGIS.AxMap axMap1;
        private System.Windows.Forms.Button originalButton;
        private System.Windows.Forms.Button generalizedButton;
        private System.Windows.Forms.Label viewLabel;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Label trackBarLabel;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button chooseFileButton;
        private System.Windows.Forms.Button bothButton;
    }
}

