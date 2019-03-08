using System;
using csproject.Models;
using csproject.Repositories;
using csproject.Exceptions;
using System.Threading.Tasks;

namespace csproject.Services
{
    /// <summary>
    /// Сервис для работы с IP-адресами.
    /// </summary>
    public class IPService: IIPService
    {
        /// <summary>
        /// Репозиторий IP-адресов.
        /// </summary>
        private readonly IIPsRepository _ipsRepository;
        /// <summary>
        /// Сервис SubnetsService.
        /// </summary>
        private readonly SubnetsService _subnetsService;

        /// <summary>
        /// Консутруктор IPControllerService, в котором инициализируются сервис SubnetsService и репозиторий IIPsRepository для дальнейшего использования.
        /// </summary>
        /// <param name="ips_repository">Репозиторий IP-адресов. Интерфейс IIPsRepository.</param>
        public IPService (IIPsRepository ips_repository)
        {
            _ipsRepository = ips_repository;
            _subnetsService = new SubnetsService();
        }

        /// <summary>
        /// Вернуть объект с сообщением ошибки.
        /// </summary>
        /// <param name="error_string">Строка сообщения ошибки.</param>
        /// <returns>Возвращает объект с сообщением ошибки.</returns>
        private object ReturnError(string error_string)
        {
            var response = new
            {
                error = true,
                message = error_string
            };

            return response;
        }

        /// <summary>
        /// Получить список IP-адресов.
        /// </summary>
        /// <returns>Возвращает объект со списком IP-адресов в поле data в случае получения корректного ответа от репозитория.</returns>
        public object GetIPList()  
        {
            try
            {
                var ip_list = _ipsRepository.GetIPList();

                if (ip_list.Data.Count == 0) 
                    return ReturnError("Db is empty.");

                var response = new
                {
                    error = false,
                    data = ip_list.Data
                };
                return response;
            }
            catch (Exception ex)
            {
                return (ex is IPException) ? ReturnError(ex.Message) : ReturnError("Server error.");
            }
        } 

        /// <summary>
        /// Создать новый IP-адрес.
        /// </summary>
        /// <param name="subnet">Новый IP-адрес с подсетью.</param>
        /// <returns>Возвращает объект с новым IP-адресом в поле data в случае корректного создания IP-адреса.</returns>
        public async Task<object> CreateIPAsync(string subnet)
        {
            try
            {
                if (!_subnetsService.ValidateIP(subnet)) 
                    return ReturnError("IP validation error.");

                var ip_list = _ipsRepository.GetIPList();

                var id = _subnetsService.GenerateId();
                var ip = new IP(id, subnet);

                if (ip_list.Data.Count == 0)
                {
                    await _ipsRepository.CreateIPAsync(ip);
                }
                else
                {
                    if (!ip_list.CheckIsIPUnique(ip.Subnet)) 
                        throw new IPException("This IP address already exists.");

                    await _ipsRepository.CreateIPAsync(ip);
                }

                var response = new 
                {
                    error = false,
                    ip
                };
                return response;
            }
            catch (Exception ex)
            {
                return (ex is IPException) ? ReturnError(ex.Message) : ReturnError("Server error.");
            }
        }

        /// <summary>
        /// Удалить IP-адрес.
        /// </summary>
        /// <param name="id">Id удаляемого IP-адреса.</param>
        /// <returns>Возвращает объект с полем error=false в случае корректного удаления IP-адреса.</returns>
        public async Task<object> DeleteIPAsync(string id)
        {
            try
            {
                await _ipsRepository.DeleteIPAsync(id);

                var response = new 
                {
                    error = false
                };

                return response;
            }
            catch
            {
                return ReturnError("Server error.");
            }
        }

        /// <summary>
        /// Получить IP-адрес для его последующего обновления в контроллере.
        /// </summary>
        /// <param name="id">Id IP-адреса, который нужно получить.</param>
        /// <returns>Возвращает IP-адрес с переданным id.</returns>
        public IP GetIPForUpdate(string id)
        {
			try
			{
				return _ipsRepository.GetIPById(id);
			}
			catch
			{
				return null;
			}

		}

        /// <summary>
        /// Обновить IP-адрес.
        /// </summary>
        /// <param name="id">Id IP-адреса, который нужно обновить.</param>
        /// <param name="subnet">Новые IP-адрес с префиксом маски подсети.</param>
        /// <returns>Возвращает объект с полем error=false в случае корректного обновления IP-адреса.</returns>
        public async Task<object> UpdateIPAsync(string id, string subnet)
        {
            try
            {
                if (!_subnetsService.ValidateIP(subnet)) 
                    return ReturnError("IP validation error.");

                var ip_list = _ipsRepository.GetIPList();

                if (!ip_list.CheckIsIPUnique(subnet)) 
                    throw new IPException("This IP address already exists.");

                await _ipsRepository.UpdateIPAsync(new IP(id, subnet));

                var response = new
                {
                    error = false
                };
                return response;
            }
            catch (Exception ex)
            {
                return (ex is IPException) ? ReturnError(ex.Message) : ReturnError("Server error.");
            }
        }

        /// <summary>
        /// Сгруппировать IP-адреса.
        /// </summary>
        /// <returns>Возвращает объект со списком сгруппированных IP-адресов (объект класса GroupedIPsList) в поле data.</returns>
        public object GroupIPs()
        {
            try
            {
                var ip_list = _ipsRepository.GetIPList();

                if (ip_list.Data.Count == 0) 
                    return ReturnError("Db is empty.");

                var grouped_ips_list =
                    new GroupedIPsList(_subnetsService.GroupIPs(ip_list));

                var response = new
                {
                    error = false,
                    data = grouped_ips_list.Data
                };
                return response;
            }
            catch (Exception ex)
            {
                return (ex is IPException) ? ReturnError(ex.Message) : ReturnError("Server error.");
            }
        }
    }
}