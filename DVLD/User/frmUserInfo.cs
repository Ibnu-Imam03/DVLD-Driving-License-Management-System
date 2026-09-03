using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.User
{
    public partial class frmUserInfo : Form
    {
        private int _User = -1;
        public frmUserInfo(int User)
        {
            InitializeComponent();
            _User = User;
        }
        private void frmUserInfo_Load(object sender, EventArgs e)
        {
            usUserInfo1.LoadUserInfo(_User);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
