using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SymbolFactoryDotNet;
using S7.Net;

namespace HethongTronCamTuDong
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {

        }

        Plc myPlc  = new Plc(CpuType.S71200, "192.168.1.55", 0, 1);

        InputDataPlc iData = new InputDataPlc();

        OutputPlc qData = new OutputPlc();

        //kiểm tra kết nối tới Plc

        bool connect = true;
        private void BtnConnect_Click(object sender, EventArgs e)
        {
            if (connect)
            {
                myPlc.Open();
                if (myPlc.IsConnected == true)
                {
                    MessageBox.Show("Connect succesfully !");
                    prbConnect.Value = 100;
                    BtnConnect.BackColor = Color.Yellow;
                    BtnConnect.Text = "Connect";
                    TmrPLC.Start();
                    TmrPLC.Interval = 100;
                    connect = false;
                }
                else
                {
                    MessageBox.Show("Connect erros");
                }
            }
            else
            {
                myPlc.Close();
                BtnConnect.BackColor = Color.WhiteSmoke;
                BtnConnect.Text = "Disconnect";
                prbConnect.Value = 0;
                TmrPLC.Stop();
                grbEndSystem.Visible = false;
                connect = true;
            }
        }

        private void TxtTextWeight_TextChanged(object sender, EventArgs e)
        {
            if (txtKg_Gao.Text == "" || txtKg_Ngo.Text == "" || txtKg_San.Text == "" || txtKg_DauKho.Text == "" || txtKg_BotCa.Text == "")
            {
                btnConfirm.Enabled = false;
            }
            else
                btnConfirm.Enabled = true;
        }

        //nhập công thức sản xuất

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if(Check_Connect())
            {
                iData.Set_Weight_Gao = double.Parse(txtKg_Gao.Text);
                iData.Set_Weight_Ngo = double.Parse(txtKg_Ngo.Text);
                iData.Set_Weight_San = double.Parse(txtKg_San.Text);
                iData.Set_Weight_DauKho = double.Parse(txtKg_DauKho.Text);
                iData.Set_Weight_BotCa = double.Parse(txtKg_BotCa.Text);
                myPlc.WriteClass(iData, 10);
                MessageBox.Show("Nhập dữ liệu thành công !", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                lblKg_XiloA1_Gao.Text = (Convert.ToDouble(txtKg_Gao.Text) / 3).ToString("0.000");
                lblKg_XiloA2_Gao.Text = (Convert.ToDouble(txtKg_Gao.Text) / 3).ToString("0.000");
                lblKg_XiloA3_Gao.Text = (Convert.ToDouble(txtKg_Gao.Text) / 3).ToString("0.000");
                lblKg_XiloA4_Ngo.Text = (Convert.ToDouble(txtKg_Ngo.Text) / 4).ToString("0.000");
                lblKg_XiloA5_Ngo.Text = (Convert.ToDouble(txtKg_Ngo.Text) / 4).ToString("0.000");
                lblKg_XiloA6_Ngo.Text = (Convert.ToDouble(txtKg_Ngo.Text) / 4).ToString("0.000");
                lblKg_XiloA7_Ngo.Text = (Convert.ToDouble(txtKg_Ngo.Text) / 4).ToString("0.000");
                lbl_Kg_XiloA8_San.Text = (Convert.ToDouble(txtKg_San.Text) / 2).ToString("0.000");
                lblKg_XiloA9_San.Text = (Convert.ToDouble(txtKg_San.Text) / 2).ToString("0.000");

                lblKg_XiloB1_DauKho.Text = (Convert.ToDouble(txtKg_DauKho.Text) / 3).ToString("0.000");
                lblKg_XiloB2_DauKho.Text = (Convert.ToDouble(txtKg_DauKho.Text) / 3).ToString("0.000");
                lblKg_XiloB3_DauKho.Text = (Convert.ToDouble(txtKg_DauKho.Text) / 3).ToString("0.000");
                lblKg_XiloB4_BotCa.Text = (Convert.ToDouble(txtKg_BotCa.Text) / 4).ToString("0.000");
                lblKg_XiloB5_BotCa.Text = (Convert.ToDouble(txtKg_BotCa.Text) / 4).ToString("0.000");
                lblKg_XiloB6_BotCa.Text = (Convert.ToDouble(txtKg_BotCa.Text) / 4).ToString("0.000");
                lblKg_XiloB7_BotCa.Text = (Convert.ToDouble(txtKg_BotCa.Text) / 4).ToString("0.000");

                TmrXilo.Start();
                TmrXilo.Interval = 1000;
            }
        }

        //nhập số mẻ cần sản xuất

        private void txtSoMeCanSx_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                if (Check_Connect())
                {
                    if (txtSoMeCanSx.Text == "")
                    {
                        MessageBox.Show("Chưa nhập số mẻ ! ");
                    }
                    else
                    {
                        iData.Set_SoMeTron = short.Parse(txtSoMeCanSx.Text);
                        myPlc.WriteClass(iData, 10);
                        MessageBox.Show("Số mẻ cần sản xuất : " + txtSoMeCanSx.Text, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        //nhập thời gian trộn và xả liệu

        private void TxtSetTime_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                if (Check_Connect())
                {
                    if (txtTimeMixed.Text == "")
                    {
                        MessageBox.Show("Chưa nhập thời gian trộn !");
                    }
                    else
                        if (txtTimeOut.Text == "")
                    {
                        MessageBox.Show("Chưa nhập thời gian xả !");
                    }
                    else
                    {
                        iData.Set_Time_Tron = int.Parse(txtTimeMixed.Text);
                        iData.Set_Time_Xa = int.Parse(txtTimeOut.Text);
                        myPlc.WriteClass(iData, 10);
                        MessageBox.Show("Nhập dữ liệu thành công !\n\n" + " Thời gian trộn (s) : " + txtTimeMixed.Text + "\n" + " Thời gian xả (s) : " + txtTimeOut.Text,
                                        "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        //khai báo phương thức kiểm tra kết nối tới Plc

        private bool Check_Connect()
        {
            if (myPlc.IsConnected == true)
            {
                return true;
            }
            else
            {
                MessageBox.Show("No connected !");
                return false;
            }
        }

        //khai báo phương thức điều khiển switch trên Plc

        private void SW_MANUandSensor(Button btn , string tag  )
        {
            if(Check_Connect())
            {
                if (btn.BackColor == Color.White)
                {
                    myPlc.Write(tag, 1);
                    btn.Text = "ON";
                    btn.BackColor = Color.LightGreen;
                }
                else
                {
                    myPlc.Write(tag, 0);
                    btn.Text = "OFF";
                    btn.BackColor = Color.White;
                }
            }
        }

        private void On_Off_System(Button btn , string tagPlc)
        {
            if (Check_Connect())
            {
                myPlc.Write(tagPlc, 1);
                myPlc.Write(tagPlc, 0);
            }
        }

        // bật , tắt chế độ auto/manu

        bool mAuto = true ;
        private void BtnAuto_Manu_Click(object sender, EventArgs e)
        {
            if(Check_Connect())
            {
                if (mAuto)
                {
                    myPlc.Write("DB1.DBX0.2", 1);
                    BtnAuto_Manu.Text = "AUTO";
                    grbSW_Manu.Visible = false;
                    
                    BtnSimulation.Enabled = true;
                    mAuto = false;
                }
                else
                {
                    myPlc.Write("DB1.DBX0.2", 0);
                    BtnAuto_Manu.Text = "MANU";
                    grbSW_Manu.Visible = true;
                    BtnSimulation.Enabled = false;
                    mAuto = true;
                }
            }
        }

        //bật tắt switch mô phỏng

        private void BtnSimulation_Click(object sender, EventArgs e)
        {
            SW_MANUandSensor(BtnSimulation, "DB1.DBX0.3");
            if (BtnSimulation.BackColor == Color.White)
            {
                grbSensorAuto.Visible =true;
            }
            else
                grbSensorAuto.Visible = false;
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            On_Off_System(BtnStart, "DB1.DBX0.0");
        }

        private void BtnStop_Click(object sender, EventArgs e)
        {
            On_Off_System(BtnStop, "DB1.DBX0.1");
            txtActSoMe.Text = "0";
        }

        //bật, tắt switch động cơ tải chế độ bằng tay

        private void btnSW_Tai_Click(object sender, EventArgs e)
        {
            SW_MANUandSensor(btnSW_Tai, "DB1.DBX3.1");
        }

        //bật, tắt switch van xả cân A chế độ bằng tay

        private void btnSW_ValveA_Click(object sender, EventArgs e)
        {
            SW_MANUandSensor(btnSW_ValveA, "DB1.DBX3.3");
        }

        //bật, tắt switch van xả cân B chế độ bằng tay

        private void btnSW_ValveB_Click(object sender, EventArgs e)
        {
            SW_MANUandSensor(btnSW_ValveB, "DB1.DBX3.4");
        }

        //bật, tắt switch động cơ trộn chế độ bằng tay

        private void btnSW_Mixed_Click(object sender, EventArgs e)
        {
            SW_MANUandSensor(btnSW_Mixed, "DB1.DBX3.2");
        }

        //bật, tắt switch van xả kết thúc chu trình chế độ bằng tay

        private void btnSW_ValveOut_Click(object sender, EventArgs e)
        {
            SW_MANUandSensor(btnSW_ValveOut, "DB1.DBX3.5");
        }

        //điều chỉnh tốc độ xả liệu 

        private void trbInverter_Scroll(object sender, EventArgs e)
        {
            if (Check_Connect())
            {
                myPlc.Write("MD148", (Convert.ToDouble(trbInverter.Value) / 100));
            }
            else
                trbInverter.Value = 0;
        }

        //khai báo phương thức đọc về giá trị cân

        private void ShowScales(TextBox txt, double value  , bool boolPlc)
        {
            if (boolPlc  )
            {
                txt.Text = value.ToString("0.000");
            }
            else
                txt.Text = "0";
        }

        private void ShowWeight_Xilo(Label lbl, double value, bool boolPlc)
        {
            if (boolPlc)
            {
                lbl.Text = value.ToString("0.000");
            }
            else
                lbl.Text = "0";
        }

        //khai báo phương thức đọc về giá trị thời gian thực

        private void ShowTime(TextBox txt, int value, bool boolPlc)
        {
            if (boolPlc)
            {
                txt.Text = (value).ToString();
            }
            else
                txt.Text = "0";
        }

        //khai báo phương thức đọc về tín hiệu ngõ ra Output

        private void ShowLamp_Symbol(StandardControl sd,bool boolPlc)
        {
            if (boolPlc)
            {
                sd.DiscreteValue1 = true;
            }
            else
                sd.DiscreteValue1 = false;
        }

        //khai báo phương thức đọc về tín hiệu các cảm biến

        private void Show_Sensor(StandardControl sd , string tagPlc, bool boolPlc)
        {
            if (boolPlc )
            {
                if (Convert.ToBoolean(myPlc.Read(tagPlc)) == true)
                {
                    sd.DiscreteValue1 = true;
                }
                else
                    sd.DiscreteValue1 = false;
            }
        }

        //khai báo phương thức hoàn thành chu trình

        private void EndSystem()
        {
            if (qData.EndSystem == true && qData.Lamp_Auto == true)
            {
                grbEndSystem.Visible = true;
                txtSoMeDaSx.Text = qData.Act_SoMeTron.ToString();
                txtKg_GaoDaSx.Text = iData.Set_Weight_Gao.ToString();
                txtKg_NgoDaSx.Text = iData.Set_Weight_Ngo.ToString();
                txtKg_SanDaSx.Text = iData.Set_Weight_San.ToString();
                txtKg_DauKhoDaSx.Text = iData.Set_Weight_DauKho.ToString();
                txtKg_BotCaDaSx.Text = iData.Set_Weight_BotCa.ToString();
            }
            else
                grbEndSystem.Visible = false;
        }

        // timer đọc tín hiệu từ Plc với chu kì đặt trước

        private void TmrPLC_Tick(object sender, EventArgs e)
        {
            myPlc.ReadClass(qData, 2);
            myPlc.ReadClass(iData, 10);

            ShowLamp_Symbol(sdLamp_Auto, qData.Lamp_Auto);
            ShowLamp_Symbol(sdLamp_Manu, qData.Lamp_Manu);
            
            ShowLamp_Symbol(sdMotor_Screw, qData.Motor_Tai);
            ShowLamp_Symbol(sdScrewConveyor, qData.Motor_Tai);
            ShowLamp_Symbol(sdValvesA, qData.ValveA);
            ShowLamp_Symbol(sdValvesB, qData.ValveB);
            ShowLamp_Symbol(sdMotor_Mix, qData.Motor_Mixed);
            ShowLamp_Symbol(sdValves_Xa, qData.Valve_Out);

            Show_Sensor(sdSensorScaleA, "DB1.DBX2.4", qData.Lamp_Auto);
            Show_Sensor(sdSensorScaleB, "DB1.DBX2.5", qData.Lamp_Auto);
            Show_Sensor(sdSensorLow, "DB1.DBX2.6", qData.Lamp_Auto);
            Show_Sensor(sdSensorHigh, "DB1.DBX2.7", qData.Lamp_Auto);

            Show_Sensor(sdLamp_True, "M15.1", true);

            ShowTime(txtActSoMe, qData.Act_SoMeTron, qData.Lamp_Auto);
            ShowScales(txtWeigh_ScalesA, iData.Load_ScaleA, qData.Lamp_Auto);
            ShowScales(txtWeigh_ScalesB, iData.Load_ScaleB, qData.Lamp_Auto);
            ShowTime(txtActTimeMix, qData.Act_Time_Mixed, qData.Lamp_Auto);
            ShowTime(txtActTimeOut, qData.Act_Time_Out, qData.Lamp_Auto);

            ShowWeight_Xilo(lblAct_XiloA1, qData.Act_Weight_XiloA1, qData.Lamp_Auto);
            ShowWeight_Xilo(lblAct_XiloA2, qData.Act_Weight_XiloA2, qData.Lamp_Auto);
            ShowWeight_Xilo(lblAct_XiloA3, qData.Act_Weight_XiloA3, qData.Lamp_Auto);
            ShowWeight_Xilo(lblAct_XiloA4, qData.Act_Weight_XiloA4, qData.Lamp_Auto);
            ShowWeight_Xilo(lblAct_XiloA5, qData.Act_Weight_XiloA5, qData.Lamp_Auto);
            ShowWeight_Xilo(lblAct_XiloA6, qData.Act_Weight_XiloA6, qData.Lamp_Auto);
            ShowWeight_Xilo(lblAct_XiloA7, qData.Act_Weight_XiloA7, qData.Lamp_Auto);
            ShowWeight_Xilo(lblAct_XiloA8, qData.Act_Weight_XiloA8, qData.Lamp_Auto);
            ShowWeight_Xilo(lblAct_XiloA9, qData.Act_Weight_XiloA9, qData.Lamp_Auto);

            ShowWeight_Xilo(lblAct_XiloB1, qData.Act_Weight_XiloB1, qData.Lamp_Auto);
            ShowWeight_Xilo(lblAct_XiloB2, qData.Act_Weight_XiloB2, qData.Lamp_Auto);
            ShowWeight_Xilo(lblAct_XiloB3, qData.Act_Weight_XiloB3, qData.Lamp_Auto);
            ShowWeight_Xilo(lblAct_XiloB4, qData.Act_Weight_XiloB4, qData.Lamp_Auto);
            ShowWeight_Xilo(lblAct_XiloB5, qData.Act_Weight_XiloB5, qData.Lamp_Auto);
            ShowWeight_Xilo(lblAct_XiloB6, qData.Act_Weight_XiloB6, qData.Lamp_Auto);
            ShowWeight_Xilo(lblAct_XiloB7, qData.Act_Weight_XiloB7, qData.Lamp_Auto);

            Show_Sensor(A1, "DB1.DBX0.4", qData.Lamp_Auto);
            Show_Sensor(A2, "DB1.DBX0.5", qData.Lamp_Auto);
            Show_Sensor(A3, "DB1.DBX0.6", qData.Lamp_Auto);
            Show_Sensor(A4, "DB1.DBX0.7", qData.Lamp_Auto);
            Show_Sensor(A5, "DB1.DBX1.0", qData.Lamp_Auto);
            Show_Sensor(A6, "DB1.DBX1.1", qData.Lamp_Auto);
            Show_Sensor(A7, "DB1.DBX1.2", qData.Lamp_Auto);
            Show_Sensor(A8, "DB1.DBX1.3", qData.Lamp_Auto);
            Show_Sensor(A9, "DB1.DBX1.4", qData.Lamp_Auto);

            Show_Sensor(B1, "DB1.DBX1.5", qData.Lamp_Auto);
            Show_Sensor(B2, "DB1.DBX1.6", qData.Lamp_Auto);
            Show_Sensor(B3, "DB1.DBX1.7", qData.Lamp_Auto);
            Show_Sensor(B4, "DB1.DBX2.0", qData.Lamp_Auto);
            Show_Sensor(B5, "DB1.DBX2.1", qData.Lamp_Auto);
            Show_Sensor(B6, "DB1.DBX2.2", qData.Lamp_Auto);
            Show_Sensor(B7, "DB1.DBX2.3", qData.Lamp_Auto);

            

            EndSystem();
        }

        private void TxtInputData_KeyPress(object sender, KeyPressEventArgs e) { }

        //điều khiển giá trị cân A và B

        private void trbScaleA_Scroll(object sender, EventArgs e)
        {
            if(Check_Connect())
            {
                double weightA = double.Parse(txtKg_Gao.Text) + double.Parse(txtKg_Ngo.Text) + double.Parse(txtKg_San.Text);
                iData.Load_ScaleA = weightA * Convert.ToDouble(trbScaleA.Value) / 30;
                myPlc.WriteClass(iData, 10);
            }
        }

        private void trbScaleB_Scroll(object sender, EventArgs e)
        {
            if(Check_Connect())
            {
                double weightB = double.Parse(txtKg_DauKho.Text) + double.Parse(txtKg_BotCa.Text);
                iData.Load_ScaleB = weightB * Convert.ToDouble(trbScaleB.Value) / 30;
                myPlc.WriteClass(iData, 10);
            }
        }

        // bật, tắt switch van xả cân A

        private void btnSensorValveA_Click(object sender, EventArgs e)
        {
            SW_MANUandSensor(btnSensorValveA, "DB1.DBX2.4");
        }

        //bật, tắt switch van xả cân B

        private void btnSensorValveB_Click(object sender, EventArgs e)
        {
            SW_MANUandSensor(btnSensorValveB, "DB1.DBX2.5");
        }

        //bật, tắt switch cảm biến thấp nồi trộn

        private void btnSsLow_Click(object sender, EventArgs e)
        {
            SW_MANUandSensor(btnSsLow, "DB1.DBX2.6");
        }

        //bật, tắt switch cảm biến cao nồi trộn

        private void btnSsHigh_Click(object sender, EventArgs e)
        {
            SW_MANUandSensor(btnSsHigh, "DB1.DBX2.7");
        }

        private void ShowAcceptXilo(Label lbl , string tagPlc )
        {
            if(qData.Lamp_Auto == true)
            {
                if (Convert.ToBoolean(myPlc.Read(tagPlc)) == true)
                {
                    lbl.BackColor = Color.LightGreen;
                }
                else
                    lbl.BackColor = Color.Silver;
            }
        }

        private void TmrXilo_Tick(object sender, EventArgs e)
        {
            ShowAcceptXilo(xiloA1, "DB11.DBX0.0");
            ShowAcceptXilo(xiloA2, "DB11.DBX0.0");
            ShowAcceptXilo(xiloA3, "DB11.DBX0.0");
            ShowAcceptXilo(xiloA4, "DB11.DBX0.1");
            ShowAcceptXilo(xiloA5, "DB11.DBX0.1");
            ShowAcceptXilo(xiloA6, "DB11.DBX0.1");
            ShowAcceptXilo(xiloA7, "DB11.DBX0.1");
            ShowAcceptXilo(xiloA8, "DB11.DBX0.2");
            ShowAcceptXilo(xiloA9, "DB11.DBX0.2");

            ShowAcceptXilo(xiloB1, "DB11.DBX0.3");
            ShowAcceptXilo(xiloB2, "DB11.DBX0.3");
            ShowAcceptXilo(xiloB3, "DB11.DBX0.3");
            ShowAcceptXilo(xiloB4, "DB11.DBX0.4");
            ShowAcceptXilo(xiloB5, "DB11.DBX0.4");
            ShowAcceptXilo(xiloB6, "DB11.DBX0.4");
            ShowAcceptXilo(xiloB7, "DB11.DBX0.4");
        }

        private void btnLoadXilo_Click(object sender, EventArgs e)
        {
            if(Check_Connect())
            {
                if (btnLoadXilo.BackColor == Color.White)
                {
                    myPlc.Write("M15.0", 1);
                    btnLoadXilo.BackColor = Color.Yellow;
                }
                else
                {
                    myPlc.Write("M15.0", 0);
                    btnLoadXilo.BackColor = Color.White;
                }
            }
        }

        private void xiloA3_Click(object sender, EventArgs e)
        {

        }
    }
}
