using FluentAssertions;
namespace SchoolProject.XUnitTest
{
    public class AssertTest
    {
        [Fact]
        public void calculate_2_sum_3_shoud_be_5_Without_fluentAssertion()
        {
            //Arrange
            int x = 2;
            int y = 3;
            int z;
            //Act
            z = x+y;
            //Assert
            Assert.Equal(5, z);
        }
        [Fact]
        public void calculate_2_sum_3_shoud_be_5_With_fluentAssertion()
        {
            //Arrange
            int x = 2;
            int y = 3;
            int z;
            //Act
            z = x+y;
            //Assert
            z.Should().Be(5, "sum 2 with 5 not equal 5");
        }
        [Fact]
        public void string_should_be_startwith_we()
        {
            string word = "wellcome";
            word.Should().StartWith("we");
        }
        [Fact]
        public void string_should_be_endWith_me()
        {
            string word = "wellcome";
            word.Should().EndWith("me");
        }
        [Fact]
        public void string_should_Lenght_be_8()
        {
            string word = "wellcome";
            word.Should().HaveLength(8);
        }
        [Fact]
        public void string_should_startwith_we_and_endWith_me_and_Lenght_be_8()
        {
            string word = "wellcome";
            word.Should().StartWith("we").And.EndWith("me").And.HaveLength(8);
        }
        [Fact]
        public void string_should_be_wellcome()
        {
            string word = "wellcome";
            word.Should().Be("wellcome");
        }
        [Fact]
        public void string_should_be_not_null()
        {
            string word = "wellcome";
            word.Should().NotBeNullOrWhiteSpace();
        }
        [Fact]
        public void string_should_be_type_of_string()
        {
            string word = "wellcome";
            word.Should().BeOfType<string>();
        }
        [Fact]
        public void verify_value_is_true()
        {
            bool first = true;
            first.Should().BeTrue();
            first.Should().NotBe(false);
        }
        [Fact]
        public void verify_number_is_Positive()
        {
            int x = 5;
            x.Should().BePositive();
            x.Should().BeGreaterThanOrEqualTo(5);
            x.Should().BeGreaterThan(4);
            x.Should().NotBeInRange(4, 6);
        }
    }
}