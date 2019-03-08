using System.Runtime.Serialization;
using System;

namespace IPsWCF.Models
{
    /// <summary>
    /// Модель IP-адреса.
    /// </summary>
    [DataContract]
    public class IP
    {
        /// <summary>
        /// Id IP-адреса.
        /// </summary>
        [DataMember]
        public string Id { get; set; }
        /// <summary>
        /// Адресная часть IP-адреса.
        /// </summary>
        [DataMember]
        public string Address { get; set; }
        /// <summary>
        /// Префикс маски подсети IP-адреса.
        /// </summary>
        [DataMember]
        public int Mask { get; set; }
        /// <summary>
        /// IP-адрес + префикс маски подсети. 
        /// </summary>
        [DataMember]
        public string Subnet { get; set; }

        /// <summary>
        /// Конструктор создания модели IP.
        /// </summary>
        /// <param name="id">Id IP-адреса.</param>
        /// <param name="subnet">Адрес + префикс маски подсети IP-адреса.</param>
        public IP(string id, string subnet)
        {
            var mask_index = subnet.IndexOfAny("/".ToCharArray());
            var address = subnet.Substring(0, mask_index);
            var mask = int.Parse(subnet.Substring(mask_index + 1));

            Id = id;
            Address = address;
            Mask = mask;
            Subnet = subnet;
        }

        /// <summary>
        /// Конструктор модели IP для базы данных.
        /// </summary>
        /// <param name="PK_ip_id">Id IP-адреса.</param>
        /// <param name="address">Адресная часть IP-адреса.</param>
        /// <param name="mask">Префикс маски подсети IP-адреса.</param>
        /// <param name="subnet">Адрес + префикс маски подсети IP-адреса.</param>
        public IP(string PK_ip_id, string address, int mask, string subnet)
        {
            Id = PK_ip_id;
            Address = address;
            Mask = mask;
            Subnet = subnet;
        }
    }
}
