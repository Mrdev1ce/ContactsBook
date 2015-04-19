using System.Web.Mvc;
using ContactsBookWeb.Models;

namespace ContactsBookWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (Book.Contacts.Count == 0)
            {
                Book.LoadFromXml(Server.MapPath("/Content/SavedContacts/SavedContacts.xml"));
            }

            return View(Book.Contacts);
        }
        
    }
}