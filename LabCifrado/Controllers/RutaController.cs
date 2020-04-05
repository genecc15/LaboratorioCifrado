using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LabCifrado.Cifrados;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace LabCifrado.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RutaController : ControllerBase
    {
        public static IWebHostEnvironment _environment;
        private readonly RutaMetodos RutaCifrado = new RutaMetodos();
        private readonly RutaMetodos RutaDesc = new RutaMetodos();

        public RutaController(IWebHostEnvironment env)
        {
            _environment = env;
        }

        public class FileUploadApi
        {
            public IFormFile Files { get; set; }
            public string Filas { get; set; }
            public string Nombre { get; set; }
        }

        [Route("/Cipher/Espiral")]
        [HttpPost]
        public async Task<string> UploadFileText([FromForm] FileUploadApi objFile, [FromForm] FileUploadApi fil, [FromForm]FileUploadApi nombreobj)
        {

            try
            {
                if (objFile.Files.Length > 0)
                {
                    int i;

                    string nombre = nombreobj.Nombre;
                    int num = Convert.ToInt32(fil.Filas);
                    bool resultado = false;
                    if (int.TryParse(fil.Filas, out i))
                    {
                        if (i < 10000 && i > 0)
                        {
                            if (!Directory.Exists(_environment.WebRootPath + "\\UploadEspiral\\")) Directory.CreateDirectory(_environment.WebRootPath + "\\UploadEspiral\\");
                            using var _fileStream = System.IO.File.Create(_environment.WebRootPath + "\\UploadEspiral\\" + objFile.Files.FileName);
                            objFile.Files.CopyTo(_fileStream);
                            _fileStream.Flush();
                            _fileStream.Close();
                            resultado = true;
                            EspiralCifrado2(objFile, num, nombre);
                        }
                        else
                        {
                            return "La contraseña está fuera del rango";
                        }
                    }
                    else
                    {
                        return "La contraseña debe consistir de números";
                    }
                    return "Archivo subido y cifrado correctamente";
                }
                else return "Archivo Vacio";

            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }

        }

        public void EspiralCifrado2(FileUploadApi objFile, int contra, string nombre)
        {
            string archivo = nombre;
            int clave = contra;
            string[] FileName1 = objFile.Files.FileName.Split(".");
            RutaMetodos.EspiralAlgortimo(_environment.WebRootPath + "\\UploadEspiral\\" + objFile.Files.FileName, _environment.WebRootPath + "\\UploadEspiral\\" + archivo + ".txt", clave);

        }


        [Route("/Decipher/Espiral")]
        [HttpPost]
        public async Task<string> UploadFileZigZag([FromForm] FileUploadApi objFile, [FromForm] FileUploadApi key, [FromForm]FileUploadApi nombreobj)
        {
            try
            {
                if (objFile.Files.Length > 0)
                {
                    int i;

                    string nombre = nombreobj.Nombre;
                    //string clave = key.Niveles;
                    int num = Convert.ToInt32(key.Filas);
                    bool resultado = false;
                    if (int.TryParse(key.Filas, out i))
                    {
                        if (i < 10000 && i > 0)
                        {
                            if (!Directory.Exists(_environment.WebRootPath + "\\UploadEspiral\\")) Directory.CreateDirectory(_environment.WebRootPath + "\\UploadEspiral\\");
                            using var _fileStream = System.IO.File.Create(_environment.WebRootPath + "\\UploadEspiral\\" + objFile.Files.FileName);
                            objFile.Files.CopyTo(_fileStream);
                            _fileStream.Flush();
                            _fileStream.Close();
                            resultado = true;
                            EspiralDescifrado(objFile, num, nombre);
                        }
                        else
                        {
                            return "La contraseña está fuera del rango";
                        }
                    }
                    else
                    {
                        return "La contraseña debe consistir de números";
                    }
                    return "Archivo Descfrado correctamente";
                }
                else return "Archivo Vacio";

            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }

        public void EspiralDescifrado(FileUploadApi objFile, int contra, string nombre)
        {
            string archivo = nombre;
            int clave = contra;
            string[] FileName1 = objFile.Files.FileName.Split(".");
            RutaMetodos.EspiralAlgortimo2(_environment.WebRootPath + "\\UploadEspiral\\" + objFile.Files.FileName, _environment.WebRootPath + "\\UploadEspiral\\" + archivo + ".txt", clave);
        }
    }
}