using FinalProgramacion2023.Entidades;
using System.Drawing;

namespace FinalProgramacion2023.Datos
{
    public class RepositorioDeCuadrilateros
    {
        private readonly string _archivo = Environment.CurrentDirectory + "//Cuadrilatero.txt";
        private readonly string _archivoCopia = Environment.CurrentDirectory + "//Cuadrilatero.bak";

        private List<Cuadrilatero> listaCuadrilatero;
        public RepositorioDeCuadrilateros()
        {
            listaCuadrilatero = new List<Cuadrilatero>();
            LeerDatos();
        }

        private void LeerDatos()
        {
            listaCuadrilatero.Clear();
            if (File.Exists(_archivo))
            {
                var lector = new StreamReader(_archivo);
                while (!lector.EndOfStream)
                {
                    string lineaLeida = lector.ReadLine();
                    Cuadrilatero cuadrilatero = ConstruirCuadrilatero(lineaLeida);
                    listaCuadrilatero.Add(cuadrilatero);
                }
                lector.Close();
            }
        }

        private Cuadrilatero ConstruirCuadrilatero(string? lineaLeida)
        {
            var campos = lineaLeida.Split('|');
            int ladoA = int.Parse(campos[0]);
            int ladoB = int.Parse(campos[1]);
            TipoDeBorde borde = (TipoDeBorde)int.Parse(campos[2]);
            ColorRelleno color = (ColorRelleno)int.Parse(campos[3]);
            Cuadrilatero r = new Cuadrilatero(ladoA, ladoB, borde, color);

            return r;
        }
        public void Editar(Cuadrilatero cuadrilateroViejo, Cuadrilatero cuadrilateroEditar)
        {
            using (var lector = new StreamReader(_archivo))
            {
                using (var escritor = new StreamWriter(_archivoCopia))
                {
                    while (!lector.EndOfStream)
                    {
                        string lineaLeida = lector.ReadLine();
                        Cuadrilatero cuadrilatero = ConstruirCuadrilatero(lineaLeida);
                        if (cuadrilatero.GetLadoA() == cuadrilateroViejo.GetLadoA() && cuadrilatero.GetLadoB() == cuadrilateroViejo.GetLadoB())
                        {
                            lineaLeida = ConstruirLinea(cuadrilateroEditar);
                            escritor.WriteLine(lineaLeida);

                        }
                        else
                        {
                            escritor.WriteLine(lineaLeida);

                        }
                    }
                }
            }
            File.Delete(_archivo);
            File.Move(_archivoCopia, _archivo);
        }

        private string? ConstruirLinea(Cuadrilatero cuadrilatero)
        {
            return $"{cuadrilatero.GetLadoA()}|" +
               $"{cuadrilatero.GetLadoB()}|" +
               $"{cuadrilatero.TipoDeBorde.GetHashCode()}|" +
               $"{cuadrilatero.TipoDeBorde.GetHashCode()}";
        }
        public void Agregar(Cuadrilatero cuadrilatero)
        {
            using (var escritor = new StreamWriter(_archivo, true))
            {
                string lineaEscribir = ConstruirLinea(cuadrilatero);
                escritor.WriteLine(lineaEscribir);
            }
            listaCuadrilatero.Add(cuadrilatero);
        }
        public int GetCantidad(int? valorFiltro = 0)
        {
            if (valorFiltro > 0)
            {
                return listaCuadrilatero.Count(c => c.LadoA > valorFiltro);
            }
            return listaCuadrilatero.Count();
        }
        public void Borrar(Cuadrilatero cuadrilateroBorrar)
        {
            using (var lector = new StreamReader(_archivo))
            {
                using (var escritor = new StreamWriter(_archivoCopia))
                {
                    while (!lector.EndOfStream)
                    {
                        string lineaLeida = lector.ReadLine();
                        Cuadrilatero cuadrilateroLeido = ConstruirCuadrilatero(lineaLeida);
                        if (cuadrilateroBorrar.GetLadoA() != cuadrilateroLeido.GetLadoA())
                        {
                            escritor.WriteLine(lineaLeida);
                        }
                    }
                }
            }
            File.Delete(_archivo);
            File.Move(_archivoCopia, _archivo);
            listaCuadrilatero.Remove(cuadrilateroBorrar);
        }
        public List<Cuadrilatero> GetLista()
        {
            LeerDatos();
            return listaCuadrilatero;
        }
        public List<Cuadrilatero> Filtrar(int valorFiltro)
        {
            return listaCuadrilatero.Where(l => l.GetLadoA() >= valorFiltro).ToList();
        }
        public List<Cuadrilatero> OrdenarAscL1()
        {
            return listaCuadrilatero.OrderBy(l => l.GetPerimetro()).ToList();
        }

        public List<Cuadrilatero> OrdenarDescL1()
        {
            return listaCuadrilatero.OrderByDescending(l => l.GetPerimetro()).ToList();
        }
        public bool Existe(Cuadrilatero cuadrilatero)
        {
            listaCuadrilatero.Clear();
            LeerDatos();
            bool existe = false;
            foreach (var itemCuadrilatero in listaCuadrilatero)
            {
                if (itemCuadrilatero.GetLadoA() == cuadrilatero.GetLadoA() && itemCuadrilatero.GetLadoB() == cuadrilatero.GetLadoB())
                {
                    return true;
                }
            }
            return false;
        }
    }


}
