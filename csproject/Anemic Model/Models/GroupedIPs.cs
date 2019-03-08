using System;
using System.Collections.Generic;
using System.Linq;

namespace csproject.Models
{
    /// <summary>
    /// Модель сгруппированных IP-адресов.
    /// </summary>
    public class GroupedIPs: IEquatable<GroupedIPs>
    {
        /// <summary>
        /// Список IP-адресов, которые входят в подсеть, по которой были сгуппированы меньшие подсети.
        /// </summary>
        public List<IP> IPsList { get; }
        /// <summary>
        /// Список IP-адресов с подсетями, которые входят в подсеть списка IPsList.
        /// </summary>
        public List<IP> InnerIPsList { get; }

        /// <summary>
        /// Конструктор создания модели GroupedIPs.
        /// </summary>
        /// <param name="ip">IP-адресс (класс IP).</param>  
        public GroupedIPs(IP ip)
        {
            IPsList = new List<IP> { ip };

            InnerIPsList = new List<IP>();
        }

        /// <summary>
        /// Метод для сравнения объектов класса GroupedIPs.
        /// </summary>
        /// <param name="other_grouped_ips">Объект GroupedIPs, с которым будем сравнивать.</param>
        /// <returns>Возвращает true если объекты равны, в противном случае возвращает false.</returns>
        public bool Equals(GroupedIPs other_grouped_ips)
        {
            var ips_list = IPsList;
            ips_list.GetEnumerator();

            var inner_ips_list = InnerIPsList;
            inner_ips_list.GetEnumerator();

            var other_ips_list =
                other_grouped_ips.IPsList;
            other_ips_list.GetEnumerator();

            var other_inner_ips_list =
                other_grouped_ips.InnerIPsList;
            other_inner_ips_list.GetEnumerator();

            return ips_list.SequenceEqual(other_ips_list) &&
                inner_ips_list.SequenceEqual(other_inner_ips_list);
        }
    }
}