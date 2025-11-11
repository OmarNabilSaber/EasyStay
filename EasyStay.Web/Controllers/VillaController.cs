using EasyStay.Domain.Entities;
using EasyStay.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace EasyStay.Web.Controllers
{
    public class VillaController : Controller
    {
        private readonly ApplicationDbContext _db;
        public VillaController(ApplicationDbContext db)
        {
            _db = db;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var villas = _db.Villas.ToList();
            return View(villas);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Villa obj)
        {
            
            if (ModelState.IsValid)
            {
                _db.Villas.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "The villa has been created successfully";
                return RedirectToAction("Index","Villa");
            }
            TempData["error"] = "The villa can't be created";
            return View(obj);
        }
        [HttpGet]
        public IActionResult Update(int villaId)
        {
            Villa? villa = _db.Villas.FirstOrDefault(u => u.Id == villaId);
            if (villa is null)
            {
                return RedirectToAction("Error","Home");
            }
            return View(villa);
        }
        [HttpPost]
        public IActionResult Update(Villa obj)
        {
            if (ModelState.IsValid)
            {
                _db.Villas.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "The villa has been updated successfully";
                return RedirectToAction("Index", "Villa");
            }
            TempData["error"] = "The villa can't be updated";
            return View(obj);
        }
        [HttpGet]
        public IActionResult Delete(int villaId)
        {
            Villa? villa = _db.Villas.FirstOrDefault(u => u.Id == villaId);
            if (villa is null)
            {
                return RedirectToAction("Error","Home");
            }
            return View(villa);
        }
        [HttpPost]
        public IActionResult Delete(Villa obj)
        {
            Villa? villaFromDb = _db.Villas.FirstOrDefault(v => v.Id == obj.Id);

            if (villaFromDb is not null)
            {
                _db.Villas.Remove(villaFromDb);
                _db.SaveChanges();
                TempData["success"] = "The villa has been deleted successfully";
                return RedirectToAction("Index", "Villa");
            }
            TempData["error"] = "The villa can't be deleted";
            return View(obj);
        }
    }
}
