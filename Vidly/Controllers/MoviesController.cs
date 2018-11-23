using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;
using System.Data.Entity;
using Vidly.Migrations;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        private ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Movies
        public ViewResult Index()
        {
            if (User.IsInRole(RoleName.CanManageMovies))
                return View("List");
            return View("ReadOnlyList");
        }

        [Authorize(Roles=RoleName.CanManageMovies)]
        public ViewResult New()
        {
            var genre = _context.Genre.ToList();
            var viewModel = new MovieFormViewModel
            {
                Genre=genre
            };
            return View("MovieForm", viewModel);
        }
        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult Edit(int id)
        {
            var movie = _context.Movie.SingleOrDefault(c => c.Id == id);

            if (movie == null)
                return HttpNotFound();

            var viewModel = new MovieFormViewModel(movie)
            {
                Genre = _context.Genre.ToList()
            };

            return View("MovieForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult Save(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new MovieFormViewModel(movie)
                {
                    Genre = _context.Genre.ToList()
                };

                return View("MovieForm", viewModel);
            }
            if (movie.Id == 0)
            {
                movie.DateAdded = DateTime.Now;
                _context.Movie.Add(movie);
            }
            else
            {
                var movieInDb = _context.Movie.Single(c => c.Id == movie.Id);

                movieInDb.Name = movie.Name;
                movieInDb.ReleaseDate = movie.ReleaseDate;
                movieInDb.GenreId = movie.GenreId;
                movieInDb.NumberInStock = movie.NumberInStock;
                movieInDb.DateAdded = movie.DateAdded;
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Movies");
        }

        public ActionResult Details(int id)
        {
            var movies = _context.Movie.Include(c => c.Genre).SingleOrDefault(c => c.Id == id);
            if (movies == null)
                return HttpNotFound();
            return View(movies);
        }

        //private IEnumerable<Movie> GetMovies()
        //{
        //    return new List<Movie>
        //    {
        //        new Movie { Id = 1, Name = "Shrek" },
        //        new Movie { Id = 2, Name = "Wall-e" }
        //    };
        //}
    }
}