using NUnit.Framework;
using System.Collections.Generic;
using csproject.Services;
using csproject.Models;

namespace csproject.UnitTests.Services
{
    [TestFixture]
    public class SubnetsServiceTests
    {
        private readonly SubnetsService _subnetsService;
        public SubnetsServiceTests()
        {
            _subnetsService = new SubnetsService();
        }

        [TestCase("0.0.0.0/+0")]
        [TestCase("0.0.0.0/-0")]
        [TestCase("0.0.0.0/0")]
        public void Test_Correct_IP_ValidateIP(string subnet)
        {
            Assert.True(_subnetsService.ValidateIP(subnet));
        }

        [TestCase("-1.0.0.0/32")]
        [TestCase("256.0.0.0/+32")]
        [TestCase("256.0.0.0/-32")]
        [TestCase("255.0.0./32")]
        [TestCase("255.0.0.0/33")]
        [TestCase("255.0.0.0/-1")]
        public void Test_Incorrect_ValidateIP(string subnet)
        {
            Assert.False(_subnetsService.ValidateIP(subnet));
        }

        [TestCase("0.0.0.0/1", "0.0.0.0/0")]
        [TestCase("12.12.12.12/32", "12.12.12.12/30")]
        public void Test_Return_1_CheckIsInSubnet(string subnet, string subnet2)
        {
            var ip = new IP("1", subnet);
            var ip2 = new IP("2", subnet2);
            Assert.AreEqual(1, _subnetsService.CheckIsInSubnet(ip, ip2));
        }

        [TestCase("0.0.0.0/0", "12.12.12.12/30")]
        [TestCase("12.12.12.12/31", "12.12.12.12/32")]
        public void Test_Return_0_CheckIsInSubnet(string subnet, string subnet2)
        {
            var ip = new IP("1", subnet);
            var ip2 = new IP("2", subnet2);
            Assert.AreEqual(0, _subnetsService.CheckIsInSubnet(ip, ip2));
        }

        [TestCase("0.0.0.0/0", "255.255.255.255/0")]
        [TestCase("12.12.12.12/32", "12.12.12.12/32")]
        public void Test_Return_2_CheckIsInSubnet(string subnet, string subnet2)
        {
            var ip = new IP("1", subnet);
            var ip2 = new IP("2", subnet2);
            Assert.AreEqual(2, _subnetsService.CheckIsInSubnet(ip, ip2));
        }

        [Test]
        public void Test_True_GroupIPs()
        {
            var ip = new IP("1", "12.12.12.12/30");
            var ip2 = new IP("2", "12.12.12.12/31");
            var ip3 = new IP("3", "12.12.12.12/32");
            var ip4 = new IP("4", "14.14.14.14/30");

            var grouped_ips = new GroupedIPs(ip);
            grouped_ips.InnerIPsList.Add(ip2);
            grouped_ips.InnerIPsList.Add(ip3);

            var grouped_ips2 = new GroupedIPs(ip4);

            var expected_grouped_ips_list = new List<GroupedIPs>
            {
                grouped_ips,
                grouped_ips2
            };

            var ip_list = new IPList(ip);
            ip_list.Data.Add(ip2);
            ip_list.Data.Add(ip3);
            ip_list.Data.Add(ip4);

            var ret = _subnetsService.GroupIPs(ip_list);

            CollectionAssert.AreEqual(ret, expected_grouped_ips_list);
        }

        [Test]
        public void Test_True_GroupIPs2()
        {
            var ip = new IP("1", "12.12.12.12/30");
            var ip2 = new IP("2", "12.12.12.12/30");
            var ip3 = new IP("3", "12.12.12.12/32");
            var ip4 = new IP("4", "14.14.14.14/30");

            var grouped_ips = new GroupedIPs(ip);
            grouped_ips.IPsList.Add(ip2);
            grouped_ips.InnerIPsList.Add(ip3);

            var grouped_ips2 = new GroupedIPs(ip4);

            var expected_grouped_ips_list = new List<GroupedIPs>
            {
                grouped_ips,
                grouped_ips2
            };

            var ip_list = new IPList(ip);
            ip_list.Data.Add(ip2);
            ip_list.Data.Add(ip3);
            ip_list.Data.Add(ip4);

            var ret = _subnetsService.GroupIPs(ip_list);

            CollectionAssert.AreEqual(ret, expected_grouped_ips_list);
        }

        [Test]
        public void Test_False_GroupIPs()
        {
            var ip = new IP("1", "13.13.13.13/29");
            var ip2 = new IP("2", "13.13.13.13/1");
            var ip3 = new IP("3", "13.13.13.13/32");
            var ip4 = new IP("4", "13.13.13.13/30");

            var grouped_ips = new GroupedIPs(ip);
            grouped_ips.InnerIPsList.Add(ip2);
            grouped_ips.InnerIPsList.Add(ip3);

            var grouped_ips2 = new GroupedIPs(ip4);

            var expected_grouped_ips_list = new List<GroupedIPs>
            {
                grouped_ips,
                grouped_ips2
            };

            var ip_list = new IPList(ip);
            ip_list.Data.Add(ip2);
            ip_list.Data.Add(ip3);
            ip_list.Data.Add(ip4);

            var ret = _subnetsService.GroupIPs(ip_list);

            CollectionAssert.AreNotEqual(ret, expected_grouped_ips_list);
        }
    }
}