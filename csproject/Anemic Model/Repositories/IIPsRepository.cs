using csproject.Models;
using System.Threading.Tasks;

namespace csproject.Repositories
{
    /// <summary>
    /// Интерфейс репозитория для работы с базой данных IP-адресов.
    /// </summary>
    public interface IIPsRepository
    {
        /// <summary>
        /// Получить список IP-адресов.
        /// </summary>
        /// <returns>Возвращает объект класса IPList со списком IP-адресов.</returns>
        IPList GetIPList();
        
        /// <summary>
        /// Добавить новый IP-адрес.
        /// </summary>
        /// <param name="ip">Новый IP-адрес.</param>
        Task CreateIPAsync(IP ip);
        
        /// <summary>
        /// Удалить IP-адрес.
        /// </summary>
        /// <param name="id">Id IP-адреса, который будет удален.</param>
        Task DeleteIPAsync(string id);
        
        /// <summary>
        /// Получить IP-адрес по Id.
        /// </summary>
        /// <param name="id">Id IP-адреса, который хотим получить.</param>
        /// <returns>Возвращает IP-адрес с переданным Id.</returns>
        IP GetIPById(string id);
        
        /// <summary>
        /// Изменяет значение IP-адреса.
        /// </summary>
        /// <param name="ip">Объект IP с Id IP-адреса, который хотим изменить и новыми значениями остальных полей.</param>
        Task UpdateIPAsync(IP ip);
    }
}