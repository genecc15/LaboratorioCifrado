using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LabCifrado.Cifrados;

namespace LabCifrado.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ZigZagController : ControllerBase
    {
        public static IWebHostEnvironment _environment;
        private readonly ZigZagMetodos ZigZagCifrado = new ZigZagMetodos();
        private readonly ZigZagMetodos ZigZagDesc = new ZigZagMetodos();

        public ZigZagController (IWebHostEnvironment env)
        {
            _environment = env;
        }

        public class FileUploadApi
        {
            public IFormFile Files { get; set; }
            public string Niveles { get; set; }
            public string Nombre { get; set; }
        }

        [Route("/Cipher/ZigZag")]
        [HttpPost]
        public async Task<string> UploadFileText([FromForm] FileUploadApi objFile, [FromForm] FileUploadApi key, [FromForm]FileUploadApi nombreobj)
        {
            try
            {
                if (objFile.Files.Length > 0)
                {
                    int i;
   
                    string nombre = nombreobj.Nombre;
                    //string clave = key.Niveles;
                    int num = Convert.ToInt32(key.Niveles);
                    bool resultado = false;
                    if (int.TryParse(key.Niveles, out i))
                    {
                        if (i < 10000 && i > 0)
                        {
                            if (!Directory.Exists(_environment.WebRootPath + "\\UploadZigZag\\")) Directory.CreateDirectory(_environment.WebRootPath + "\\UploadZigZag\\");
                            using var _fileStream = System.IO.File.Create(_environment.WebRootPath + "\\UploadZigZag\\" + objFile.Files.FileName);
                            objFile.Files.CopyTo(_fileStream);
                            _fileStream.Flush();
                            _fileStream.Close();
                            resultado = true;
                            ZigZagCifrado2(objFile, num, nombre);
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

        public void ZigZagCifrado2(FileUploadApi objFile, int contra, string nombre)
        {
            string archivo = nombre;
            int clave = contra;
            string[] FileName1 = objFile.Files.FileName.Split(".");
            ZigZagMetodos.ZigZagAlgortimo(_environment.WebRootPath + "\\UploadZigZag\\" + objFile.Files.FileName, _environment.WebRootPath + "\\UploadZigZag\\" + archivo + ".txt", clave);
        }

        [Route("/Decipher/ZigZag")]
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
                    int num = Convert.ToInt32(key.Niveles);
                    bool resultado = false;
                    if (int.TryParse(key.Niveles, out i))
                    {
                        if (i < 10000 && i > 0)
                        {
                            if (!Directory.Exists(_environment.WebRootPath + "\\UploadZigZag\\")) Directory.CreateDirectory(_environment.WebRootPath + "\\UploadZigZag\\");
                            using var _fileStream = System.IO.File.Create(_environment.WebRootPath + "\\UploadZigZag\\" + objFile.Files.FileName);
                            objFile.Files.CopyTo(_fileStream);
                            _fileStream.Flush();
                            _fileStream.Close();
                            resultado = true;
                            ZigZagDescifrado(objFile, num, nombre);
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

        public void ZigZagDescifrado(FileUploadApi objFile, int contra, string nombre)
        {
            string archivo = nombre;
            int clave = contra;
            string[] FileName1 = objFile.Files.FileName.Split(".");
            ZigZagMetodos.ZigZagAlgortimo2(_environment.WebRootPath + "\\UploadZigZag\\" + objFile.Files.FileName, _environment.WebRootPath + "\\UploadZigZag\\" + archivo + ".txt", clave);
        }

    }
}