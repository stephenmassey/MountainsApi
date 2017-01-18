using System.Collections.Generic;

namespace Mountains.V1.Client.Dtos
{
    public sealed class HikeCollectionDto
    {
        public IEnumerable<HikeDto> Hikes { get; set; }
    }
}
