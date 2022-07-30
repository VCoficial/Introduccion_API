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
using Newtonsoft.Json; //Instalar Nugget "Newtonsoft.Json"

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
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create("http://localhost:5000/Persona/seleccionarpersona?id=" + TxtID.Text);
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            if (resp.StatusCode == HttpStatusCode.OK)
            {
                StreamReader lector = new StreamReader(resp.GetResponseStream());
                string json = lector.ReadToEnd();
                if (json.Length > 0)
                {
                    MessageBox.Show("JSON: " + json);
                }
                else
                {
                    MessageBox.Show("Persona NO Existe");
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create("http://localhost:5000/Persona");
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            if (resp.StatusCode == HttpStatusCode.OK)
            {
                StreamReader lector = new StreamReader(resp.GetResponseStream());
                string json = lector.ReadToEnd();
                if (json.Length > 0)
                {
                    DataTable dt = (DataTable)JsonConvert.DeserializeObject(json, (typeof(DataTable)));
                    DGVPersonas.DataSource = dt;
                }
                else
                {
                    MessageBox.Show("Personas NO Existen");
                }

            }
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            if (TxtNombre.Text.Length != 0 && TxtTel.Text.Length != 0)
            {
                string nom = TxtNombre.Text.Replace(" ", "%20");
                string url = "";
                if (TxtID.Enabled == false)
                {
                    url = "http://localhost:5000/Persona/actualizarpersona?id=" + Convert.ToInt64(TxtID.Text) + "&nom=" + nom + "&cel=" + Convert.ToInt64(TxtTel.Text);
                }
                else
                {
                    url = "http://localhost:5000/Persona/insertarpersona?name=" + nom + "&cel=" + Convert.ToInt64(TxtTel.Text);
                }
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
                if (resp.StatusCode == HttpStatusCode.OK)
                {
                    StreamReader lector = new StreamReader(resp.GetResponseStream());
                    string json = lector.ReadToEnd();
                    if (json.Length > 0)
                    {
                        if (json == "Agregado")
                        {
                            MessageBox.Show("Persona Agregada");
                            Form1_Load(sender, e);
                        }
                        if (json == "Editado")
                        {
                            MessageBox.Show("Persona Actualizada");
                            Form1_Load(sender, e);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Personas NO Existen");
                    }
                }
            }
            else
            {
                MessageBox.Show("Nombre o Telefono Vacio");
            }
        }

        private void BtnBorrar_Click(object sender, EventArgs e)
        {
            long id = Convert.ToInt64(DGVPersonas.CurrentRow.Cells["ID"].Value.ToString());
            DialogResult rpta = MessageBox.Show("Desea Elimina el ID= " + id.ToString(), "Advertencia!!!", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (rpta == DialogResult.OK)
            {
                string url = "http://localhost:5000/Persona/EliminarPersona?ID=" + id;
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();

                if (resp.StatusCode == HttpStatusCode.OK)
                {
                    StreamReader lector = new StreamReader(resp.GetResponseStream());
                    string json = lector.ReadToEnd();
                    if (json.Length > 0)
                    {
                        if (json == "Eliminado")
                        {
                            MessageBox.Show("Persona Eliminada");
                            Form1_Load(sender, e);
                        }
                    }
                }
            }
        }

        private void BtnNuevo_Click(object sender, EventArgs e)
        {
            BtnGuardar.Visible=true;
            BtnNuevo.Visible = false;
            BtnCancelar.Visible=true;
            BtnBuscar.Visible=false;
            BtnEditar.Visible = false;
            TxtNombre.Visible=true;
            TxtTel.Visible = true;
            label2.Visible = true;
            label3.Visible = true;
            label1.Visible = true;
            TxtID.Visible = false;
            label1.Visible = false;
        }

        private void BtnEditar_Click(object sender, EventArgs e)
        {
            BtnGuardar.Visible = true;
            BtnNuevo.Visible = false;
            BtnCancelar.Visible = true;
            BtnBuscar.Visible = false;
            BtnEditar.Visible = false;
            TxtNombre.Visible = true;
            TxtTel.Visible = true;
            TxtID.Visible = true;
            label2.Visible = true;
            label3.Visible = true;
            TxtID.Enabled = false;
            long id = Convert.ToInt64(DGVPersonas.CurrentRow.Cells["ID"].Value.ToString());
            string nom = DGVPersonas.CurrentRow.Cells["Nombre"].Value.ToString();
            long tel = Convert.ToInt64(DGVPersonas.CurrentRow.Cells["Celular"].Value.ToString());
            TxtID.Text = id.ToString();
            TxtNombre.Text = nom.ToString();
            TxtTel.Text = tel.ToString();
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            BtnEditar.Visible = true;
            BtnGuardar.Visible = false;
            BtnNuevo.Visible = true;
            BtnCancelar.Visible = false;
            TxtNombre.Visible = false;
            TxtTel.Visible = false;
            TxtID.Visible = true;
            label1.Visible = true;
            label2.Visible = false; 
            label3.Visible = false;
            TxtID.Clear();
            TxtTel.Clear();
            TxtNombre.Clear();
        }
    }
    }
