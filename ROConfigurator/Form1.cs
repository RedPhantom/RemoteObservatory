using StandAlone.Modules;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ROConfigurator
{
    public partial class frmConfigurator : Form
    {
        string[] comPorts;

        SettingsFile settings;

        public frmConfigurator()
        {
            InitializeComponent();

            settings = new SettingsFile();

            // populate com ports:
            comPorts = SerialPort.GetPortNames();

            foreach (var s in comPorts)
            {
                cbTeleCom.Items.Add(s);
                cbDomeCom.Items.Add(s);
            }

            // populate telescope models:
            // todo: get all dictionary files and try to parse them.

            // populate camera devices
            // todo: get all camera devices from DirectShow.
        }

        private void cbTeleCom_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (var item in comPorts)
            {
                if (item.ToString() != cbTeleCom.SelectedItem.ToString())
                {
                    cbDomeCom.Items.Clear();
                    cbDomeCom.Items.Add(item);
                }
            }
        }

        private void cbDomeCom_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (var item in comPorts)
            {
                if (item.ToString() != cbDomeCom.SelectedItem.ToString())
                {
                    cbTeleCom.Items.Clear();
                    cbTeleCom.Items.Add(item);
                }
            }
        }

        private void btnTestConnection_Click(object sender, EventArgs e)
        {
            // run the server with the -test parameter.
            TestConnection();
        }

        bool ValidateFields()
        {
            if (!comPorts.Contains(cbTeleCom.SelectedItem.ToString()))
            {
                MessageBox.Show(this, "Invalid field: Telescope COM Port, value is invalid/unselected.", "Invalid Field", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (chkDomeUse.Checked && !comPorts.Contains(cbDomeCom.SelectedItem.ToString()))
            {
                MessageBox.Show(this, "Invalid field: Dome COM Port, value is invalid/unselected. Consider disabling this feature by unselecting the In Use? checkbox.", "Invalid Field", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }


            if (cbTeleModel.SelectedIndex == -1)
            {
                MessageBox.Show(this, "Invalid field: Telescope Model, value is invalid/unselected.", "Invalid Field", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (chkCameraUse.Checked && cbCameraDevice.SelectedIndex == -1)
            {
                MessageBox.Show(this, "Invalid field: Camera Device, value is invalid/unselected. Consider disabling this feature by unselecting the In Use? checkbox.", "Invalid Field", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        void SaveSettings()
        {
            if (ValidateFields())
            {

            }
        }

        void TestConnection()
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // show dialog for config file location.

            // then save the settings.
            SaveSettings();
        }

        private void chkDomeUse_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkDomeUse.Checked)
                cbDomeCom.Enabled = false;
        }

        private void chkCameraUse_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkCameraUse.Checked)
                cbCameraDevice.Enabled = false;
        }
    }
}
