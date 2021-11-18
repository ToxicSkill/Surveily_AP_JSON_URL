using System.Text;
using System.Threading.Tasks;
using JSON_URL.Modules;
using Xunit;

namespace JSON_URL.UnitTests
{
    public class InternetDataTests
    {
        private readonly InternetData _idtDefault;

        public InternetDataTests()
        {
            _idtDefault = new();
        }
        
        [Fact]
        public async Task GetDataFromUrlsShould0TheirEmpty()
        {
            var urls = string.Empty;
            Assert.Equal(0, await _idtDefault.GetDataFromUrls(urls));
        }
        
        [Theory]
        [ClassData(typeof(TestHelper))]
        public async Task GetDataFromUrlsShouldEqualTheory(int expected, params string[] strings)
        {
            StringBuilder sw = new();
            foreach (var value in strings)
            {
                sw.Append(value);
            }

            Assert.Equal(expected, await _idtDefault.GetDataFromUrls(sw.ToString()));
        }
        
    }
}