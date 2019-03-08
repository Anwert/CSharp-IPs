using System.Collections.Generic;
using NUnit.Framework;
using System;
using System.Linq;
using Newtonsoft.Json;
using csproject.Models;
using csproject.Repositories;
using csproject.Services;
using Moq;
using System.Threading.Tasks;

namespace csproject.UnitTests.Repositories
{
    [TestFixture]
    public class IPServiceTests
    {
        private IPList SetDefaultIPList()
        {
            var ip = new IP("ca3d03674e2045ef85b824a14a386698", "12.12.12.12/32");
            var ip2 = new IP("38a8e64feb7244da816063d63868f38e", "12.12.12.12/30");
            var ip_list = new IPList(ip);
            ip_list.Data.Add(ip2);

            return ip_list;
        }

        private bool CompareObjects(object obj, object another)
        {
            if (ReferenceEquals(obj, another)) return true;
            if ((obj == null) || (another == null)) return false;

            var objJson = JsonConvert.SerializeObject(obj);
            var anotherJson = JsonConvert.SerializeObject(another);

            return objJson == anotherJson;
        }

        [Test]
        public void Test_True_GetIPList()
        {
            var ip_list = SetDefaultIPList();

            var mock = new Mock<IIPsRepository>();
            mock.Setup(p => p.GetIPList()).Returns(ip_list);

            var ip_controller_service = new IPService(mock.Object);

            var returned_ip_list_object = ip_controller_service.GetIPList();

            var expected_ip_list_data = new List<IP>
            {
                new IP("ca3d03674e2045ef85b824a14a386698", "12.12.12.12/32"),
                new IP("38a8e64feb7244da816063d63868f38e", "12.12.12.12/30")
            };

            var expected_ip_list_object = new {
                error = false,
                data = expected_ip_list_data
            };

            Assert.True(CompareObjects(returned_ip_list_object, expected_ip_list_object));
        }

        [Test]
        public void Test_False_GetIPList()
        {
            var ip_list = SetDefaultIPList();

            var mock = new Mock<IIPsRepository>();
            mock.Setup(p => p.GetIPList()).Returns(ip_list);

            var ip_controller_service = new IPService(mock.Object);

            var returned_ip_list_object = ip_controller_service.GetIPList();

            var expected_ip_list_data = new List<IP>
            {
                new IP("1", "13.13.13.13/32"),
                new IP("2", "12.12.12.12/30")
            };

            var expected_ip_list_object = new {
                error = false,
                data = expected_ip_list_data
            };

            Assert.False(CompareObjects(returned_ip_list_object, expected_ip_list_object));
        }

        [Test]
        public async Task Test_CreateIPAsync()
        {
            var ip_list = SetDefaultIPList();

            var subnet = "15.15.15.15/24";

            var mock = new Mock<IIPsRepository>();
            mock.Setup(P => P.GetIPList()).Returns(ip_list);
            mock.Setup(p => p.CreateIPAsync(It.IsAny<IP>())).Callback((IP ip_arg) => {
                    ip_list.Data.Add(ip_arg);
                });

            var ip_controller_service = new IPService(mock.Object);
            await ip_controller_service.CreateIPAsync(subnet);

            var expected_ip = ip_list.Data.Single(ip => ip.Subnet == subnet);

            Assert.NotNull(expected_ip);
        }

        [Test]
        public async Task Test_IP_Is_Not_Unique_Exception_CreateIP()
        {
            var ip_list = SetDefaultIPList();

            var mock = new Mock<IIPsRepository>();
            mock.Setup(p => p.GetIPList()).Returns(ip_list);

            var ip_controller_service = new IPService(mock.Object);

            var returned_obj = await ip_controller_service.CreateIPAsync("12.12.12.12/32");

            var expected_obj = new {
                error = true,
                message = "This IP address already exists."
            };

            Assert.True(CompareObjects(returned_obj, expected_obj));
        }

        [Test]
        public async Task Test_True_DeleteIP()
        {
            var ip_list = SetDefaultIPList();

            var mock = new Mock<IIPsRepository>();
            mock.Setup(p => p.GetIPList()).Returns(ip_list);
            mock.Setup(p => p.DeleteIPAsync(It.IsAny<string>())).Callback((string id) => {
                    int index = ip_list.Data.FindIndex(ip => ip.Id == id);
                    ip_list.Data.RemoveAt(index);
                });

            var ip_controller_service = new IPService(mock.Object);

            await ip_controller_service.DeleteIPAsync("ca3d03674e2045ef85b824a14a386698");

            var returned_obj = ip_controller_service.GetIPList();

            var expected_ip_list_data = new List<IP>
            {
                new IP("38a8e64feb7244da816063d63868f38e", "12.12.12.12/30")
            };

            var expected_obj = new {
                error = false,
                data = expected_ip_list_data
            };

            Assert.True(CompareObjects(returned_obj, expected_obj));
        }

