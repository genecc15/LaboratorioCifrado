using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using System.IO;

namespace LabCifrado
{
    public class ZigZagMetodos
    {
        public static string CurrentFile = "";
        public static void ZigZagAlgortimo(string Rpath, string Wpath, int llave)
        {
            Cifrado(Rpath, Wpath, llave);
        }
        public static void Cifrado(string Rpath, string Wpath, int Corrimiento)
        {
            string Data = System.IO.File.ReadAllText(Rpath, Encoding.Default);
            string mensaje = Data;
            var lineas = new List<StringBuilder>();
            int niveles = Corrimiento;
            for (int i = 0; i < niveles; i++)
            {
                lineas.Add(new StringBuilder());
            }
            int ActualL = 0;
            int Direccion = 1;
            //For para saber donde empezamos

            for (int i = 0; i < mensaje.Length; i++)
            {
                lineas[ActualL].Append(mensaje[i]);

                if (ActualL == 0)
                    Direccion = 1;
                else if (ActualL == niveles - 1)
                    Direccion = -1;

                ActualL += Direccion;
            }
            StringBuilder CifradoFinal = new StringBuilder();

            //Saber donde se encuentra cada caracter
            for (int i = 0; i < niveles; i++)
                CifradoFinal.Append(lineas[i].ToString());

            string Cifrados = CifradoFinal.ToString();

            File.WriteAllText(Wpath, Cifrados);
            CurrentFile = Wpath;
        }
    }
}
