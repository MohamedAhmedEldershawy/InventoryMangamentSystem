using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InventoryMangamentSystem
{
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                // التحقق من المدخلات
                if (string.IsNullOrWhiteSpace(textUsername.Text) ||
                    string.IsNullOrWhiteSpace(textPassword.Text) ||
                    string.IsNullOrWhiteSpace(textConfirmPassword.Text))
                {
                    MessageBox.Show("All fields are required.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (textPassword.Text != textConfirmPassword.Text)
                {
                    MessageBox.Show("Passwords do not match.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // إدخال البيانات في الجدول بدون تمرير UserId (Auto Increment)
                this.userTableAdapter.InsertLogin(
                    textUsername.Text,
                    textPassword.Text,
                    textConfirmPassword.Text
                );

                // تحديث البيانات في الفورم
                this.userTableAdapter.Fill(this.inventoryDbDataSet.User);
                this.Validate();
                this.userBindingSource.EndEdit();

                MessageBox.Show("Registration successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void userBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.userBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.inventoryDbDataSet);
        }

        private void Register_Load(object sender, EventArgs e)
        {
            // تحميل البيانات
            this.userTableAdapter.Fill(this.inventoryDbDataSet.User);
        }
    }
}
