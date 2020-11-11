using System.Threading.Tasks;
using FluentAssertions;
using Flurl;
using Shared;
using Xunit;

namespace Flurl2Tests
{
    public class BasePathTests : FlurlHttpClientTestsBase
    {
        [Fact]
        public async Task HttpClientWithBasePathTest()
        {
            const string baseAddress = "http://fake.com/api/";
            const string path1 = "path1";
            const string path2 = "path2";
            Url expectedUrl = baseAddress.AppendPathSegments(path1, path2);

            Url requestUri = path1.AppendPathSegment(path2);
            var actualUrl = await GetRequestUrl(baseAddress, requestUri);

            actualUrl.Should().Be(expectedUrl);
        }
    }
}