using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SchoolProject.Data.Entities;
using SchoolProject.Infrustructure.Abstracts;
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
        private readonly IInstructorsRepository _instructorsRepository;
        private readonly IFileService _fileService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        #endregion
        #region Constructors
        public InstructorService(ApplicationDBContext dbContext,
                                 IInstructorFunctionsRepository instructorFunctionsRepository,
                                 IInstructorsRepository instructorsRepository,
                                 IFileService fileService,
                                 IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _instructorFunctionsRepository = instructorFunctionsRepository;
            _instructorsRepository = instructorsRepository;
            _fileService = fileService;
            _httpContextAccessor = httpContextAccessor;
        }



        #endregion
        #region Handle Functions
        public async Task<decimal> GetSalarySummationOfInstructor()
        {
            decimal result = 0;
            result= _instructorFunctionsRepository.GetSalarySummationOfInstructor("select dbo.GetSalarySummation()");
            return result;
        }

        public async Task<bool> IsNameArExist(string nameAr)
        {
            //Check if the name is Exist Or not
            var student = _instructorsRepository.GetTableNoTracking().Where(x => x.ENameAr.Equals(nameAr)).FirstOrDefault();
            if (student== null) return false;
            return true;
        }

        public async Task<bool> IsNameArExistExcludeSelf(string nameAr, int id)
        {
            //Check if the name is Exist Or not
            var student = await _instructorsRepository.GetTableNoTracking().Where(x => x.ENameAr.Equals(nameAr)&x.InsId!=id).FirstOrDefaultAsync();
            if (student== null) return false;
            return true;
        }

        public async Task<bool> IsNameEnExist(string nameEn)
        {
            //Check if the name is Exist Or not
            var student = await _instructorsRepository.GetTableNoTracking().Where(x => x.ENameEn.Equals(nameEn)).FirstOrDefaultAsync();
            if (student== null) return false;
            return true;
        }

        public async Task<bool> IsNameEnExistExcludeSelf(string nameEn, int id)
        {
            //Check if the name is Exist Or not
            var student = await _instructorsRepository.GetTableNoTracking().Where(x => x.ENameEn.Equals(nameEn)&x.InsId!=id).FirstOrDefaultAsync();
            if (student== null) return false;
            return true;
        }
        public async Task<string> AddInstructorAsync(Instructor instructor, IFormFile file)
        {
            var context = _httpContextAccessor.HttpContext.Request;
            var baseUrl = context.Scheme+"://"+context.Host;
            var imageUrl = await _fileService.UploadImage("Instructors", file);
            switch (imageUrl)
            {
                case "NoImage": return "NoImage";
                case "FailedToUploadImage": return "FailedToUploadImage";
            }
            instructor.Image=baseUrl+imageUrl;
            try
            {
                await _instructorsRepository.AddAsync(instructor);
                return "Success";
            }
            catch (Exception)
            {
                return "FailedInAdd";
            }
        }

        #endregion
    }
}
