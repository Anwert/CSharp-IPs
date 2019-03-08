using System.Runtime.Serialization;

namespace IPsWCF.Models
{
    /// <summary>
    /// Модель массива IP-адресов.
    /// </summary>
    [DataContract]
    public class IPList
    {
        /// <summary>
        /// Поле, в котором хранится массив IP-адресов.
        /// </summary>
        [DataMember]
        public IP[] Data { get; set; }

        /// <summary>
        /// Конструктор создания модели IPList.
        /// </summary>
        /// <param name="ip_array">Массив IP-адресов, из которого будет создан объект класса IPList</param>
        public IPList(IP[] ip_array)
        {
            Data = ip_array;
        }
    }
}
