using AutoMapper;
using BLL_Proj.Interfaces;
using BLL_Proj.Repositories;
using DAL_Proj.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PL_Proj.Utilities;
using PL_Proj.ViewModels;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PL_Proj.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EmployeeController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task<IActionResult> Index(string SearchValue)
        {
            IEnumerable<Employee> employees;
            if (string.IsNullOrEmpty(SearchValue))
                 employees = await _unitOfWork.EmployeeRepo.GetAll();
            else
                employees = _unitOfWork.EmployeeRepo.SearchEmployeesByName(SearchValue);

            var EmpMapped = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employees);
            return View(EmpMapped);

        }


        public async Task<IActionResult> Create()
        {
            ViewBag.Departments = await _unitOfWork.DepartmentRepo.GetAll();
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeViewModel employee)
        {
            if (ModelState.IsValid)
            {
                employee.ImageName = DocumentSettings.UploadFile(employee.Image, "Images");
                var EmpMapped = _mapper.Map<EmployeeViewModel, Employee>(employee);
                await _unitOfWork.EmployeeRepo.Add(EmpMapped);
                await _unitOfWork.Complete();
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }


        public async Task<IActionResult> Details(int? Id, string viewName = "Details")
        {
            if (Id is null)
                return BadRequest();
            var employee = await _unitOfWork.EmployeeRepo.GetById(Id.Value);
            if (employee == null)
                return NotFound();
            var EmpMapped = _mapper.Map<Employee, EmployeeViewModel>(employee);
            return View(viewName, EmpMapped);
        }


        public async Task<IActionResult> Edit(int? Id) 
        {
            ViewBag.Departments = await _unitOfWork.DepartmentRepo.GetAll();

            return await Details(Id, "Edit");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EmployeeViewModel employee, [FromRoute] int Id)
        {
            if (Id != employee.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    employee.ImageName = DocumentSettings.UploadFile(employee.Image, "Images");
                    var EmpMapped = _mapper.Map<EmployeeViewModel, Employee>(employee);
                    _unitOfWork.EmployeeRepo.Update(EmpMapped);
                    await _unitOfWork.Complete();
                    return RedirectToAction(nameof(Index));
                }
                catch (System.Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }

            }
            return View(employee);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            return await Details(id, "Delete");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(EmployeeViewModel employee, [FromRoute] int id)
        {
            if (id != employee.Id)
                return BadRequest();
            try
            {
                var EmpMapped = _mapper.Map<EmployeeViewModel, Employee>(employee);
                _unitOfWork.EmployeeRepo.Delete(EmpMapped);
                var Result = await _unitOfWork.Complete();
                if (Result > 0)
                    DocumentSettings.DeleteFile(employee.ImageName, "Images");

                return RedirectToAction(nameof(Index));
                
            }
            catch (System.Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(employee);
            }
        }
    }
}
