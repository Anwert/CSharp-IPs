using System;
using System.Net;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using csproject.Models;

namespace csproject.Services
{
    /// <summary>
    /// Сервис высчисления и валидации подсетей.
    /// </summary>
    public class SubnetsService: ISubnetService
    {
        /// <summary>
        /// Получить адрес подсети.
        /// </summary>
        /// <param name="address_bytes">Массив byte[] IP-адреса.</param>
        /// <param name="mask_bytes">Массив byte[] маски подсети.</param>
        /// <returns>Возвращает массив byte[] адреса подсети.</returns>
        private byte[] GetNetworkBytes(byte[] address_bytes, byte[] mask_bytes)
        {
            var broadcast_address = new byte[address_bytes.Length];

            for (var i = 0; i < broadcast_address.Length; i++)
                broadcast_address[i] = (byte)(address_bytes[i] & (mask_bytes[i]));
            
            return broadcast_address;
        }

        /// <summary>
        /// Получить широковещательный адрес.
        /// </summary>
        /// <param name="address_bytes">Массив byte[] IP-адреса.</param>
        /// <param name="mask_address_bytes">Массив byte[] адреса маски подсети.</param>
        /// <returns>Возвращает массив byte[] широковещательного адреса.</returns>
        private byte[] GetBroadcastBytes(byte[] address_bytes, byte[] mask_address_bytes)
        {
            var broadcast_address = new byte[address_bytes.Length];

            for (var i = 0; i < broadcast_address.Length; i++)
                broadcast_address[i] = (byte)(address_bytes[i] | (mask_address_bytes[i] ^ 255));
            
            return broadcast_address;
        }

        /// <summary>
        /// Получить десятичное представление октета по количеству бит в октете.
        /// </summary>
        /// <param name="bits">Количество бит в октете.</param>
        /// <returns>Вернуть десятичное представление октета по количеству бит.</returns>
        private byte GetDecByBits(int bits)
        {
            var binary_builder = new StringBuilder();
            for (var i = 0; i < bits; i++)
                binary_builder.Append(1);

            var restBits = 8 - bits;
            for (var i = 0; i < restBits; i++)
                binary_builder.Append(0);

            return Convert.ToByte(binary_builder.ToString(), 2);
        }

        /// <summary>
        /// Создать массив byte[] маски подсети с помощью префиска.
        /// </summary>
        /// <param name="mask_length">Длина префикса подсети.</param>
        /// <returns>Вернуть массив byte[] маски подсети.</returns>
        private byte[] CreateBytesByBitLength(int mask_length)
        {
            var binary_mask = new byte[4];

            if (mask_length > 24)
            {
                if (mask_length == 32)
                {
                    for (byte i = 0; i < 4; i++)
                        binary_mask[i] = 255;
                }
                else
                {
                    for (byte i = 0; i < 3; i++)
                        binary_mask[i] = 255;

                    binary_mask[3] = GetDecByBits(mask_length % 8);
                }
            }
            else if (mask_length > 16)
            {
                if (mask_length == 24)
                {
                    for (byte i = 0; i < 3; i++)
                        binary_mask[i] = 255;
                }
                else
                {
                    for (byte i = 0; i < 2; i++)
                        binary_mask[i] = 255;

                    binary_mask[2] = GetDecByBits(mask_length % 8);
                }
            }
            else if (mask_length > 8)
            {
                if (mask_length == 16)
                {
                    for (byte i = 0; i < 2; i++)
                        binary_mask[i] = 255;
                }
                else
                {
                    binary_mask[0] = 255;
                    binary_mask[1] = GetDecByBits(mask_length % 8);
                }
            }
            else
            {
                binary_mask[0] = (mask_length == 8) ? (byte)255 : GetDecByBits(mask_length % 8);
            }
            return binary_mask;
        }

        /// <summary>
        /// Сгенерировать id для IP-адреса.
        /// </summary>
        /// <returns>Возращает сгенерированный id для IP-адреса.</returns>
        public string GenerateId()
        {
            return Guid.NewGuid().ToString("N");
        }

