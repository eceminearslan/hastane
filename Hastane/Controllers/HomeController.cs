using Hastane.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Hastane.Controllers;

public class HomeController : Controller
{
    private readonly HastaneContext _context = new HastaneContext();

    public IActionResult Index(int? id)
    {
        var branslar = _context.Branslars.ToList();
        var doktorlar = _context.Doktorlars.Include(x => x.Brans).ToList();//5 sütunlu tablo
        var allData = (branslar, doktorlar);

        if (string.IsNullOrWhiteSpace(id.ToString()))
        {
            return View(allData);
        }
        else
        {
            var filtreDoktorlar = _context.Doktorlars.Include(x => x.Brans).Where(x => x.BransId == id).ToList();
            allData.doktorlar = filtreDoktorlar;
            return View(allData);
        }
    }

    public IActionResult BransEkle()   
    {
        return View();
    }
    [HttpPost]
    public IActionResult BransEkle(Branslar brans)
    {
        _context.Branslars.Add(brans);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }
    public IActionResult DoktorEkle()  
    {
        var data = _context.Branslars.ToList();
        ViewBag.branslars = new SelectList(data, "BransId", "BransAd");
        return View();
    }
    [HttpPost]
    public IActionResult DoktorEkle(Doktorlar doktor)
    {
        _context.Doktorlars.Add(doktor);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }

    public IActionResult DeleteDoktor(int id){
        var data= _context.Doktorlars.FirstOrDefault(x=>x.DoktorId == id);  
        _context.Doktorlars.Remove(data!);
        _context.SaveChanges();
        return RedirectToAction("Index");  
    }
}







