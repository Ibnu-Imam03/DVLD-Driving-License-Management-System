namespace DVLD
{
    partial class ViewDetails
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
            this.clsPersonCardWithFilter1 = new DVLD.People.controls.clsPersonCardWithFilter();
            this.SuspendLayout();
            // 
            // clsPersonCardWithFilter1
            // 
            this.clsPersonCardWithFilter1.BackColor = System.Drawing.Color.SkyBlue;
            this.clsPersonCardWithFilter1.FilterEnabled = true;
            this.clsPersonCardWithFilter1.Location = new System.Drawing.Point(71, 90);
            this.clsPersonCardWithFilter1.Name = "clsPersonCardWithFilter1";
            this.clsPersonCardWithFilter1.ShowAddPerson = true;
            this.clsPersonCardWithFilter1.Size = new System.Drawing.Size(714, 348);
            this.clsPersonCardWithFilter1.TabIndex = 8;
            // 
            // ViewDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(877, 466);
            this.Controls.Add(this.clsPersonCardWithFilter1);
            this.Name = "ViewDetails";
            this.Text = "ViewDetails";
            this.Load += new System.EventHandler(this.ViewDetails_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private People.controls.clsPersonCardWithFilter clsPersonCardWithFilter1;
    }
}