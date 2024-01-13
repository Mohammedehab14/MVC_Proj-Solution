
using AutoMapper;
using BLL_Proj.Interfaces;
using DAL_Proj.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PL_Proj.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PL_Proj.Controllers
{
    [Authorize]
    public class DepartmentController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DepartmentController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var departments = await _unitOfWork.DepartmentRepo.GetAll();
            var DepMapped = _mapper.Map<IEnumerable<Department>, IEnumerable<DepartmentViewModel>>(departments);
            return View(DepMapped);
        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DepartmentViewModel department)
        {
            if(ModelState.IsValid)
            {
                var DepMapped = _mapper.Map<DepartmentViewModel, Department>(department);
                await _unitOfWork.DepartmentRepo.Add(DepMapped);
                await _unitOfWork.Complete();
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }



        public async Task<IActionResult> Details(int? Id, string viewName = "Details")
        {
            if (Id is null)
                return BadRequest();
            var department = await _unitOfWork.DepartmentRepo.GetById(Id.Value);
            if(department == null)
                return NotFound();
            var DepMapped = _mapper.Map<Department, DepartmentViewModel>(department);

            return View(viewName, DepMapped);
        }


        public async Task<IActionResult> Edit(int? Id)
        {
            return await Details(Id, "Edit");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(DepartmentViewModel department, [FromRoute] int Id)
        {
            if(Id != department.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    var DepMapped = _mapper.Map<DepartmentViewModel, Department>(department);
                    _unitOfWork.DepartmentRepo.Update(DepMapped);
                    await _unitOfWork.Complete();
                    return RedirectToAction(nameof(Index));
                }
                catch(System.Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
                
            }
            return View(department);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            return await Details(id, "Delete");
        }


        [HttpPost]
        public async Task<IActionResult> Delete(DepartmentViewModel department, [FromRoute] int id)
        {
            if(id != department.Id)
                return BadRequest();
            try
            {
                var DepMapped = _mapper.Map<DepartmentViewModel, Department>(department);
                _unitOfWork.DepartmentRepo.Delete(DepMapped);
                await _unitOfWork.Complete();
                return RedirectToAction(nameof(Index));
            }
            catch (System.Exception ex)
            {
                ModelState.AddModelError(string.Empty,ex.Message);
                return View(department);
            }
        }
    }
}
