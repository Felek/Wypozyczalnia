using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Wypozyczalnia.View
{
    public partial class WarehouseView : BaseView
    {
        #region Elementy widoku
        private ToolStrip toolStrip1;
        private ToolStripLabel toolStripLabel1;
        private ToolStripComboBox filterStatus;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripLabel toolStripLabel2;
        private ToolStripTextBox filterName;
        private ToolStripButton toolStripButton1;
        #endregion

        private List<string> statuses = null;
        private string everyStatus = "Każda";
    
        public WarehouseView()
        {
            InitializeComponent();
            filterStatus.Items.Add(everyStatus);
            filterStatus.SelectedItem = everyStatus;
        }

        public override void SetColumnsWidth()
        {
            try
            {
                double width = dataGridView1.Width - 20;
                dataGridView1.Columns[0].Width = (int)(0.3 * width);
                dataGridView1.Columns[1].Width = (int)(0.2 * width);
                dataGridView1.Columns[2].Width = (int)(0.1 * width);
                dataGridView1.Columns[3].Width = (int)(0.2 * width);
                dataGridView1.Columns[3].Width = (int)(0.2 * width);
            }
            catch (ArgumentOutOfRangeException ex)
            {

            }
        }

        public void FillStatusList(List<string> statuses)
        {
            this.statuses = statuses;
            filterStatus.Items.Clear();
            filterStatus.Items.Add(everyStatus);
            filterStatus.SelectedItem = everyStatus;
            foreach (string status in statuses)
            {
                filterStatus.Items.Add(status);
            }
        }

        private void ActionResized(object sender, EventArgs e)
        {
            this.SetColumns();
        }

        #region InitializeComponent()
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WarehouseView));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.filterStatus = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.filterName = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.filterStatus,
            this.toolStripSeparator1,
            this.toolStripLabel2,
            this.filterName,
            this.toolStripButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(734, 25);
            this.toolStrip1.TabIndex = 7;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(48, 22);
            this.toolStripLabel1.Text = "Status";
            // 
            // filterStatus
            // 
            this.filterStatus.Name = "filterStatus";
            this.filterStatus.Size = new System.Drawing.Size(121, 25);
            this.filterStatus.Text = "Status";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(57, 22);
            this.toolStripLabel2.Text = "Nazwa";
            // 
            // filterName
            // 
            this.filterName.Name = "filterName";
            this.filterName.Size = new System.Drawing.Size(100, 25);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "Szukaj";
            // 
            // WarehouseView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(734, 512);
            this.Controls.Add(this.toolStrip1);
            this.Name = "WarehouseView";
            this.SizeChanged += new System.EventHandler(this.ActionResized);
            this.Controls.SetChildIndex(this.buttonAdd, 0);
            this.Controls.SetChildIndex(this.buttonEdit, 0);
            this.Controls.SetChildIndex(this.buttonDelete, 0);
            this.Controls.SetChildIndex(this.toolStrip1, 0);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
}
