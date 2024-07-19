using Interview.IRepos;
using Interview.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Caching.Memory;

namespace Interview.Controllers
{
    public class InvoiceDetailsController(IMemoryCache memoryCache,IUnitRepo unitRepo, IInvoiceDetailRepo invoiceDetailsRepo) : Controller
    {
        IUnitRepo _unitRepo = unitRepo;
        IInvoiceDetailRepo _invoiceDetailsRepo = invoiceDetailsRepo;
        private readonly IMemoryCache _memoryCache = memoryCache;


         

        // GET: InvoiceController1
        public ActionResult Index()
        {
            var cacheData = _memoryCache.Get<IEnumerable<InvoiceDetail>>("InvoiceDetails");
            if (cacheData != null)
            {
                return View(cacheData);
            }

            var expirationTime = DateTimeOffset.Now.AddMinutes(5.0);
            var units = _invoiceDetailsRepo.GetAllInvoiceDetails();
            cacheData = units.ToList();
            _memoryCache.Set("InvoiceDetails", cacheData, expirationTime);

            return View(units);
        }

        // GET: InvoiceController1/Details/5
        public ActionResult Details(int id)
        {
            var InvoiceDetails=_invoiceDetailsRepo.GetInvoiceDetail(id);
            return View(InvoiceDetails);
        }

        // GET: InvoiceController1/Create
        public ActionResult Create()
        {
         var units=   _unitRepo.GetAllUnits();
        
            ViewBag.UnitNo =  new SelectList(units.ToList(), nameof(Unit.UnitNo) , nameof(Unit.UnitName));
            return View();
        }

        // POST: InvoiceController1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(InvoiceDetail InvoiceDetail)
        {
            try
            {
                _invoiceDetailsRepo.CreateInvoiceDetail(InvoiceDetail);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: InvoiceController1/Edit/5
        public ActionResult Edit(int id)
        {
            var InvoiceDetails = _invoiceDetailsRepo.GetInvoiceDetail(id);
            var units = _unitRepo.GetAllUnits();

            ViewBag.UnitNo = new SelectList(units.ToList(), nameof(Unit.UnitNo), nameof(Unit.UnitName),InvoiceDetails.UnitNo);

            return View(InvoiceDetails);
        }

        // POST: InvoiceController1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, InvoiceDetail InvoiceDetail)
        {
            try
            {
                _invoiceDetailsRepo.UpdateInvoiceDetail(id, InvoiceDetail);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: InvoiceController1/Delete/5
        public ActionResult Delete(int id)
        {
            var InvoiceDetails = _invoiceDetailsRepo.GetInvoiceDetail(id);
            return View(InvoiceDetails);
        }

        // POST: InvoiceController1/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, InvoiceDetail unit)
        {
            try
            {
                _invoiceDetailsRepo.DeleteInvoiceDetail(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


       
    }
}
