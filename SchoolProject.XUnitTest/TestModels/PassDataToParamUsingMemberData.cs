using System.Collections;

namespace SchoolProject.XUnitTest.TestModels
{
    public class PassDataToParamUsingMemberData : IEnumerable<object[]>
    {
        public static IEnumerable<object[]> GetParamData()
        {
            return new List<object[]>
            {
                new object[]{1},
                new object[]{2}
            };
        }
        public IEnumerator<object[]> GetEnumerator()
        {
            return (IEnumerator<object[]>)GetParamData();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
