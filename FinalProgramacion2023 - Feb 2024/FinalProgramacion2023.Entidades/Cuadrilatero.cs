using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProgramacion2023.Entidades
{
    public class Cuadrilatero:ICloneable
    {
        public int LadoA { get; set; }
        public int LadoB { get; set; }
 

        private TipoDeBorde tipoDeBorde;

        public TipoDeBorde TipoDeBorde
        {
            get { return tipoDeBorde; }
            set { tipoDeBorde = value; }
        }

        private ColorRelleno colorRelleno;

        public ColorRelleno ColorRelleno
        {
            get { return colorRelleno; }
            set { colorRelleno = value; }
        }

        public Cuadrilatero()
        {
        }

        public Cuadrilatero(int ladoA, int ladoB, TipoDeBorde borde, ColorRelleno color)
        {
            LadoA = ladoA;
            LadoB = ladoB;
            TipoDeBorde = borde;
            ColorRelleno = color;
        }
        public double GetLadoA() => LadoA;
        public void SetLadoA(int medida1)
        {
            if (medida1 > 0)
            {
                LadoA = medida1;
            }
        }
        public double GetLadoB() => LadoB;
        public void SetLadoB(int medida2)
        {
            if (medida2 > 0)
            {
                LadoB = medida2;
            }
        }
        public double GetPerimetro() => (LadoA * 2) + (LadoB * 2);
        public double GetArea() => LadoA * LadoB;

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public object TipoCuadrilatero()
        {
            if (LadoA==LadoB)
            {
                return "Cuadrado";
            }
            if (LadoA != LadoB)
            {
                return "Rectangulo";
            }
            else
            {
                return "No Conforma Cuadrilatero";
            }
        }
    }
}
