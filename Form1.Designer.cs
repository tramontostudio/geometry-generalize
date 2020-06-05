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
            this.zoomButton = new System.Windows.Forms.Button();
            this.moveButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axMap1)).BeginInit();
            this.SuspendLayout();
            // 
            // originalButton
            // 
            this.originalButton.Location = new System.Drawing.Point(15, 155);
            this.originalButton.Name = "originalButton";
            this.originalButton.Size = new System.Drawing.Size(140, 29);
            this.originalButton.TabIndex = 1;
            this.originalButton.Text = "Original";
            this.originalButton.UseVisualStyleBackColor = true;
            this.originalButton.Click += new System.EventHandler(this.OriginalButton_Click);
            // 
            // generalizedButton
            // 
            this.generalizedButton.Location = new System.Drawing.Point(15, 190);
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
            this.viewLabel.Size = new System.Drawing.Size(0, 24);
            this.viewLabel.TabIndex = 3;
            this.viewLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(33, 302);
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
            this.trackBarLabel.Location = new System.Drawing.Point(14, 316);
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
            this.chooseFileButton.Location = new System.Drawing.Point(15, 387);
            this.chooseFileButton.Name = "chooseFileButton";
            this.chooseFileButton.Size = new System.Drawing.Size(140, 29);
            this.chooseFileButton.TabIndex = 6;
            this.chooseFileButton.Text = "Choose File";
            this.chooseFileButton.UseVisualStyleBackColor = true;
            this.chooseFileButton.Click += new System.EventHandler(this.ChooseFileButton_Click);
            // 
            // bothButton
            // 
            this.bothButton.Location = new System.Drawing.Point(15, 225);
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
            // zoomButton
            // 
            this.zoomButton.Location = new System.Drawing.Point(87, 42);
            this.zoomButton.Name = "zoomButton";
            this.zoomButton.Size = new System.Drawing.Size(68, 71);
            this.zoomButton.TabIndex = 8;
            this.zoomButton.Text = "Zoom";
            this.zoomButton.UseVisualStyleBackColor = true;
            this.zoomButton.Click += new System.EventHandler(this.ZoomButton_Click);
            // 
            // moveButton
            // 
            this.moveButton.Location = new System.Drawing.Point(17, 42);
            this.moveButton.Name = "moveButton";
            this.moveButton.Size = new System.Drawing.Size(68, 71);
            this.moveButton.TabIndex = 9;
            this.moveButton.Text = "Move";
            this.moveButton.UseVisualStyleBackColor = true;
            this.moveButton.Click += new System.EventHandler(this.MoveButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(49, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 17);
            this.label1.TabIndex = 10;
            this.label1.Text = "Cursor Type";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(61, 126);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 17);
            this.label2.TabIndex = 11;
            this.label2.Text = "Layers";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3.Location = new System.Drawing.Point(36, 273);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(99, 17);
            this.label3.TabIndex = 12;
            this.label3.Text = "Corridor Width";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 450);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.moveButton);
            this.Controls.Add(this.zoomButton);
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
        private System.Windows.Forms.Button zoomButton;
        private System.Windows.Forms.Button moveButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}

