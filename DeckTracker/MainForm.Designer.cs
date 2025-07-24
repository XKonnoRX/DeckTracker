namespace DeckTracker
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.Panel panel1;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.timerPos = new System.Windows.Forms.Timer(this.components);
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.EquityPanel = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.OddsPanel = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.CardsPanel = new System.Windows.Forms.Panel();
            this.panel9 = new System.Windows.Forms.Panel();
            this.panel10 = new System.Windows.Forms.Panel();
            this.TableBox = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(47)))));
            panel1.Dock = System.Windows.Forms.DockStyle.Top;
            panel1.Location = new System.Drawing.Point(0, 24);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(430, 10);
            panel1.TabIndex = 26;
            // 
            // timer
            // 
            this.timer.Enabled = true;
            this.timer.Interval = 1000;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // timerPos
            // 
            this.timerPos.Enabled = true;
            this.timerPos.Interval = 10;
            this.timerPos.Tick += new System.EventHandler(this.timerPos_Tick);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(430, 24);
            this.pictureBox1.TabIndex = 24;
            this.pictureBox1.TabStop = false;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(47)))));
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 75);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(430, 10);
            this.panel3.TabIndex = 28;
            // 
            // EquityPanel
            // 
            this.EquityPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.EquityPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.EquityPanel.Location = new System.Drawing.Point(0, 85);
            this.EquityPanel.Name = "EquityPanel";
            this.EquityPanel.Size = new System.Drawing.Size(430, 148);
            this.EquityPanel.TabIndex = 29;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(47)))));
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(0, 233);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(430, 10);
            this.panel5.TabIndex = 30;
            // 
            // OddsPanel
            // 
            this.OddsPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.OddsPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.OddsPanel.Location = new System.Drawing.Point(0, 243);
            this.OddsPanel.Name = "OddsPanel";
            this.OddsPanel.Size = new System.Drawing.Size(430, 230);
            this.OddsPanel.TabIndex = 31;
            // 
            // panel7
            // 
            this.panel7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(47)))));
            this.panel7.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel7.Location = new System.Drawing.Point(0, 473);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(430, 10);
            this.panel7.TabIndex = 32;
            // 
            // CardsPanel
            // 
            this.CardsPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CardsPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.CardsPanel.Location = new System.Drawing.Point(0, 483);
            this.CardsPanel.Name = "CardsPanel";
            this.CardsPanel.Size = new System.Drawing.Size(430, 240);
            this.CardsPanel.TabIndex = 33;
            // 
            // panel9
            // 
            this.panel9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(47)))));
            this.panel9.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel9.Location = new System.Drawing.Point(0, 723);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(430, 10);
            this.panel9.TabIndex = 34;
            // 
            // panel10
            // 
            this.panel10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel10.Location = new System.Drawing.Point(0, 733);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(430, 103);
            this.panel10.TabIndex = 35;
            // 
            // TableBox
            // 
            this.TableBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(57)))));
            this.TableBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TableBox.Font = new System.Drawing.Font("Yu Gothic UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.TableBox.ForeColor = System.Drawing.SystemColors.Menu;
            this.TableBox.Location = new System.Drawing.Point(1, 12);
            this.TableBox.Name = "TableBox";
            this.TableBox.ReadOnly = true;
            this.TableBox.Size = new System.Drawing.Size(428, 22);
            this.TableBox.TabIndex = 20;
            this.TableBox.TabStop = false;
            this.TableBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TableBox.DoubleClick += new System.EventHandler(this.TableBox_Click);
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.TableBox);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 34);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(430, 41);
            this.panel2.TabIndex = 27;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(57)))));
            this.ClientSize = new System.Drawing.Size(430, 836);
            this.Controls.Add(this.panel10);
            this.Controls.Add(this.panel9);
            this.Controls.Add(this.CardsPanel);
            this.Controls.Add(this.panel7);
            this.Controls.Add(this.OddsPanel);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.EquityPanel);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(panel1);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Timer timerPos;
        private OpenFileDialog openFileDialog1;
        private PictureBox pictureBox1;
        private Panel panel1;
        private Panel panel3;
        private Panel EquityPanel;
        private Panel panel5;
        private Panel OddsPanel;
        private Panel panel7;
        private Panel CardsPanel;
        private Panel panel9;
        private Panel panel10;
        private TextBox TableBox;
        private Panel panel2;
    }
}