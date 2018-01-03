using System;
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
            Assert.That(_converter.Convert("Not Empty"), Is.Not.Null.And.InstanceOf<IStringConverter>());
            Assert.That(_converter.Convert(string.Empty), Is.Not.Null.And.InstanceOf<IStringConverter>());
            Assert.That(_converter.Convert(default(string)), Is.Not.Null.And.InstanceOf<IStringConverter>());
            Assert.That(_converter.Convert(null), Is.Not.Null.And.InstanceOf<IStringConverter>());
        }

        [Test]
        public void To_Should_Return_Default_Of_Type()
        {
            Assert.That(_converter.To<bool>(), Is.EqualTo(default(bool)));
            Assert.That(_converter.To<char>(), Is.EqualTo(default(char)));
            Assert.That(_converter.To<string>(), Is.EqualTo(default(string)));
            Assert.That(_converter.To<byte>(), Is.EqualTo(default(byte)));
            Assert.That(_converter.To<sbyte>(), Is.EqualTo(default(sbyte)));
            Assert.That(_converter.To<decimal>(), Is.EqualTo(default(decimal)));
            Assert.That(_converter.To<double>(), Is.EqualTo(default(double)));
            Assert.That(_converter.To<float>(), Is.EqualTo(default(float)));
            Assert.That(_converter.To<int>(), Is.EqualTo(default(int)));
            Assert.That(_converter.To<uint>(), Is.EqualTo(default(uint)));
            Assert.That(_converter.To<long>(), Is.EqualTo(default(long)));
            Assert.That(_converter.To<ulong>(), Is.EqualTo(default(ulong)));
            Assert.That(_converter.To<short>(), Is.EqualTo(default(short)));
            Assert.That(_converter.To<ushort>(), Is.EqualTo(default(ushort)));
        }

        [Test]
        public void Convert_To_Should_Return_Correct_Type_And_Expected_Conversion()
        {
            string character = "a";
            char characterConverted = 'a';

            Assert.That(_converter.Convert(character).To<char>(), Is.TypeOf<char>().And.EqualTo(characterConverted));

            string boolean = "false";
            bool booleanConverted = false;

            Assert.That(_converter.Convert(boolean).To<bool>(), Is.TypeOf<bool>().And.EqualTo(booleanConverted));

            string number = "123";
            int numberConverted = 123;

            Assert.That(_converter.Convert(number).To<int>(), Is.TypeOf<int>().And.EqualTo(numberConverted));
            Assert.That(_converter.Convert(number).To<uint>(), Is.TypeOf<uint>().And.EqualTo(numberConverted));
            Assert.That(_converter.Convert(number).To<long>(), Is.TypeOf<long>().And.EqualTo(numberConverted));
            Assert.That(_converter.Convert(number).To<ulong>(), Is.TypeOf<ulong>().And.EqualTo(numberConverted));
            Assert.That(_converter.Convert(number).To<short>(), Is.TypeOf<short>().And.EqualTo(numberConverted));
            Assert.That(_converter.Convert(number).To<ushort>(), Is.TypeOf<ushort>().And.EqualTo(numberConverted));
            Assert.That(_converter.Convert(number).To<decimal>(), Is.TypeOf<decimal>().And.EqualTo(numberConverted));
            Assert.That(_converter.Convert(number).To<double>(), Is.TypeOf<double>().And.EqualTo(numberConverted));
            Assert.That(_converter.Convert(number).To<float>(), Is.TypeOf<float>().And.EqualTo(numberConverted));
            Assert.That(_converter.Convert(number).To<byte>(), Is.TypeOf<byte>().And.EqualTo(numberConverted));
            Assert.That(_converter.Convert(number).To<sbyte>(), Is.TypeOf<sbyte>().And.EqualTo(numberConverted));
        }

        [Test]
        public void Convert_To_Should_Throw_InvalidCastException()
        {
            Assert.That(() => _converter.Convert("test").To<StringConverter>(), Throws.TypeOf<InvalidCastException>());
            Assert.That(() => _converter.Convert("test").To<IStringConverter>(), Throws.TypeOf<InvalidCastException>());
        }
    }
}