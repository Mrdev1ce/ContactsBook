﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mime;
using System.Web.Mvc;
using ContactsBookWeb.Models;

namespace ContactsBookWeb.Controllers
{
    public class ContactsController : Controller
    {
            //
        // GET: /Contacts/
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
                return Json(new {success = true});
            }
            return View(contact);
        }

        public ActionResult DeleteContact(int? id)
        {
            if (id == null || id < 0) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Book.RemoveContact((int)id);
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
                return Json(new { success = true });
            }
            return View(contact);
        }

        public ActionResult SaveContactsToXml()
        {
            string path = Server.MapPath("/Content/SavedContacts/SavedContacts.xml");
            Book.SaveToXml(path);
            return RedirectToAction("Index", "Home");
        }
        public ActionResult DownloadListOfContacts()
        {
            string path = Server.MapPath("/Content/SavedContacts/SavedContacts.xml");
            if (!System.IO.File.Exists(path)) return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            byte[] fileBytes = System.IO.File.ReadAllBytes(path);
            return File(fileBytes, MediaTypeNames.Application.Octet, "SavedContacts.xml");
        }
	}
}