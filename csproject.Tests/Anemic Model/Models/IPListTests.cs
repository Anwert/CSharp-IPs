using NUnit.Framework;
using System.Collections.Generic;
using csproject.Models;

namespace csproject.UnitTests.Models
{
    [TestFixture]
    public class IPListTests
    {
        [Test]
        public void Test_Constructor()
        {
            var ip_list = new IPList();

            Assert.NotNull(ip_list.Data);
        }

        [Test]
        public void Test_True_Constructor_2()
        {
            var ip_list = new IPList(new IP("1", "12.12.12.12/32"));
            var ip = new IP("1", "12.12.12.12/32");

            Assert.True(ip_list.Data[0].Equals(ip));
        }

        [TestCase("1", "13.12.12.12/32")]
        [TestCase("2", "12.12.12.12/32")]
        [TestCase("1", "12.12.12.12/31")]    
        public void Test_False_Constructor_2(string id, string subnet)
        {
            var ip_list = new IPList(new IP("1", "12.12.12.12/32"));
            var ip = new IP(id, subnet);

            Assert.False(ip_list.Data[0].Equals(ip));
        }

        [Test]
        public void Test_True_Constructor_3()
        {
            var ip = new IP("1", "12.12.12.12/32");
            var tmp_ip_list = new List<IP>();
            tmp_ip_list.Add(ip);

            var ip_list = new IPList(tmp_ip_list);

            var expected_ip_list = new IPList();
            expected_ip_list.Data.Add(ip);

            Assert.True(ip_list.Equals(expected_ip_list));
        }


        [TestCase("1", "13.12.12.12/32")]
        [TestCase("2", "12.12.12.12/32")]
        [TestCase("1", "12.12.12.12/31")]    
        public void Test_False_Constructor_3(string id, string subnet)
        {
            var ip = new IP("1", "12.12.12.12/32");
            var tmp_ip_list = new List<IP> { ip };
            var ip_list = new IPList(tmp_ip_list);

            var expected_ip = new IP(id, subnet);
            var expected_ip_list = new IPList();
            expected_ip_list.Data.Add(expected_ip);

            Assert.False(ip_list.Equals(expected_ip_list));
        }

        [Test]
        public void Test_True_Equals()
        {
            var ip = new IP("1", "12.12.12.12/32");

            var ip_list = new IPList(ip);
            var expected_ip_list = new IPList(ip);

            Assert.True(ip_list.Equals(expected_ip_list));
        }

        [TestCase("1", "13.12.12.12/32")]
        [TestCase("2", "12.12.12.12/32")]
        [TestCase("1", "12.12.12.12/31")]  
        public void Test_False_Equals(string id, string subnet)
        {
            var ip = new IP("1", "12.12.12.12/32");
            var expected_ip = new IP(id, subnet);

            var ip_list = new IPList(ip);
            var expected_ip_list = new IPList(expected_ip);

            Assert.False(ip_list.Equals(expected_ip_list));
        }

        [Test]
        public void Test_False_Equals2()
        {
            var ip = new IP("1", "12.12.12.12/32");

            var ip_list = new IPList(ip);
            var expected_ip_list = new IPList();

            Assert.False(ip_list.Equals(expected_ip_list));
        }

        [Test]
        public void Test_False_Equals3()
        {
            var ip = new IP("1", "12.12.12.12/32");

            var ip_list = new IPList(ip);

            var expected_ip_list = new IPList(ip);
            expected_ip_list.Data.Add(new IP("1", "13.13.13.13/29"));

            Assert.False(ip_list.Equals(expected_ip_list));
        }
    }
}