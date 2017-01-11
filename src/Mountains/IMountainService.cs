using Mountains.ServiceModels;
using System.Collections.ObjectModel;

namespace Mountains
{
    public interface IMountainService
    {
        Mountain GetMountain(int id);

        ReadOnlyCollection<Mountain> GetMountains();

        Mountain AddMountain(Mountain mountain);

        Mountain UpdateMountain(int id, Mountain mountain);

        void DeleteMountain(int id);
    }
}
