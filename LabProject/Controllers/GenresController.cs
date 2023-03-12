﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LabProject.Models;

namespace LabProject.Controllers
{
    public class GenresController : Controller
    {
        private readonly CinemaContext _context;

        public GenresController(CinemaContext context)
        {
            _context = context;
        }

        // GET: Genres
        public async Task<IActionResult> Index(int? movieId)
        {
            //var dbGenreContext = _context.Genres.Include(g => g.GenreName);
            ViewBag.MovieId = movieId;
            return View(await _context.Genres.ToListAsync());
        }

        // GET: Genres/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Genres == null)
            {
                return NotFound();
            }

            var genre = await _context.Genres
                .FirstOrDefaultAsync(m => m.GenreId == id);
            if (genre == null)
            {
                return NotFound();
            }

            return View(genre);
        }

        // GET: Genres/Create
        public IActionResult Create()
        {
            return View();
        }

        public async Task<IActionResult> AddGenre(int? id)
        {
            //var MovieGenre = _context.MovieGenres.Where(b => b.MovieId == movieId).Where(b => b.GenreId == genreId).FirstOrDefault();
            if (id == null || _context.Genres == null)
            {
                return NotFound();
            }
            //if(MovieGenre != null)
            //{
            //    // You already add this genre
            //}
            var genres = await _context.Genres.ToArrayAsync();
            ViewBag.MovieId = id;
            return View(genres);

        }

        // POST: Genres/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GenreId,GenreName")] Genre genre)
        {
            if (ModelState.IsValid)
            {
                _context.Add(genre);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(genre);
        }

        // GET: Genres/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Genres == null)
            {
                return NotFound();
            }

            var genre = await _context.Genres.FindAsync(id);
            if (genre == null)
            {
                return NotFound();
            }
            return View(genre);
        }

        // POST: Genres/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GenreId,GenreName")] Genre genre)
        {
            if (id != genre.GenreId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(genre);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GenreExists(genre.GenreId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(genre);
        }

        // GET: Genres/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Genres == null)
            {
                return NotFound();
            }

            var genre = await _context.Genres
                .FirstOrDefaultAsync(m => m.GenreId == id);
            if (genre == null)
            {
                return NotFound();
            }

            return View(genre);
        }

        // POST: Genres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Genres == null)
            {
                return Problem("Entity set 'CinemaContext.Genres'  is null.");
            }
            var genre = await _context.Genres.FindAsync(id);
            if (genre != null)
            {
                _context.Genres.Remove(genre);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GenreExists(int id)
        {
            return _context.Genres.Any(e => e.GenreId == id);
        }


        // Genre/Confrim
        public async Task<IActionResult> ConfirmGenre(int genreId, int movieId)
        {
            var MovieGenre = _context.MovieGenres.Where(b => b.MovieId == movieId).Where(b => b.GenreId == genreId).FirstOrDefault();
            if (MovieGenre != null)
            {
                // You already add this genre
                return RedirectToAction("Index", "Genres", new { movieId });
            }
            var movieGenre = new MovieGenre
            {
                GenreId = genreId,
                MovieId = movieId
            };

            _context.MovieGenres.Add(movieGenre);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Genres", new { movieId });
        }
        // redirect to MovieCast
        public async Task<IActionResult> ProceedToMovieCast(int movieId)
        {
            var MovieGenre = _context.MovieGenres.Where(m => m.MovieId == movieId).FirstOrDefault();
            if (MovieGenre == null)
            {
                return RedirectToAction("Index", new { movieId = movieId });
            }
            return RedirectToAction("Create", "MovieCasts", new { movieId = movieId });
        }

    }
}
