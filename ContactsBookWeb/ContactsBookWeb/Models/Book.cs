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

        public static List<Contact> SearchContacts(string searchData)
        {
            List<Contact> searchResult = new List<Contact>();
            if (searchData == "")
            {
                return Contacts;
            }
            if (searchData != null)
            {
                searchResult.AddRange(
                    Contacts.Where(
                        contact =>
                            contact.FirstName.Contains(searchData, StringComparison.OrdinalIgnoreCase) || contact.SecondName.Contains(searchData, StringComparison.OrdinalIgnoreCase) ||
                            contact.PhoneNumber.Contains(searchData, StringComparison.OrdinalIgnoreCase) ));
                
            }
            return searchResult;
        }

        public static List<Contact> SortSearchResult(string sortBy, List<Contact> searchResult)
        {
            switch (sortBy)
            {
                case "First name": searchResult.Sort(CompareContactsByFirstName); break;
                case "Second name": searchResult.Sort(CompareContactsBySecondName); break;
                case "Year of birth": searchResult.Sort(CompareContactsByBirthYear); break;
            }
            return searchResult;
        }

        public static bool Contains(this string source, string toCheck, StringComparison comparsion)
        {
            return source.IndexOf(toCheck, comparsion) >= 0;
        }

        public static void SortContacts(string sortBy)
        {
            switch (sortBy)
            {
                case "First name": Contacts.Sort(CompareContactsByFirstName); break;
                case "Second name": Contacts.Sort(CompareContactsBySecondName); break;
                case "Year of birth": Contacts.Sort(CompareContactsByBirthYear); break;
            }
        }

        private static int CompareContactsByFirstName(Contact first, Contact second)
        {
            return String.Compare(first.FirstName, second.FirstName, StringComparison.OrdinalIgnoreCase);
        }
        private static int CompareContactsBySecondName(Contact first, Contact second)
        {
            return String.Compare(first.SecondName, second.SecondName, StringComparison.OrdinalIgnoreCase);
        }

        private static int CompareContactsByBirthYear(Contact first, Contact second)
        {
            return first.BirthYear.CompareTo(second.BirthYear);
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
