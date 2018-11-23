using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Models;
using Vidly.Dtos;
using AutoMapper;

namespace Vidly.Controllers.Api
{
    public class MoviesController : ApiController
    {
        private ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        // GET /api/movies
        public IHttpActionResult GetMovies(string query = null)
        {
            var moviesQuery = _context.Movie
                .Include(c => c.Genre)
                .Where(m => m.NumberAvailable > 0);

            if (!String.IsNullOrWhiteSpace(query))
                moviesQuery = moviesQuery.Where(m => m.Name.Contains(query));

            var moviesDto=moviesQuery
                .ToList()
                .Select(Mapper.Map<Movie, MovieDto>);
            return Ok(moviesDto);
        }

        // GET /api/Movies/1
        public IHttpActionResult GetMovie(int id)
        {
            var movie = _context.Movie
                .SingleOrDefault(c => c.Id == id);
            if (movie == null)
                return NotFound();
            return Ok(Mapper.Map < Movie, MovieDto>(movie));
        }

        // POST /api/movies
        [HttpPost]
        public IHttpActionResult CreateMovie(MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var movie = Mapper.Map<MovieDto, Movie>(movieDto);
            _context.Movie.Add(movie);
            _context.SaveChanges();

            movieDto.Id = movie.Id;

            return Created(new Uri(Request.RequestUri + "/" + movie.Id), movieDto);
        }

        // PUT /api/movies/1
        [HttpPut]
        public void UpdateMovie(int id,MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var movieInDb = _context.Movie.SingleOrDefault(c => c.Id == id);

            if(movieInDb==null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            Mapper.Map(movieDto, movieInDb);

            _context.SaveChanges();
        }

        // DELETE /api/movies/1
        [HttpDelete]
        public void DeleteMovie(int id)
        {
            var movieInDb = _context.Movie.SingleOrDefault(c => c.Id == id);

            if (movieInDb==null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _context.Movie.Remove(movieInDb);
            _context.SaveChanges();
        }
    }
}