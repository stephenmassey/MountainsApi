using Mountains.ServiceModels;
using System.Collections.ObjectModel;

namespace Mountains
{
    public interface IMountainService
    {
        Mountain GetMountain(int id);

        ReadOnlyCollection<Mountain> GetMountains(int start, int count, int? mountainRangeId = null);

        Mountain AddMountain(Mountain mountain);

        Mountain UpdateMountain(int id, Mountain mountain);

        void DeleteMountain(int id);

        MountainRange GetMountainRange(int id);

        ReadOnlyCollection<MountainRange> GetMountainRanges(int start, int count);

        MountainRange AddMountainRange(MountainRange mountain);

        MountainRange UpdateMountainRange(int id, MountainRange mountain);

        void DeleteMountainRange(int id);
    }
}
