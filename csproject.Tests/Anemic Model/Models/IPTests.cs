using NUnit.Framework;
using csproject.Models;

namespace csproject.UnitTests.Models
{
    [TestFixture]
    public class IPTests
    {
        [TestCase("0.0.0.0/+0")]
        [TestCase("0.0.0.0/-0")]
        [TestCase("0.0.0.0/0")]
        public void Test_Constructor(string subnet)
        {
            var ip = new IP("1", subnet);

            var other_ip = new IP("1", "0.0.0.0/0");

            Assert.True(ip.Equals(other_ip));
        }


        [Test]
        public void Test_True_Equals()
        {
            var ip = new IP("1", "12.12.12.12/32");
            var other_ip = new IP("1", "12.12.12.12/32");

            Assert.True(ip.Equals(other_ip));
        }

        [TestCase("1", "13.12.12.12/32")]
        [TestCase("2", "12.12.12.12/32")]
        [TestCase("1", "12.12.12.12/31")]
        public void Test_False_Equals(string id, string subnet)
        {
            var ip = new IP(id, subnet);
            var other_ip = new IP("1", "13.13.13.13/32");

            Assert.False(ip.Equals(other_ip));
        }
    }
}