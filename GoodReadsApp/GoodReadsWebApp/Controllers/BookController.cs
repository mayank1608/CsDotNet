using GoodReadsWebApp.BLRepo;
using GoodReadsWebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace GoodReadsWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookBL _bookBL;

        public BookController(IBookBL bookBL)
        {
            _bookBL = bookBL;
        }

        // API 1:
        // GET: api/reviews/by-book-title/harry
        [HttpGet("by-book-title/{title}")]
        public async Task<IActionResult> GetReviewsByBookTitle(string title)
        {
            var result = await _bookBL.GetReviewsByBookTitleAsync(title);

            if (result.StatusCode == 400)
            {
                return BadRequest(result);
            }

            if (result.StatusCode == 404)
            {
                return NotFound(result);
            }

            return Ok(result);
        }

        // API 2:
        // GET: api/reviews/by-author/Robert Martin
        [HttpGet("by-author/{authorName}")]
        public async Task<IActionResult> GetHighRatedReviewsByAuthorName(string authorName)
        {
            var result = await _bookBL
                .GetHighRatedReviewsByAuthorNameAsync(authorName);

            if (result.StatusCode == 400)
            {
                return BadRequest(result);
            }

            if (result.StatusCode == 404)
            {
                return NotFound(result);
            }

            return Ok(result);
        }

        // Optional helper endpoint used by CreatedAtAction
        // GET: api/reviews/1
        [HttpGet("{reviewId:int}")]
        public async Task<IActionResult> GetReviewById(int reviewId)
        {
            var result = await _bookBL.GetReviewByIdAsync(reviewId);

            if (result.StatusCode == 400)
            {
                return BadRequest(result);
            }

            if (result.StatusCode == 404)
            {
                return NotFound(result);
            }

            return Ok(result);
        }

        // API 3:
        // POST: api/reviews
        [HttpPost]
        public async Task<IActionResult> AddReview([FromBody] AddReviewRequest request)
        {
            var result = await _bookBL.AddReviewAsync(request);

            if (result.StatusCode == 400)
            {
                return BadRequest(result);
            }

            return CreatedAtAction(
                nameof(GetReviewById),
                new { reviewId = result.Data!.ReviewId },
                result);
        }

        // API 4:
        // PUT: api/reviews/1
        [HttpPut("{reviewId:int}")]
        public async Task<IActionResult> UpdateReview(
            int reviewId,
            [FromBody] UpdateReviewRequest request)
        {
            var result = await _bookBL.UpdateReviewAsync(reviewId, request);

            if (result.StatusCode == 400)
            {
                return BadRequest(result);
            }

            if (result.StatusCode == 404)
            {
                return NotFound(result);
            }

            return Ok(result);
        }

        // PATCH version also allowed:
        // PATCH: api/reviews/1
        [HttpPatch("{reviewId:int}")]
        public async Task<IActionResult> PatchReview(
            int reviewId,
            [FromBody] UpdateReviewRequest request)
        {
            var result = await _bookBL.UpdateReviewAsync(reviewId, request);

            if (result.StatusCode == 400)
            {
                return BadRequest(result);
            }

            if (result.StatusCode == 404)
            {
                return NotFound(result);
            }

            return Ok(result);
        }
    }
}