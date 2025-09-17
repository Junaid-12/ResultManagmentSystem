
using Microsoft.AspNetCore.Mvc;
using ResultMamangementSystem.Models;
using ResultManagmentSystem.DataAcess;
namespace ResultManagmentSystem.Controllers
{
    public class AdminController : Controller
    {
       
        Student _student = new Student();
        public IActionResult Index()
        {
            try
            {
                if (HttpContext.Session.GetString("UserName") == null)
                {
                    return RedirectToAction("LoginForm","Admin");
                }

               

                return View(_student.GetStudentList().ToList());
              
            }
            catch (Exception ex) { return View(ex.Message); }
          
        }
        [HttpGet]
        public IActionResult LoginForm()
        {
            
            return View();
        }
        [HttpPost]
        public IActionResult LoginForm(AdminMo adminMo)
        {
            
           try{

              

                Login login = new Login();              
                    login.LoginUser(adminMo);
                if (login.LoginUser(adminMo) == true)
                {
                    HttpContext.Session.SetString("UserName", adminMo.UserId);
                   
                    ModelState.Clear();
                    TempData["Login"] = "Login successful,'Welcome'";
                    return RedirectToAction("Index", "Admin");
                }
                else {  TempData["AlertMessage"] = "Incorrect username or password.";
                    
                    ModelState.Clear();
                    return View();
                }
              
               
              }

            catch (Exception ex) { 
            
              return View(ex.Message);
            }

           
        }
        [HttpGet]
        public IActionResult Logout()
        {
            //Specific UserKeyName
        /*    HttpContext.Session.Remove("UserName");*/
            HttpContext.Session.Clear();
            return RedirectToAction("Index","Home");
        }
        [HttpGet]
        public IActionResult Create( )
        {

            if (HttpContext.Session.GetString("UserName") != null)
            {
                return View();
            }
           

            return RedirectToAction("LoginForm");
        }
        [HttpPost]
        public IActionResult Create(StudentMo studentMo)
        {
            try
            {
             
                
                    studentMo.DateOfBirth = studentMo.DateOfBirth.Date;
                    _student.CreateSudent(studentMo);

                    TempData["Success"] = "form Submit SucessFully";
                    return RedirectToAction("Index", "Admin");
                
               
            }
            catch (Exception ex) {
                return View(ex);
            }
           
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            try
            {
                if (HttpContext.Session.GetString("UserName") != null)
                {
                    return View(_student.GetStudentList().Find(StudentMo => StudentMo.Id == id));
                }
               
                return RedirectToAction("LoginForm");

            }
            catch (Exception ex) 
            {
                return View(ex);
            }
        }
        [HttpPost]
        public IActionResult Edit(StudentMo studentMo)
        {
            try
            {
                _student.UpdateStudent(studentMo);
                TempData["Update"] = "Update Student Data SuccessFully!";
                return RedirectToAction("Index", "Admin");

            }
            catch (Exception ex)
            { return View(ex); }

        }     
        public IActionResult Delete(int id)
        {
            try
            {
                if (HttpContext.Session.GetString("UserName") != null)
                {
                    _student.DeleteStudent(id);
                    TempData["Delete"] = "Delete Student  SuccessFully!";
                    return RedirectToAction("index", "Admin");
                }

                return RedirectToAction("Loginform");
            }
            catch (Exception ex)
            
                {  return View(ex); }
        }
        [HttpGet]
        public IActionResult Details(int id)
        {
            try
            { 
                if (HttpContext.Session.GetString("UserName") != null)
                {
                    return View(_student.GetStudentList().Find(StudentMo => StudentMo.Id == id));
                }
                return RedirectToAction("LoginForm");
            }
            catch (Exception ex)
            {

                return View(ex);
            }
        }
    }
   
}
