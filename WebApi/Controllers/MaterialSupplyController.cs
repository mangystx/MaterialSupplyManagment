using Microsoft.AspNetCore.Mvc;
using MaterialSupplyManagement;
using MaterialSupplyManagement.DAL;
using WebApi.Models;

namespace WebApi.Controllers;

public class MaterialSupplyController : Controller
{
    private readonly MaterialSupplyRepository _repository;

    public MaterialSupplyController(MaterialSupplyRepository repository)
    {
        _repository = repository;
    }

    public async Task<IActionResult> Index()
    {
        ViewBag.RawMaterials = await _repository.GetAllRawMaterials();
        ViewBag.InventoryItems = await _repository.GetAllInventoryItems(); 
        ViewBag.ActiveTab = "#raw-materials-tab";
        
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> AddRawMaterial(RawMaterial rawMaterial)
    {
        if (ModelState.IsValid)
        {
            await _repository.AddRawMaterial(rawMaterial);
        }
        
        ViewBag.RawMaterials = await _repository.GetAllRawMaterials();
        ViewBag.InventoryItems = await _repository.GetAllInventoryItems();
        ViewBag.ActiveTab = "#raw-materials-tab";
        
        return View("Index");
    }

    [HttpPost]
    public async Task<IActionResult> AddInventoryItem(InventoryItemViewModel inventoryItem)
    {
        if (ModelState.IsValid)
        { 
            await _repository.AddInventoryItem(ViewModelToInventoryItem(inventoryItem));
        }
        
        ViewBag.RawMaterials = await _repository.GetAllRawMaterials();
        ViewBag.InventoryItems = await _repository.GetAllInventoryItems();
        ViewBag.ActiveTab = "#inventory-items-tab";
        
        return View("Index");
    }

    [HttpPost]
    public async Task<IActionResult> DeleteRawMaterial(string name)
    {
        var rawMaterial = await _repository.GetRawMaterial(name);
        if (rawMaterial != null)
        {
            await _repository.DeleteRawMaterial(rawMaterial);
        }

        ViewBag.RawMaterials = await _repository.GetAllRawMaterials();
        ViewBag.InventoryItems = await _repository.GetAllInventoryItems();
        ViewBag.ActiveTab = "#raw-materials-tab";
        
        return View("Index");
    }

    [HttpPost]
    public async Task<IActionResult> DeleteInventoryItem(string name)
    {
        var inventoryItem = await _repository.GetInventoryItemByName(name);
        if (inventoryItem != null)
        {
            await _repository.DeleteInventoryItem(inventoryItem);
        }

        ViewBag.RawMaterials = await _repository.GetAllRawMaterials();
        ViewBag.InventoryItems = await _repository.GetAllInventoryItems();
        ViewBag.ActiveTab = "#inventory-items-tab";
        
        return View("Index");
    }

    [HttpPost]
    public async Task<IActionResult> UpdateRawMaterial(RawMaterial rawMaterial)
    {
        if (ModelState.IsValid)
        {
            await _repository.UpdateRawMaterial(rawMaterial);
        }

        ViewBag.RawMaterials = await _repository.GetAllRawMaterials();
        ViewBag.InventoryItems = await _repository.GetAllInventoryItems();
        ViewBag.ActiveTab = "#raw-materials-tab";

        return View("Index");
    }

    [HttpPost]
    public async Task<IActionResult> UpdateInventoryItem(InventoryItemViewModel inventoryItem)
    {
        if (ModelState.IsValid)
        {
            await _repository.UpdateInventoryItem(ViewModelToInventoryItem(inventoryItem));
        }

        ViewBag.RawMaterials = await _repository.GetAllRawMaterials();
        ViewBag.InventoryItems = await _repository.GetAllInventoryItems();
        ViewBag.ActiveTab = "#inventory-items-tab";

        return View("Index");
    }

    private InventoryItem ViewModelToInventoryItem(InventoryItemViewModel inventoryItem)
    {
        return new InventoryItem(inventoryItem.Name, inventoryItem.Type, inventoryItem.Description, inventoryItem.Number, 
            inventoryItem.Price, new Manufacturer(inventoryItem.ManufacturerName, inventoryItem.ManufacturerAddress));
    }
}