using System;
using System.Windows.Forms;

namespace DVLD
{
    public partial class ViewDetails : Form
    {
        private int _PersonID = -1;

        public ViewDetails(int ID)
        {
            InitializeComponent();
            _PersonID = ID;
        }

        private void ViewDetails_Load(object sender, EventArgs e)
        {

        }
    }
}