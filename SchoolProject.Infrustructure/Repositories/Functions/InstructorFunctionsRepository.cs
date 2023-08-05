using SchoolProject.Infrustructure.Abstracts.Functions;
using System.Data.Common;

namespace SchoolProject.Infrustructure.Repositories.Functions
{
    public class InstructorFunctionsRepository : IInstructorFunctionsRepository
    {
        #region Fileds

        #endregion
        #region Constructors
        public InstructorFunctionsRepository()
        {

        }

        #endregion
        #region Handle Functions
        public decimal GetSalarySummationOfInstructor(string query, DbCommand cmd)
        {

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
            return response;
        }
        #endregion
    }
}
