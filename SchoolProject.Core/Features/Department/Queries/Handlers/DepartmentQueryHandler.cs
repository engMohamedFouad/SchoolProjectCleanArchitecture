using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Department.Queries.Models;
using SchoolProject.Core.Features.Department.Queries.Results;
using SchoolProject.Core.Resources;
using SchoolProject.Core.Wrappers;
using SchoolProject.Data.Entities;
using SchoolProject.Service.Abstracts;
using System.Linq.Expressions;

namespace SchoolProject.Core.Features.Department.Queries.Handlers
{
    public class DepartmentQueryHandler : ResponseHandler,
         IRequestHandler<GetDepartmentByIDQuery, Response<GetDepartmentByIDResponse>>
    {

        #region Fields
        private readonly IDepartmentService _departmentService;
        private readonly IStudentService _studentService;
        private readonly IStringLocalizer<SharedResources> _stringLocalizer;
        private readonly IMapper _mapper;
        #endregion

        #region Constructors
        public DepartmentQueryHandler(IStringLocalizer<SharedResources> stringLocalizer,
                                      IDepartmentService departmentService,
                                      IMapper mapper,
                                      IStudentService studentService) : base(stringLocalizer)
        {
            _stringLocalizer=stringLocalizer;
            _mapper=mapper;
            _studentService=studentService;
            _departmentService= departmentService;
        }

        public async Task<Response<GetDepartmentByIDResponse>> Handle(GetDepartmentByIDQuery request, CancellationToken cancellationToken)
        {
            //service Get By Id include St sub ins
            var response = await _departmentService.GetDepartmentById(request.Id);
            //check Is Not exist
            if (response == null) return NotFound<GetDepartmentByIDResponse>(_stringLocalizer[SharedResourcesKeys.NotFound]);
            //mapping 
            var mapper = _mapper.Map<GetDepartmentByIDResponse>(response);

            //pagination
            Expression<Func<Student, StudentResponse>> expression = e => new StudentResponse(e.StudID, e.Localize(e.NameAr, e.NameEn));
            var studentQuerable = _studentService.GetStudentsByDepartmentIDQuerable(request.Id);
            var PaginatedList = await studentQuerable.Select(expression).ToPaginatedListAsync(request.StudentPageNumber, request.StudentPageSize);
            mapper.StudentList = PaginatedList;

            //return response
            return Success(mapper);
        }
        #endregion

        #region Handle Functions
        #endregion
    }
}
