using FinalProgramacion2023.Datos;
using FinalProgramacion2023.Entidades;
using System.Drawing;

namespace FinalProgramacion2023.Windows
{
    public partial class Form1 : Form
    {
        private RepositorioDeCuadrilateros repo;
        private List<Cuadrilatero> lista;
        int valorFiltro;
        bool filterOn = false;
        public Form1()
        {
            InitializeComponent();
            repo = new RepositorioDeCuadrilateros();
            ActualizarCantidadDeRegistros();
            txtCantidad.Text = repo.GetCantidad().ToString();
        }

        private void ActualizarCantidadDeRegistros()
        {
            if (valorFiltro > 0)
            {
                txtCantidad.Text = repo.GetCantidad(valorFiltro).ToString();
            }
            else
            {
                txtCantidad.Text = repo.GetCantidad().ToString();
            }
        }

        private void tsbSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void tsbNuevo_Click(object sender, EventArgs e)
        {
            frmCuadrilatero form = new frmCuadrilatero() { Text = "Ingresar Cuadrilatero" };
            DialogResult dr = form.ShowDialog(this);
            if (dr == DialogResult.Cancel) { return; }
            Cuadrilatero cuadrilatero = form.GetCuadrilatero();
            if (!repo.Existe(cuadrilatero))
            {
                repo.Agregar(cuadrilatero);
                ActualizarCantidadDeRegistros();
                DataGridViewRow l = ConstruirFila();
                SetearFila(l, cuadrilatero);
                AgregarFila(l);

                MessageBox.Show("Fila agregada", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Registro ya existente", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AgregarFila(DataGridViewRow l)
        {
            dgvDatos.Rows.Add(l);
        }

        private void SetearFila(DataGridViewRow l, Cuadrilatero cuadrilatero)
        {
            l.Cells[col1.Index].Value = cuadrilatero.GetLadoA();
            l.Cells[col2.Index].Value = cuadrilatero.GetLadoB();
            l.Cells[col3.Index].Value = cuadrilatero.ColorRelleno;
            l.Cells[col4.Index].Value = cuadrilatero.TipoDeBorde;
            l.Cells[col5.Index].Value = cuadrilatero.GetArea().ToString(".00");
            l.Cells[col6.Index].Value = cuadrilatero.GetPerimetro().ToString(".00");
            l.Cells[col7.Index].Value = cuadrilatero.TipoCuadrilatero();
            l.Tag = cuadrilatero;
        }

        private DataGridViewRow ConstruirFila()
        {
            var l = new DataGridViewRow();
            l.CreateCells(dgvDatos);
            return l;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (repo.GetCantidad() > 0)
            {
                RecargarGrilla();
            }
        }

        private void RecargarGrilla()
        {
            valorFiltro = 0;
            filterOn = false;
            tsbFiltrar.BackColor = SystemColors.Control;
            lista = repo.GetLista();
            MostrarDatosEnGrilla();
            ActualizarCantidadDeRegistros();
        }

        private void MostrarDatosEnGrilla()
        {
            dgvDatos.Rows.Clear();
            foreach (var rect in lista)
            {
                DataGridViewRow l = ConstruirFila();
                SetearFila(l, rect);
                AgregarFila(l);
            }
        }

        private void tsbBorrar_Click(object sender, EventArgs e)
        {

            if (dgvDatos.SelectedRows.Count == 0)
            {
                return;
            }
            DialogResult dr = MessageBox.Show("Quieres eliminar la fila seleccionada?", "Confirmar eliminación", MessageBoxButtons.YesNo,
               MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (dr == DialogResult.No) { return; }
            else
            {
                var l = dgvDatos.SelectedRows[0];
                QuitarFila(l);
                var cuadBorrar = (Cuadrilatero)l.Tag;
                repo.Borrar(cuadBorrar);
                ActualizarCantidadDeRegistros();
                MessageBox.Show("Fila eliminada", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void QuitarFila(DataGridViewRow l)
        {
            dgvDatos.Rows.Remove(l);
        }

        private void tsbEditar_Click(object sender, EventArgs e)
        {
            if (dgvDatos.SelectedRows.Count == 0)
            {
                return;
            }

            var FilaSeleccionada = dgvDatos.SelectedRows[0];
            Cuadrilatero cuadrilatero = (Cuadrilatero)FilaSeleccionada.Tag;
            Cuadrilatero cuadrilateroCopia = (Cuadrilatero)cuadrilatero.Clone();
            frmCuadrilatero frm = new frmCuadrilatero() { Text = "Editar cuadrilatero" };
            frm.SetCuadrilatero(cuadrilatero);
            DialogResult dr = frm.ShowDialog(this);
            if (dr == DialogResult.Cancel)
            {
                return;
            }
            cuadrilatero = frm.GetCuadrilatero();
            if (!repo.Existe(cuadrilatero))
            {
                repo.Editar(cuadrilateroCopia, cuadrilatero);
                SetearFila(FilaSeleccionada, cuadrilatero);
                MessageBox.Show("Fila editada", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                SetearFila(FilaSeleccionada, cuadrilateroCopia);
                MessageBox.Show("Registro existente", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsbFiltrar_Click(object sender, EventArgs e)
        {
            if (!filterOn)
            {
                var lado1Filtro = Microsoft.VisualBasic.Interaction.InputBox("Ingrese un valor para filtrar por Area",
            "Filtrar por Mayor",
            "0", 200, 200);
                if (!int.TryParse(lado1Filtro, out valorFiltro))
                {
                    return;
                }
                if (valorFiltro <= 0)
                {
                    return;
                }
                lista = repo.Filtrar(valorFiltro);
                tsbFiltrar.BackColor = Color.LightPink;
                filterOn = true;
                MostrarDatosEnGrilla();
                ActualizarCantidadDeRegistros();

            }
            else
            {
                MessageBox.Show("Filtro aplicado! \n Debe actualizar la grilla",
                    "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void tsbActualizar_Click(object sender, EventArgs e)
        {
            RecargarGrilla();
        }

        private void ascendenteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lista = repo.OrdenarAscL1();
            MostrarDatosEnGrilla();
            ActualizarCantidadDeRegistros();
        }

        private void descendenteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            lista = repo.OrdenarDescL1();
            MostrarDatosEnGrilla();
            ActualizarCantidadDeRegistros();
        }
    }
}
