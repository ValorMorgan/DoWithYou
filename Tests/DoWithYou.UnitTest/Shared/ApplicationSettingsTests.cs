using System;
using System.IO;
using DoWithYou.Interface.Shared;
using DoWithYou.Shared;
using DoWithYou.Shared.Constants.SettingPaths;
using DoWithYou.Shared.Repositories;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace DoWithYou.UnitTest.Shared
{
    [TestFixture]
    public class ApplicationSettingsTests
    {
        #region Test Setup
        private IApplicationSettings _settings;

        private IApplicationSettings Settings
        {
            get
            {
                if (_settings != null)
                    return _settings;

                ResolverFactory.SetupResolverForTesting();
                _settings = new ApplicationSettings(Resolver.Resolve<IConfiguration>());

                return _settings;
            }
        }
        #endregion

        #region Setting Paths
        private const string ROOT_PATH = "Testing";
        private readonly string STRING_PATH = $"{ROOT_PATH}:String";
        private readonly string CHARACTER_PATH = $"{ROOT_PATH}:Character";
        private readonly string BOOLEAN_PATH = $"{ROOT_PATH}:Boolean";
        private readonly string INTEGER_PATH = $"{ROOT_PATH}:Integer";
        #endregion

        [Theory]
        public void Can_Get_Setting()
        {
            Assert.That(Settings[STRING_PATH], Is.Not.Null.And.Not.Empty);
            Console.WriteLine($"{STRING_PATH} = {Settings[Logging.LogLevel.Default]}");

            Assert.That(Settings[ROOT_PATH, "String"], Is.Not.Null.And.Not.Empty);
        }

        [Theory]
        public void Get_Setting_From_Bad_Path_Returns_Null()
        {
            string badPath = "Some:Bad:Path";
            string[] badPathParams = {"Some", "Bad", "Path"};

            Assert.That(() => Settings[badPath], Throws.Nothing);
            Assert.That(Settings[badPath], Is.Null);
            
            Assert.That(() => Settings[badPathParams], Throws.Nothing);
            Assert.That(Settings[badPathParams], Is.Null);
        }

        [Theory]
        public void Get_Returns_Not_Null_And_InstanceOf_IApplicationSettings()
        {
            Assert.That(Settings.Get(STRING_PATH), Is.Not.Null.And.InstanceOf<IApplicationSettings>());
            Assert.That(Settings.Get(ROOT_PATH, "String"), Is.Not.Null.And.InstanceOf<IApplicationSettings>());
        }

        [Theory]
        [Retry(2)]
        public void As_When_No_Key_Provided_Returns_Default_Of_Type()
        {
            Assert.That(Settings.As<bool>(), Is.EqualTo(default(bool)));
            Assert.That(Settings.As<char>(), Is.EqualTo(default(char)));
            Assert.That(Settings.As<string>(), Is.EqualTo(default(string)));
            Assert.That(Settings.As<byte>(), Is.EqualTo(default(byte)));
            Assert.That(Settings.As<sbyte>(), Is.EqualTo(default(sbyte)));
            Assert.That(Settings.As<decimal>(), Is.EqualTo(default(decimal)));
            Assert.That(Settings.As<double>(), Is.EqualTo(default(double)));
            Assert.That(Settings.As<float>(), Is.EqualTo(default(float)));
            Assert.That(Settings.As<int>(), Is.EqualTo(default(int)));
            Assert.That(Settings.As<uint>(), Is.EqualTo(default(uint)));
            Assert.That(Settings.As<long>(), Is.EqualTo(default(long)));
            Assert.That(Settings.As<ulong>(), Is.EqualTo(default(ulong)));
            Assert.That(Settings.As<short>(), Is.EqualTo(default(short)));
            Assert.That(Settings.As<ushort>(), Is.EqualTo(default(ushort)));
        }

        [Theory]
        public void As_When_Key_Provided_Returns_Not_Null_And_Setting_Converted()
        {
            Assert.That(Settings.As<string>(STRING_PATH), Is.TypeOf<string>().And.Not.EqualTo(default(string)));
            Assert.That(Settings.As<string>(ROOT_PATH, "String"), Is.TypeOf<string>().And.Not.EqualTo(default(string)));

            Assert.That(Settings.As<char>(CHARACTER_PATH), Is.TypeOf<char>().And.Not.EqualTo(default(char)));
            Assert.That(Settings.As<char>(ROOT_PATH, "Character"), Is.TypeOf<char>().And.Not.EqualTo(default(char)));

            // Default for boolean is "false" which isn't good for testing
            Assert.That(Settings.As<bool>(BOOLEAN_PATH), Is.TypeOf<bool>());
            Assert.That(Settings.As<bool>(ROOT_PATH, "Boolean"), Is.TypeOf<bool>());

            Assert.That(Settings.As<int>(INTEGER_PATH), Is.TypeOf<int>().And.Not.EqualTo(default(int)));
            Assert.That(Settings.As<int>(ROOT_PATH, "Integer"), Is.TypeOf<int>().And.Not.EqualTo(default(int)));

            Assert.That(Settings.As<uint>(INTEGER_PATH), Is.TypeOf<uint>().And.Not.EqualTo(default(uint)));
            Assert.That(Settings.As<uint>(ROOT_PATH, "Integer"), Is.TypeOf<uint>().And.Not.EqualTo(default(uint)));

            Assert.That(Settings.As<long>(INTEGER_PATH), Is.TypeOf<long>().And.Not.EqualTo(default(long)));
            Assert.That(Settings.As<long>(ROOT_PATH, "Integer"), Is.TypeOf<long>().And.Not.EqualTo(default(long)));

            Assert.That(Settings.As<ulong>(INTEGER_PATH), Is.TypeOf<ulong>().And.Not.EqualTo(default(ulong)));
            Assert.That(Settings.As<ulong>(ROOT_PATH, "Integer"), Is.TypeOf<ulong>().And.Not.EqualTo(default(ulong)));

            Assert.That(Settings.As<short>(INTEGER_PATH), Is.TypeOf<short>().And.Not.EqualTo(default(short)));
            Assert.That(Settings.As<short>(ROOT_PATH, "Integer"), Is.TypeOf<short>().And.Not.EqualTo(default(short)));

            Assert.That(Settings.As<ushort>(INTEGER_PATH), Is.TypeOf<ushort>().And.Not.EqualTo(default(ushort)));
            Assert.That(Settings.As<ushort>(ROOT_PATH, "Integer"), Is.TypeOf<ushort>().And.Not.EqualTo(default(ushort)));

            Assert.That(Settings.As<double>(INTEGER_PATH), Is.TypeOf<double>().And.Not.EqualTo(default(double)));
            Assert.That(Settings.As<double>(ROOT_PATH, "Integer"), Is.TypeOf<double>().And.Not.EqualTo(default(double)));

            Assert.That(Settings.As<decimal>(INTEGER_PATH), Is.TypeOf<decimal>().And.Not.EqualTo(default(decimal)));
            Assert.That(Settings.As<decimal>(ROOT_PATH, "Integer"), Is.TypeOf<decimal>().And.Not.EqualTo(default(decimal)));

            Assert.That(Settings.As<float>(INTEGER_PATH), Is.TypeOf<float>().And.Not.EqualTo(default(float)));
            Assert.That(Settings.As<float>(ROOT_PATH, "Integer"), Is.TypeOf<float>().And.Not.EqualTo(default(float)));

            Assert.That(Settings.As<byte>(INTEGER_PATH), Is.TypeOf<byte>().And.Not.EqualTo(default(byte)));
            Assert.That(Settings.As<byte>(ROOT_PATH, "Integer"), Is.TypeOf<byte>().And.Not.EqualTo(default(byte)));

            Assert.That(Settings.As<sbyte>(INTEGER_PATH), Is.TypeOf<sbyte>().And.Not.EqualTo(default(sbyte)));
            Assert.That(Settings.As<sbyte>(ROOT_PATH, "Integer"), Is.TypeOf<sbyte>().And.Not.EqualTo(default(sbyte)));
        }

        [Theory]
        public void Get_As_Returns_Not_Null_And_Setting_Converted()
        {
            Assert.That(Settings.Get(STRING_PATH).As<string>(), Is.TypeOf<string>().And.Not.EqualTo(default(string)));
            Assert.That(Settings.Get(ROOT_PATH, "String").As<string>(), Is.TypeOf<string>().And.Not.EqualTo(default(string)));

            Assert.That(Settings.Get(CHARACTER_PATH).As<char>(), Is.TypeOf<char>().And.Not.EqualTo(default(char)));
            Assert.That(Settings.Get(ROOT_PATH, "Character").As<char>(), Is.TypeOf<char>().And.Not.EqualTo(default(char)));

            // Default for boolean is "false" which isn't good for testing
            Assert.That(Settings.Get(BOOLEAN_PATH).As<bool>(), Is.TypeOf<bool>());
            Assert.That(Settings.Get(ROOT_PATH, "Boolean").As<bool>(), Is.TypeOf<bool>());

            Assert.That(Settings.Get(INTEGER_PATH).As<int>(), Is.TypeOf<int>().And.Not.EqualTo(default(int)));
            Assert.That(Settings.Get(ROOT_PATH, "Integer").As<int>(), Is.TypeOf<int>().And.Not.EqualTo(default(int)));

            Assert.That(Settings.Get(INTEGER_PATH).As<uint>(), Is.TypeOf<uint>().And.Not.EqualTo(default(uint)));
            Assert.That(Settings.Get(ROOT_PATH, "Integer").As<uint>(), Is.TypeOf<uint>().And.Not.EqualTo(default(uint)));

            Assert.That(Settings.Get(INTEGER_PATH).As<long>(), Is.TypeOf<long>().And.Not.EqualTo(default(long)));
            Assert.That(Settings.Get(ROOT_PATH, "Integer").As<long>(), Is.TypeOf<long>().And.Not.EqualTo(default(long)));

            Assert.That(Settings.Get(INTEGER_PATH).As<ulong>(), Is.TypeOf<ulong>().And.Not.EqualTo(default(ulong)));
            Assert.That(Settings.Get(ROOT_PATH, "Integer").As<ulong>(), Is.TypeOf<ulong>().And.Not.EqualTo(default(ulong)));

            Assert.That(Settings.Get(INTEGER_PATH).As<short>(), Is.TypeOf<short>().And.Not.EqualTo(default(short)));
            Assert.That(Settings.Get(ROOT_PATH, "Integer").As<short>(), Is.TypeOf<short>().And.Not.EqualTo(default(short)));

            Assert.That(Settings.Get(INTEGER_PATH).As<ushort>(), Is.TypeOf<ushort>().And.Not.EqualTo(default(ushort)));
            Assert.That(Settings.Get(ROOT_PATH, "Integer").As<ushort>(), Is.TypeOf<ushort>().And.Not.EqualTo(default(ushort)));

            Assert.That(Settings.Get(INTEGER_PATH).As<double>(), Is.TypeOf<double>().And.Not.EqualTo(default(double)));
            Assert.That(Settings.Get(ROOT_PATH, "Integer").As<double>(), Is.TypeOf<double>().And.Not.EqualTo(default(double)));

            Assert.That(Settings.Get(INTEGER_PATH).As<decimal>(), Is.TypeOf<decimal>().And.Not.EqualTo(default(decimal)));
            Assert.That(Settings.Get(ROOT_PATH, "Integer").As<decimal>(), Is.TypeOf<decimal>().And.Not.EqualTo(default(decimal)));

            Assert.That(Settings.Get(INTEGER_PATH).As<float>(), Is.TypeOf<float>().And.Not.EqualTo(default(float)));
            Assert.That(Settings.Get(ROOT_PATH, "Integer").As<float>(), Is.TypeOf<float>().And.Not.EqualTo(default(float)));

            Assert.That(Settings.Get(INTEGER_PATH).As<byte>(), Is.TypeOf<byte>().And.Not.EqualTo(default(byte)));
            Assert.That(Settings.Get(ROOT_PATH, "Integer").As<byte>(), Is.TypeOf<byte>().And.Not.EqualTo(default(byte)));

            Assert.That(Settings.Get(INTEGER_PATH).As<sbyte>(), Is.TypeOf<sbyte>().And.Not.EqualTo(default(sbyte)));
            Assert.That(Settings.Get(ROOT_PATH, "Integer").As<sbyte>(), Is.TypeOf<sbyte>().And.Not.EqualTo(default(sbyte)));
        }

        [Theory]
        public void Get_As_When_Called_Several_Ways_Returns_Expected_Results()
        {
            // Happy Path (Settings.Get.As)
            Assert.That(Settings.Get(INTEGER_PATH).As<int>(), Is.TypeOf<int>().And.Not.EqualTo(default(int)));

            // Sad Path (stored settings no long just returns default when not provided a key; key is stored internally)
            IApplicationSettings newSettings = Settings.Get(INTEGER_PATH);
            Assert.That(Settings.As<int>(), Is.TypeOf<int>().And.EqualTo(default(int)));
            Assert.That(newSettings.As<int>(), Is.TypeOf<int>().And.Not.EqualTo(default(int)));
        }
    }
}