using Microsoft.EntityFrameworkCore;
using SchoolProject.Infrustructure.Abstracts.Functions;
using SchoolProject.Infrustructure.Data;
using System.Data;

namespace SchoolProject.Infrustructure.Repositories.Functions
{
    public class InstructorFunctionsRepository : IInstructorFunctionsRepository
    {
        #region Fileds
        private readonly ApplicationDBContext _dbContext;
        #endregion
        #region Constructors
        public InstructorFunctionsRepository(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        #endregion
        #region Handle Functions
        public decimal GetSalarySummationOfInstructor(string query)
        {
            using (var cmd = _dbContext.Database.GetDbConnection().CreateCommand())
            {
                if (cmd.Connection.State!=ConnectionState.Open)
                {
                    cmd.Connection.Open();
                }

                //read From List

                //  var reader = await cmd.ExecuteReaderAsync();
                // var value = await reader.ToListAsync<GetInstructorFunctionResult>();

                decimal response = 0;
                cmd.CommandText = query;
                var value = cmd.ExecuteScalar();
                var result = value.ToString();
                if (decimal.TryParse(result, out decimal d))
                {
                    response= d;
                }
                cmd.Connection.Close();
                return response;
            }
        }
        #endregion
    }
}
