using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Minerva.Web.Models
{
    public class ThemeModel
    {
        public static List<ThemeModel> GetAll()
        {
            List<ThemeModel> Temalar = new List<ThemeModel>();
            Temalar.Add(new ThemeModel() {Name = "Elle eklenen tema 1 "});
            Temalar.Add(new ThemeModel() { Name = "Elle eklenen tema 2 " });
            Temalar.Add(new ThemeModel() { Name = "Elle eklenen tema 2 " });
            return Temalar;
        }

        public static ThemeModel GetFirst()
        {
            return new ThemeModel {Name = "Koddan gelen tema"};
        }

        public string Name;
        public List<BookModel> Books;
    }
}
