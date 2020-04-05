using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LabCifrado.Cifrados
{
    public class RutaMetodos
    {
        private const char EOF = '\u0003';
        private const int bufferLength = 1048576;
        public static string CurrentFile = "";
        public static void ZigZagAlgortimo(string Rpath, string Wpath, int llave)
        {
            Cifrar(Rpath, Wpath, llave);
        }

        public static void ZigZagAlgortimo2(string Rpath, string Wpath, int llave)
        {
            //Descifrar(Rpath, Wpath, llave);
        }
        public static void Cifrar(string Rpath, string WPath, int password)
        {

            using (var file = new FileStream(Rpath, FileMode.Open))
            {
                using (var reader = new BinaryReader(file))
                {
                    //Buffer para cifrar
                    while (reader.BaseStream.Position != reader.BaseStream.Length)
                    {
                        var buffer = reader.ReadBytes(count: bufferLength);

                        #region Variables

                        int bufferPosition = 0;

                        int fileLength = bufferLength;

                        int m = password;
                        int n = (int)fileLength / m;
                        if (fileLength % m != 0) { n++; }//Redondear al siguinete entero

                        //Corregir error
                        while (fileLength < (n * m))
                        {
                            fileLength++;
                        }
                        char[,] matriz = new char[n, m];
                        List<byte> respuesta = new List<byte>();

                        int inicio = 0;
                        int limitefila = n;
                        int limitecolumna = m;

                        int valores = 1; //valores dentro de la matriz

                        int i = 0, j = 0;

                        FileStream fs = new FileStream(WPath, FileMode.Append);
                        BinaryWriter bw = new BinaryWriter(fs);

                        #endregion

                        #region Llenado
                        //llenado original de la matriz
                        for (int k = 0; k < m; k++)
                        {
                            for (int l = 0; l < n; l++)
                            {
                                if (bufferPosition < buffer.Length)
                                {
                                    if (buffer[bufferPosition] != default(byte))
                                    {
                                        matriz[l, k] = (char)buffer[bufferPosition];
                                        bufferPosition++;
                                    }
                                    else
                                    {
                                        matriz[l, k] = EOF;
                                    }
                                }
                            }
                        }
                        #endregion

                        //Recorrido Espiral
                        #region Recorrido

                        while (valores <= matriz.Length)
                        {
                            for (j = inicio; j < limitecolumna; j++)
                            {
                                respuesta.Add((byte)matriz[i, j]);
                                valores++;
                            }
                            for (i = inicio + 1; i < limitefila; i++)
                            {
                                respuesta.Add((byte)matriz[i, j - 1]);
                                valores++;
                            }
                            for (j = limitecolumna - 1; j > inicio && i > inicio + 1; j--)
                            {
                                respuesta.Add((byte)matriz[i - 1, j - 1]);
                                valores++;
                            }
                            for (i = limitefila - 1; i > inicio + 1; i--)
                            {
                                respuesta.Add((byte)matriz[i - 1, j]);
                                valores++;
                            }

                            inicio++;
                            limitecolumna--;
                            limitefila--;
                        }
                        #endregion

                        bw.Write(respuesta.ToArray());
                        bw.Close();
                    }
                }
            }

            CurrentFile = WPath;
        }

    }
}
