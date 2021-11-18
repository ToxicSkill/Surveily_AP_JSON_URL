using System.Collections;
using System.Collections.Generic;

namespace JSON_URL.UnitTests
{
    public class TestHelper : IEnumerable<object[]>
    { 
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] {0, new[] {"https://www;", "https://    "}};
            yield return new object[] {0, new[] {"", "", ""}};
            yield return new object[] {0, new[] {";;;;;;;"}};
            yield return new object[] {0, new[] {"abc;", "def;"}};
            yield return new object[] {2, new[] {"https://www.lipsum.com;",
                "https://surveily.com/pl/surveily-zwieksz-bezpieczenstwo-miejsca-pracy/;",
                "abc",
                ""}};
            yield return new object[] {2, new[] {"https://www.lipsum.com;", "https://www.lipsum.com;"}};
            yield return new object[] {3, new[] {"https://www.lipsum.com;",
                "https://surveily.com/pl/surveily-zwieksz-bezpieczenstwo-miejsca-pracy/;",
                "https://stackoverflow.com"}};
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}