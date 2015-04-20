using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace ContactsBookWeb.Models
{
    /// <summary>
    /// Contains list of all contacts and methods for managing contacts
    /// </summary>
    public static class Book
    {
        public static List<Contact> Contacts = new List<Contact>();
        /// <summary>
        /// Adds new contact to contact list
        /// </summary>
        /// <param name="newContact"></param>
        public static void AddContact(Contact newContact)
        {
            int idCount = Contacts.Count == 0 ? 0 : Contacts.Max(cnt => cnt.Id) + 1;
            newContact.Id = idCount;
            Contacts.Add(newContact);
        }
        /// <summary>
        /// Permanently removes contact from contact list
        /// </summary>
        /// <param name="id">Id of the contact which need to remove</param>
        public static void RemoveContact(int id)
        {
            if (id < 0) return;
            Contact contact = Contacts.Find(cnt => cnt.Id == id);
            Contacts.Remove(contact);
        }
        /// <summary>
        /// Edits contact and saves chages to contact list
        /// </summary>
        /// <param name="editedContact">Contact with altered properties</param>
        public static void EditContact(Contact editedContact)
        {
            if (editedContact.Id < 0) return;
            Contact contact = Contacts.Find(cnt => cnt.Id == editedContact.Id);
            contact.BirthYear = editedContact.BirthYear;
            contact.FirstName = editedContact.FirstName;
            contact.SecondName = editedContact.SecondName;
            contact.PhoneNumber = editedContact.PhoneNumber;
        }
        /// <summary>
        /// Saves a list of contacts in the specified path
        /// </summary>
        /// <param name="path">A path to the file. The path must contain name and extension of the file</param>
        public static void SaveToXml(string path)
        {
            
            XmlSerializer writer = new XmlSerializer(Contacts.GetType());
            using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                writer.Serialize(fs, Contacts);
            }
        }
        /// <summary>
        /// Reads list of contacts from XML file
        /// </summary>
        /// <param name="path">A path to the file. The path must contain name and extension of the file</param>
        public static void LoadFromXml(string path)
        {
            if (!File.Exists(path)) return;
            XmlSerializer reader = new XmlSerializer(Contacts.GetType());
            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                Contacts = (List<Contact>) reader.Deserialize(fs);
            }
        }
    }


}
