using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OnlineStoreforElectronicComponents.Controllers
{
    [Authorize(Roles = nameof(Roles.Admin))]
    public class TypeOfComponentController : Controller
    {
        private readonly ITypeOfComponentRepository _typeOfComponentRepo;

        public TypeOfComponentController(ITypeOfComponentRepository typeOfComponentRepo)
        {
            _typeOfComponentRepo = typeOfComponentRepo;
        }

        public async Task<IActionResult> Index()
        {
            var genres = await _typeOfComponentRepo.GetTypeOfComponents();
            return View(genres);
        }

        public IActionResult AddTypeOfComponent()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddTypeOfComponent(TypeOfComponentDTO typeofcomponent)
        {
            if(!ModelState.IsValid)
            {
                return View(typeofcomponent);
            }
            try
            {
                var genreToAdd = new TypeOfComponent { TypeOfComponentName = typeofcomponent.TypeOfComponentName, Id = typeofcomponent.Id };
                await _typeOfComponentRepo.AddTypeOfComponent(genreToAdd);
                TempData["successMessage"] = "TypeOfComponent added successfully";
                return RedirectToAction(nameof(AddTypeOfComponent));
            }
            catch(Exception ex)
            {
                TempData["errorMessage"] = "TypeOfComponent could not added!";
                return View(typeofcomponent);
            }

        }

        public async Task<IActionResult> UpdateTypeOfComponent(int id)
        {
            var typeofcomponent = await _typeOfComponentRepo.GetTypeOfComponentById(id);
            if (typeofcomponent is null)
                throw new InvalidOperationException($"TypeOfComponent with id: {id} does not found");
            var typeOfComponentToUpdate = new TypeOfComponentDTO
            {
                Id = typeofcomponent.Id,
                TypeOfComponentName = typeofcomponent.TypeOfComponentName
            };
            return View(typeOfComponentToUpdate);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateTypeOfComponent(TypeOfComponentDTO typeOfComponentToUpdate)
        {
            if (!ModelState.IsValid)
            {
                return View(typeOfComponentToUpdate);
            }
            try
            {
                var typeofcomponent = new TypeOfComponent { TypeOfComponentName = typeOfComponentToUpdate.TypeOfComponentName, Id = typeOfComponentToUpdate.Id };
                await _typeOfComponentRepo.UpdateTypeOfComponent(typeofcomponent);
                TempData["successMessage"] = "TypeOfComponent is updated successfully";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = "TypeOfComponent could not updated!";
                return View(typeOfComponentToUpdate);
            }

        }

        public async Task<IActionResult> DeleteTypeOfComponent(int id)
        {
            var typeofcomponent = await _typeOfComponentRepo.GetTypeOfComponentById(id);
            if (typeofcomponent is null)
                throw new InvalidOperationException($"TypeOfComponent with id: {id} does not found");
            await _typeOfComponentRepo.DeleteTypeOfComponent(typeofcomponent);
            return RedirectToAction(nameof(Index));

        }

    }
}
