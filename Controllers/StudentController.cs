using Microsoft.AspNetCore.Mvc;
using ResultManagmentSystem.DataAcess;
using ResultMamangementSystem.Models;
using ResultManagmentSystem.Models.Data;
using Rotativa.AspNetCore;
namespace ResultManagmentSystem.Controllers
{
    public class StudentController : Controller
    {
        Result result = new Result();
        
        public IActionResult Index()
        {
            if(TempData["StudentResult"] != null)
            {
                var data = TempData["StudentResult"] != null
               ? Newtonsoft.Json.JsonConvert.DeserializeObject<List<ReportCardMo>>(TempData["StudentResult"].ToString())
               : new List<ReportCardMo>();
                return View(data);
            }
            else
            {

                return RedirectToAction("StudentResportCard");
            }
           
         
        }
        [HttpGet]
        public IActionResult StudentResportCard()
        {
            return View();
        }
        [HttpPost]
        public IActionResult StudentResportCard(string rollno )
        {
            try
            {
                var data=  result.GetReportCards(rollno);
                if (data != null && data.Any())
                {
                    TempData["Resultfound"] = "Result is here";
                    TempData["StudentResult"] = Newtonsoft.Json.JsonConvert.SerializeObject(data);
                    return RedirectToAction("Index");   
                }
                else {

                    TempData["Resultnofound"] = "InValid RollNo";
                    return RedirectToAction("StudentResportCard");
                }
            }
            catch (Exception ex)
            {  
                return View(ex.Message);
            }
        }
   
        public IActionResult DownloadResultAsPdf(string rollNo)
        {
            var studentData = result.GetReportCards(rollNo);

            if (studentData == null)
            {
                return NotFound();
            }

            return new ViewAsPdf("ResultPdf", result)
            {
                FileName = "Result.pdf"
            };
        }

    }
}
