using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RPm_Gackan1.Models;
using System.Diagnostics;

namespace RPm_Gackan1.Controllers
{
	public class CRUDController : Controller
	{
		public FlorenceeContext db;

		public CRUDController(FlorenceeContext context)
		{
			db = context;
		}

		public ActionResult Index()
		{
			return View();
		}

		public async Task<IActionResult> CRUD()
		{
			return View(await db.Cataloges.ToListAsync());
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(Cataloge cataloge)
		{
			db.Cataloges.Add(cataloge);
			await db.SaveChangesAsync();
			return RedirectToAction("CRUD");
		}

		public async Task<IActionResult> Details(int? id)
		{
			if (id != null)
			{
				Cataloge cataloge = await db.Cataloges.FirstOrDefaultAsync(x => x.IdCataloge == id);
				if (cataloge != null)
				{
					return View(cataloge);
				}
			}
			return NotFound();
		}

		public async Task<IActionResult> Edit(int? id)
		{
			if (id != null)
			{
				Cataloge cataloge = await db.Cataloges.FirstOrDefaultAsync(x => x.IdCataloge == id);
				if (cataloge != null)
				{
					return View(cataloge);
				}
			}
			return NotFound();
		}
		[HttpPost]

		public async Task<IActionResult> Edit(Cataloge cataloge)
		{
			db.Cataloges.Update(cataloge);
			await db.SaveChangesAsync();
			return RedirectToAction("CRUD");
		}

		[HttpGet]
		[ActionName("Delete")]
		public async Task<IActionResult> ConfirmDelete(int? id)
		{
			if (id != null)
			{
				Cataloge cataloge = await db.Cataloges.FirstOrDefaultAsync(x => x.IdCataloge == id);
				if (cataloge != null)
				{
					return View(cataloge);
				}
			}
			return NotFound();
		}

		[HttpPost]
		public async Task<IActionResult> Delete(int? id)
		{
			if (id != null)
			{
				Cataloge cataloge = await db.Cataloges.FirstOrDefaultAsync(x => x.IdCataloge == id);
				if (cataloge != null)
				{
					db.Cataloges.Remove(cataloge);
					await db.SaveChangesAsync();
					return RedirectToAction("CRUD");
				}
			}
			return NotFound();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
