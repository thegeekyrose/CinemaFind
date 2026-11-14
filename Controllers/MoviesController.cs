
using CinemaFind.Algorithms;
using CinemaFind.Data;
using CinemaFind.Models;
using CinemaFind.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace CinemaFind.Controllers
{
    [AllowAnonymous]
    public class MoviesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public MoviesController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: /Movies
        public async Task<IActionResult> Index(
            [FromQuery] MovieIndexViewModel model)
        {
            int currentYear = DateTime.Today.Year;

            // Additional validation: do not allow future release years.
            if (model.FromYear.HasValue &&
                model.FromYear.Value > currentYear)
            {
                ModelState.AddModelError(
                    nameof(model.FromYear),
                    $"The year cannot be greater than {currentYear}.");
            }

            if (model.ToYear.HasValue &&
                model.ToYear.Value > currentYear)
            {
                ModelState.AddModelError(
                    nameof(model.ToYear),
                    $"The year cannot be greater than {currentYear}.");
            }

            // Validate the relationship between the two boundaries.
            if (model.FromYear.HasValue &&
                model.ToYear.HasValue &&
                model.FromYear.Value > model.ToYear.Value)
            {
                ModelState.AddModelError(
                    nameof(model.ToYear),
                    "The ending year must be greater than or equal to the starting year.");
            }

            var query = _context.Films
                .Include(f => f.FilmGenres)
                    .ThenInclude(fg => fg.Genre)
                .AsQueryable();

            // Search by movie title.
            if (!string.IsNullOrWhiteSpace(model.SearchQuery))
            {
                query = query.Where(f =>
                    f.Title.Contains(model.SearchQuery));
            }

            // Filter by genre.
            if (model.SelectedGenreId.HasValue)
            {
                query = query.Where(f =>
                    f.FilmGenres.Any(fg =>
                        fg.GenreId == model.SelectedGenreId.Value));
            }

            // Apply year filters only if both entries are valid.
            bool validYearFilters =
                ModelState.GetFieldValidationState(
                    nameof(model.FromYear)) !=
                    Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid
                &&
                ModelState.GetFieldValidationState(
                    nameof(model.ToYear)) !=
                    Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid;

            if (validYearFilters)
            {
                // Lower boundary: release year >= FromYear.
                if (model.FromYear.HasValue)
                {
                    query = query.Where(f =>
                        f.ReleaseDate.HasValue &&
                        f.ReleaseDate.Value.Year >= model.FromYear.Value);
                }

                // Upper boundary: release year <= ToYear.
                if (model.ToYear.HasValue)
                {
                    query = query.Where(f =>
                        f.ReleaseDate.HasValue &&
                        f.ReleaseDate.Value.Year <= model.ToYear.Value);
                }
            }

            // Retrieve the filtered results.
            var films = await query.ToListAsync();

            // Sorting.
            switch (model.SortOrder)
            {
                case "year":
                    films = films
                        .OrderBy(f => f.ReleaseDate)
                        .ToList();
                    break;

                case "year_desc":
                    films = films
                        .OrderByDescending(f => f.ReleaseDate)
                        .ToList();
                    break;

                case "quick":
                    QuickSort.Sort(films);
                    break;

                default:
                    films = films
                        .OrderBy(f => f.Title)
                        .ToList();
                    break;
            }

            model.Films = films;

            // Populate genre dropdown.
            model.Genres = await _context.Genres
                .OrderBy(g => g.Name)
                .Select(g => new SelectListItem
                {
                    Value = g.Id.ToString(),
                    Text = g.Name
                })
                .ToListAsync();

            return View(model);
        }

        // GET: /Movies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var film = await _context.Films
                .Include(f => f.FilmGenres)
                    .ThenInclude(fg => fg.Genre)
                .FirstOrDefaultAsync(f => f.Id == id);

            if (film == null)
            {
                return NotFound();
            }

            // Get overall movie rating statistics.
            var ratingsQuery = _context.UserRatings
                .Where(r => r.FilmId == film.Id);

            int ratingCount = await ratingsQuery.CountAsync();

            double? averageRating = ratingCount > 0
                ? await ratingsQuery.AverageAsync(r => (double)r.Score)
                : null;

            // Prepare user-specific information.
            int? userRating = null;
            int? viewingProgress = null;
            bool hasViewingHistory = false;

            var userId = _userManager.GetUserId(User);

            if (userId != null)
            {
                userRating = await _context.UserRatings
                    .Where(r =>
                        r.UserId == userId &&
                        r.FilmId == film.Id)
                    .Select(r => (int?)r.Score)
                    .FirstOrDefaultAsync();

                var history = await _context.ViewingHistories
                    .FirstOrDefaultAsync(h =>
                        h.UserId == userId &&
                        h.FilmId == film.Id);

                if (history != null)
                {
                    hasViewingHistory = true;
                    viewingProgress = history.Progress;
                }
            }

            var viewModel = new MovieDetailsViewModel
            {
                Film = film,
                AverageRating = averageRating,
                RatingCount = ratingCount,
                UserRating = userRating,
                ViewingProgress = viewingProgress,
                HasViewingHistory = hasViewingHistory
            };

            return View(viewModel);
        }

    }
}
