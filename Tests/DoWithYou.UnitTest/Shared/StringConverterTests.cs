using DoWithYou.Interface;
using DoWithYou.Shared.Converters;
using NUnit.Framework;

namespace DoWithYou.UnitTest.Shared
{
    [TestFixture]
    public class StringConverterTests
    {
        private readonly IStringConverter _converter = new StringConverter();

        [Test]
        public void Convert_Should_Return_IStringConverter()
        {
            Assert.That(_converter.Convert("test"), Is.Not.Null.And.InstanceOf<IStringConverter>());
        }

        [Test]
        public void To_Should_Return_Default_Of_Type()
        {
            Assert.That(_converter.To<int>(), Is.EqualTo(default(int)));
        }

        [Test]
        public void Convert_To_Should_Return_Correct_Type_And_Expected_Conversion()
        {
            Assert.That(_converter.Convert("123").To<int>(), Is.TypeOf<int>().And.EqualTo(123));
        }
    }
}