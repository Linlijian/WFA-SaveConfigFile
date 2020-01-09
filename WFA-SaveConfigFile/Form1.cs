using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace WFA_SaveConfigFile
{
    public partial class Form1 : Form
    {
        //Now, declare a member for your Form with this object:
        MyFormState state = new MyFormState();

        public Form1()
        {
            InitializeComponent();
        }

        //On form load, check if the config exists, then load it:
        private void Form1_Load(object sender, EventArgs e)
        {
            if (File.Exists("config.xml"))
            {
                loadConfig();
            }

            button1.BackColor = System.Drawing.ColorTranslator.FromHtml(state.ButtonBackColor);
            label1.Text = state.lbl;
        }

        private void loadConfig()
        {
            XmlSerializer ser = new XmlSerializer(typeof(MyFormState));
            using (FileStream fs = File.OpenRead("config.xml"))
            {
                state = (MyFormState)ser.Deserialize(fs);
            }
        }

        //When your form is closing.. save the config:
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            writeConfig();
        }

        private void writeConfig()
        {
            using (StreamWriter sw = new StreamWriter("config.xml"))
            {
                state.ButtonBackColor = System.Drawing.ColorTranslator.ToHtml(button1.BackColor);
                state.lbl = "State Complete!";
                XmlSerializer ser = new XmlSerializer(typeof(MyFormState));
                ser.Serialize(sw, state);
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {

        }
    }
}
