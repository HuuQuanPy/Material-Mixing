using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace HethongTronCamTuDong
{
    public class ConnectData
    {
        public static SqlConnection strConnect = new SqlConnection();
        public static void Create_Connect()
        {
            strConnect.ConnectionString = @"Data Source=DESKTOP-Q3REFE2\SQLEXPRESS;Initial Catalog=TkDangnhap;Integrated Security=True";
            strConnect.Open();
        }
    }
}
