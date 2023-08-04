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
    public partial class FrmChangePass : Form
    {
        public FrmChangePass()
        {
            InitializeComponent();
        }

        private void txt_Changed(object sender, EventArgs e)
        {
            if (txtUserName.Text == "" || txtOldPass.Text == "" || txtNewPass.Text == "" || txtConfirmPass.Text == "")
            {
                BtnChange.Enabled = false;
            }
            else
                BtnChange.Enabled = true;
        }

        private void BtnChange_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtNewPass.Text != txtConfirmPass.Text)
                {
                    lblShowInfor.Text = "Mật khẩu xác nhận không đúng";
                    lblShowInfor.ForeColor = Color.Red;
                    txtNewPass.Text = "";
                    txtConfirmPass.Text = "";
                    txtNewPass.Focus();
                }
                else
                {
                    ConnectData.Create_Connect();
                    SqlCommand command = new SqlCommand("exec spDoiMatkhau '" + txtUserName.Text + "','"
                        + txtOldPass.Text + "','" + txtNewPass.Text + "' ", ConnectData.strConnect);
                    int code = Convert.ToInt32(command.ExecuteScalar());
                    if (code == 1)
                    {
                        lblShowInfor.Text = "Thay đổi mật khẩu thành công";
                        lblShowInfor.ForeColor = Color.Blue;
                        txtNewPass.Text = "";
                        txtOldPass.Text = "";
                        txtConfirmPass.Text = "";
                        txtUserName.Focus();
                    }
                    else
                    {
                        lblShowInfor.Text = "Tài khoản hoặc mật khẩu không chính xác";
                        lblShowInfor.ForeColor = Color.Red;
                        txtUserName.Text = "";
                        txtOldPass.Text = "";
                        txtUserName.Focus();
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Lỗi kết nối CSDL");
            }
            finally
            {
                ConnectData.strConnect.Close();
            }
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn muốn thoát", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                this.Close();
            }
        }

        private void chkPassword_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPassword.Checked == true)
            {
                txtOldPass.PasswordChar = (char)0;
                txtNewPass.PasswordChar = (char)0;
                txtConfirmPass.PasswordChar = (char)0;
                chkPassword.Text = "Hiện MK";
            }
            else
            {
                txtOldPass.PasswordChar = '*';
                txtNewPass.PasswordChar = '*';
                txtConfirmPass.PasswordChar = '*';
                chkPassword.Text = "Ẩn MK";
            }
        }

        private void lblShowInfor_Click(object sender, EventArgs e) { }

        private void FrmChangePass_Load(object sender, EventArgs e)
        {

        }
    }
}
