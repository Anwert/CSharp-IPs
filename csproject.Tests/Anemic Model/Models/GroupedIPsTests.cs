using NUnit.Framework;
using csproject.Models;

namespace csproject.UnitTests.Models
{
    [TestFixture]
    public class GroupedIPsTests
    {
        [Test]
        public void Test_True_Constructor()
        {
            var ip = new IP("1", "12.12.12.12/32");
            var grouped_ips = new GroupedIPs(ip);

            Assert.True(grouped_ips.IPsList[0].Equals(ip));
        }

        [TestCase("1", "13.12.12.12/32")]
        [TestCase("2", "12.12.12.12/32")]
        [TestCase("1", "12.12.12.12/31")]
        public void Test_False_Constructor(string id, string subnet)
        {
            var ip = new IP("1", "12.12.12.12/32");
            var grouped_ips = new GroupedIPs(ip);

            var expected_ip = new IP(id, subnet);

            Assert.False(grouped_ips.IPsList[0].Equals(expected_ip));
        }

        [Test]
        public void Test_True_Equals()
        {
            var ip = new IP("1", "12.12.12.12/32");
            var grouped_ips = new GroupedIPs(ip);

            var expected_grouped_ips = new GroupedIPs(ip);

            Assert.True(grouped_ips.Equals(expected_grouped_ips));
        }

        [TestCase("1", "13.12.12.12/32")]
        [TestCase("2", "12.12.12.12/32")]
        [TestCase("1", "12.12.12.12/31")]
        public void Test_False_Equals(string id, string subnet)
        {
            var ip = new IP("1", "12.12.12.12/32");
            var grouped_ips = new GroupedIPs(ip);

            var expected_ip = new IP(id, subnet);
            var expected_grouped_ips = new GroupedIPs(expected_ip);

            Assert.False(grouped_ips.Equals(expected_grouped_ips));
        }
    }
}