using System;
using System.ComponentModel.DataAnnotations;

namespace ContactsBookWeb.Models
{
    [Serializable]
    public class Contact
    {
        private int _id;
        private string _firstName;
        private string _secondName;
        private int _birthYear;
        private string _phoneNumber;

        [Required]
        [ScaffoldColumn(false)]
        public int Id
        {
            get { return _id; }
            set
            {
                if (value >= 0)
                {
                    _id = value;
                }
            }
        }
        [Required(ErrorMessage = "The field must be set")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "The string length must be between 2 and 50 characters")]
        [Display (Name = "First name")]
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                if (CheckName(value))
                {
                    _firstName = value;
                }
            }
        }
        [Required(ErrorMessage = "The field must be set")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "The string length must be between 2 and 50 characters")]
        [Display(Name = "Second name")]
        public string SecondName
        {
            get { return _secondName; }
            set
            {
                if (CheckName(value))
                {
                    _secondName = value;
                }
            }
        }
        [Required(ErrorMessage = "The field must be set")]
        [Range(1885, 2015, ErrorMessage = "Invalid year")]
        [Display(Name = "Birth year")]
        public int BirthYear
        {
            get { return _birthYear; }
            set
            {
                if (value > DateTime.Now.Year - 130 && value <= DateTime.Now.Year)
                {
                    _birthYear = value;
                }
            }
        }
        [Required(ErrorMessage = "The field must be set")]
        [StringLength(30, MinimumLength = 6, ErrorMessage = "The string length must be between 6 and 30 characters")]
        [RegularExpression(@"[0-9]+" , ErrorMessage = "Incorrect telephone number")]
        [Display(Name = "Phone number")]
        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set
            {
                if (!string.IsNullOrEmpty(value) && value.Length >= 6 && value.Length < 30)
                {
                    _phoneNumber = value;
                }
            }
        }
        private static bool CheckName(string value)
        {
            return !string.IsNullOrEmpty(value) && value.Length > 1 && value.Length < 30;
        }

        
    }
}
