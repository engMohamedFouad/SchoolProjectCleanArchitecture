using SchoolProject.Core.Wrappers;
using SchoolProject.Data.Entities;
using SchoolProject.XUnitTest.Wrappers.Interfaces;

namespace SchoolProject.XUnitTest.Wrappers.Implementations
{
    public class PaginatedService : IPaginatedService<Student>
    {
        public async Task<PaginatedResult<Student>> ReturnPaginatedResult(IQueryable<Student> source, int pageNumber, int pageSize)
        {
            return await source.ToPaginatedListAsync(pageNumber, pageSize);
        }
    }
}
