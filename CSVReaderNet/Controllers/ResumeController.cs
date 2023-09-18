using CsvHelper;
using CSVReaderNet.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Globalization;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace CSVReaderNet.Controllers
{
    public class ResumeController : Controller
    {
        [HttpGet]
        public IActionResult Index(List<Resum> resume = null)
        {
            resume = resume == null ? new List<Resum>() : resume;
            return View(resume);
        }

        [HttpPost]
        public IActionResult Index(IFormFile file, [FromServices] IHostingEnvironment hostingEnvironment)
        {
            #region Upload CSV
            string fileName = $"{hostingEnvironment.WebRootPath}\\files\\{file.FileName}";
            using (FileStream fileStream = System.IO.File.Create(fileName))
            {
                file.CopyTo(fileStream);
                fileStream.Flush();
            }
            #endregion

            var resume = this.GetResumList(file.FileName);
            return Index(resume);
        }

        private List<Resum> GetResumList(string fileName)
        {
            List<Resum> resume = new List<Resum>();

            #region Read CSV
            var path = $"{Directory.GetCurrentDirectory()}{@"\wwwroot\files"}" + "\\" + fileName;
            using (var reader = new StreamReader(path))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Read();
                csv.ReadHeader();
                while (csv.Read())
                {
                    var resum = csv.GetRecord<Resum>();
                    resume.Add(resum);
                }
            }
            #endregion

            #region Create CSV
            
            path = $"{Directory.GetCurrentDirectory()}{@"\wwwroot\files"}";
            using (var write = new StreamWriter(path + "\\NewFile.csv"))
                using (var csv = new CsvWriter(write, CultureInfo.InvariantCulture))
                    { csv.WriteRecords(resume); }

            #endregion

            return resume;
        }
    }
}
