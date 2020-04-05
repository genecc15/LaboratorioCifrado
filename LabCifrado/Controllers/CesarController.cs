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
    [Route("[controller]")]
    [ApiController]
    public class CesarController : ControllerBase
    {
        public static IWebHostEnvironment _environment;
        private readonly CesarMetodos CesarCifrado = new CesarMetodos();
        private readonly CesarMetodos CesarDesc = new CesarMetodos();
        public CesarController(IWebHostEnvironment env)
        {
            _environment = env;
        }

        public class FileUploadApi
        {
            public IFormFile Files { get; set; }
            public string Clave { get; set; }
            public string Nombre { get; set; }
        }

        [Route("/Cipher/Cesar")]
        [HttpPost]
        public async Task<string> UploadFileText([FromForm] FileUploadApi objFile,[FromForm] FileUploadApi key, [FromForm]FileUploadApi nombreobj)
        {
            try
            {
                if (objFile.Files.Length > 0)
                {
                    string nombre = nombreobj.Nombre;
                    string clave = key.Clave;
                    bool resultado = false;
                    if (clave.All(char.IsLetter))
                    {
                        if (clave.Distinct().Count() == clave.Length)
                        {
                            if (!Directory.Exists(_environment.WebRootPath + "\\UploadCesar\\")) Directory.CreateDirectory(_environment.WebRootPath + "\\UploadCesar\\");
                            using var _fileStream = System.IO.File.Create(_environment.WebRootPath + "\\UploadCesar\\" + objFile.Files.FileName);
                            objFile.Files.CopyTo(_fileStream);
                            _fileStream.Flush();
                            _fileStream.Close();
                            resultado = true;
                            CesarCifrado2(objFile, clave, nombre);
                        }
                        else
                        {
                            return "No se puede usar esa clave, porfavor elige una palabra con letras diferentes.";
                        }
                    }
                    else
                    {
                        return "No se puede usar esa clave, porfavor escribe una palabra";
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

        public void CesarCifrado2(FileUploadApi objFile, string contra, string nombre)
        {
            string archivo = nombre;
            string clave = contra;
            string[] FileName1 = objFile.Files.FileName.Split(".");
            CesarMetodos.CesarAlgoritmo(_environment.WebRootPath + "\\UploadCesar\\" + objFile.Files.FileName, _environment.WebRootPath + "\\UploadCesar\\" + archivo+".txt", clave);
        }

        [Route("/Decipher/Cesar")]
        [HttpPost]
        public async Task<string> UploadFileCesar([FromForm] FileUploadApi objFile, [FromForm] FileUploadApi key, [FromForm]FileUploadApi nombreobj)
        {
            try
            {
                if (objFile.Files.Length > 0)
                {
                    string nombre = nombreobj.Nombre;
                    string clave = key.Clave;
                    bool resultado = false;
                    if (clave.All(char.IsLetter))
                    {
                        if (clave.Distinct().Count() == clave.Length)
                        {
                            if (!Directory.Exists(_environment.WebRootPath + "\\UploadCesar\\")) Directory.CreateDirectory(_environment.WebRootPath + "\\UploadCesar\\");
                            using var _fileStream = System.IO.File.Create(_environment.WebRootPath + "\\UploadCesar\\" + objFile.Files.FileName);
                            objFile.Files.CopyTo(_fileStream);
                            _fileStream.Flush();
                            _fileStream.Close();
                            resultado = true;
                            CesarDescifrado(objFile, clave, nombre);
                        }
                        else
                        {
                            return "No se puede usar esa clave, porfavor elige una palabra con letras diferentes.";
                        }
                    }
                    else
                    {
                        return "No se puede usar esa clave, porfavor escribe una palabra";
                    }
                    return "Archivo Descifrado correctamente";
                }
                else return "Archivo Vacio";

            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }


        }
        public void CesarDescifrado(FileUploadApi objFile, string contra, string nombre)
        {
            string archivo = nombre;
            string clave = contra;
            string[] FileName1 = objFile.Files.FileName.Split(".");
            CesarMetodos.CesarAlgoritmo2(_environment.WebRootPath + "\\UploadCesar\\" + objFile.Files.FileName, _environment.WebRootPath + "\\UploadCesar\\" + archivo + ".txt", clave);
        }


    }
}