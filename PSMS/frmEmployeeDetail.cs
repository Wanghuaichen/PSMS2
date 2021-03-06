﻿using MetroFramework;
using MetroFramework.Forms;
using PSMS.EmpDataSetTableAdapters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PSMS
{
    public partial class frmEmployeeDetail : MetroForm
    {
        frmEmployeeFunction empFun;
        public frmEmployeeDetail()
        {
            InitializeComponent();
            empFun = new frmEmployeeFunction();
           
            btnClr.Visible = false;
            itemPanel1.Items.Remove(itemContainer1);
            itemPanel1.Items.Remove(itemContainer4);

            comboBox1.SelectedIndex = 0;
        }
        
        private void frmEmployeeDetail_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'pSMS2DataSet2.EmployeeDetails' table. You can move, or remove it, as needed.
            this.employeeDetailsTableAdapter.Fill(this.pSMS2DataSet2.EmployeeDetails);
            // TODO: This line of code loads data into the 'pSMS2DataSet2.Employee' table. You can move, or remove it, as needed.

            comboBox1.SelectedIndex = 0;
            //empFun.FillDataGridView(ref dataGridView1);
         
        }
        private frmEmp GetEmp()
        {
            frmEmp emp = new frmEmp();
            emp.emp_id = int.Parse(txtEmpID.Text);
            emp.empcode = txtEmpCode.Text;
            emp.emp_lnkh = txtKh1.Text;
            emp.emp_fnkh = txtKh2.Text;
            emp.emp_lnen = txtEn1.Text;
            emp.emp_fnen = txtEn2.Text;
            emp.gender = cbBGender.SelectedItem.ToString();
            emp.idcard = txtIDCard.Text;
            emp.address = rtxtAddress.Text;
            emp.phone = txtPhone.Text;
            emp.email = txtEmail.Text;
            emp.pos_id = Convert.ToInt32(txtPosID.Text);
            emp.salary = Convert.ToInt32(txtSalary.Text);
            emp.join_date = Join_date.Text;
            return emp;
        }
        private void btnNew_Click(object sender, EventArgs e)
        {
            //int result = (int)empFun.Insert(GetEmp());
            //if (result > 0)
            //{
            //    MetroMessageBox.Show(this, "New Record Insert", "MetroMessagebox", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    empFun.FillDataGridView(ref dataGridView1);
            //    txtEmpID.Text = result.ToString();

            //}
            employeeBindingNavigator.AddNewItem.PerformClick();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //int result = (int)empFun.Update(GetEmp());
            //if (result > 0)
            //{
            //    MetroMessageBox.Show(this, "Record Update", "MetroMessagebox", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    empFun.FillDataGridView(ref dataGridView1);

            //}
            employeeBindingNavigatorSaveItem_Click(this, null);
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            //int result = (int)empFun.Delete(Convert.ToInt32(txtEmpID.Text));
            //DialogResult dia = MetroMessageBox.Show(this, "Are you sure you want permanently delete this record?", "MetroMessagebox", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //if (dia == DialogResult.Yes)
            //{
            //    if (result > 0)
            //    {
            //        MetroMessageBox.Show(this, "Record Deleted", "MetroMessagebox", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        empFun.FillDataGridView(ref dataGridView1);
            //        txtEmpCode.Text = "";
            //        txtKh1.Text = "";
            //        txtKh2.Text = "";
            //        txtEn1.Text = "";
            //        txtEn2.Text = "";
            //        txtIDCard.Text = "";
            //        rtxtAddress.Text = "";
            //        txtPhone.Text = "";
            //        txtEmail.Text = "";
            //        txtPosID.Text = "";
            //        txtSalary.Text = "";
            //        Join_date.Text = "";

            //    }
            //}
            employeeBindingNavigator.DeleteItem.PerformClick();
        }

        private void btnClr_Click(object sender, EventArgs e)
        {
            txtEmpCode.Text = "";
            txtKh1.Text = "";
            txtKh2.Text = "";
            txtEn1.Text = "";
            txtEn2.Text = "";
            txtIDCard.Text = "";
            rtxtAddress.Text = "";
            txtPhone.Text = "";
            txtEmail.Text = "";
            txtPosID.Text = "";
            txtSalary.Text = "";
            Join_date.Text = "";
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int emp_id = 0;
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                if (row.Cells != null && row.Cells[0].Value != null)
                {
                    emp_id = Convert.ToInt32(row.Cells[0].Value.ToString());
                }
            }

            DataTable dt = empFun.GetData("SELECT * FROM Employee WHERE EmpID = " + emp_id);

            foreach (DataRow row in dt.Rows)
            {
                txtEmpID.Text = row["EmpID"].ToString();
                txtEmpCode.Text = row["EmpCode"].ToString();
                txtKh1.Text = row["EmpLNKH"].ToString();
                txtKh2.Text = row["EmpFNKH"].ToString();
                txtEn1.Text = row["EmpLNEN"].ToString();
                txtEn2.Text = row["EmpFNEN"].ToString();
                cbBGender.SelectedItem = row["Gender"].ToString();
                txtIDCard.Text = row["IDCard"].ToString();
                rtxtAddress.Text = row["Address"].ToString();
                txtPhone.Text = row["Phone"].ToString();
                txtEmail.Text = row["email"].ToString();
                txtPosID.Text = row["PosID"].ToString();
                txtSalary.Text = row["Salary"].ToString();
                Join_date.Text = row["JoinDate"].ToString();
            }
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            int emp_id = 0;
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                if (row.Cells != null && row.Cells[0].Value != null)
                {
                    emp_id = Convert.ToInt32(row.Cells[0].Value.ToString());
                }
            }

            DataTable dt = empFun.GetData("SELECT * FROM Employee WHERE EmpID = " + emp_id);

            foreach (DataRow row in dt.Rows)
            {
                txtEmpID.Text = row["EmpID"].ToString();
                txtEmpCode.Text = row["EmpCode"].ToString();
                txtKh1.Text = row["EmpLNKH"].ToString();
                txtKh2.Text = row["EmpFNKH"].ToString();
                txtEn1.Text = row["EmpLNEN"].ToString();
                txtEn2.Text = row["EmpFNEN"].ToString();
                cbBGender.SelectedItem = row["Gender"].ToString();
                txtIDCard.Text = row["IDCard"].ToString();
                rtxtAddress.Text = row["Address"].ToString();
                txtPhone.Text = row["Phone"].ToString();
                txtEmail.Text = row["email"].ToString();
                txtPosID.Text = row["PosID"].ToString();
                txtSalary.Text = row["Salary"].ToString();
                Join_date.Text = row["JoinDate"].ToString();
            }
        }

     
        private void employeeBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            try
            {
                this.Validate();
                this.employeeBindingSource.EndEdit();
                this.tableAdapterManager.UpdateAll(this.pSMS2DataSet2);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {

        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {

        }

        private void bindingNavigatorMoveNextItem_Click(object sender, EventArgs e)
        {

        }

        private void frmEmployeeDetail_FormClosing(object sender, FormClosingEventArgs e)
        {
            /*
            DialogResult diag = MetroMessageBox.Show(this, "Do you Want to Save Change ? ", "MetroMessage", MessageBoxButtons.YesNo);
            if (diag == DialogResult.Yes)
            {
                try
                {
                    btnSave_Click(this, null);
                }
                catch(Exception ex)
                {
                    MetroMessageBox.Show(this, ex.Message);
                    e.Cancel=true;
                }
            }*/
        }

        private void txtfilter_ButtonClick(object sender, EventArgs e)
        {
            string sql = "Select * from viewEmployeeDetails ";
            sql += txtfilter.Text.Trim() == "" ? "" : "Where " + comboBox1.Text + " like N'%" + txtfilter.Text.Trim() + "%' COLLATE Latin1_General_100_BIN2";

this.employeeDetailsTableAdapter.Adapter.SelectCommand.CommandText = sql;
            this.employeeDetailsTableAdapter.Fill(this.pSMS2DataSet2.EmployeeDetails);
           
        }

        private void txtfilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtfilter_ButtonClick(this, null);
            }
        }

        private void txtfilter_Click(object sender, EventArgs e)
        {

        }

        private void txtfilter_TextChanged(object sender, EventArgs e)
        {
            txtfilter_ButtonClick(sender, e);
        }
    }
}
