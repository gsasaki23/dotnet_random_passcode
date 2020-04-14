using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using random_passcode.Models;
using Microsoft.AspNetCore.Http;

namespace random_passcode.Controllers
{
    public class HomeController : Controller
    {
        
        
        public IActionResult Index()
        {
            // Very first render: set counter at 1
            if (HttpContext.Session.GetInt32("Counter") == null)
            {
                HttpContext.Session.SetInt32("Counter",1);
            }
            // Otherwise, get the counter from session
            ViewBag.Counter = HttpContext.Session.GetInt32("Counter");
            
            // Very first render: start with a random passcode
            if (HttpContext.Session.GetString("Passcode") == null)
            {
                string options = "QWERTYUIOPASDFGHJKLZXCVBNM1234567890";
                string passcode = "";
                for (int i = 0; i < 14; i++)
                {
                    passcode += options[new Random().Next(0,options.Length)];
                }
                HttpContext.Session.SetString("Passcode",passcode);
            }
            // Otherwise, get the passcode from session
            ViewBag.Passcode = HttpContext.Session.GetString("Passcode");

            return View();
        }

        [HttpGet("/generate")]
        public IActionResult Generate()
        {
            // Update "Counter" to be vvv
            HttpContext.Session.SetInt32("Counter",
                // the "Counter" from session, incremented by 1
                (int)HttpContext.Session.GetInt32("Counter") + 1
            );

            // Update "Passcode" with new 14char string
            string options = "QWERTYUIOPASDFGHJKLZXCVBNM1234567890";
            string passcode = "";
            for (int i = 0; i < 14; i++)
            {
                passcode += options[new Random().Next(0,options.Length)];
            }
            HttpContext.Session.SetString("Passcode",passcode);

            return RedirectToAction("Index");
        }
        
        [HttpGet("/reset")]
        public IActionResult Reset()
        {
            // Update "Counter" to be 1
            HttpContext.Session.SetInt32("Counter",1);
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
