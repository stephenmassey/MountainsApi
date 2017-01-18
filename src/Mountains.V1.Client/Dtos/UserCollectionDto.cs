using System.Collections.Generic;

namespace Mountains.V1.Client.Dtos
{
    public sealed class UserCollectionDto
    {
        public IEnumerable<UserDto> Users { get; set; }
    }
}
