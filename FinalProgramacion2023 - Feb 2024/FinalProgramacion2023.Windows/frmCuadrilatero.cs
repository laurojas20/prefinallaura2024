using FinalProgramacion2023.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinalProgramacion2023.Windows
{
    public partial class frmCuadrilatero : Form
    {
        public frmCuadrilatero()
        {
            InitializeComponent();
        }
        private Cuadrilatero cuadrilatero;
        public Cuadrilatero GetCuadrilatero()
        { return cuadrilatero; }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void frmCuadrilatero_Load(object sender, EventArgs e)
        {
            CargarDatosComboColorRelleno();
            if (cuadrilatero != null)
            {
                txtLadoA.Text = cuadrilatero.LadoA.ToString();
                txtLadoB.Text = cuadrilatero.LadoB.ToString();

                cboRelleno.SelectedItem = cuadrilatero.ColorRelleno;
                if (cuadrilatero.TipoDeBorde == TipoDeBorde.Lineal)
                {
                    rbtLineal.Checked = true;
                }
                else if (cuadrilatero.TipoDeBorde == TipoDeBorde.Rayas)
                {
                    rbtRayas.Checked = true;
                }
                else
                {
                    rbtPuntos.Checked = true;
                }
            }
        }

        private void CargarDatosComboColorRelleno()
        {
            var listaColores = Enum.GetValues(typeof(ColorRelleno)).Cast<ColorRelleno>().ToList();
            cboRelleno.DataSource = listaColores;
            cboRelleno.SelectedIndex = 0;
        }
        public void SetCuadrilatero(Cuadrilatero cuadrilatero)
        {
            this.cuadrilatero = cuadrilatero;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (ValidarDatos())
            {
                if (cuadrilatero == null)
                {
                    cuadrilatero = new Cuadrilatero();
                }

                cuadrilatero.SetLadoA(int.Parse(txtLadoA.Text));
                cuadrilatero.SetLadoB(int.Parse(txtLadoB.Text));
                cuadrilatero.ColorRelleno = (ColorRelleno)cboRelleno.SelectedItem;

                if (rbtLineal.Checked)
                {
                    cuadrilatero.TipoDeBorde = TipoDeBorde.Lineal;
                }
                else if (rbtRayas.Checked)
                {
                    cuadrilatero.TipoDeBorde = TipoDeBorde.Rayas;
                }
                else
                {
                    cuadrilatero.TipoDeBorde = TipoDeBorde.Puntos;
                }
                DialogResult = DialogResult.OK;
            }
        }

        private bool ValidarDatos()
        {
            bool valido = true;
            LadoAerrorProvider1.Clear();
            LadoBerrorProvider2.Clear();

            if (!int.TryParse(txtLadoA.Text, out int ladoA) || ladoA <= 0)
            {
                valido = false;
                LadoAerrorProvider1.SetError(txtLadoA, "Número no válido");
            }

            if (!int.TryParse(txtLadoB.Text, out int ladoB) || ladoB <= 0)
            {
                valido = false;
                LadoBerrorProvider2.SetError(txtLadoB, "Número no válido");
            }
            return valido;
        }
    }
}
