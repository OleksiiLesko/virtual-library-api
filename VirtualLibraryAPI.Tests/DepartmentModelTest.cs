using Microsoft.Extensions.Logging;
using Moq;
using VirtualLibraryAPI.Repository;

namespace VirtualLibraryAPI.Tests
{
    public class DepartmentModelTest
    {
        private readonly ILogger<Models.DepartmentModel> _logger;
        private readonly Mock<IDepartmentRepository> _departmentRepository;

        public DepartmentModelTest()
        {
            _departmentRepository = new Mock<IDepartmentRepository>();
            _logger = new Mock<ILogger<Models.DepartmentModel>>().Object;
        }

        [Fact]
        public void AddDepartment_ReturnsAddedDepartment()
        {
            var departmentDTO = new Domain.DTOs.Department { DepartmentID = 2, DepartmentName = "324235" };
            var addedDepartment = new Domain.DTOs.Department { DepartmentID = 1, DepartmentName = "324235" };
            _departmentRepository.Setup(x => x.AddDepartment(departmentDTO)).Returns(addedDepartment);
            var departmentModel = new Models.DepartmentModel(_logger, _departmentRepository.Object);

            var result = departmentModel.AddDepartment(departmentDTO);

            Assert.NotNull(result);
            Assert.Equal(departmentDTO.DepartmentName, result.DepartmentName);
            _departmentRepository.Verify(x => x.AddDepartment(departmentDTO), Times.Once());
        }
        [Fact]
        public void UpdateDepartment_Should_Return_Updated_Department()
        {
            var departmentID = 1;
            var departmentDto = new Domain.DTOs.Department { DepartmentID = 2, DepartmentName = "324235" };
            var updatedDepartment = new Domain.DTOs.Department { DepartmentID = departmentID, DepartmentName = "324238" };
            _departmentRepository.Setup(x => x.UpdateDepartment(departmentID, departmentDto)).Returns(updatedDepartment);
            var deaprtmentModel = new Models.DepartmentModel(_logger, _departmentRepository.Object);

            var result = deaprtmentModel.UpdateDepartment(departmentID, departmentDto);

            Assert.True(result.DepartmentID == 1);
            Assert.Equal(updatedDepartment.DepartmentID, result.DepartmentID);
            Assert.Equal(updatedDepartment.DepartmentName, result.DepartmentName);
            _departmentRepository.Verify(x => x.UpdateDepartment(departmentID, departmentDto), Times.Once());
        }
        [Fact]
        public void DeleteDepartment_ReturnsDeletedDepartment()
        {
            int departmentIdToDelete = 1;
            var expectedDeletedDepartment = new Domain.DTOs.Department { DepartmentID = departmentIdToDelete, DepartmentName = "324238" };
            _departmentRepository.Setup(x => x.DeleteDepartment(departmentIdToDelete)).Returns(expectedDeletedDepartment);
            var departmentModel = new Models.DepartmentModel(_logger, _departmentRepository.Object);

            var deletedDepartment = departmentModel.DeleteDepartment(departmentIdToDelete);

            _departmentRepository.Verify(x => x.DeleteDepartment(departmentIdToDelete), Times.Once());
            Assert.Equal(expectedDeletedDepartment, deletedDepartment);
        }
        [Fact]
        public void GetAllDepartments_ReturnsAllDepartments()
        {
            var expectedDepartments = new List<Domain.DTOs.Department>
        {
            new Domain.DTOs.Department { DepartmentID = 1, DepartmentName = "324235" },
            new Domain.DTOs.Department { DepartmentID = 2, DepartmentName = "327235" },
            new Domain.DTOs.Department {DepartmentID = 3, DepartmentName = "324245"}
        };
            _departmentRepository.Setup(x => x.GetAllDepartments()).Returns(expectedDepartments);
            var departmentModel = new Models.DepartmentModel(_logger, _departmentRepository.Object);

            var allDepartments = departmentModel.GetAllDepartments();

            _departmentRepository.Verify(x => x.GetAllDepartments(), Times.Once());
            Assert.Equal(expectedDepartments, allDepartments);
        }
        [Fact]
        public void GetDepartmentById_ReturnsCorrectDepartment()
        {
            var departmentId = 1;
            var expectedDepartment = new Domain.DTOs.Department { DepartmentID = departmentId, DepartmentName = "324235" };
            _departmentRepository.Setup(x => x.GetDepartmentById(departmentId)).Returns(expectedDepartment);
            var departmentModel = new Models.DepartmentModel(_logger, _departmentRepository.Object);

            var department = departmentModel.GetDepartmentById(departmentId);

            _departmentRepository.Verify(x => x.GetDepartmentById(departmentId), Times.Once());
            Assert.Equal(expectedDepartment, department);
        }
        [Fact]
        public void GetAllDepartmentsResponse_ReturnsExpectedResult()
        {
            var departmentModel = new Models.DepartmentModel(_logger, _departmentRepository.Object);
            var expectedDepartments = new List<Domain.DTOs.Department>
            {
                new Domain.DTOs.Department  {DepartmentID = 1, DepartmentName = "324235"},
                new Domain.DTOs.Department  {DepartmentID = 2, DepartmentName = "324235"},
                new Domain.DTOs.Department  {DepartmentID = 3, DepartmentName = "324235"}
            };

            _departmentRepository.Setup(repo => repo.GetAllDepartmentsResponse())
                .Returns(expectedDepartments);

            var result = departmentModel.GetAllDepartmentsResponse();

            Assert.Equal(expectedDepartments, result);
            _departmentRepository.Verify(x => x.GetAllDepartmentsResponse(), Times.Once);
        }
       
    }
}
