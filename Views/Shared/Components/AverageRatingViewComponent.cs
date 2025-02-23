using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RPm_Gackan1.Models;
using System.Linq;
using System.Threading.Tasks;

namespace RPm_Gackan1.ViewComponents
{
    public class AverageRatingViewComponent : ViewComponent
    {
        private readonly FlorenceeContext _db;

        public AverageRatingViewComponent(FlorenceeContext db)
        {
            _db = db;
        }

        public async Task<IViewComponentResult> InvokeAsync(int catalogeId)
        {
            var reviews = await _db.Reviews
                .Where(r => r.CatalogeId == catalogeId)
                .ToListAsync();

            double averageRating = reviews.Any() ? reviews.Average(r => r.Rating) : 0;

            return View(averageRating);
        }
    }
}