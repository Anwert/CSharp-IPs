using System.ServiceModel;
using IPsWCF.Models;

namespace IPsWCF
{
    /// <summary>
    /// Интерфейс WCF сервиса для работы с базой данных IP-адресов.
    /// </summary>
    [ServiceContract]
    public interface IIPsRepositoryWCF
    {
        /// <summary>
        /// Получить список IP-адресов.
        /// </summary>
        /// <returns>Возвращает объект модели IPList.</returns>
        [OperationContract]
        IPList GetIPList();

        /// <summary>
        /// Создать IP-адрес.
        /// </summary>
        /// <param name="ip">IP-адрес, который будет внесен в базу данных.</param>
        [OperationContract]
        void CreateIP(IP ip);

        /// <summary>
        /// Удалить IP-адрес.
        /// </summary>
        /// <param name="id">Id удаляемого IP-адреса.</param>
        [OperationContract]
        void DeleteIP(string id);

        /// <summary>
        /// Изменить IP-адрес.
        /// </summary>
        /// <param name="ip">IP-адрес с id адреса, который будет изменен и обновленными остальными полями.</param>
        [OperationContract]
        void UpdateIP(IP ip);
    }
}
