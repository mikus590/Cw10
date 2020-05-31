using Cw3.ORMModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Cw10
{
    class Program
    {
        static void Main(string[] args)
        {
            ShowStudent();
            UpdateStudent();
            Remove();

        }

        public static void ShowStudent()
        {
            var db = new s17371Context();
            var s1 = db.Student.First();

            s1.LastName = "LastNameUpdate";

            db.SaveChanges();
        }
        public static void UpdateStudent()
        {

            var db = new s17371Context();

            var res = db.Student.ToList();
        }
        public static void Remove() {

            var db = new s17371Context();

            var s2 = db.Student.OrderByDescending(s => s.IndexNumber).First();

            db.Student.Remove(s2);

            db.SaveChanges();
        }
    }
}
