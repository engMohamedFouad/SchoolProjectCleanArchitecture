using System.Data.Common;

namespace SchoolProject.Infrustructure.Abstracts.Functions
{
    public interface IInstructorFunctionsRepository
    {
        public decimal GetSalarySummationOfInstructor(string query, DbCommand cmd);
    }
}
