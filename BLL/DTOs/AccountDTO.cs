using System;
using FilterBuilder.FilterAttributes;

namespace BLL.DTOs
{
    public class AccountDTO
    {
        public Guid Id { get; set; }
        [Filtered]
        public string Name { get; set; }
        [Filtered]
        public string Login { get; set; }
        public string Password { get; set; }
        [Filtered]
        public string Email { get; set; }
    }
}
