using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.People
{
    public partial class frmFindPerson : Form
    {
        public delegate void DataBackEventHndler(object sender, int PersonID);
        public event DataBackEventHndler DataBack;

        public frmFindPerson()
        {
            InitializeComponent();
        }

        private void frmFindPerson_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataBack?.Invoke(this, clsPersonCardWithFilter1.PersonID);
            this.Close();
        }
    }
}
