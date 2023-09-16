using System.Collections;

namespace SchoolProject.XUnitTest.TestModels
{
    public class PassDataUsingClassData : IEnumerable<object[]>
    {
        private readonly List<object[]> data = new List<object[]>()
        {
            new object[] {1},
            new object[] {2}
        };
        public IEnumerator<object[]> GetEnumerator()
        {
            return data.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
