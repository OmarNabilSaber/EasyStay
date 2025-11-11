using EasyStay.Domain.Entities;
using EasyStay.Infrastructure.Data;
using EasyStay.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EasyStay.Web.Controllers
{
    public class VillaNumberController : Controller
    {
        private readonly ApplicationDbContext _db;
        public VillaNumberController(ApplicationDbContext db)
        {
            _db = db;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var villaNumbers = _db.VillaNumbers.Include(vn => vn.Villa).ToList();
            return View(villaNumbers);
        }
        [HttpGet]
        public IActionResult Create()
        {
            VillaNumberVM villaNumber = new()
            {
                VillaList = _db.Villas.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                })
            };
            return View(villaNumber);
        }
        [HttpPost]
        public IActionResult Create(VillaNumberVM obj)
        {
            var isVillaNumberExists = _db.VillaNumbers.Any(vn => vn.Villa_Number == obj.VillaNumber.Villa_Number);
            
            if (ModelState.IsValid && !isVillaNumberExists)
            {
                _db.VillaNumbers.Add(obj.VillaNumber);
                _db.SaveChanges();
                TempData["success"] = "The villa Number has been created successfully";
                return RedirectToAction("Index","VillaNumber");
            }
            if (isVillaNumberExists)
            {
                TempData["error"] = "The villa Number aleady exists";
            }
            obj.VillaList = _db.Villas.Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });
            return View(obj);
        }
        [HttpGet]
        public IActionResult Update(int villaNumberId)
        {
            
            VillaNumberVM villaNumber = new()
            {
                VillaList = _db.Villas.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
                VillaNumber = _db.VillaNumbers.FirstOrDefault(u => u.Villa_Number == villaNumberId)
            };
            if (villaNumber.VillaNumber is null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(villaNumber);
        }
        [HttpPost]
        public IActionResult Update(VillaNumberVM villaNumberVM)
        {
            if (ModelState.IsValid)
            {
                _db.VillaNumbers.Add(villaNumberVM.VillaNumber);
                _db.SaveChanges();
                TempData["success"] = "The villa Number has been created successfully";
                return RedirectToAction("Index", "VillaNumber");
            }

            villaNumberVM.VillaList = _db.Villas.Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });
            return View(villaNumberVM);
        }
        [HttpGet]
        public IActionResult Delete(int villaNumberId)
        {
            VillaNumber? obj = _db.VillaNumbers.FirstOrDefault(u => u.Villa_Number == villaNumberId);
            if (obj is null)
            {
                return RedirectToAction("Error","Home");
            }
            return View(obj);
        }
        [HttpPost]
        public IActionResult Delete(VillaNumber obj)
        {
            VillaNumber? objFromDb = _db.VillaNumbers.FirstOrDefault(v => v.Villa_Number == obj.Villa_Number);

            if (objFromDb is not null)
            {
                _db.VillaNumbers.Remove(objFromDb);
                _db.SaveChanges();
                TempData["success"] = "The villa Number has been deleted successfully";
                return RedirectToAction("Index", "VillaNumber");
            }
            TempData["error"] = "The villa Number can't be deleted";
            return View(obj);
        }
    }
}
