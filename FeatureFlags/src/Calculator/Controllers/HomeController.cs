using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LaunchDarkly.Client;
using Microsoft.ApplicationInsights;

namespace Calculator.Controllers
{
    public class HomeController : Controller
    {
        static LdClient client = new LdClient("<your SDK Key>");
        TelemetryClient tc = new TelemetryClient();

        public ActionResult Index()
        {
            User user = GetUser();
            bool sum = client.BoolVariation("sum", user, false);
            bool subtract = client.BoolVariation("subtract", user, false);
            

            ViewData["sumtoggle"] = sum;
            ViewData["subtracttoggle"] = subtract;
            return View();
        }

        public ActionResult Sum(double? firstNumber, double? secondNumber)
        {
            User user = GetUser();
            bool sum = client.BoolVariation("sum", user, false);
            bool subtract = client.BoolVariation("subtract", user, false);
            if (sum)
            {
                if (firstNumber.HasValue && secondNumber.HasValue)
                {
                    double result = firstNumber.Value + secondNumber.Value;

                    this.ViewBag.firstNumber = firstNumber.ToString();
                    this.ViewBag.secondNumber = secondNumber.ToString();
                    this.ViewBag.Sum = result.ToString();

                    ViewData["sumtoggle"] = sum;
                    ViewData["subtracttoggle"] = subtract;
                    tc.TrackEvent("Successful sum");
                    return this.View();
                }

                this.ViewBag.Sum = this.ViewBag.firstNumber = this.ViewBag.secondNumber = 0;
                ViewData["sumtoggle"] = sum;
                ViewData["subtracttoggle"] = subtract;
                return this.View();
            } else
            {
                return Content("You are not authorised to access this page.");
                tc.TrackEvent("Unauthorised access");
            }
        }

        public ActionResult Subtract(double? firstNumber, double? secondNumber)
        {
            User user = GetUser();
            bool sum = client.BoolVariation("sum", user, false);
            bool subtract = client.BoolVariation("subtract", user, false);
            if (subtract)
            {
                if (firstNumber.HasValue && secondNumber.HasValue)
                {
                    double result = firstNumber.Value - secondNumber.Value;

                    this.ViewBag.firstNumber = firstNumber.ToString();
                    this.ViewBag.secondNumber = secondNumber.ToString();
                    this.ViewBag.Subtract = result.ToString();

                    ViewData["sumtoggle"] = sum;
                    ViewData["subtracttoggle"] = subtract;
                    return this.View();
                }

                this.ViewBag.Subtract = this.ViewBag.firstNumber = this.ViewBag.secondNumber = 0;
                ViewData["sumtoggle"] = sum;
                ViewData["subtracttoggle"] = subtract;
                tc.TrackEvent("Successful subtraction");
                return this.View();
            }
            else
            {
                return Content("You are not authorised to access this page.");
                tc.TrackEvent("Unauthorised access");
            }
        }

        private static User GetUser()
        {
            return LaunchDarkly.Client.User.WithKey("bob@example.com")
                            .AndFirstName("Bob")
                            .AndLastName("Loblaw")
                            .AndCustomAttribute("groups", "beta_testers");
        }
    }
}
