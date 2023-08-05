using Microsoft.EntityFrameworkCore;
using SchoolProject.Infrustructure.Abstracts.Functions;
using SchoolProject.Infrustructure.Data;
using SchoolProject.Service.Abstracts;
using System.Data;


namespace SchoolProject.Service.Implementations
{
    public class InstructorService : IInstructorService
    {
        #region Fileds
        private readonly ApplicationDBContext _dbContext;
        private readonly IInstructorFunctionsRepository _instructorFunctionsRepository;
        #endregion
        #region Constructors
        public InstructorService(ApplicationDBContext dbContext, IInstructorFunctionsRepository instructorFunctionsRepository)
        {
            _dbContext = dbContext;
            _instructorFunctionsRepository = instructorFunctionsRepository;
        }


        #endregion
        #region Handle Functions
        public async Task<decimal> GetSalarySummationOfInstructor()
        {
            decimal result = 0;
            using (var cmd = _dbContext.Database.GetDbConnection().CreateCommand())
            {
                if (cmd.Connection.State!=ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }
                result= _instructorFunctionsRepository.GetSalarySummationOfInstructor("select dbo.GetSalarySummation()", cmd);
            }
            return result;
        }
        #endregion
    }
}
