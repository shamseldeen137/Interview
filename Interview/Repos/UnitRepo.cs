using Interview.IRepos;
using Interview.Models;
using Microsoft.EntityFrameworkCore;

namespace Interview.Repos
{
    public class UnitRepo(InterviewContext context) : IUnitRepo
    {
        InterviewContext _Context=context;
        public Unit CreateUnit(Unit unit)
        {
            _Context.Units.Add(unit);
            _Context.SaveChanges();
            return unit;
        }

        public bool DeleteUnit(int unitId)
        {
           var unit= _Context.Units.FirstOrDefault(u=>u.UnitNo== unitId);
            if (unit!=null)
            {
              var alldetails=  _Context.InvoiceDetails.Where(a => a.UnitNo == unitId);
                _Context.RemoveRange(alldetails);
                _Context.Remove(unit);
                _Context.SaveChanges();
                return true;
            }
            return false;
        }

        public IQueryable<Unit> GetAllUnits()
        {
            return _Context.Units;
               
                }

        public Unit GetUnit(int unitId)
        {
            return _Context.Units.FirstOrDefault(u=>u.UnitNo==unitId)!;
        }

        public Unit UpdateUnit(int unitId, Unit unit)
        {
           
           var updatedunit=  _Context.Units.Update(unit).Entity;
            _Context.SaveChanges();
            return updatedunit;

        }
    }
}
