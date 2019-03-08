using System;

namespace csproject.Models
{
    /// <summary>
    /// Модель IP-адреса.
    /// </summary>
    public class IP: IEquatable<IP>
    {
        /// <summary>
        /// Id IP-адреса.
        /// </summary>
        public string Id { get; }
        /// <summary>
        /// Адресная часть IP-адрес.
        /// </summary>
        public string Address { get; }
        /// <summary>
        /// Маска (префикс) IP-адреса.
        /// </summary>
        public int Mask { get; }
        /// <summary>
        /// Строка IP-адрес + префикс маски подсети.
        /// </summary>
        public string Subnet { get; set; }

        /// <summary>
        /// Конструктор создания модели IP.
        /// </summary>
        /// <param name="id">Id IP-адреса.</param>
        /// <param name="subnet">Адрес + префикс маски подсети.</param>
        public IP (string id, string subnet)
        {
            var mask_index = subnet.IndexOfAny("/".ToCharArray());
            var address = subnet.Substring(0, mask_index);
            var mask = int.Parse(subnet.Substring(mask_index + 1));

            Id = id;
            Address = address;
            Mask = mask;
            // для того, чтобы в таблицу вносились нормальные значения если 
            // пользователь введет подсеть вида "127.0.0.1/+24"
            Subnet = $"{address}/{mask.ToString()}";
        }

        /// <summary>
        /// Метод для сравнения объектов класса IP.
        /// </summary>
        /// <param name="other_ip">Объект IP, с которым будем сравнивать.</param>
        /// <returns>Возвращает true если объекты равны, в противном случае возвращает false.</returns>
        public bool Equals(IP other_ip)
        {
            return Id == other_ip.Id && Subnet == other_ip.Subnet;
        }
    }
}