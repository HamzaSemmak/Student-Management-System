using log4net;
using Student_Management.Modules.LoggerManager;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Student_Management.IHM_s.ComposentsGraphique
{
    public partial class FailAlert : Form
    {
        public FailAlert(string Msg)
        {
            InitializeComponent();
            this.label2.Text = $"{Msg.ToString()}";
            Program.logger.Error($"Error : {Msg} ");
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
