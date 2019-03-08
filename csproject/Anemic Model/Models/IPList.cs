using System;
using System.Collections.Generic;
using System.Linq;

namespace csproject.Models
{
    /// <summary>
    /// Модель списка IP-адресов.
    /// </summary>
    public class IPList: IEquatable<IPList>
    {
        /// <summary>
        /// Поле, в котором хранится список IP-адресов.
        /// </summary>
        public List<IP> Data { get; }

        /// <summary>
        /// Конструктор создания модели IPList, создает пустой IPList.
        /// </summary>
        public IPList()
        {
            Data = new List<IP>();
        }

        /// <summary>
        /// Конструктор создания модели IPList.
        /// </summary>
        /// <param name="ip">IP-адрес, который будет первым элементом в списке IP-адресов.</param>
        public IPList(IP ip)
        {
            Data = new List<IP> { ip };
        }

        /// <summary>
        /// Конструктор создания модели IPList.
        /// </summary>
        /// <param name="ip_list">Список IP-адресов, из которого будет создан объект класса IPList.</param>
        public IPList(List<IP> ip_list)
        {
            Data = ip_list;
        }

        /// <summary>
        /// Метод для сравнения объектов класса IPList.
        /// </summary>
        /// <param name="other_ip_list">Список IP-адресов, который будет сравниваться с ip_list_data, объект класса IPList.</param>
        /// <returns>Возвращает true если списки IP-адресов равны, в противном случае возвращает false.</returns>
        public bool Equals(IPList other_ip_list)
        {
            var other_ip_list_data = other_ip_list.Data;
            other_ip_list_data.GetEnumerator();

            var ip_list_data = Data;
            ip_list_data.GetEnumerator();

            return ip_list_data.SequenceEqual(other_ip_list_data);
        }

        /// <summary>
        /// Проверить уникален ли IP-адрес.
        /// </summary>
        /// <param name="subnet">IP-адрес с префиксом маски подсети (поле Subnet в моделе IP), которые будут проверяться на уникальность.</param>
        /// <returns>Возвращает true если IP-адрес уникален, в противном случае возвращает false.</returns>
        public bool CheckIsIPUnique(string subnet)
        {
            return Data.FirstOrDefault(ip => ip.Subnet == subnet) == null;
        }
    }
}