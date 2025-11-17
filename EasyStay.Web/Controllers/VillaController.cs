using EasyStay.Application.Common.Interfaces;
using EasyStay.Domain.Entities;
using EasyStay.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace EasyStay.Web.Controllers
{
    public class VillaController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public VillaController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var villas = _unitOfWork.Villa.GetAll();
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
                _unitOfWork.Villa.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "The villa has been created successfully";
                return RedirectToAction(nameof(Index));
            }
            TempData["error"] = "The villa can't be created";
            return View(obj);
        }
        [HttpGet]
        public IActionResult Update(int villaId)
        {
            Villa? villa = _unitOfWork.Villa.Get(u => u.Id == villaId);
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
                _unitOfWork.Villa.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "The villa has been updated successfully";
                return RedirectToAction(nameof(Index));
            }
            TempData["error"] = "The villa can't be updated";
            return View(obj);
        }
        [HttpGet]
        public IActionResult Delete(int villaId)
        {
            Villa? villa = _unitOfWork.Villa.Get(u => u.Id == villaId);
            if (villa is null)
            {
                return RedirectToAction("Error","Home");
            }
            return View(villa);
        }
        [HttpPost]
        public IActionResult Delete(Villa obj)
        {
            Villa? villaFromDb = _unitOfWork.Villa.Get(v => v.Id == obj.Id);

            if (villaFromDb is not null)
            {
                _unitOfWork.Villa.Remove(villaFromDb);
                _unitOfWork.Save();
                TempData["success"] = "The villa has been deleted successfully";
                return RedirectToAction(nameof(Index));
            }
            TempData["error"] = "The villa can't be deleted";
            return View(obj);
        }
    }
}
