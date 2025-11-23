using EasyStay.Application.Common.Interfaces;
using EasyStay.Domain.Entities;
using EasyStay.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace EasyStay.Web.Controllers
{
    public class VillaController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public VillaController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
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
                if(obj.Image is not null)
                {
                    var imageName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(_webHostEnvironment.WebRootPath, @"images\Villas");
                    var extension = Path.GetExtension(obj.Image.FileName);

                    if (!Directory.Exists(uploads))
                        Directory.CreateDirectory(uploads);

                    using (var fileStreams = new FileStream(Path.Combine(uploads, imageName + extension), FileMode.Create))
                    {
                        obj.Image.CopyTo(fileStreams);
                    }
                    obj.ImageUrl = @"\images\Villas\" + imageName + extension;
                }
                else
                {
                    obj.ImageUrl = "https://placehold.co/600x400";
                }

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
                if (obj.Image is not null)
                {
                    var imageName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(_webHostEnvironment.WebRootPath, @"images\Villas");
                    var extension = Path.GetExtension(obj.Image.FileName);

                    if (!Directory.Exists(uploads))
                        Directory.CreateDirectory(uploads);

                    if (obj.ImageUrl is not null)
                    {
                        var oldFilePath = Path.Combine(_webHostEnvironment.WebRootPath, obj.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldFilePath))
                            System.IO.File.Delete(oldFilePath);
                    }
                    using (var fileStreams = new FileStream(Path.Combine(uploads, imageName + extension), FileMode.Create))
                    {
                        obj.Image.CopyTo(fileStreams);
                    }
                    obj.ImageUrl = @"\images\Villas\" + imageName + extension;
                }
                else
                {
                    if (obj.ImageUrl is null)
                        obj.ImageUrl = "https://placehold.co/600x400";
                }
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
                if (villaFromDb.ImageUrl is not null)
                {
                    var filePath = Path.Combine(_webHostEnvironment.WebRootPath, villaFromDb.ImageUrl.TrimStart('\\'));
                    if (System.IO.File.Exists(filePath))
                        System.IO.File.Delete(filePath);
                }
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
