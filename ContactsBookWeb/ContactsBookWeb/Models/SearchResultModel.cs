using System.Collections.Generic;

namespace ContactsBookWeb.Models
{
    public class SearchResultModel
    {
        public string SortBy { get; set; }
        public List<Contact> SearchResult { get; set; } 
    }
}