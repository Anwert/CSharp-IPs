using csproject.Models;
using System.Threading.Tasks;

namespace csproject.Repositories
{
    /// <summary>
    /// Репозиторий работы с базой данных IP-адресов, реализует интерфейс IIPsRepository.
    /// </summary>
    public class IPsRepository: IIPsRepository
    {
        /// <summary>
        /// Поле клиента для подключения к веб-сервису.
        /// </summary>
        private readonly IPsRepositoryWCFClient _client;
        /// <summary>
        /// Поле списка IP-адресов, объект класса IPList.
        /// </summary>
        private readonly IPList _ipList;

        /// <summary>
        /// Получить индекс IP-адреса по id.
        /// </summary>
        /// <param name="id">Id IP-адреса.</param>
        /// <returns>Возвращает индекс IP-адреса.</returns>
        private int GetIndexById(string id)
        {
            return _ipList.Data.FindIndex(ip => ip.Id == id);
        }

        /// <summary>
        /// Конструктор репозитория. В нем происходит инициализация клиента для подклбчения к веб службе и загрузка списка IP-адресов.
        /// </summary>
        public IPsRepository()
        {
			_client = new IPsRepositoryWCFClient();

			_ipList = new IPList();

			Task.WaitAll(Task.Run(() => GetIpListFromIPsWCFAsync()));
        }

		/// <summary>
		/// Получает список IP-адресов с WCF сервиса IPsWCF.
		/// </summary>
		private async Task GetIpListFromIPsWCFAsync()
		{
			var response = await _client.GetIPListAsync();
			var wcf_ip_list = response.Body.GetIPListResult;

			foreach (IPsWCF.Models.IP wcf_ip in wcf_ip_list.Data)
			{
				_ipList.Data.Add(new IP(wcf_ip.Id, wcf_ip.Subnet));
			}
		}

        /// <summary>
        /// Получить список IP-адресов.
        /// </summary>
        /// <returns>Возвращает объект класса IPList со списком IP-адресов из базы данных.</returns>
        public IPList GetIPList()
        {
			return _ipList;
        }

        /// <summary>
        /// Добавить новый IP-адрес.
        /// </summary>
        /// <param name="ip">Новый IP-адрес.</param>
        public async Task CreateIPAsync(IP ip)
        {
            var wcf_ip = new IPsWCF.Models.IP 
            {
                Id = ip.Id,
                Address = ip.Address,
                Mask = ip.Mask,
                Subnet = ip.Subnet
            };
    
            await _client.CreateIPAsync(wcf_ip);
    
            _ipList.Data.Add(ip);
        }

        /// <summary>
        /// Удалить IP-адрес.
        /// </summary>
        /// <param name="id">Id IP-адреса, который будем удалять.</param>
        public async Task DeleteIPAsync(string id)
        {
            await _client.DeleteIPAsync(id);
            
            _ipList.Data.RemoveAt(GetIndexById(id));
        }

        /// <summary>
        /// Получить IP-адрес по Id.
        /// </summary>
        /// <param name="id">Id IP-адреса, который хотим получить.</param>
        /// <returns>Возвращает IP-адрес с переданным Id.</returns>
        public IP GetIPById(string id)
        {
			var index = GetIndexById(id);
			return _ipList.Data[index];
        }

        /// <summary>
        /// Изменяет значение IP-адреса.
        /// </summary>
        /// <param name="ip">Объект IP с Id IP-адреса, который хотим изменить и новыми значениями остальных полей.</param>
        public async Task UpdateIPAsync(IP ip)
        {
            var wcf_ip = new IPsWCF.Models.IP
            {
                Id = ip.Id,
                Address = ip.Address,
                Mask = ip.Mask,
                Subnet = ip.Subnet
            };
    
            await _client.UpdateIPAsync(wcf_ip);
    
            _ipList.Data[GetIndexById(ip.Id)] = ip;
        }
    }
}