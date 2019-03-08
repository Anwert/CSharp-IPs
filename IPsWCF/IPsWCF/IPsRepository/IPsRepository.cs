using System.Linq;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using IPsWCF.Models;

namespace IPsWCF
{
    /// <summary>
    /// Сервис работы с базой данных IP-адресов. Реализация интерфейса IIPsRepositoryService.
    /// </summary>
    public class IPsRepositoryWCF: IIPsRepositoryWCF
    {
        /// <summary>
        /// Строка подключения к базе данных.
        /// </summary>
        private readonly string _connectionString;

        /// <summary>
        /// Конструктор для создания нового объекта класса IPsRepositoryService, в котором происходит инициализация поля _connectionString.
        /// </summary>
        public IPsRepositoryWCF()
        {
            _connectionString = "Data Source=(local);Initial Catalog=IPsDB;Integrated Security=True";
        }

        /// <summary>
        /// Подключение к базе данных.
        /// </summary>
        private IDbConnection Connection => new SqlConnection(_connectionString);

        /// <summary>
        /// Получить список IP-адресов. Метод написан с помощью Dapper.
        /// </summary>
        /// <returns>Возвращает объект модели IPList.</returns>
        public IPList GetIPList()
        {
            using (var db_connection = Connection)
            {
                db_connection.Open();

                return new IPList(db_connection.Query<IP>(@"
select PK_ip_id, address, mask, subnet from dbo.IPs
").ToArray());
            }
        }

        /// <summary>
        /// Создать IP-адрес. Метод написан с помощью ADO.Net.
        /// </summary>
        /// <param name="ip">IP-адрес, который будет внесен в базу данных.</param>
        public void CreateIP(IP ip)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = @"
insert into dbo.IPs (PK_ip_id, address, mask, subnet)
values(@Id, @Address, @Mask, @Subnet)
";

                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", ip.Id);
                command.Parameters.AddWithValue("@Address", ip.Address);
                command.Parameters.AddWithValue("@Mask", ip.Mask);
                command.Parameters.AddWithValue("@Subnet", ip.Subnet);

                connection.Open();
                var reader = command.ExecuteReader();
                reader.Close();
            }
        }

        /// <summary>
        /// Удалить IP-адрес. Метод написан с помощью ADO.Net.
        /// </summary>
        /// <param name="id">Id удаляемого IP-адреса.</param>
        public void DeleteIP(string id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = @"
delete from dbo.IPs
where PK_ip_id = @Id
";

                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", id);

                connection.Open();
                var reader = command.ExecuteReader();
                reader.Close();
            }
        }

        /// <summary>
        /// Изменить IP-адрес. Метод написан с помощью Dapper.
        /// </summary>
        /// <param name="ip">IP-адрес с id адреса, который будет изменен и обновленными остальными полями.</param>
        public void UpdateIP(IP ip)
        {
            using (var db_connection = Connection)
            {
                var query = @"
update dbo.IPs
set address = @Address, mask = @Mask, subnet = @Subnet
where PK_ip_id = @Id
";
                db_connection.Open();
                db_connection.Query(query, ip);
            }
        }
    }
}
