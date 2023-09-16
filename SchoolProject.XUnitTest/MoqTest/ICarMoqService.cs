using SchoolProject.XUnitTest.TestModels;

namespace SchoolProject.XUnitTest.MoqTest
{
    public interface ICarMoqService
    {
        public bool AddCar(Car car);
        public bool RemoveCar(int? id);
        public List<Car> GetAll();
    }
}
