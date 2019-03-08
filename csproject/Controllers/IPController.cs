using Microsoft.AspNetCore.Mvc;
using csproject.Models;
using csproject.Repositories;
using csproject.Services;
using System.Threading.Tasks;

namespace csproject.Controllers
{
    /// <summary>
    /// Контроллер IP–адресов.
    /// </summary>
    public class IPController : Controller
    {        
        /// <summary>
        /// Сервис контроллера IP-адресов IPControllerService.
        /// </summary>
        private readonly IPService _ipService;

        /// <summary>
        /// Пустой консутруктор IPController, в котором инициализируются сервис IPControllerService и репозиторий IIPsRepository для дальнейшего использования.
        /// </summary>
        public IPController ()
        {
            var ips_repository = new IPsRepository();
            _ipService = new IPService(ips_repository);
        }

        /// <summary>
        /// View Index.
        /// </summary>
        /// <returns>Возвращает View Index'a</returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Получить список IP-адресов.
        /// </summary>
        /// <returns>Возвращает список IP-адресов в формате JSON.</returns>
        [HttpGet]
        public JsonResult GetIPList()  
        {
            return Json(_ipService.GetIPList());
        }

        /// <summary>
        /// Создать IP–адрес.
        /// </summary>
        /// <returns>Возвращает ответ о результате создания IP-адреса в формате JSON.</returns>
        [HttpPost]
        public async Task<JsonResult> CreateIP(string subnet)
        {
            return Json(await _ipService.CreateIPAsync(subnet));
        }

        /// <summary>
        /// Удалить IP-адрес.
        /// </summary>
        /// <returns>Возвращает ответ о результате удаления IP-адреса в формате JSON.</returns>
        [HttpDelete]
        public async Task<JsonResult> DeleteIP(string id)
        {
            return Json(await _ipService.DeleteIPAsync(id));
        }

        /// <summary>
        /// View обновления IP-адреса.
        /// </summary>
        /// <returns>Возвращает View страницы изменения IP-адреса.</returns>
        public IActionResult Update(string id)
        {
			var ip = _ipService.GetIPForUpdate(id);
			if (ip != null)
			{
				return View(ip);
			}
			else
			{
				return NotFound();
			}
		}

        /// <summary>
        /// Обновить IP-адрес.
        /// </summary>
        /// <returns>Возвращает ответ о результате обновления IP-адреса в формате JSON.</returns>
        [HttpPost]
        public async Task<JsonResult> UpdateIP(string id, string subnet)
        {
            return Json(await _ipService.UpdateIPAsync(id, subnet));
        }

        /// <summary>
        /// Сгруппировать IP-адреса.
        /// </summary>
        /// <returns>Возвращает сгруппированный список IP-адресов в формате JSON.</returns>
        [HttpGet]
        public JsonResult GroupIPs()
        {
            return Json(_ipService.GroupIPs());
        }
    }
}