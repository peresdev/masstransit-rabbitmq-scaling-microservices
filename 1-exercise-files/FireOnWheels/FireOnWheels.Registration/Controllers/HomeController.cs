using Microsoft.AspNet.Mvc;
using FireOnWheels.Registration.ViewModels;
using FireOnWheels.Registration.Messages;

namespace FireOnWheels.Registration.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult RegisterOrder()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RegisterOrder(OrderViewModel model)
        {
            var registerOrderCommand = new RegisterOrderCommand(model);

            //Send RegisterOrderCommand

            return View("Thanks");
        }
    }
}
