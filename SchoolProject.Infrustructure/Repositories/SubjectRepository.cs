using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Infrustructure.Abstracts;
using SchoolProject.Infrustructure.Data;
using SchoolProject.Infrustructure.InfrastructureBases;

namespace SchoolProject.Infrustructure.Repositories
{
    public class SubjectRepository : GenericRepositoryAsync<Subjects>, ISubjectRepository
    {
        #region Fields
        private DbSet<Subjects> subjects;
        #endregion

        #region Constructors
        public SubjectRepository(ApplicationDBContext dbContext) : base(dbContext)
        {
            subjects=dbContext.Set<Subjects>();
        }
        #endregion

        #region Handle Functions

        #endregion
    }
}