
public class StudentDetailedInfoDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? PhoneNumber { get; set; }
    public string? CountryCode { get; set; }
    public string? Country { get; set; }
    public bool Gender { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string? Image { get; set; }
    public byte[]? ByteArrayFormat { get; set; }
    public string? FileType { get; set; }
    public string Address { get; set; }
    public Guid BranchId { get; set; }
    public string? IdentityId { get; set; }
}