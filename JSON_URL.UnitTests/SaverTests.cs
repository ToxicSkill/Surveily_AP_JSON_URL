using System.Text;
using System.Threading.Tasks;
using JSON_URL.Modules;
using Xunit;

namespace JSON_URL.UnitTests
{
    public class SaverTests
    {
        private readonly InternetData _idtDefault;
        private readonly InternetData _idtCustom;
        private const string FolderName = "NewJsonFolder";

        public SaverTests()
        {
            _idtDefault = new();
            _idtCustom = new(FolderName);
        }
        
        [Fact]
        public async Task SaveDataShould0TheirEmpty()
        {
            var urls = string.Empty;
            Assert.Equal(0, await _idtDefault.GetDataFromUrls(urls));
        }
        
        private string CombineData(string[] data)
        {
            StringBuilder sw = new();
            foreach (var value in data)
            {
                sw.Append(value);
            }
            return sw.ToString();
        }

        [Theory]
        [ClassData(typeof(TestHelper))]
        public async Task SaveDataShouldEqualTheory_Default(int expected, params string[] strings)
        {
            if (expected.Equals(await _idtDefault.GetDataFromUrls(CombineData(strings))) && expected != 0)
                Assert.True(await _idtDefault.SaveData());
        }
        
        [Theory]
        [ClassData(typeof(TestHelper))]
        public async Task SaveDataShouldEqualTheory(int expected, params string[] strings)
        {
            if (expected.Equals(await _idtCustom.GetDataFromUrls(CombineData(strings))) && expected != 0)
                Assert.True(await _idtCustom.SaveData());
        }
        
    }
}