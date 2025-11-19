using EasyStay.Application.Common.Interfaces;
using EasyStay.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EasyStay.Web.Controllers
{
    public class AmenityController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public AmenityController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var Amenities = _unitOfWork.Amenity.GetAll(includeProperties:"Villa");
            return View(Amenities);
        }
        [HttpGet]
        public IActionResult Create()
        {
            AmenityVM amenityVM = new()
            {
                VillaList = _unitOfWork.Villa.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                })
            };
            return View(amenityVM);
        }
        [HttpPost]
        public IActionResult Create(AmenityVM amenityVM)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Amenity.Add(amenityVM.Amenity);
                _unitOfWork.Save();
                TempData["success"] = "The villa Amenity has been created successfully";
                return RedirectToAction("Index");
            }
            amenityVM.VillaList = _unitOfWork.Villa.GetAll().Select(i => new SelectListItem
                        {
                            Text = i.Name,
                            Value = i.Id.ToString()
                        });
            TempData["error"] = "The villa Amenity can't be created";
            return View(amenityVM);
        }
        [HttpGet]
        public IActionResult Update(int AmenityId)
        {
            AmenityVM amenityVM = new()
            {
                Amenity = _unitOfWork.Amenity.Get(u => u.Id == AmenityId),
                VillaList = _unitOfWork.Villa.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                })
            };
            if (amenityVM.Amenity is null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(amenityVM);
        }
        [HttpPost]
        public IActionResult Update(AmenityVM amenityVM)
        {
            if (ModelState.IsValid)
            {

                _unitOfWork.Amenity.Update(amenityVM.Amenity);
                _unitOfWork.Save();
                TempData["success"] = "The villa Number has been updated successfully";
                return RedirectToAction(nameof(Index));
                
            }
            amenityVM.VillaList = _unitOfWork.Villa.GetAll().Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });
            TempData["error"]  = "The villa Amenity can't be updated";
            return View(amenityVM);
        }
        [HttpGet]
        public IActionResult Delete(int AmenityId)
        {
            AmenityVM amenityVM = new()
            {
                Amenity = _unitOfWork.Amenity.Get(u => u.Id == AmenityId),
                VillaList = _unitOfWork.Villa.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                })
            };
            if (amenityVM.Amenity is null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(amenityVM);
        }
        [HttpPost]
        public IActionResult Delete(AmenityVM amenityVM)
        {
            var objFromDb = _unitOfWork.Amenity.Get(u => u.Id == amenityVM.Amenity.Id);
            if (objFromDb is not null)
            {
                _unitOfWork.Amenity.Remove(objFromDb);
                _unitOfWork.Save();
                TempData["success"] = "The villa Number has been Deleted successfully";
                return RedirectToAction(nameof(Index));

            }
            amenityVM.VillaList = _unitOfWork.Villa.GetAll().Select(i => new SelectListItem
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });
            TempData["error"] = "The villa Amenity can't be Deleted";
            return View(amenityVM);
        }
    }
}
