
using CinemaFind.Data;
using CinemaFind.Models;
using CinemaFind.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CinemaFind.Controllers
{
    [Authorize]
    public class UserActivityController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserActivityController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // POST: /UserActivity/RateMovie
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RateMovie(
            RateMovieViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] =
                    "Please provide a valid rating between 1 and 10.";

                return RedirectToAction(
                    "Details", "Movies", new { id = model.FilmId });
            }

            var filmExists = await _context.Films
                .AnyAsync(f => f.Id == model.FilmId);

            if (!filmExists)
            {
                return NotFound();
            }

            // Always obtain the user ID from the authenticated account.
            var userId = _userManager.GetUserId(User);

            if (userId == null)
            {
                return Challenge();
            }

            var rating = await _context.UserRatings
                .FirstOrDefaultAsync(r =>
                    r.UserId == userId &&
                    r.FilmId == model.FilmId);

            if (rating == null)
            {
                rating = new UserRating
                {
                    UserId = userId,
                    FilmId = model.FilmId,
                    Score = model.Score,
                    RatedAt = DateTime.UtcNow
                };

                _context.UserRatings.Add(rating);
            }
            else
            {
                rating.Score = model.Score;
                rating.RatedAt = DateTime.UtcNow;
            }

            await _context.SaveChangesAsync();

            TempData["StatusMessage"] = "Your rating has been saved.";

            return RedirectToAction(
                "Details", "Movies", new { id = model.FilmId });
        }

        // POST: /UserActivity/SaveViewingProgress
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveViewingProgress(
            ViewingProgressViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] =
                    "Please enter a valid progress value between 0 and 100.";

                return RedirectToAction(
                    "Details", "Movies", new { id = model.FilmId });
            }

            var filmExists = await _context.Films
                .AnyAsync(f => f.Id == model.FilmId);

            if (!filmExists)
            {
                return NotFound();
            }

            var userId = _userManager.GetUserId(User);

            if (userId == null)
            {
                return Challenge();
            }

            var history = await _context.ViewingHistories
                .FirstOrDefaultAsync(h =>
                    h.UserId == userId &&
                    h.FilmId == model.FilmId);

            if (history == null)
            {
                history = new ViewingHistory
                {
                    UserId = userId,
                    FilmId = model.FilmId
                };

                _context.ViewingHistories.Add(history);
            }

            history.Progress = model.Progress;
            history.Completed = model.Progress == 100;
            history.ViewedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            TempData["StatusMessage"] =
                model.Progress == 100
                    ? "Movie marked as completed."
                    : "Viewing progress has been saved.";

            return RedirectToAction(
                "Details", "Movies", new { id = model.FilmId });
        }
    }
}
