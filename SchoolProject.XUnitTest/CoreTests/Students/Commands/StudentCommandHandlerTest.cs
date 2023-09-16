using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Localization;
using Moq;
using SchoolProject.Core.Features.Students.Commands.Handlers;
using SchoolProject.Core.Features.Students.Commands.Models;
using SchoolProject.Core.Mapping.Students;
using SchoolProject.Core.Resources;
using SchoolProject.Data.Entities;
using SchoolProject.Service.Abstracts;
using System.Net;

namespace SchoolProject.XUnitTest.CoreTests.Students.Commands
{
    public class StudentCommandHandlerTest
    {
        private readonly Mock<IStudentService> _studentServiceMock;
        private readonly IMapper _mapperMock;
        private readonly Mock<IStringLocalizer<SharedResources>> _localizerMock;
        private readonly StudentProfile _studentProfile;


        public StudentCommandHandlerTest()
        {
            _studentProfile = new();
            _studentServiceMock = new();
            _localizerMock = new();
            var configuration = new MapperConfiguration(c => c.AddProfile(_studentProfile));
            _mapperMock=new Mapper(configuration);
        }
        [Fact]
        public async Task Handle_AddStudent_Should_Add_Data_And_StatusCode201()
        {
            //Arrange
            var handler = new StudentCommandHandler(_studentServiceMock.Object, _mapperMock, _localizerMock.Object);
            var addStudentCommand = new AddStudentCommand() { NameAr="محمد", NameEn="Mohamed" };
            _studentServiceMock.Setup(x => x.AddAsync(It.IsAny<Student>())).Returns(Task.FromResult("Success"));
            //act
            var result = await handler.Handle(addStudentCommand, default);
            //Assert
            result.Succeeded.Should().BeTrue();
            result.StatusCode.Should().Be(HttpStatusCode.Created);
            _studentServiceMock.Verify(x => x.AddAsync(It.IsAny<Student>()), Times.Once, "Not Called");
        }
        [Fact]
        public async Task Handle_AddStudent_Should_return_StatusCode400()
        {
            //Arrange
            var handler = new StudentCommandHandler(_studentServiceMock.Object, _mapperMock, _localizerMock.Object);
            var addStudentCommand = new AddStudentCommand() { NameAr="محمد", NameEn="Mohamed" };
            _studentServiceMock.Setup(x => x.AddAsync(It.IsAny<Student>())).Returns(Task.FromResult(""));
            //act
            var result = await handler.Handle(addStudentCommand, default);
            //Assert
            result.Succeeded.Should().BeFalse();
            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            _studentServiceMock.Verify(x => x.AddAsync(It.IsAny<Student>()), Times.Once, "Not Called");
        }
        [Fact]
        public async Task Handle_UpdateStudent_Should_Return_StatusCode404()
        {
            //Arrange
            var handler = new StudentCommandHandler(_studentServiceMock.Object, _mapperMock, _localizerMock.Object);
            var updateStudentCommand = new EditStudentCommand() { Id=6, NameAr="محمد", NameEn="Mohamed" };
            Student? student = null;
            int xResult = 0;
            _studentServiceMock.Setup(x => x.GetByIDAsync(updateStudentCommand.Id)).Returns(Task.FromResult(student)).Callback((int x) => xResult=x);
            //act
            var result = await handler.Handle(updateStudentCommand, default);
            //Assert
            result.Succeeded.Should().BeFalse();
            result.StatusCode.Should().Be(HttpStatusCode.NotFound);
            xResult.Should().Be(6);
            Assert.Equal(xResult, updateStudentCommand.Id);
            _studentServiceMock.Verify(x => x.GetByIDAsync(It.IsAny<int>()), Times.Once, "Not Called");
        }
    }
}
