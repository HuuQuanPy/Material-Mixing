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
    public partial class FrmRegister : Form
    {
        public FrmRegister()
        {
            InitializeComponent();
        }

        private void FrmRegister_Load(object sender, EventArgs e)
        {
            
        }
        private bool Check_Text()
        {
            if (txtNewUserName.Text == "")
            {
                lbl_Information.Text = "Vui lòng nhập tên tài khoản";
                lbl_Information.ForeColor = Color.Red;
                return false;
            }
            else if (txtNewPassword.Text == "")
            {
                lbl_Information.Text ="Vui lòng nhập mật khẩu";
                lbl_Information.ForeColor = Color.Red;
                return false;
            }
            else if (txtConfirmPass.Text == "")
            {
                lbl_Information.Text = "Vui lòng xác nhận mật khẩu";
                lbl_Information.ForeColor = Color.Red;
                return false;
            }
            else if(txtConfirmPass.Text != txtNewPassword.Text)
            {
                lbl_Information.Text = "Mật khẩu xác nhận không đúng";
                lbl_Information.ForeColor = Color.Red;
                txtConfirmPass.Text = "";
                txtNewPassword.Text = "";
                txtNewPassword.Focus();
                return false;
            }
            return true;
        }

        private void BtnRegister_Click(object sender, EventArgs e)
        {
            try
            {
                if (Check_Text())
                {
                    ConnectData.Create_Connect();
                    SqlCommand command = new SqlCommand("exec spSVDangky '" + txtNewUserName.Text + "','" + txtNewPassword.Text + "' ", ConnectData.strConnect);

                    int code = Convert.ToInt32(command.ExecuteScalar());
                    if (code == 1)
                    {
                        MessageBox.Show("Đăng ký tài khoản ( " + txtNewUserName.Text + " ) thành công");
                        lbl_Information.Text = "";
                        txtNewUserName.Text = "";
                        txtNewPassword.Text = "";
                        txtConfirmPass.Text = "";
                        txtNewUserName.Focus();
                    }
                }
            }
            catch 
            {
                MessageBox.Show("Tài khoản đã tồn tại!","",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                txtNewUserName.Text = "";
                txtNewUserName.Focus();
                lbl_Information.Text = "";
            }
            finally
            {
                ConnectData.strConnect.Close();
            }
        }

        private void txt_Changed(object sender, EventArgs e)
        {
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn muốn thoát", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                this.Close();
            }
        }

        private void lbl_Information_Click(object sender, EventArgs e) { }
       
    }
}
