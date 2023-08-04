using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HethongTronCamTuDong
{
    class OutputPlc
    {
        public bool Lamp_Auto { get; set; }
        public bool Lamp_Manu { get; set; }
        public bool Lamp_Fault { get; set; }
        public bool Motor_Tai { get; set; }
        public bool ValveA { get; set; }
        public bool ValveB { get; set; }
        public bool Valve_Out { get; set; }
        public bool Motor_Mixed { get; set; }
        public bool EndSystem { get; set; }
        public short Act_SoMeTron { get; set; }
        public double Act_Weight_XiloA1 { get; set; }
        public double Act_Weight_XiloA2 { get; set; }
        public double Act_Weight_XiloA3 { get; set; }
        public double Act_Weight_XiloA4 { get; set; }
        public double Act_Weight_XiloA5 { get; set; }
        public double Act_Weight_XiloA6 { get; set; }
        public double Act_Weight_XiloA7 { get; set; }
        public double Act_Weight_XiloA8 { get; set; }
        public double Act_Weight_XiloA9 { get; set; }
        public double Act_Weight_XiloB1 { get; set; }
        public double Act_Weight_XiloB2 { get; set; }
        public double Act_Weight_XiloB3 { get; set; }
        public double Act_Weight_XiloB4 { get; set; }
        public double Act_Weight_XiloB5 { get; set; }
        public double Act_Weight_XiloB6 { get; set; }
        public double Act_Weight_XiloB7 { get; set; }
        public int Act_Time_Out { get; set; }
        public int Act_Time_Mixed { get; set; }

    }
}
