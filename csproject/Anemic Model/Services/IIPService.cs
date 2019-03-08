using csproject.Models;
using System.Threading.Tasks;

namespace csproject.Repositories
{
    /// <summary>
    /// Интерфейс сервиса для работы с IP-адресами.
    /// </summary>
    public interface IIPService
    {
        /// <summary>
        /// Получить список IP-адресов.
        /// </summary>
        /// <returns>Возвращает объект со списком IP-адресов в поле data в случае получения корректного ответа от репозитория.</returns>
        object GetIPList();
        /// <summary>
        /// Создать новый IP-адрес.
        /// </summary>
        /// <param name="subnet">Новый IP-адрес с подсетью.</param>
        /// <returns>Возвращает объект с новым IP-адресом в поле data в случае корректного создания IP-адреса.</returns>
        Task<object> CreateIPAsync(string subnet);
        /// <summary>
        /// Удалить IP-адрес.
        /// </summary>
        /// <param name="id">Id удаляемого IP-адреса.</param>
        /// <returns>Возвращает объект с полем error=false в случае корректного удаления IP-адреса.</returns>
        Task<object> DeleteIPAsync(string id);
        /// <summary>
        /// Получить IP-адрес для его последующего обновления в контроллере.
        /// </summary>
        /// <param name="id">Id IP-адреса, который нужно получить.</param>
        /// <returns>Возвращает IP-адрес с переданным id.</returns>
        IP GetIPForUpdate(string id);
        /// <summary>
        /// Обновить IP-адрес.
        /// </summary>
        /// <param name="id">Id IP-адреса, который нужно обновить.</param>
        /// <param name="subnet">Новые IP-адрес с префиксом маски подсети.</param>
        /// <returns>Возвращает объект с полем error=false в случае корректного обновления IP-адреса.</returns>
        Task<object> UpdateIPAsync(string id, string subnet);
        /// <summary>
        /// Сгруппировать IP-адреса.
        /// </summary>
        /// <returns>Возвращает объект со списком сгруппированных IP-адресов (объект класса GroupedIPsList) в поле data.</returns>
        object GroupIPs();
    }
}