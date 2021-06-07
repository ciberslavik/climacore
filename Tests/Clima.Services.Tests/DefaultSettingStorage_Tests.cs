using System.Diagnostics;
using Clima.Services.Configuration;
using NSubstitute;
using NUnit.Framework;

namespace Clima.Services.Tests
{
    public class DefaultSettingStorage_Tests
    {
        [SetUp]
        public void Setup()
        {
            TextWriterTraceListener myWriter = new TextWriterTraceListener(System.Console.Out);
            
        }

        [Test]
        public void RegisterNewItem_Test()
        {
            var serializer = Substitute.For<IConfigurationSerializer>();
            var fs = Substitute.For<IFileSystem>();
            var store = new DefaultConfigurationStorage(serializer, fs);
            
            store.RegisterConfig<FakeConfigItem>();
            
            
            Assert.Pass();
        }
    }
}