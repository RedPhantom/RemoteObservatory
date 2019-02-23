namespace ROConfigurator
{
    partial class frmConfigurator
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
            this.label3 = new System.Windows.Forms.Label();
            this.cbTeleModel = new System.Windows.Forms.ComboBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkDomeUse = new System.Windows.Forms.CheckBox();
            this.cbDomeCom = new System.Windows.Forms.ComboBox();
            this.cbTeleCom = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chkCameraUse = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbCameraDevice = new System.Windows.Forms.ComboBox();
            this.btnTestConnection = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(116, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "Telescope Model";
            // 
            // cbTeleModel
            // 
            this.cbTeleModel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTeleModel.FormattingEnabled = true;
            this.cbTeleModel.Location = new System.Drawing.Point(133, 28);
            this.cbTeleModel.Name = "cbTeleModel";
            this.cbTeleModel.Size = new System.Drawing.Size(206, 24);
            this.cbTeleModel.TabIndex = 5;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(336, 349);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 34);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(255, 349);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 34);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkDomeUse);
            this.groupBox1.Controls.Add(this.cbDomeCom);
            this.groupBox1.Controls.Add(this.cbTeleCom);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(13, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(398, 100);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Communication";
            // 
            // chkDomeUse
            // 
            this.chkDomeUse.AutoSize = true;
            this.chkDomeUse.Location = new System.Drawing.Point(282, 60);
            this.chkDomeUse.Name = "chkDomeUse";
            this.chkDomeUse.Size = new System.Drawing.Size(78, 21);
            this.chkDomeUse.TabIndex = 9;
            this.chkDomeUse.Text = "In Use?";
            this.chkDomeUse.UseVisualStyleBackColor = true;
            this.chkDomeUse.CheckedChanged += new System.EventHandler(this.chkDomeUse_CheckedChanged);
            // 
            // cbDomeCom
            // 
            this.cbDomeCom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDomeCom.FormattingEnabled = true;
            this.cbDomeCom.Location = new System.Drawing.Point(155, 58);
            this.cbDomeCom.Name = "cbDomeCom";
            this.cbDomeCom.Size = new System.Drawing.Size(121, 24);
            this.cbDomeCom.TabIndex = 8;
            this.cbDomeCom.SelectedIndexChanged += new System.EventHandler(this.cbDomeCom_SelectedIndexChanged);
            // 
            // cbTeleCom
            // 
            this.cbTeleCom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTeleCom.FormattingEnabled = true;
            this.cbTeleCom.Location = new System.Drawing.Point(155, 28);
            this.cbTeleCom.Name = "cbTeleCom";
            this.cbTeleCom.Size = new System.Drawing.Size(121, 24);
            this.cbTeleCom.TabIndex = 7;
            this.cbTeleCom.SelectedIndexChanged += new System.EventHandler(this.cbTeleCom_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(109, 17);
            this.label2.TabIndex = 6;
            this.label2.Text = "Dome COM port";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(138, 17);
            this.label1.TabIndex = 5;
            this.label1.Text = "Telescope COM port";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.cbTeleModel);
            this.groupBox2.Location = new System.Drawing.Point(13, 118);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(398, 100);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Telescope";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.chkCameraUse);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.cbCameraDevice);
            this.groupBox3.Location = new System.Drawing.Point(13, 224);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(398, 100);
            this.groupBox3.TabIndex = 10;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Imaging";
            // 
            // chkCameraUse
            // 
            this.chkCameraUse.AutoSize = true;
            this.chkCameraUse.Location = new System.Drawing.Point(314, 28);
            this.chkCameraUse.Name = "chkCameraUse";
            this.chkCameraUse.Size = new System.Drawing.Size(78, 21);
            this.chkCameraUse.TabIndex = 10;
            this.chkCameraUse.Text = "In Use?";
            this.chkCameraUse.UseVisualStyleBackColor = true;
            this.chkCameraUse.CheckedChanged += new System.EventHandler(this.chkCameraUse_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 28);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(104, 17);
            this.label4.TabIndex = 6;
            this.label4.Text = "Camera Device";
            // 
            // cbCameraDevice
            // 
            this.cbCameraDevice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCameraDevice.FormattingEnabled = true;
            this.cbCameraDevice.Location = new System.Drawing.Point(121, 25);
            this.cbCameraDevice.Name = "cbCameraDevice";
            this.cbCameraDevice.Size = new System.Drawing.Size(187, 24);
            this.cbCameraDevice.TabIndex = 7;
            // 
            // btnTestConnection
            // 
            this.btnTestConnection.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnTestConnection.Location = new System.Drawing.Point(13, 349);
            this.btnTestConnection.Name = "btnTestConnection";
            this.btnTestConnection.Size = new System.Drawing.Size(127, 34);
            this.btnTestConnection.TabIndex = 11;
            this.btnTestConnection.Text = "Test Connection";
            this.btnTestConnection.UseVisualStyleBackColor = true;
            this.btnTestConnection.Click += new System.EventHandler(this.btnTestConnection_Click);
            // 
            // frmConfigurator
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(423, 395);
            this.Controls.Add(this.btnTestConnection);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "frmConfigurator";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RemoteObservatory Server Configurator";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbTeleModel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cbDomeCom;
        private System.Windows.Forms.ComboBox cbTeleCom;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbCameraDevice;
        private System.Windows.Forms.Button btnTestConnection;
        private System.Windows.Forms.CheckBox chkDomeUse;
        private System.Windows.Forms.CheckBox chkCameraUse;
    }
}

