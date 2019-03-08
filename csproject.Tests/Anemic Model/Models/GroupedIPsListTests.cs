using NUnit.Framework;
using System.Collections.Generic;
using csproject.Models;

namespace csproject.UnitTests.Models
{
    [TestFixture]
    public class GroupedIPsListTests
    {
        [Test]
        public void Test_True_Constructor()
        {
            var ip = new IP("1", "12.12.12.12/32");
            var grouped_ips = new GroupedIPs(ip);

            var grouped_ips_list = new GroupedIPsList(grouped_ips);

            Assert.True(grouped_ips_list.Data[0].Equals(grouped_ips));
        }

        [TestCase("1", "13.12.12.12/32")]
        [TestCase("2", "12.12.12.12/32")]
        [TestCase("1", "12.12.12.12/31")]
        public void Test_False_Constructor(string id, string subnet)
        {
            var ip = new IP("1", "12.12.12.12/32");
            var grouped_ips = new GroupedIPs(ip);

            var grouped_ips_list = new GroupedIPsList(grouped_ips);

            var expected_ip = new IP(id, subnet);
            var expected_grouped_ips = new GroupedIPs(expected_ip);

            Assert.False(grouped_ips_list.Data[0].Equals(expected_grouped_ips));
        }

        [Test]
        public void Test_True_Constructor2()
        {
            var ip = new IP("1", "12.12.12.12/32");
            var grouped_ips = new GroupedIPs(ip);

            var tmp_grouped_ips_list = new List<GroupedIPs> { grouped_ips };

            var grouped_ips_list = new GroupedIPsList(tmp_grouped_ips_list);
            
            Assert.True(grouped_ips_list.Data[0].Equals(grouped_ips));
        }

        [TestCase("1", "13.12.12.12/32")]
        [TestCase("2", "12.12.12.12/32")]
        [TestCase("1", "12.12.12.12/31")]
        public void Test_False_Constructor2(string id, string subnet)
        {
            var ip = new IP("1", "12.12.12.12/32");
            var grouped_ips = new GroupedIPs(ip);

            var tmp_grouped_ips_list = new List<GroupedIPs> { grouped_ips };

            var grouped_ips_list = new GroupedIPsList(
                tmp_grouped_ips_list
            );

            var expected_ip = new IP(id, subnet);
            var expected_grouped_ips = new GroupedIPs(expected_ip);

            Assert.False(grouped_ips_list.Data[0].Equals(expected_grouped_ips));
        }

        [Test]
        public void Test_True_Equals()
        {
            var ip = new IP("1", "12.12.12.12/32");
            var grouped_ips = new GroupedIPs(ip);
            var grouped_ips_list = new GroupedIPsList(grouped_ips);

            var expected_grouped_ips = new GroupedIPs(ip);
            var expected_grouped_ips_list = new GroupedIPsList(expected_grouped_ips);

            Assert.True(grouped_ips_list.Equals(expected_grouped_ips_list));
        }

        [TestCase("1", "13.12.12.12/32")]
        [TestCase("2", "12.12.12.12/32")]
        [TestCase("1", "12.12.12.12/31")]
        public void Test_False_Equals(string id, string subnet)
        {
            var ip = new IP("1", "12.12.12.12/32");
            var grouped_ips = new GroupedIPs(ip);
            var grouped_ips_list = new GroupedIPsList(grouped_ips);

            var expected_ip = new IP(id, subnet);
            var expected_grouped_ips = new GroupedIPs(expected_ip);
            var expected_grouped_ips_list = new GroupedIPsList(expected_grouped_ips);

            Assert.False(grouped_ips_list.Equals(expected_grouped_ips_list));
        }
    }
}