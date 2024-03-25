using Assessment.Models;
using Assessment.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Assessment.Controllers
{
    public class HomeController : Controller
    {
        private static DataStore _dataStore = new DataStore();
        private static int _status = 4;
        private static string _email = "tyagiji@gmail.com";
        private static int _uid = 0;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            ViewBag.UserID = _uid;
            ViewBag.UserName = _email;
            ViewBag.LoginStatus =_status;
            return View();
        }

        public IActionResult Restricted()
        {
            ViewBag.UserID = _uid;
            ViewBag.UserName = _email;
            ViewBag.LoginStatus = _status;
            return View();
        }
        public IActionResult Privacy()
        {
            ViewBag.UserID = _uid;
            ViewBag.UserName = _email;
            ViewBag.LoginStatus = _status;
            return View();
        }

        public IActionResult UserDashboard(int Uid)
        {
            if (_status <= 2 || _uid == Uid)
            {
                List<int> filteredTids = (from E in _dataStore.EnrollData where E.UserId == Uid select E.TrainId).ToList(); ;

                var trainingdata = (from train in _dataStore.TrainingData
                                    where filteredTids.Contains(train.Tid)
                                    orderby train.TrainingStatus
                                    select train).ToList();
                ViewBag.TrainingData = trainingdata;
                ViewBag.UserDetails = _dataStore.UserData.Where(U => U.Uid == Uid).FirstOrDefault();
                ViewBag.UserName = _email;
                ViewBag.UserID = _uid;
                ViewBag.LoginStatus = _status;
                return View();
            }
            else
            {
                ViewBag.UserID = _uid;
                ViewBag.UserName = _email;
                ViewBag.LoginStatus = _status;
                return View();
            }
        }

        public IActionResult TrainingDashboard(int Tid)
        {

                List<int> filteredUids = (from E in _dataStore.EnrollData where E.TrainId == Tid select E.UserId).ToList(); ;

            var userdata = (from user in _dataStore.UserData
                                where filteredUids.Contains(user.Uid)
                                orderby user.Uid
                                select user).ToList();
            ViewBag.UserData = userdata;
            ViewBag.TrainingDetails = _dataStore.TrainingData.Where(T => T.Tid == Tid).FirstOrDefault();
            ViewBag.UserName = _email;
            ViewBag.UserID = _uid;
            ViewBag.LoginStatus = _status;
            return View();
            
        }
        public IActionResult Login()
        {
            ViewBag.UserName = _email;
            ViewBag.LoginStatus = _status;
            return View();
        }
        public IActionResult Logout()
        {
            _email = null;
            _status = 4;
            _uid = 0;
            ViewBag.UserName = _email;
            ViewBag.LoginStatus = _status;
            return View("Index");
        }
        public IActionResult AuthUser(UserModel credentials)
        {
            var user = _dataStore.UserData.SingleOrDefault(p =>
                  p.Email == credentials.Email
                  && p.Pswd == credentials.Pswd);

            if (user != null)
            {
                _email = user.Email;
                _status = (int)user.Role;
                _uid = user.Uid;
                ViewBag.UserID = _uid;
                ViewBag.UserName = _email;
                ViewBag.LoginStatus = _status;
                ViewBag.loginFailed = 0;
                ViewBag.FName = user.FName;
                ViewBag.LName = user.LName;
                //return UserDashboard(_uid);
                return View("Index");
            }
            else
            {
                _status = 4;
                ViewBag.loginFailed = 1;
                ViewBag.UserName = _email;
                ViewBag.LoginStatus = _status;
                return View("Login");
            }

        
        }

        public IActionResult ViewEnrollments()
        {
            if (_status <= 2)
            {
                ViewBag.UserData = _dataStore.UserData;
                ViewBag.TrainingData = from t in _dataStore.TrainingData
                                       orderby t.Tid
                                       select t;
                ViewBag.EnrollData = _dataStore.EnrollData;
                ViewBag.UserName = _email;
                ViewBag.UserID = _uid;
                ViewBag.LoginStatus = _status;
                return View();
            }
            else
            {
                ViewBag.UserID = _uid;
                ViewBag.UserName = _email;
                ViewBag.LoginStatus = _status;
                return View("Restricted");
            }
        }
        public IActionResult ViewAllUsers()
        {
            if (_status <= 2)
            {
                ViewBag.UserID = _uid;
                ViewBag.UserName = _email;
                ViewBag.LoginStatus = _status;
                var data = from t in _dataStore.UserData
                           orderby t.Role, t.Uid
                           select t;
                return View(data);
            }
            else
            {
                ViewBag.UserID = _uid;
                ViewBag.UserName = _email;
                ViewBag.LoginStatus = _status;
                return View("Restricted");
            }
        }
        public IActionResult ViewAllTrainings()
        {
            if (_status <= 3)
            {
                ViewBag.UserID = _uid;
            ViewBag.UserName = _email;
            ViewBag.LoginStatus = _status;
            var data = from t in _dataStore.TrainingData
                       orderby t.CreatedDate
                       select t;
            return View(data);
            }
            else
            {
                ViewBag.UserID = _uid;
                ViewBag.UserName = _email;
                ViewBag.LoginStatus = _status;
                return View("Restricted");
            }
        }


        //[HttpPost]

        public IActionResult AddEnrollments()
        {
            if (_status <= 2)
            {
                var TempData1 = from u in _dataStore.UserData
                            where u.Role == Roles.Trainer
                            orderby u.Uid
                            select u;
            var TempData2 = from u in _dataStore.UserData
                            orderby u.Uid
                            select u;
            var TempData3 = from t in _dataStore.TrainingData
                            orderby t.Tid
                            select t;
            ViewData["Trainers"] = TempData1.ToList<UserModel>();
            ViewData["Users"] = TempData2.ToList<UserModel>();
            ViewData["TrainingData"] = TempData3.ToList<TrainingModel>();
            ViewBag.doneEnroll = false;
            ViewBag.UserName = _email;
            ViewBag.UserID = _uid;
            ViewBag.LoginStatus = _status;
            return View(); 
        }
            else
            {
                ViewBag.UserID = _uid;
                ViewBag.UserName = _email;
                ViewBag.LoginStatus = _status;
                return View("Restricted");
            }
        }
      

        [HttpPost]
        public IActionResult EnrollTrainer(EnrollModel Temp)
        {
                Console.WriteLine("Enrollment Data : " + Temp.UserId + " " + Temp.TrainId);
                _dataStore.AddNewEnrolled(Temp.UserId, Temp.TrainId);


            var TempData1 = from u in _dataStore.UserData
                            where u.Role == Roles.Trainer
                            orderby u.Uid
                            select u;
            var TempData2 = from u in _dataStore.UserData
                            orderby u.Uid
                            select u;
            var TempData3 = from t in _dataStore.TrainingData
                            orderby t.Tid
                            select t;
            ViewData["Trainers"] = TempData1.ToList<UserModel>();
            ViewData["Users"] = TempData2.ToList<UserModel>();
            ViewData["TrainingData"] = TempData3.ToList<TrainingModel>();
            ViewBag.doneEnroll = true;
            ViewBag.UserName = _email;
            ViewBag.UserID = _uid;
            ViewBag.LoginStatus = _status;

            return View("AddEnrollments");
        }

        [HttpPost]
        public IActionResult EnrollTrainee(EnrollModel Temp)
        {    Console.WriteLine("Enrollment Data" + Temp.UserId + " " + Temp.TrainId);
            _dataStore.AddNewEnrolled(Temp.UserId, Temp.TrainId);


            var TempData1 = from u in _dataStore.UserData
                            where u.Role == Roles.Trainer
                            orderby u.Uid
                            select u;
            var TempData2 = from u in _dataStore.UserData
                            orderby u.Uid
                            select u;
            var TempData3 = from t in _dataStore.TrainingData
                            orderby t.Tid
                            select t;
            ViewData["Trainers"] = TempData1.ToList<UserModel>();
            ViewData["Users"] = TempData2.ToList<UserModel>();
            ViewData["TrainingData"] = TempData3.ToList<TrainingModel>();
            ViewBag.doneEnroll = true;
            ViewBag.UserName = _email;
            ViewBag.UserID = _uid;
            ViewBag.LoginStatus = _status;

            return View("AddEnrollments");
        }

        public IActionResult DeleteEnrollments(int Uid, int Tid)
        {
            // return View("Login");
            if (_status == 1)
            {
                _dataStore.DeleteEnrolled(Uid, Tid);
                ViewBag.UserData = _dataStore.UserData;
                ViewBag.TrainingData = from t in _dataStore.TrainingData
                                       orderby t.Tid
                                       select t;
                ViewBag.EnrollData = _dataStore.EnrollData;
                ViewBag.UserName = _email;
                ViewBag.UserID = _uid;
                ViewBag.LoginStatus = _status;
                return View("ViewEnrollments");
            }
            else
            {

                ViewBag.UserID = _uid;
                ViewBag.UserName = _email;
                ViewBag.LoginStatus = _status;
                return View("Restricted");
            }
        }
        public IActionResult RegisterUser()
        {
            if (_status == 1)
            {
                ViewBag.UserName = _email;
                ViewBag.UserID = _uid;
                ViewBag.LoginStatus = _status;
                return View();
            }
            else
            {

                ViewBag.UserID = _uid;
                ViewBag.UserName = _email;
                ViewBag.LoginStatus = _status;
                return View("Restricted");
            }
        }
        public IActionResult EditUserPage(int Uid)
        {
            if (_status == 1)
            {
            ViewBag.UserDetails = _dataStore.UserData.Where(U => U.Uid == Uid).FirstOrDefault();
            ViewBag.UserName = _email;
            ViewBag.UserID = _uid;
            ViewBag.LoginStatus = _status;
            return View();
            }
            else
            {

                ViewBag.UserID = _uid;
                ViewBag.UserName = _email;
                ViewBag.LoginStatus = _status;
                return View("Restricted");
            }
        }
        public IActionResult EditUser(UserModel Updateduser)
        {
            if (_status == 1)
            {
                _dataStore.UpdateUser(Updateduser);
            ViewBag.UserName = _email;
            ViewBag.UserID = _uid;
            ViewBag.LoginStatus = _status;
            var data = from t in _dataStore.UserData
                       orderby t.Role, t.Uid
                       select t;
            return View("ViewAllUsers",data);
              }
            else
            {

                ViewBag.UserID = _uid;
                ViewBag.UserName = _email;
                ViewBag.LoginStatus = _status;
                return View("Restricted");
              }
            }
        public IActionResult EditTrainingPage(int Tid)
        {
            if (_status <= 2)
            {
                ViewBag.UserDetails = _dataStore.TrainingData.Where(T => T.Tid == Tid).FirstOrDefault();
                ViewBag.UserName = _email;
                ViewBag.UserID = _uid;
                ViewBag.LoginStatus = _status;
                return View();
            }
            else
            {

                ViewBag.UserID = _uid;
                ViewBag.UserName = _email;
                ViewBag.LoginStatus = _status;
                return View("Restricted");
            }
         }
        public IActionResult EditTraining(TrainingModel Updatedtraining)
        {
            if (_status <= 2)
            {
                _dataStore.UpdateTraining(Updatedtraining);
            ViewBag.UserName = _email;
            ViewBag.UserID = _uid;
            ViewBag.LoginStatus = _status;
            var data = from t in _dataStore.TrainingData
                       orderby t.CreatedDate
                       select t;
            return View("ViewAllTrainings",data);
        }
            else
            {

                ViewBag.UserID = _uid;
                ViewBag.UserName = _email;
                ViewBag.LoginStatus = _status;
                return View("Restricted");
    }
}
        public IActionResult DeleteTraining(int Tid)
        {
            if (_status == 1)
            {
                _dataStore.DeleteTraining(Tid);
                ViewBag.UserName = _email;
                ViewBag.UserID = _uid;
                ViewBag.LoginStatus = _status;
                return View("Index");
            }
            else
            {

                ViewBag.UserID = _uid;
                ViewBag.UserName = _email;
                ViewBag.LoginStatus = _status;
                return View("Restricted");
            }
        }

        public IActionResult DeleteUser(int Uid)
        {
            if (_status == 1)
            {
                _dataStore.DeleteUser(Uid);
            ViewBag.UserName = _email;
            ViewBag.UserID = _uid;
            ViewBag.LoginStatus = _status;
            return View("Index");
        }
            else
            {

                ViewBag.UserID = _uid;
                ViewBag.UserName = _email;
                ViewBag.LoginStatus = _status;
                return View("Restricted");
    }
}
        public IActionResult RegisterTraining()
        {
            if (_status <= 2)
            {
                ViewBag.UserName = _email;
                ViewBag.UserID = _uid;
                ViewBag.LoginStatus = _status;
                return View();
            }
            else
            {

                ViewBag.UserID = _uid;
                ViewBag.UserName = _email;
                ViewBag.LoginStatus = _status;
                return View("Restricted");
            }
            }
        
        [HttpPost]
        public IActionResult AddNewUser(UserModel user)
        {
            if (_status <= 2)
            {
                ViewBag.UserID = _uid;
            ViewBag.UserName = _email;
            ViewBag.LoginStatus = _status;
            _dataStore.AddNewUser(user.FName, user.LName, user.Email, user.Pswd, user.Role);
            var data = from t in _dataStore.UserData
                       orderby t.Role, t.Uid
                       select t;
            return View("ViewAllUsers", data);
        }
            else
            {

                ViewBag.UserID = _uid;
                ViewBag.UserName = _email;
                ViewBag.LoginStatus = _status;
                return View("Restricted");
    }
}
        
        [HttpPost]
        public IActionResult AddNewTraining(TrainingModel training)
    {
        if (_status <= 2)
        {
            ViewBag.UserID = _uid;
            ViewBag.UserName = _email;
            ViewBag.LoginStatus = _status;
            _dataStore.AddNewTraining(training.TrainingName, DateTime.Now, training.Department, training.TrainingModes, training.TrainingStatus);
            var data = from t in _dataStore.TrainingData
                       orderby t.CreatedDate
                       select t;
            return View("ViewAllTrainings", data);
            }
            else
            {

                ViewBag.UserID = _uid;
                ViewBag.UserName = _email;
                ViewBag.LoginStatus = _status;
                return View("Restricted");
            }
        }
     
       


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
