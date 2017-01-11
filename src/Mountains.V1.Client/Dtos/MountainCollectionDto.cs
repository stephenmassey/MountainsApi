using System.Collections.Generic;

namespace Mountains.V1.Client.Dtos
{
    public sealed class MountainCollectionDto
    {
        public IEnumerable<MountainDto> Mountains { get; set; }
    }
}
