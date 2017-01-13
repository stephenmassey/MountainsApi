using System.Collections.Generic;

namespace Mountains.V1.Client.Dtos
{
    public sealed class MountainRangeCollectionDto
    {
        public IEnumerable<MountainRangeDto> MountainRanges { get; set; }
    }
}
