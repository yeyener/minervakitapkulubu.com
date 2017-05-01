using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Minerva.Web.Models
{
    public class JoinUsModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int BirthDateDay { get; set; }
        public int BirthDateMonth { get; set; }
        public int BirthDateYear { get; set; }
        public string Occupation { get; set; }
        public string Email { get; set; }
        public string Gsm { get; set; }
        public string LastBook1 { get; set; }
        public string LastBook2 { get; set; }
        public string LastBook3 { get; set; }
        public string FavoriteBook { get; set; }
        public string FavoriteBookReason { get; set; }
        public string PreferedType1 { get; set; }
        public string PreferedAuthor1 { get; set; }
        public string PreferedType2 { get; set; }
        public string PreferedAuthor2 { get; set; }
        public string WhyBookClub { get; set; }
        public string Extra { get; set; }
        public bool Rule1 { get; set; }
        public bool Rule2 { get; set; }
        public bool Rule3 { get; set; }
        public bool Rule4 { get; set; }
        public bool Rule5 { get; set; }
        public bool Rule6 { get; set; }
    }
}
