using Interview.IRepos;
using Interview.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Interview.Controllers
{
    public class UnitController(IMemoryCache memoryCache, IUnitRepo unitRepo,IInvoiceDetailRepo invoiceDetailsRepo) : Controller
    {
        IMemoryCache _memoryCache = memoryCache;
        IUnitRepo _unitRepo = unitRepo;
        IInvoiceDetailRepo _invoiceDetailsRepo = invoiceDetailsRepo;

        // GET:  UnitController1
        public ActionResult Index()
        {
            var cacheData = _memoryCache.Get<IEnumerable<Unit>>("Units");
            if (cacheData != null)
            {
                return View(cacheData);
            }

            var expirationTime = DateTimeOffset.Now.AddMinutes(5.0);
            var units = _unitRepo.GetAllUnits();
            cacheData = units.ToList();
            _memoryCache.Set("Units", cacheData, expirationTime);


        
            return View(units.ToList());
        }

        // GET:  UnitController1/Details/5
        public ActionResult Details(int id)
        {
            var unit= _unitRepo.GetUnit(id);
            return View(unit);
        }
       
        public ActionResult UnitInvoiceDetails(int id)
        {
            var cacheData = _memoryCache.Get<IEnumerable<InvoiceDetail>>("UnitInvoiceDetails");
            if (cacheData != null)
            {
                return View(cacheData);
            }

            var expirationTime = DateTimeOffset.Now.AddMinutes(5.0);
            var units = _invoiceDetailsRepo.GetInvoiceDetailsByUnitId(id);
            cacheData = units.Item2.ToList();
            _memoryCache.Set("UnitInvoiceDetails", cacheData, expirationTime);

            ViewBag.Unit = units.Item1;
            return View( units.Item2.ToList());
        }
        // GET:  UnitController1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST:  UnitController1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Unit Unit)
        {
            try
            {
                _unitRepo.CreateUnit(Unit);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET:  UnitController1/Edit/5
        public ActionResult Edit(int id)
        {
         var unit=   _unitRepo.GetUnit(id);
            return View(unit);
        }

        // POST:  UnitController1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Unit unit)
        {
            try
            {
                _unitRepo.UpdateUnit(id,unit);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET:  UnitController1/Delete/5
        public ActionResult Delete(int id)
        {
         var unit=   _unitRepo.GetUnit(id);
            return View(unit);
        }

        // POST:  UnitController1/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteItem(int UnitNo)
        {
            try
            {
                _unitRepo.DeleteUnit(UnitNo);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
