namespace KnockTests
{
    using System;
    using Knock;
    using Xunit;

    public class KnockClientTest
    {
        [Fact]
        public void TestEmptyAPIKey()
        {
            Assert.Throws<ArgumentException>(
                () => new KnockClient(new KnockOptions { }));
        }
    }
}
