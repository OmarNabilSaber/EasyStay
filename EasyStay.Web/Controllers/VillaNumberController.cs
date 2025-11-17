using EasyStay.Application.Common.Interfaces;
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
        private readonly IUnitOfWork _unitOfWork;
        public VillaNumberController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var villaNumbers = _unitOfWork.VillaNumber.GetAll(includeProperties : "Villa");
            return View(villaNumbers);
        }
        [HttpGet]
        public IActionResult Create()
        {
            VillaNumberVM villaNumber = new()
            {
                VillaList = _unitOfWork.Villa.GetAll().Select(i => new SelectListItem
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
            var isVillaNumberExists = _unitOfWork.VillaNumber.GetAll()
                .Any(vn => vn.Villa_Number == obj.VillaNumber.Villa_Number);
            
            if (ModelState.IsValid && !isVillaNumberExists)
            {
                _unitOfWork.VillaNumber.Add(obj.VillaNumber);
                _unitOfWork.Save();
                TempData["success"] = "The villa Number has been created successfully";
                return RedirectToAction(nameof(Index));
            }
            if (isVillaNumberExists)
            {
                TempData["error"] = "The villa Number aleady exists";
            }
            obj.VillaList = _unitOfWork.Villa.GetAll().Select(i => new SelectListItem
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
                VillaList = _unitOfWork.Villa.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
                VillaNumber = _unitOfWork.VillaNumber.Get(u => u.Villa_Number == villaNumberId)
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
                _unitOfWork.VillaNumber.Update(villaNumberVM.VillaNumber);
                _unitOfWork.Save();
                TempData["success"] = "The villa Number has been updated successfully";
                return RedirectToAction(nameof(Index));
            }

            villaNumberVM.VillaList = _unitOfWork.Villa.GetAll().Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });
            return View(villaNumberVM);
        }
        [HttpGet]
        public IActionResult Delete(int villaNumberId)
        {
            VillaNumberVM villaNumber = new()
            {
                VillaList = _unitOfWork.Villa.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
                VillaNumber = _unitOfWork.VillaNumber.Get(u => u.Villa_Number == villaNumberId)
            };
            if (villaNumber.VillaNumber is null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(villaNumber);
        }
        [HttpPost]
        public IActionResult Delete(VillaNumberVM villaNumberVM)
        {
            VillaNumber? villaNumberFromDb = _unitOfWork.VillaNumber.Get(v => v.Villa_Number == villaNumberVM.VillaNumber.Villa_Number);

            if (villaNumberFromDb is not null)
            {
                _unitOfWork.VillaNumber.Remove(villaNumberFromDb);
                _unitOfWork.Save();
                TempData["success"] = "The villa Number has been deleted successfully";
                return RedirectToAction(nameof(Index));
            }

            villaNumberVM.VillaList = _unitOfWork.Villa.GetAll().Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });
            return View(villaNumberVM);
        }
    }
}