        /// <summary>
        /// Проверить входит ли IP-адрес ip в подсеть адреса ip2.
        /// </summary>
        /// <param name="ip">IP-адрес, который будем проверять.</param>
        /// <param name="ip2">IP-адрес с подсетью, в которую должнен входить IP-адрес ip.</param>
        /// <returns>Вернуть 1 если ip не входит в подсеть адреса ip2, 0 в случае, если не входит, и 2 в случае, если подсети ip и ip2 совпадают.</returns>
        public int CheckIsInSubnet(IP ip, IP ip2)
        {
            var mask_bytes = CreateBytesByBitLength(ip.Mask);
            var mask_bytes2 = CreateBytesByBitLength(ip2.Mask);

            var address_bytes = IPAddress.Parse(ip.Address).GetAddressBytes();
            var address_bytes2 = IPAddress.Parse(ip2.Address).GetAddressBytes();

            var net_address_bytes = GetNetworkBytes(address_bytes, mask_bytes);
            var net_address_bytes2 = GetNetworkBytes(address_bytes2, mask_bytes2);

            var broadcast_address_bytes = GetBroadcastBytes(address_bytes, mask_bytes);
            var broadcast_address_bytes2 = GetBroadcastBytes(address_bytes2, mask_bytes2);

            // флаг для проверки, не дали ли нам одну и ту же подсеть
            var is_same_subnet = true;

            for (var i = 0; i < 4; i++)
            {
                // если мы уже 1 раз поменяли наш флаг, то нам не нужно
                // каждый раз сравнивать остальные октеты, поэтому
                // флаг проверяется в первую очередь
                if (is_same_subnet
                    && broadcast_address_bytes[i] == broadcast_address_bytes2[i]
                    && net_address_bytes[i] == net_address_bytes2[i])
                    continue;
                
                // если мы попали сюда, то мы уверены, что у нас
                // разные подсети и можно сделать флаг false
                is_same_subnet = false;
                    

                if (broadcast_address_bytes[i] > broadcast_address_bytes2[i] 
                    || net_address_bytes[i] < net_address_bytes2[i]) 
                    // ip не входит в ip2
                    return 0;
            }

            // если за все время флаг is_same_subnet не поменялся, то у нас
            // одинаковые подсети и возвращаем 2, если is_same_subnet = false,
            // то возращаем 1, что означает, что ip входит в ip2
            return is_same_subnet ? 2 : 1; 
        }

        /// <summary>
        /// Проверить валиден ли IP-адрес.
        /// </summary>
        /// <param name="subnet">IP-адрес с префиксом маски подсети (поле Subnet модели IP), который будем проверять.</param>
        /// <returns>Вернуть true если IP-адрес валиден или false в противном случае.</returns>
        public bool ValidateIP(string subnet)
        {
            if (subnet.Count(c => c == '.') != 3 && subnet.Count(c => c == '/') != 1) return false;

            var subnet_ip = new IP("1", subnet);

            return (IPAddress.TryParse(subnet_ip.Address, out IPAddress ip_address)
                && subnet_ip.Mask >= 0 && subnet_ip.Mask < 33);
        }

        /// <summary>
        /// Метод группровки IP-адресов.
        /// </summary>
        /// <param name="ip_list">Объект класса IPList, со список IP-адресов, который будет сгруппирован.</param>
        /// <returns>Список объектов класса GroupedIPs с сгруппированными IP-адресами.</returns>
        public List<GroupedIPs> GroupIPs(IPList ip_list)
        {
            var sorted_ip_list = ip_list.Data;
            sorted_ip_list.Sort((ip, ip2) => ip.Mask.CompareTo(ip2.Mask));

            var storage = new List<GroupedIPs>
            {
                new GroupedIPs(sorted_ip_list[0])
            };

            sorted_ip_list.RemoveAt(0);

            foreach (var ip in sorted_ip_list)
            {
                for (var i = 0; i < storage.Count; i++)
                {
                    var result = CheckIsInSubnet(ip, storage[i].IPsList[0]);

                    // если ip и storage[i].IPsList[0] это одна и та же подсеть
                    if (result == 2)
                    {
                        storage[i].IPsList.Add(ip);
                        break;
                    }

                    // если ip входит в storage[i].IPsList[0]
                    if (result == 1)
                    {
                        storage[i].InnerIPsList.Add(ip);
                        break;
                    }

                    // если ip  не входит в storage[i].IPsList[0]
                    // и это последний ip, который есть в storage
                    if (result == 0 && i.Equals(storage.Count - 1))
                    {
                        storage.Add(new GroupedIPs(ip));
                        break;
                    }
                }
            }
            return storage;
        }
    }
}