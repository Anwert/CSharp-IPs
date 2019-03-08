namespace csproject.Models
{
    /// <summary>
    /// Модель View ошибки.
    /// </summary>
    public class ErrorViewModel
    {
        /// <summary>
        /// Получает или устанавливает request id.
        /// </summary>
        /// <value>Id запроса.</value>
        public string RequestId { get; }

        /// <summary>
        /// Проверить request id.
        /// </summary>
        /// <returns>Возвращает true если RequstId не пустой или null, в противном случае возвращает false.</returns>
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}