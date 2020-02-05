using System;
using FilterBuilder.FilterAttributes;

namespace BLL.DTOs
{
    public class AccountDTO
    {
        public Guid Id { get; set; }
        [Filtered]
        public string Name { get; set; }
    }
}
