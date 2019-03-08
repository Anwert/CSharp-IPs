using System.Collections.Generic;
using csproject.Models;

namespace csproject.Services
{
    public interface ISubnetService
    {
        /// <summary>
        /// Сгенерировать id для IP-адреса.
        /// </summary>
        /// <returns>Возращает сгенерированный id для IP-адреса.</returns>
        string GenerateId();

        /// <summary>
        /// Проверить входит ли IP-адрес ip в подсеть адреса ip2.
        /// </summary>
        /// <param name="ip">IP-адрес, который будем проверять.</param>
        /// <param name="ip2">IP-адрес с подсетью, в которую должнен входить IP-адрес ip.</param>
        /// <returns>Вернуть 1 если ip не входит в подсеть адреса ip2, 0 в случае, если не входит, и 2 в случае, если подсети ip и ip2 совпадают.</returns>
        int CheckIsInSubnet(IP ip, IP ip2);

        /// <summary>
        /// Проверить валиден ли IP-адрес.
        /// </summary>
        /// <param name="subnet">IP-адрес с префиксом маски подсети (поле Subnet модели IP), который будем проверять.</param>
        /// <returns>Вернуть true если IP-адрес валиден или false в противном случае.</returns>
        bool ValidateIP(string subnet);

        /// <summary>
        /// Метод группровки IP-адресов.
        /// </summary>
        /// <param name="ip_list">Объект класса IPList, со список IP-адресов, который будет сгруппирован.</param>
        /// <returns>Список объектов класса GroupedIPs с сгруппированными IP-адресами.</returns>
        List<GroupedIPs> GroupIPs(IPList ip_list);
    }
}