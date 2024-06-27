using OnlineStoreforElectronicComponents.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace OnlineStoreforElectronicComponents.Controllers;

[Authorize(Roles = nameof(Roles.Admin))]
public class ComponentController : Controller
{
    private readonly IComponentRepository _componentRepo;
    private readonly ICategoryRepository _categoryRepo;
    private readonly IFileService _fileService;

    public ComponentController(IComponentRepository componentRepo, ICategoryRepository categoryRepo, IFileService fileService)
    {
        _componentRepo = componentRepo;
        _categoryRepo = categoryRepo;
        _fileService = fileService;
    }

    public async Task<IActionResult> Index()
    {
        var components = await _componentRepo.GetComponents();
        return View(components);
    }

    public async Task<IActionResult> AddComponent()
    {
        var categorySelectList = (await _categoryRepo.GetCategories()).Select(category => new SelectListItem
        {
            Text = category.CategoryName,
            Value = category.Id.ToString(),
        });
        ComponentDTO componentToAdd = new() { CategoryList = categorySelectList };
        return View(componentToAdd);
    }

    [HttpPost]
    public async Task<IActionResult> AddComponent(ComponentDTO componentToAdd)
    {
        var categorySelectList = (await _categoryRepo.GetCategories()).Select(category => new SelectListItem
        {
            Text = category.CategoryName,
            Value = category.Id.ToString(),
        });
        componentToAdd.CategoryList = categorySelectList;

        if (!ModelState.IsValid)
            return View(componentToAdd);

        try
        {
            if (componentToAdd.ImageFile != null)
            {
                if(componentToAdd.ImageFile.Length> 1 * 1024 * 1024)
                {
                    throw new InvalidOperationException("Image file can not exceed 1 MB");
                }
                string[] allowedExtensions = [".jpeg",".jpg",".png"];
                string imageName=await _fileService.SaveFile(componentToAdd.ImageFile, allowedExtensions);
                componentToAdd.Image = imageName;
            }
            
            Component component = new()
            {
                Id = componentToAdd.Id,
                ComponentName = componentToAdd.ComponentName,
                PackageName = componentToAdd.PackageName,
                Image = componentToAdd.Image,
                CategoryId = componentToAdd.CategoryId,
                Price = componentToAdd.Price
            };
            await _componentRepo.AddComponent(component);
            TempData["successMessage"] = "Component is added successfully";
            return RedirectToAction(nameof(AddComponent));
        }
        catch (InvalidOperationException ex)
        {
            TempData["errorMessage"]= ex.Message;
            return View(componentToAdd);
        }
        catch (FileNotFoundException ex)
        {
            TempData["errorMessage"] = ex.Message;
            return View(componentToAdd);
        }
        catch (Exception ex)
        {
            TempData["errorMessage"] = "Error on saving data";
            return View(componentToAdd);
        }
    }

    public async Task<IActionResult> UpdateComponent(int id)
    {
        var component = await _componentRepo.GetComponentById(id);
        if(component==null)
        {
            TempData["errorMessage"] = $"Component with the id: {id} does not found";
            return RedirectToAction(nameof(Index));
        }
        var categorySelectList = (await _categoryRepo.GetCategories()).Select(category => new SelectListItem
        {
            Text = category.CategoryName,
            Value = category.Id.ToString(),
            Selected=category.Id==component.CategoryId
        });
        ComponentDTO componentToUpdate = new() 
        { 
            CategoryList = categorySelectList,
            ComponentName=component.ComponentName,
            PackageName=component.PackageName,
            CategoryId=component.CategoryId,
            Price=component.Price,
            Image=component.Image 
        };
        return View(componentToUpdate);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateComponent(ComponentDTO componentToUpdate)
    {
        var categorySelectList = (await _categoryRepo.GetCategories()).Select(category => new SelectListItem
        {
            Text = category.CategoryName,
            Value = category.Id.ToString(),
            Selected=category.Id==componentToUpdate.CategoryId
        });
        componentToUpdate.CategoryList = categorySelectList;

        if (!ModelState.IsValid)
            return View(componentToUpdate);

        try
        {
            string oldImage = "";
            if (componentToUpdate.ImageFile != null)
            {
                if (componentToUpdate.ImageFile.Length > 1 * 1024 * 1024)
                {
                    throw new InvalidOperationException("Image file can not exceed 1 MB");
                }
                string[] allowedExtensions = [".jpeg", ".jpg", ".png"];
                string imageName = await _fileService.SaveFile(componentToUpdate.ImageFile, allowedExtensions);
               
                oldImage = componentToUpdate.Image;
                componentToUpdate.Image = imageName;
            }
           
            Component component = new()
            {
                Id=componentToUpdate.Id,
                ComponentName = componentToUpdate.ComponentName,
                PackageName = componentToUpdate.PackageName,
                CategoryId = componentToUpdate.CategoryId,
                Price = componentToUpdate.Price,
                Image = componentToUpdate.Image
            };
            await _componentRepo.UpdateComponent(component);
            
            if(!string.IsNullOrWhiteSpace(oldImage))
            {
                _fileService.DeleteFile(oldImage);
            }
            TempData["successMessage"] = "Component is updated successfully";
            return RedirectToAction(nameof(Index));
        }
        catch (InvalidOperationException ex)
        {
            TempData["errorMessage"] = ex.Message;
            return View(componentToUpdate);
        }
        catch (FileNotFoundException ex)
        {
            TempData["errorMessage"] = ex.Message;
            return View(componentToUpdate);
        }
        catch (Exception ex)
        {
            TempData["errorMessage"] = "Error on saving data";
            return View(componentToUpdate);
        }
    }

    public async Task<IActionResult> DeleteComponent(int id)
    {
        try
        {
            var component = await _componentRepo.GetComponentById(id);
            if (component == null)
            {
                TempData["errorMessage"] = $"Component with the id: {id} does not found";
            }
            else
            {
                await _componentRepo.DeleteComponent(component);
                if (!string.IsNullOrWhiteSpace(component.Image))
                {
                    _fileService.DeleteFile(component.Image);
                }
            }
        }
        catch (InvalidOperationException ex)
        {
            TempData["errorMessage"] = ex.Message;
        }
        catch (FileNotFoundException ex)
        {
            TempData["errorMessage"] = ex.Message;
        }
        catch (Exception ex)
        {
            TempData["errorMessage"] = "Error on deleting the data";
        }
        return RedirectToAction(nameof(Index));
    }

}
