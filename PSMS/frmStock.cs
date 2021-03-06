﻿using CrystalDecisions.CrystalReports.Engine;
using MetroFramework.Forms;
using PSMS.Class;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PSMS
{
    public partial class frmStock : MetroForm
    {
        public frmStock()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
        }
        
        private void frmStock_Load(object sender, EventArgs e)
        {
            cbFilter.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;

            listStock.Items.Clear();
            loadData();

            comboBox1_SelectedIndexChanged(this, null);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            View view = comboBox1.SelectedIndex == 0 ? View.Details : View.LargeIcon;
            listStock.View = view;

        }

        private void loadData(string sql="")
        {
            ImageList imglist = new ImageList();
            foreach (ListViewItem item in Helper.getListStock(ref imglist, sql))
            {

                if (Helper.checkStock(item.Text) <= 0)
                {
                    MetroFramework.MetroMessageBox.Show(this, "This item is of out stock?", "Warning : Item Code " + item.Text);

                }

                listStock.Items.Add(item);
                listStock.LargeImageList = imglist;
                listStock.Items[item.Index].ImageIndex = item.Index;
            }
        }

        private void exec(string table)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM " + table + ";", Connection.con);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    cbFilter2.Items.Add(reader.GetValue(2) + "");
                }

                reader.Close();
                cmd.Dispose();

                cbFilter2.SelectedIndex = 0;
                
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "EXEC");
            }
        }

        private void loadFilterData(int filtertype)
        {
            try
            {
                if (filtertype.Equals(0))
                {
                    exec("Model");
                }
                else
                {
                    exec("Phone_Type");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "loadFilter");
            }
        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbFilter2.Enabled = true;

            if (cbFilter.SelectedIndex.Equals(1))
            {
                cbFilter2.Items.Clear();
                loadFilterData(0);

            }
            else if (cbFilter.SelectedIndex.Equals(2))
            {
                cbFilter2.Items.Clear();
                loadFilterData(1);

            }
            else
            {
                cbFilter2.Items.Clear();
                cbFilter2.Enabled = false;

                listStock.Items.Clear();
                loadData("");
            }
        }

        private void cbFilter2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilter.SelectedIndex.Equals(1))
            {
                listStock.Items.Clear();
                loadData("WHERE Brand = '" + cbFilter2.SelectedItem + "'");
            }
            else if (cbFilter.SelectedIndex.Equals(2))
            {
                listStock.Items.Clear();
                loadData(" WHERE PhoneType = '" + cbFilter2.SelectedItem + "';");
            }
            else
            {
                listStock.Items.Clear();
                loadData("");
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            Connection.ExecuteScalar("Delete from Product where [PCode]='" + listStock.SelectedItems[0].Text + "'");
            listStock.Items.RemoveAt(listStock.SelectedIndices[0]);
        }

        private void listStock_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnupdate.Enabled = listStock.SelectedIndices.Count > 0 ? true : false;
            btnRemove.Enabled = listStock.SelectedIndices.Count > 0 ? true : false;
           
            if(listStock.SelectedIndices.Count > 0)
            {
                int qty = Convert.ToInt32(listStock.SelectedItems[0].SubItems[4].Text);
                double cost = Convert.ToDouble(listStock.SelectedItems[0].SubItems[9].Text);
                double saleprice = Convert.ToDouble(listStock.SelectedItems[0].SubItems[8].Text);

                txtQty.Text = qty + "";
                txtCost.Text = "$" + (qty*cost);
                txtSalePrice.Text = "$" + (qty *saleprice);
                
            }

        }
        

        
        private void button1_Click(object sender, EventArgs e)
        {
            frmProduct frmpro = new frmProduct();
            frmpro.itemPanel1.Items.Remove(frmpro.itemContainer4);
            frmpro.closed = true;
            frmpro.dgProduct.Visible = false;
            ListViewItem.ListViewSubItemCollection selectitem = listStock.SelectedItems[0].SubItems;
            
            frmpro.txtPID.Text = int.Parse(PSMS.Class.Connection.ExecuteScalar("Select PID from Product where PCode='" + selectitem[0].Text + "'") + "")+"";
            frmpro.txtPCode.Text = selectitem[0].Text;
            frmpro.txtPName.Text = selectitem[1].Text;
            frmpro.txtPSize.Text = selectitem[2].Text;
            frmpro.txtColor.Text = selectitem[3].Text;
            frmpro.txtQuantity.Text = selectitem[4].Text;
            frmpro.model = int.Parse(Connection.ExecuteScalar("Select MID from Model where Description='" + selectitem[5].Text + "'") + "");
            frmpro.phonetype = int.Parse(Connection.ExecuteScalar("Select TID from Type where Description='" + selectitem[7].Text + "'") + "");
            frmpro.type = int.Parse(Connection.ExecuteScalar("Select PTypeID from Phone_Type where Description='" + selectitem[6].Text + "'") + "");      
            frmpro.txtSalePrice.Text = selectitem[8].Text;
            frmpro.txtUnitprice.Text = selectitem[9].Text;
            frmpro.pro_img.Image = listStock.LargeImageList.Images[listStock.SelectedIndices[0]];
            
            frmpro.ShowDialog();

            if (frmpro.updated)
            {
               foreach(ListViewItem temp in listStock.Items)
               {
                   listStock.Items.Remove(temp);
               }
               frmStock_Load(this, null);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmProduct frmpro = new frmProduct();
            frmpro.dgProduct.Visible = false;
            frmpro.itemPanel1.Items.Remove(frmpro.itemContainer2);
            frmpro.itemPanel1.Items.Remove(frmpro.itemContainer3);

            frmpro.closed = true;
            frmpro.ShowDialog();
            foreach (ListViewItem temp in listStock.Items)
            {
                listStock.Items.Remove(temp);
            }
            frmStock_Load(this, null);
        }

        private void txtfilter_TextChanged(object sender, EventArgs e)
        {
            foreach (ListViewItem temp in listStock.Items)
                listStock.Items.Remove(temp);

            String addSql = "";

            if (comboBox2.SelectedIndex.Equals(0))
            {
                addSql = txtfilter.Text == "" ? "" : "Where PCode like '%" + txtfilter.Text.Trim() + "%'";
            }
            else
            {
                addSql = txtfilter.Text == "" ? "" : "Where PName like '%" + txtfilter.Text.Trim() + "%'";
            }

            
            ImageList imglist = new ImageList();
            foreach (ListViewItem item in Class.Helper.getListStock(ref imglist, addSql))
            {
                listStock.Items.Add(item);
                listStock.LargeImageList = imglist;
                listStock.Items[item.Index].ImageIndex = item.Index;

            }
            comboBox1_SelectedIndexChanged(this, null);
        }

        
    }
}
