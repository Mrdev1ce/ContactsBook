using System.Net;
using System.Net.Mime;
using System.Web.Mvc;
using ContactsBookWeb.Models;

namespace ContactsBookWeb.Controllers
{
    public class ContactsController : Controller
    {
        
        [HttpGet]
        public ActionResult EditContact(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = Book.Contacts.Find(cnt => cnt.Id == id);
            return View(contact);
        }

        [HttpPost]
        public ActionResult EditContact(Contact contact)
        {
            if (ModelState.IsValid)
            {
                Book.EditContact(contact);
                string path = Server.MapPath("/Content/SavedContacts/SavedContacts.xml");
                Book.SaveToXml(path);
                return Json(new {success = true});
            }
            return View(contact);
        }

        public ActionResult DeleteContact(int? id)
        {
            if (id == null || id < 0) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Book.RemoveContact((int)id);
            string path = Server.MapPath("/Content/SavedContacts/SavedContacts.xml");
            Book.SaveToXml(path);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult CreateContact()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateContact(Contact contact)
        {
            if (ModelState.IsValid)
            {
                Book.AddContact(contact);
                string path = Server.MapPath("/Content/SavedContacts/SavedContacts.xml");
                Book.SaveToXml(path);
                return Json(new { success = true });
            }
            return View(contact);
        }

        /// <summary>
        /// Calls method that saves a contact list on the server in /Content/SavedContacts
        /// </summary>
        /// <returns></returns>
        public ActionResult SaveContactsToXml()
        {
            string path = Server.MapPath("/Content/SavedContacts/SavedContacts.xml");
            Book.SaveToXml(path);
            return RedirectToAction("Index", "Home");
        }
        /// <summary>
        /// Provides a link to download the contacts list from the server
        /// </summary>
        /// <returns></returns>
        public ActionResult DownloadListOfContacts()
        {
            string path = Server.MapPath("/Content/SavedContacts/SavedContacts.xml");
            if (!System.IO.File.Exists(path)) return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            byte[] fileBytes = System.IO.File.ReadAllBytes(path);
            return File(fileBytes, MediaTypeNames.Application.Octet, "SavedContacts.xml");
        }
	}
}