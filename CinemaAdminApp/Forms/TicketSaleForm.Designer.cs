namespace CinemaAdminApp.Forms
{
    partial class TicketSaleForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.cmbSessions = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblHallInfo = new System.Windows.Forms.Label();
            this.pnlSeats = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.lblSelectedSeat = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblPrice = new System.Windows.Forms.Label();
            this.btnSell = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.panelLegend = new System.Windows.Forms.Panel();
            this.panelTaken = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.panelFree = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panelHeader.SuspendLayout();
            this.panelLegend.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(102)))));
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1079, 60);
            this.panelHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(1079, 60);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Продажа билетов";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmbSessions
            // 
            this.cmbSessions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSessions.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.cmbSessions.FormattingEnabled = true;
            this.cmbSessions.Location = new System.Drawing.Point(20, 100);
            this.cmbSessions.Name = "cmbSessions";
            this.cmbSessions.Size = new System.Drawing.Size(400, 39);
            this.cmbSessions.TabIndex = 1;
            this.cmbSessions.SelectedIndexChanged += new System.EventHandler(this.cmbSessions_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(20, 80);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(241, 31);
            this.label1.TabIndex = 2;
            this.label1.Text = "Выберите сеанс:";
            // 
            // lblHallInfo
            // 
            this.lblHallInfo.AutoSize = true;
            this.lblHallInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.lblHallInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(102)))));
            this.lblHallInfo.Location = new System.Drawing.Point(20, 140);
            this.lblHallInfo.Name = "lblHallInfo";
            this.lblHallInfo.Size = new System.Drawing.Size(371, 31);
            this.lblHallInfo.TabIndex = 3;
            this.lblHallInfo.Text = "Зал: - | Свободных мест: 0";
            // 
            // pnlSeats
            // 
            this.pnlSeats.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlSeats.Location = new System.Drawing.Point(20, 170);
            this.pnlSeats.Name = "pnlSeats";
            this.pnlSeats.Size = new System.Drawing.Size(400, 299);
            this.pnlSeats.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(450, 100);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(262, 31);
            this.label2.TabIndex = 5;
            this.label2.Text = "Выбранное место:";
            // 
            // lblSelectedSeat
            // 
            this.lblSelectedSeat.AutoSize = true;
            this.lblSelectedSeat.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold);
            this.lblSelectedSeat.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.lblSelectedSeat.Location = new System.Drawing.Point(450, 130);
            this.lblSelectedSeat.Name = "lblSelectedSeat";
            this.lblSelectedSeat.Size = new System.Drawing.Size(402, 73);
            this.lblSelectedSeat.TabIndex = 6;
            this.lblSelectedSeat.Text = "Не выбрано";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(450, 180);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 31);
            this.label3.TabIndex = 7;
            this.label3.Text = "Цена:";
            // 
            // lblPrice
            // 
            this.lblPrice.AutoSize = true;
            this.lblPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold);
            this.lblPrice.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.lblPrice.Location = new System.Drawing.Point(450, 210);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Size = new System.Drawing.Size(124, 73);
            this.lblPrice.TabIndex = 8;
            this.lblPrice.Text = "0 ₽";
            // 
            // btnSell
            // 
            this.btnSell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnSell.Enabled = false;
            this.btnSell.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSell.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold);
            this.btnSell.ForeColor = System.Drawing.Color.White;
            this.btnSell.Location = new System.Drawing.Point(450, 320);
            this.btnSell.Name = "btnSell";
            this.btnSell.Size = new System.Drawing.Size(250, 60);
            this.btnSell.TabIndex = 9;
            this.btnSell.Text = "ПРОДАТЬ БИЛЕТ";
            this.btnSell.UseVisualStyleBackColor = false;
            this.btnSell.Click += new System.EventHandler(this.btnSell_Click);
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnClear.ForeColor = System.Drawing.Color.White;
            this.btnClear.Location = new System.Drawing.Point(450, 400);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(250, 40);
            this.btnClear.TabIndex = 11;
            this.btnClear.Text = "Очистить выбор";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // panelLegend
            // 
            this.panelLegend.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelLegend.Controls.Add(this.panelTaken);
            this.panelLegend.Controls.Add(this.label7);
            this.panelLegend.Controls.Add(this.panelFree);
            this.panelLegend.Controls.Add(this.label6);
            this.panelLegend.Controls.Add(this.label5);
            this.panelLegend.Location = new System.Drawing.Point(450, 260);
            this.panelLegend.Name = "panelLegend";
            this.panelLegend.Size = new System.Drawing.Size(250, 50);
            this.panelLegend.TabIndex = 12;
            // 
            // panelTaken
            // 
            this.panelTaken.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.panelTaken.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelTaken.Location = new System.Drawing.Point(140, 15);
            this.panelTaken.Name = "panelTaken";
            this.panelTaken.Size = new System.Drawing.Size(20, 20);
            this.panelTaken.TabIndex = 4;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label7.Location = new System.Drawing.Point(170, 15);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(131, 29);
            this.label7.TabIndex = 3;
            this.label7.Text = " - занятые";
            // 
            // panelFree
            // 
            this.panelFree.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.panelFree.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelFree.Location = new System.Drawing.Point(15, 15);
            this.panelFree.Name = "panelFree";
            this.panelFree.Size = new System.Drawing.Size(20, 20);
            this.panelFree.TabIndex = 2;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label6.Location = new System.Drawing.Point(45, 15);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(160, 29);
            this.label6.TabIndex = 1;
            this.label6.Text = " - свободные";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.label5.Location = new System.Drawing.Point(3, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(123, 29);
            this.label5.TabIndex = 0;
            this.label5.Text = "Легенда:";
            // 
            // TicketSaleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(15F, 30F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1079, 692);
            this.Controls.Add(this.panelLegend);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnSell);
            this.Controls.Add(this.lblPrice);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblSelectedSeat);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pnlSeats);
            this.Controls.Add(this.lblHallInfo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbSessions);
            this.Controls.Add(this.panelHeader);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.Name = "TicketSaleForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Продажа билетов";
            this.panelHeader.ResumeLayout(false);
            this.panelLegend.ResumeLayout(false);
            this.panelLegend.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #region Windows Form Designer generated code

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.ComboBox cmbSessions;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblHallInfo;
        private System.Windows.Forms.Panel pnlSeats;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblSelectedSeat;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.Button btnSell;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Panel panelLegend;
        private System.Windows.Forms.Panel panelTaken;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel panelFree;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;

        #endregion
    }
}