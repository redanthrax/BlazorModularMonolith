using Common.Domain;

namespace API.Modules.Authentication.Entities;

public class User : BaseEntity {
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
}
