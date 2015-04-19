using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace ContactsBookWeb.Models
{
    public static class Book
    {
        public static List<Contact> Contacts = new List<Contact>();
        public static void AddContact(Contact newContact)
        {
            int idCount = Contacts.Count == 0 ? 0 : Contacts.Max(cnt => cnt.Id) + 1;
            newContact.Id = idCount;
            Contacts.Add(newContact);
        }

        public static void RemoveContact(int id)
        {
            if (id < 0) return;
            Contact contact = Contacts.Find(cnt => cnt.Id == id);
            Contacts.Remove(contact);
        }

        public static void EditContact(Contact editedContact)
        {
            if (editedContact.Id < 0) return;
            Contact contact = Contacts.Find(cnt => cnt.Id == editedContact.Id);
            contact.BirthYear = editedContact.BirthYear;
            contact.FirstName = editedContact.FirstName;
            contact.SecondName = editedContact.SecondName;
            contact.PhoneNumber = editedContact.PhoneNumber;
        }

        public static void SaveToXml(string path)
        {
            
            XmlSerializer writer = new XmlSerializer(Contacts.GetType());
            using (FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                writer.Serialize(fs, Contacts);
            }
        }

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
