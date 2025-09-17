
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ResultMamangementSystem.Models;
using ResultManagmentSystem.DataAcess;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Drawing;
using MailKit.Net.Smtp;
using MimeKit;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace ResultManagmentSystem.Controllers
{
    public class ResultController : Controller
    {
        Result result = new Result();
        public IActionResult Index()
        {
            try
            {
                ResultMo resultMo = new ResultMo();
               

                if (HttpContext.Session.GetString("UserName") != null)
                {


                    return View(result.GetResultList().ToList());
                }

              
                return RedirectToAction("LoginForm","Admin");

            }
            catch (Exception ex) { return View(ex); }


        }
        [HttpGet]
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("UserName") == null)
            {

                return RedirectToAction("LoginForm", "Admin");
            }
            LoadRollNo();
                return View(); 
        
        }
        [NonAction]
        private void LoadRollNo()
        {
         
            var RnList = result.GetRollfromDatabase().ToList();
            ViewBag.rnList  = new SelectList(RnList, "RollNo","RollNo");
        }
        [HttpPost]
        public IActionResult Create(ResultMo resultMo )
        {
            try
            {
                var Rollno=resultMo.RollNo;
                result.CreateResult(resultMo);
                
                TempData["ResultAdd"] = "Result Add SucessFully";
                SendEmail(Rollno);
                return RedirectToAction("Index", "Result");
              
            }

            catch (Exception ex) { return View(ex); }
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            try
            {
                if (HttpContext.Session.GetString("UserName") == null)
                {
                    return RedirectToAction("LoginForm", "Admin");

                }
               
                return View(result.GetResultList().Find(ResultMo => ResultMo.Id == id));
            }
            catch (Exception ex) { return View(ex); }
        }
        [HttpPost]
        public IActionResult Edit(ResultMo resultmo)
        {
            try
            {
                result.UpdateResult(resultmo);
                ModelState.Clear();
                TempData["Resultupdate"] = "Result Update SucessFully!";
                return RedirectToAction("Index", "Result");
            }
            catch (Exception ex) { return View(ex); }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                if (HttpContext.Session.GetString("UserName") == null)
                {
                    return RedirectToAction("LoginForm", "Admin");

                }
                result.DeleteResult(id);

                ModelState.Clear();
                TempData["ResultDelete"] = "Result Delete SucessFully!!";
                return RedirectToAction("Index", "Result");
            }
            catch (Exception ex) { return View(ex); }
        }
        [HttpGet]
        public IActionResult Details(int id)
        {
            if(HttpContext.Session.GetString("UserName")== null)
            {
                return RedirectToAction("LoginForm", "Admin");
            }
            return View(result.GetResultList().Find(x => x.Id == id));
        }
      
        public IActionResult SendEmail(string rollno )
        {
            try
            {
               var SendEMaidetails = result.SendEmailUser(rollno);
                if (SendEMaidetails.email == null)
                {
                    throw new Exception("Student not found or not eligible for results.");
                }
               var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Testing Smtp Client!!", "junaidalam85000@gmail.com"));
                message.To.Add(new MailboxAddress(SendEMaidetails.studentname,SendEMaidetails.email));
                message.Subject = "test Mail in Asp.net core";
                message.Body = new TextPart("Plain")
                {
                  Text = $@"Dear {SendEMaidetails.studentname },

                We hope this email finds you well. We are pleased to inform you that your results are now available.
                To view your results, please click on the link below:

               Check Your Results: https://localhost:44340/Student/StudentReportCard

            If you have any questions or need further assistance, please do not hesitate to contact us.
             Gmail : junaid85000@gmail.com
          Best regards,
         [BSM College/Engineering Roorke]
                                        "

                };
                using (var client = new SmtpClient())
                {

                    client.Connect("smtp.gmail.com", 587, false);
                    client.Authenticate("junaidalam85000@gmail.com", "phvv akuj lrpo gjzj");
                    client.Send(message);

                    client.Disconnect(true);
                };

            }
            catch (Exception ex) {
                return View(ex);
            }

            return View();

        }

    }
}
