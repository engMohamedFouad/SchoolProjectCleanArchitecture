namespace SchoolProject.Core.Features.ApplicationUser.Queries.Results
{
    public class GetUserByIdResponse
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string? Address { get; set; }
        public string? Country { get; set; }
    }
}
