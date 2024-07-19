using Interview.Models;

namespace Interview.IRepos
{
    public interface IInvoiceDetailRepo
    {
        InvoiceDetail CreateInvoiceDetail(InvoiceDetail InvoiceDetail);
        InvoiceDetail GetInvoiceDetail(int InvoiceDetailId);
        bool DeleteInvoiceDetail(int InvoiceDetailId);
        InvoiceDetail UpdateInvoiceDetail(int InvoiceDetailId,InvoiceDetail InvoiceDetail);
        IQueryable<InvoiceDetail> GetAllInvoiceDetails();
       Tuple<Unit, IQueryable<InvoiceDetail>> GetInvoiceDetailsByUnitId(int UnitId);
        

    }
}
