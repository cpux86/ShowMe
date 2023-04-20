using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Orders.Commands.EditOrder
{
    public class EditOrderCommand : IRequest<bool>
    {
        public string OrderId { get; set; }
        /// <summary>
        /// Заголовок, к примеру "Микросхема ШИМ-контроллера"
        /// </summary>
        public string Title { get; set; } = "Без названия";
        /// <summary>
        /// Комментарии, к промеру "Шим-контроллер с частоного преобразователя Siemens"
        /// </summary>
        public string Comments { get; set; }
        /// <summary>
        /// URL-адреса изображений искомых товаров
        /// </summary>
        public string ImageSource { get; set; }

        public string CreatedBy { get; set; }
    }
}
