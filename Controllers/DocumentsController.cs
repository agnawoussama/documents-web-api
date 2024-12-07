using documents_web_api.Data;
using documents_web_api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Net;
using System.Reflection.Metadata;
using System.Xml.Linq;

namespace documents_web_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentsController : ControllerBase
    {
        private readonly DocumentsContext _context;


        public DocumentsController(DocumentsContext context)
        {
            _context = context;
        }

        [HttpGet("{patientId}")]
        public IActionResult GetDocuments(int patientId)
        {
            var documents = _context.Documents
                                    .Where(d => d.PatientId == patientId)
                                    .Select(d => new DocumentDto
                                    {
                                        Id = d.Id,
                                        Nom = d.nom,
                                        PatientId = d.PatientId,
                                        CategorieNom = _context.Categories
                                                              .Where(c => c.id == d.CategorieId)
                                                              .Select(c => c.nom)
                                                              .First(),
                                        CheminDocument = $"DentalClinic/DocumentsPatients/{d.PatientId}"
                                    })
                                    .ToList();

            return Ok(documents);
        }


        [HttpGet("download/{id}/{nom}")]
        public async Task<IActionResult> DownloadFile(string id, string nom)
        {
            string host = "127.0.0.1";
            string username = "oussama";
            string password = "1997";

            string ftpUrl = $"ftp://{host}/DentalClinic/DocumentsPatients/{id}/{nom}";

            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftpUrl);
            request.Method = WebRequestMethods.Ftp.DownloadFile;
            request.Credentials = new NetworkCredential(username, password);

            try
            {
                using var response = (FtpWebResponse)await request.GetResponseAsync();
                using var stream = response.GetResponseStream();

                var memoryStream = new MemoryStream();
                await stream.CopyToAsync(memoryStream);

                return File(memoryStream.ToArray(), "application/octet-stream", nom);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "Error downloading file.", Error = ex.Message });
            }
        }


        [HttpPost]
        //public IActionResult AddDocument([FromBody] Document document)
        //{
        //    var categoryExists = _context.Categories.Any(c => c.id == document.CategorieId);
        //    if (!categoryExists)
        //        return BadRequest("Invalid CategorieId");

        //    _context.Documents.Add(document);
        //    _context.SaveChanges();
        //    return CreatedAtAction(nameof(GetDocuments), new { patientId = document.PatientId }, document);
        //}

        [HttpGet("categories")]
        public IActionResult GetCategories()
        {
            var categories = _context.Categories.ToList();
            return Ok(categories);
        }

        [HttpGet("categories/{id}")]
        public IActionResult GetCategorie(int id)
        {
            var categorie = _context.Categories.FirstOrDefault(c => c.id == id);
            return Ok(categorie);
        }
    }
}
