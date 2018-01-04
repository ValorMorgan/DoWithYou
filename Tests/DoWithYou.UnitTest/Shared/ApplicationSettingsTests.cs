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

                // Validate file exists (needed for tests to work)
                if (!DoesAppSettingsFileExist())
                    Assert.Inconclusive();

                IConfigurationBuilder builder = new ConfigurationBuilder();
                builder.SetBasePath(Directory.GetCurrentDirectory());

                IConfiguration configuration = builder
                    .AddJsonFile("appsettings.json")
                    ?.Build();

                Resolver.InitializeContainerWithConfiguration(configuration);
                _settings = new ApplicationSettings(Resolver.Resolve<IConfiguration>());

                return _settings;
            }
        }

        private bool DoesAppSettingsFileExist() =>
            File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"));
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

            Assert.That(() => Settings[badPath], Throws.Nothing);
            Assert.That(Settings[badPath], Is.Null);
            
            Assert.That(() => Settings["Some", "Bad", "Path"], Throws.Nothing);
            Assert.That(Settings["Some", "Bad", "Path"], Is.Null);
        }

        [Theory]
        public void Get_Returns_Not_Null_And_InstanceOf_IApplicationSettings()
        {
            Assert.That(Settings.Get(STRING_PATH), Is.Not.Null.And.InstanceOf<IApplicationSettings>());
            Assert.That(Settings.Get(ROOT_PATH, "String"), Is.Not.Null.And.InstanceOf<IApplicationSettings>());
        }

        [Theory]
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
    }
}