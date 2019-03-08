using System;
using System.Collections.Generic;
using System.Linq;

namespace csproject.Models
{
    /// <summary>
    /// Модель списка сгруппированных IP-адресов.
    /// </summary>
    public class GroupedIPsList : IEquatable<GroupedIPsList>
    {
        /// <summary>
        /// Поле, в котором хранится список сгруппированных IP-адресов.
        /// </summary>
        public List<GroupedIPs> Data { get; }

        /// <summary>
        /// Конструктор создания модели GroupedIPsList.
        /// </summary>
        /// <param name="grouped_ips">Объект сгрупированых IP-адресов (класс GroupedIPs).</param>
        public GroupedIPsList(GroupedIPs grouped_ips)
        {
            Data = new List<GroupedIPs> { grouped_ips };
        }

        /// <summary>
        /// Конструктор создания модели GroupedIPsList.
        /// </summary>
        /// <param name="grouped_ips_list">Список объектов сгрупированых IP-адресов.</param>
        public GroupedIPsList(List<GroupedIPs> grouped_ips_list)
        {
            Data = grouped_ips_list;
        }

        /// <summary>
        /// Метод для сравнения объектов класса GroupedIPsList.
        /// </summary>
        /// <param name="other_grouped_ips_list">Объект GroupedIPsList, с которым будем сравнивать.</param>
        /// <returns>Возвращает true если объекты равны, в противном случае возвращает false.</returns>
        public bool Equals(GroupedIPsList other_grouped_ips_list)
        {
            var grouped_ips_list = Data;
            grouped_ips_list.GetEnumerator();

            var other_grouped_ips_list_data =
                other_grouped_ips_list.Data;
            other_grouped_ips_list_data.GetEnumerator();

            return grouped_ips_list.SequenceEqual(other_grouped_ips_list_data);
        }
    }
}