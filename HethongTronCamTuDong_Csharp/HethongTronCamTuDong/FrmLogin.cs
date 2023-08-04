using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace HethongTronCamTuDong
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {

        }

        private void txtUser_Pass_TextChanged(object sender, EventArgs e)
        {
            if (txtUserName.Text == "" || txtPassword.Text == "")
            {
                BtnLogin.Enabled = false;
            }
            else
                BtnLogin.Enabled = true;
        }

        private void chkCheckPass_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCheckPass.Checked == true)
            {
                txtPassword.PasswordChar = (char)0;
                chkCheckPass.Text = "Hiện mật khẩu";
            }
            else
            {
                txtPassword.PasswordChar = '*';
                chkCheckPass.Text = "Ẩn mật khẩu";
            }

        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                ConnectData.Create_Connect();
                SqlCommand command = new SqlCommand("spSVDangnhap",ConnectData.strConnect);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@taikhoan", txtUserName.Text);
                command.Parameters.AddWithValue("@matkhau", txtPassword.Text);

                int code = Convert.ToInt32( command.ExecuteScalar());
                if (code == 1)
                {
                    FrmMain main = new FrmMain();
                    main.ShowDialog();
                    txtUserName.Focus();
                }
                else if (code == 2)
                {
                    MessageBox.Show("Tài khoản hoặc mật khẩu không chính xác! ");
                }
                else
                {
                    MessageBox.Show("Tài khoản không tồn tại");
                }
            }
            catch 
            {
                MessageBox.Show("Erros connect to SQL ");
            }
            finally
            {
                ConnectData.strConnect.Close();
            }
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn muốn thoát","Thông báo",MessageBoxButtons.OKCancel,MessageBoxIcon.Question) == DialogResult.OK)
            {
                this.Close();
            }
        }

        private void BtnRegister_Click(object sender, EventArgs e)
        {
            FrmRegister register = new FrmRegister();
            register.ShowDialog();
        }

        private void BtnChangPassword_Click(object sender, EventArgs e)
        {
            FrmChangePass changePass = new FrmChangePass();
            changePass.ShowDialog();
        }

        private void txtUserName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                txtPassword.Focus();
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                txtUserName.Focus();
            }
        }
    }
}
