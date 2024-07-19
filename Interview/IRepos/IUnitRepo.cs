using Interview.Models;

namespace Interview.IRepos
{
    public interface IUnitRepo
    {
        Unit CreateUnit(Unit unit);
        Unit GetUnit(int unitId);
        bool DeleteUnit(int unitId);
        Unit UpdateUnit(int unitId,Unit unit);
        IQueryable<Unit> GetAllUnits();

    }
}
