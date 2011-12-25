using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace TableGenerator
{
    public partial class fShowTable : Form
    {
        public fShowTable(DataTable a_dataTable)
        {
            InitializeComponent();
            DataTable _tempDT = a_dataTable.Clone();
            _tempDT.Columns["terminals"].DataType = typeof(object);

            foreach (DataRow _row in a_dataTable.Rows)
                _tempDT.Rows.Add(_row.ItemArray);

            foreach (DataRow _row in _tempDT.Rows)
            {
                _row["terminals"] = String.Join(", ", _row["terminals"] as string[]);
            }
            f_dgvMain.DataSource = _tempDT;
        }
    }
}
