using Interview.IRepos;
using Interview.Models;
using Microsoft.EntityFrameworkCore;

namespace Interview.Repos
{
    public class InvoiceDetailRepo(InterviewContext context) : IInvoiceDetailRepo
    {
        InterviewContext _Context=context;
        public InvoiceDetail CreateInvoiceDetail(InvoiceDetail InvoiceDetail)
        {
            _Context.InvoiceDetails.Add(InvoiceDetail);
            _Context.SaveChanges();
            return InvoiceDetail;
        }

        public bool DeleteInvoiceDetail(int InvoiceDetailId)
        {
           var InvoiceDetail= _Context.InvoiceDetails.FirstOrDefault(u=>u.LineNumber== InvoiceDetailId);
            if (InvoiceDetail!=null)
            {
                _Context.Remove(InvoiceDetail);
                _Context.SaveChanges();
                return true;
            }
            return false;
        }

        public IQueryable<InvoiceDetail> GetAllInvoiceDetails()
        {
            return _Context.InvoiceDetails;
               
                }

        public InvoiceDetail GetInvoiceDetail(int InvoiceDetailId)
        {
            return _Context.InvoiceDetails.FirstOrDefault(u=>u.LineNumber==InvoiceDetailId)!;
        }

        public InvoiceDetail UpdateInvoiceDetail(int InvoiceDetailId, InvoiceDetail InvoiceDetail)
        {
           
           var updatedInvoiceDetail=  _Context.InvoiceDetails.Update(InvoiceDetail).Entity;
            _Context.SaveChanges();
            return updatedInvoiceDetail;

        }

   public   Tuple<Unit ,  IQueryable<InvoiceDetail>> GetInvoiceDetailsByUnitId(int UnitId)
        {
           var unitDetails=  _Context.InvoiceDetails.Where(i => i.UnitNo == UnitId);
            var Unit = _Context.Units.FirstOrDefault(a => a.UnitNo == UnitId);
            return Tuple.Create(Unit,unitDetails);
        }
    }
}
