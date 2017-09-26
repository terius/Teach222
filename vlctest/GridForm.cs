using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace vlctest
{
    public partial class GridForm : Form
    {
        public GridForm()
        {
            InitializeComponent();
        }

        private void GridForm_Load(object sender, EventArgs e)
        {
            dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.None;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.Columns.Cast<DataGridViewColumn>().ToList().ForEach(f => f.SortMode = DataGridViewColumnSortMode.NotSortable);
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            dataGridView1.AllowUserToResizeRows = false;
            DataGridViewRow row;
            for (int i = 0; i < 20; i++)
            {
                row = new DataGridViewRow();
                int index = this.dataGridView1.Rows.Add(row);
                dataGridView1.Rows[index].Cells[0].Value = "列1_" + index;
                dataGridView1.Rows[index].Cells[1].Value = "列2_" + index;
                dataGridView1.Rows[index].Cells[2].Value = "列3_" + index;
            }
        }
    }
}
