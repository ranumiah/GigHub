using GigHub.Controllers;
using GigHub.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace GigHub.ViewModels
{
    public class GigFormViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Venue { get; set; }

        [Required]
        [FutureDate]
        public string Date { get; set; }

        [Required]
        [ValidTime]
        public string Time { get; set; }

        [Required]
        public byte Genre { get; set; }

        public IEnumerable<Genre> Genres { get; set; }

        public string Heading { get; set; }

        public string Action
        {
            get
            {
                // Func is a Delegate, which allows us to call the anonymous method
                Expression<Func<GigsController, ActionResult>> update =
                    (c => c.Update(this)); // This is an anonymous method.

                // Expression ==> prevents the Func to be called instead it just represent the expression
                Expression<Func<GigsController, ActionResult>> create =
                    (c => c.Create(this));

                var action = Id != 0 ? update : create;

                // Any change to the actuall class methods will be pick up here.
                // Therefore avoiding the pitfull of magic string.
                var methodCallExpression = action.Body as MethodCallExpression;

                // Check for null; If null then return null ELSE return string
                return methodCallExpression?.Method.Name;
            }
        }

        public DateTime GetDateTime()
        {
            return DateTime.Parse($"{Date} {Time}");
        }
    }
}