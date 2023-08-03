using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Commons;

namespace SchoolProject.Data.Entities.Views
{
    [Keyless]
    public class ViewDepartment : GeneralLocalizableEntity
    {
        public int DID { get; set; }
        public string? DNameAr { get; set; }
        public string? DNameEn { get; set; }
        public int StudentCount { get; set; }
    }
}