        [Test]
        public async Task Test_False_DeleteIP()
        {
            var ip_list = SetDefaultIPList();

            var mock = new Mock<IIPsRepository>();
            mock.Setup(p => p.GetIPList()).Returns(ip_list);
            mock.Setup(p => p.DeleteIPAsync(It.IsAny<string>())).Callback((string id) => {
                    int index = ip_list.Data.FindIndex(ip => ip.Id == id);
                    ip_list.Data.RemoveAt(index);
                });

            var ip_controller_service = new IPService(mock.Object);

            await ip_controller_service.DeleteIPAsync("ca3d03674e2045ef85b824a14a386698");

            var returned_obj = ip_controller_service.GetIPList();

            var expected_ip_list_data = new List<IP>()
            {
                new IP("ca3d03674e2045ef85b824a14a386698", "12.12.12.12/32"),
                new IP("38a8e64feb7244da816063d63868f38e", "12.12.12.12/30")
            };

            var expected_obj = new {
                error = false,
                data = expected_ip_list_data
            };

            Assert.False(CompareObjects(returned_obj, expected_obj));
        }

        [Test]
        public async Task Test_True_UpdateIP()
        {
            var ip_list = SetDefaultIPList();

            var mock = new Mock<IIPsRepository>();
            mock.Setup(p => p.GetIPList()).Returns(ip_list);
            mock.Setup(p => p.UpdateIPAsync(It.IsAny<IP>())).Callback((IP ip_arg) => {
                    int index = ip_list.Data.FindIndex(ip => ip.Id ==ip_arg.Id);
                    ip_list.Data[index] = ip_arg;
                });

            var ip_controller_service = new IPService(mock.Object);

            await ip_controller_service.UpdateIPAsync("ca3d03674e2045ef85b824a14a386698", "14.14.14.14/28");

            var returned_obj = ip_controller_service.GetIPList();

            var expected_ip_list_data = new List<IP>
            {
                new IP("ca3d03674e2045ef85b824a14a386698", "14.14.14.14/28"),
                new IP("38a8e64feb7244da816063d63868f38e", "12.12.12.12/30")
            };

            var expected_obj = new {
                error = false,
                data = expected_ip_list_data
            };

            Assert.True(CompareObjects(returned_obj, expected_obj));
        }

        [Test]
        public async Task Test_False_UpdateIP()
        {

            var ip_list = SetDefaultIPList();

            var mock = new Mock<IIPsRepository>();
            mock.Setup(p => p.GetIPList()).Returns(ip_list);
            mock.Setup(p => p.UpdateIPAsync(It.IsAny<IP>())).Callback((IP ip_arg) => {
                    int index = ip_list.Data.FindIndex(ip => ip.Id ==ip_arg.Id);
                    ip_list.Data[index] = ip_arg;
                });

            var ip_controller_service = new IPService(mock.Object);

            await ip_controller_service.UpdateIPAsync("ca3d03674e2045ef85b824a14a386698", "14.14.14.14/28");

            var returned_obj = ip_controller_service.GetIPList();

            var expected_ip_list_data = new List<IP>
            {
                new IP("ca3d03674e2045ef85b824a14a386698", "12.12.12.12/32"),
                new IP("38a8e64feb7244da816063d63868f38e", "12.12.12.12/30")
            };

            var expected_obj = new {
                error = false,
                data = expected_ip_list_data
            };

            Assert.False(CompareObjects(returned_obj, expected_obj));
        }

        [Test]
        public async Task Test_IP_Is_Not_Unique_Exception_UpdateIP()
        {

            var ip_list = SetDefaultIPList();

            var mock = new Mock<IIPsRepository>();
            mock.Setup(p => p.GetIPList()).Returns(ip_list);

            var ip_controller_service = new IPService(mock.Object);

            var returned_obj = await ip_controller_service.UpdateIPAsync("ca3d03674e2045ef85b824a14a386698",
                "12.12.12.12/32");

            var expected_obj = new {
                error = true,
                message = "This IP address already exists."
            };

            Assert.True(CompareObjects(returned_obj, expected_obj));
        }

        [Test]
        public void Test_GetIPForUpdate()
        {
            var id = "ca3d03674e2045ef85b824a14a386698";
            var subnet = "12.12.12.12/32";
            var ip = new IP(id, subnet);

            var mock = new Mock<IIPsRepository>();
            mock.Setup(p => p.GetIPById(id)).Returns(ip);

            var ip_controller_service = new IPService(mock.Object);

            var returned_ip = ip_controller_service.GetIPForUpdate(id);

            Assert.AreEqual(returned_ip, ip);
        }
    }
}