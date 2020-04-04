using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LabCifrado.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class Cipher : ControllerBase
    {
        public static IWebHostEnvironment _environment;

        public Cipher(IWebHostEnvironment env)
        {
            _environment = env;
        }

        public class FileUploadApi
        {
            public IFormFile Files { get; set; }
        }
        public class ClaveUploadAPI
        {
            public string Clave { get; set; }
        }

        [Route("Upload/{id}/Cesar")]
        [HttpPost]

        public async Task<string> UploadFileText([FromForm] FileUploadApi objFile, string id)
        {
            try
            {
                if (objFile.Files.Length > 0)
                {
                    if (!Directory.Exists(_environment.WebRootPath + "\\UploadCesar\\")) Directory.CreateDirectory(_environment.WebRootPath + "\\UploadCesar\\");
                    using var _fileStream = System.IO.File.Create(_environment.WebRootPath + "\\UploadCesar\\" + objFile.Files.FileName);
                    objFile.Files.CopyTo(_fileStream);
                    _fileStream.Flush();
                    _fileStream.Close();
                    return "\\UploadCesar\\" + objFile.Files.FileName;
                }
                else return "Archivo Vacio";
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }

    }
}