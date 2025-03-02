using GrpcPersonService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrpPersonService.Tests
{
    public class PersonTests
    {
        [Theory]
        [InlineData("1234567890",true)]
        [InlineData("1111111111",false)]
        [InlineData("abcd123456", false)]
        [InlineData("12345", false)]
        [InlineData("", false)]
        public void IsValidNationalCode_ShouldValidCorrectly(string nationalCode,bool expected)
        {
            bool result = PersonModel.IsValidNationalCode(nationalCode); 
            Assert.Equal(expected, result);
        }
        [Theory]
        [InlineData("1377-04-20",true)]
        [InlineData("1377/04/20", true)]
        [InlineData("13770420",false)]
        [InlineData("abcd-ef-gh", false)]

        public void IsValidBirthDate_ShouldValidCorrectly(string birthDate, bool expected)
        {
            bool result = PersonModel.IsValidBirthDate(birthDate);
            Assert.Equal(expected, result);
        }

    }
}
