using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Media;


namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        private decimal max = 0;
        private decimal min = 100;
        public Form1()
        {
            InitializeComponent();
        }

        public class doviz
        {
            private string full_name { get; set; }
            private string name { get; set; }
            private string code { get; set; }
            private double buying { get; set; }
            private double selling { get; set; }
            private double change_rate { get; set; }
            private decimal update_date { get; set; }
            private int currency { get; set; }
        }

        private List<double> satışdeğerleri = new List<double>();
        private void buttonAKTAR_Click(object sender, EventArgs e)
        {          
                string url = textBoxLINK.Text;
                var json = new WebClient().DownloadString(url);
                textBoxDATA.Text = json;
                JObject o = JObject.Parse(json);
                listBox1.Items.Insert(0, "\n");
                listBox1.Items.Insert(0, "Satış :" + o["selling"]);
                listBox1.Items.Insert(0, DateTime.Now.ToLongTimeString());                                
                satışdeğerleri.Add(Convert.ToDouble(o["selling"]));
                

                SoundPlayer ses = new SoundPlayer();
                SoundPlayer ses2 = new SoundPlayer();
                string path = Application.StartupPath.ToString() + "\\song1.wav";
                string path2 = Application.StartupPath.ToString() + "\\song2.wav";
                ses.SoundLocation = path;
                ses2.SoundLocation = path2;

                if (Convert.ToDecimal(o["selling"]) > max)
                {
                    max = Convert.ToDecimal(o["selling"]);
                    if (satışdeğerleri.Count != 1)
                    {
                        ses.Play();                    
                    }
                }

                if (Convert.ToDecimal(o["selling"]) < min)
                {
                    min = Convert.ToDecimal(o["selling"]);
                    if (satışdeğerleri.Count != 1)
                    {
                        ses2.Play();
                    }
                    
                }

                /*if (satışdeğerleri.Count > 3)
                {
                    for (int i = 0; i < (satışdeğerleri.Count)-1; i++)
                    {
                        for (int j = i+1; j < satışdeğerleri.Count; j++)
                        {
                            if (satışdeğerleri[i] < satışdeğerleri[j])
                            {
                                double temp;
                                temp = satışdeğerleri[i];
                                satışdeğerleri[i] = satışdeğerleri[j];
                                satışdeğerleri[j] = temp;
                            }
                        }                        
                    }
                }*/

                label4.Text = "Maximum değer :" + max.ToString();
                label5.Text = "Minimum değer :" + min.ToString();
        }
    }
}

