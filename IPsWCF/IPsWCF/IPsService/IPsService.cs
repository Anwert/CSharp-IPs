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
    public class IPsRepositoryService: IIPsRepositoryService
    {
        /// <summary>
        /// Строка подключения к базе данных.
        /// </summary>
        readonly string _connectionString;

        /// <summary>
        /// Конструктор для создания нового объекта класса IPsRepositoryService, в котором происходит инициализация поля _connectionString.
        /// </summary>
        public IPsRepositoryService()
        {
            _connectionString = @"Data Source=localhost;User=SA;Password=Devilkings1;Initial Catalog=IPsDb";
        }

        /// <summary>
        /// Подключение к базе данных.
        /// </summary>
        public IDbConnection Connection
        {
            get
            {
                return new SqlConnection(_connectionString);
            }
        }

        /// <summary>
        /// Получить список IP-адресов. Метод написан с помощью Dapper.
        /// </summary>
        /// <returns>Возвращает объект модели IPList.</returns>
        public IPList GetIPList()
        {
            try
            {
                using (IDbConnection db_connection = Connection)
                {
                    db_connection.Open();

                    return
                        new IPList(db_connection.Query<IP>("select * from dbo.IPs").ToArray());
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Создать IP-адрес. Метод написан с помощью ADO.Net.
        /// </summary>
        /// <param name="ip">IP-адрес, который будет внесен в базу данных.</param>
        public void CreateIP(IP ip)
        {
            try
            {
                using (SqlConnection connection =
                    new SqlConnection(_connectionString))
                {
                    string query = "insert into dbo.IPs (PK_ip_id, address, mask, subnet)"
                        + " values(@Id, @Address, @Mask, @Subnet)";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Id", ip.Id);
                    command.Parameters.AddWithValue("@Address", ip.Address);
                    command.Parameters.AddWithValue("@Mask", ip.Mask);
                    command.Parameters.AddWithValue("@Subnet", ip.Subnet);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    reader.Close();
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Удалить IP-адрес. Метод написан с помощью ADO.Net.
        /// </summary>
        /// <param name="id">Id удаляемого IP-адреса.</param>
        public void DeleteIP(string id)
        {
            try
            {
                using (SqlConnection connection =
                    new SqlConnection(_connectionString))
                {
                    string query = "delete from dbo.IPs"
                        + " where PK_ip_id = @Id";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Id", id);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    reader.Close();
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Изменить IP-адрес. Метод написан с помощью Dapper.
        /// </summary>
        /// <param name="ip">IP-адрес с id адреса, который будет изменен и обновленными остальными полями.</param>
        public void UpdateIP(IP ip)
        {
            try
            {
                using (IDbConnection db_connection = Connection)
                {
                    string query = "update dbo.IPs"
                                + " set address = @Address, mask = @Mask, subnet = @Subnet"
                                + " where PK_ip_id = @Id";
                    db_connection.Open();
                    db_connection.Query(query, ip);
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
