using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PrintService.Models;
using PrintService.ViewModels;
using Renci.SshNet;
using System.IO;
using System.Linq.Expressions;
using static System.Net.WebRequestMethods;
using ConnectionInfo = Renci.SshNet.ConnectionInfo;

namespace PrintService.Controllers
{
    public class PrintProcessController : Controller
    {
        private readonly PrintDbContext db;
        public PrintProcessController(PrintDbContext context)
        {
            db = context;
        }
        [HttpGet]
        public IActionResult PrintConfig(int printerId)
        {
            Printer? prnt = db.Printers
                .Include(p => p.PaperDetailPrinters)
                .FirstOrDefault(x => x.PrinterId == printerId);

            PrintConfig prntConfig = new PrintConfig
            {
                PrinterID = printerId,
                PrinterModel = prnt?.Model,
                A3Amount = GetAmountPaperPrinter(prnt ?? new Printer(), "A3"),
                A4Amount = GetAmountPaperPrinter(prnt ?? new Printer(), "A4"),
                CampusName = prnt?.CampusName,
                BuildingName = prnt?.BuildingName,
                Room = prnt?.RoomNumber
            };

            return View(prntConfig);
        }

        [HttpPost]
        public IActionResult PrintConfig(PrintConfig prntConfig)
        {

            if (!ModelState.IsValid)
            {
                // Return the validation errors
                return View(prntConfig);
            }

            if (!CheckPaperAmount(prntConfig))
            {
                ViewBag.InvalidPaper = true;
                return View(prntConfig);
            }

            int documentId = SaveFileToServer(prntConfig.File);

            PrintingLog printingLog = new PrintingLog
            {
                StudentId = HttpContext.Session.GetString("AccountID"),
                PrinterId = prntConfig.PrinterID,
                DocumentId = documentId,
                PaperTypeId = prntConfig.PaperTypeID,
                IsColored = prntConfig.Colored,
                IsDoubleSided = prntConfig.IsDoubledSide,
                Copies = prntConfig.NumberOfCopies,
                Status = 0,
                StartTime = DateTime.Now,
            };

            db.PrintingLogs.Add(printingLog);
            db.SaveChanges();

            MinusPaper(prntConfig);


            return RedirectToAction("PrintConfigSuccess", new { printinLogId = printingLog.LogId });
        }

        public IActionResult PrintConfigSuccess(int printinLogId)
        {
            return View(printinLogId);
        }

        public int GetAmountPaperPrinter(Printer prnt, string typePaper)
        {
            PaperType pType = db.PaperTypes.FirstOrDefault(x => x.PaperName == typePaper) ?? new PaperType();
            PaperDetailPrinter prntDetail = prnt.PaperDetailPrinters.FirstOrDefault(x => x.PaperTypeId == pType.PaperTypeId) ?? new PaperDetailPrinter();

            return prntDetail.Amount;
        }

        
        public int SaveFileToServer(IFormFile file)
        {
            string remoteHost = "35.187.250.71";
            string username = "ltmichael";

            string remoteDirectory = "/home/ltmichael/uploads";

            string privateKey = @"-----BEGIN OPENSSH PRIVATE KEY-----
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
-----END OPENSSH PRIVATE KEY-----";

            string fileName = file.FileName;
            string uniqueFileName = $"{Path.GetFileNameWithoutExtension(fileName)}_{Guid.NewGuid()}{Path.GetExtension(fileName)}";
            string filePath = Path.Combine(remoteDirectory, uniqueFileName);
            using (var privateKeyStream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(privateKey)))
            using (var privateKeyFile = new PrivateKeyFile(privateKeyStream))
            using (var sftpClient = new SftpClient(
                new ConnectionInfo(remoteHost, username, new PrivateKeyAuthenticationMethod(username, privateKeyFile))))
            {
                // Connect to the server
                sftpClient.Connect();

                // Upload the file to the remote directory

                using (var stream = file.OpenReadStream())
                {
                    // Upload file
                    sftpClient.UploadFile(stream, filePath);
                }
                sftpClient.Disconnect();
            }

            Document saveDoc = new Document
            {
                StudentId = HttpContext.Session.GetString("AccountID"),
                FileName = fileName,
                FileType = System.IO.Path.GetExtension(file.FileName).TrimStart('.'),
                FilePath = filePath,
                UploadedAt = DateTime.Now

            };

            db.Documents.Add(saveDoc);
            db.SaveChanges();

            return saveDoc.DocumentId;
        }
        
        public void MinusPaper(PrintConfig prntConfig)
        {
            Printer prnt = db.Printers
                .Include(x=>x.PaperDetailPrinters)
                .FirstOrDefault(x => x.PrinterId == prntConfig.PrinterID) ?? new Printer();
            
            PaperDetailPrinter paperDetail = prnt.PaperDetailPrinters.FirstOrDefault(pd => pd.PaperTypeId == prntConfig.PaperTypeID) ?? new PaperDetailPrinter();

            paperDetail.Amount -= prntConfig.NumberOfCopies;
            db.SaveChanges();

            Student student = db.Students.Include(x=>x.PaperDetailStudents)
                .FirstOrDefault(x => x.StudentId == HttpContext.Session.GetString("AccountID")) ?? new Student();

           PaperDetailStudent ppStudent = student.PaperDetailStudents.FirstOrDefault(pd => pd.PaperTypeId == prntConfig.PaperTypeID) ?? new PaperDetailStudent();
            ppStudent.Amount -= prntConfig.NumberOfCopies;

            db.SaveChanges();
        }

        [HttpPut]
        public bool CheckPaperAmount(PrintConfig prntConfig)
        {
            string studentId = HttpContext.Session.GetString("AccountID");
            int amountStudentPaper = db.PaperDetailStudents.FirstOrDefault(x => x.StudentId == studentId && x.PaperTypeId == prntConfig.PaperTypeID).Amount ?? 0;

            return amountStudentPaper >= prntConfig.NumberOfCopies ;
        }
    }
}
