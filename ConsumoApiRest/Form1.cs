using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Http;
using System.IO;

namespace ConsumoApiRest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            //El otro archivo debe estar activo
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create("http://localhost:5228/persona/seleccionarpersona?id="+TxtID.Text);
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();

            if (resp.StatusCode == HttpStatusCode.OK)
            {
                StreamReader lector = new StreamReader(resp.GetResponseStream());
                string json = lector.ReadToEnd();
                MessageBox.Show("JSON : "+ json);
            }
            else
            {
                MessageBox.Show("Persona NO existe");
            }
        }
    }
}
