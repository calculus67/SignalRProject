﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SignalR.BusinessLayer.Abstract;
using SignalR.DtoLayer.NotificationDto;
using SignalR.EntityLayer.Entities;

namespace SignalRApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class NotificationsController : ControllerBase
	{
		private readonly INotificationService _notificationService;

		public NotificationsController(INotificationService notificationService)
		{
			_notificationService = notificationService;
		}

		[HttpGet]
		public IActionResult NotificationList()
		{
			return Ok(_notificationService.TGetListAll());

		}

		[HttpGet("NotificationCountByStatusFalse")]

		public IActionResult NotificationCountByStatusFalse()
		{
			return Ok(_notificationService.TNotificationCountByStatusFalse());

		}

		[HttpGet("GetAllNotificationByFalse")]

		public IActionResult GetAllNotificationByFalse()
		{
			return Ok(_notificationService.TGetAllNotificationByFalse());
		}
		[HttpPost]
		public IActionResult CreateNotifications(CreateNotificationDto createNotificationDto)
		{
			Notification notification = new Notification()
			{
				Description = createNotificationDto.Description,
				Icon = createNotificationDto.Icon,
				Status = false,
				Type = createNotificationDto.Type,
				Date = Convert.ToDateTime(DateTime.Now.ToShortDateString())

			};
			_notificationService.TAdd(notification);
			return Ok("Ekleme işlemi başarılı");

		}
		[HttpDelete("{id}")]
		public IActionResult DeleteNotifications(int id)
		{
			var value = _notificationService.TGetById(id);
			_notificationService.TDelete(value);
			return Ok("Bildirim silindi");
		}

		[HttpGet("{id}")]
		public IActionResult GetNotification(int id)
		{
			var value=_notificationService.TGetById(id);
			return Ok(value);

		}

		[HttpPut]
		public IActionResult UpdateNotifications(UpdateNotificationDto updateNotificationDto)
		{
			Notification notification = new Notification()
			{
				NotificationID=updateNotificationDto.NotificationID,
				Description = updateNotificationDto.Description,
				Icon = updateNotificationDto.Icon,
				Status = updateNotificationDto.Status,
				Type = updateNotificationDto.Type,
				Date = updateNotificationDto.Date

			};
			_notificationService.TUpdate(notification);
			return Ok("Güncelleme işlemi başarılı");

		}
		[HttpGet("NotificationStatusChangeTrue/{id}")]

		public IActionResult NotificationStatusChangeTrue(int id)
		{
			_notificationService.TNotificationStatusChangeTrue(id);
			return Ok("Güncelleme yapıldı");
		}

        [HttpGet("NotificationStatusChangeFalse/{id}")]

        public IActionResult NotificationStatusChangeFalse(int id)
        {
            _notificationService.TNotificationStatusChangeFalse(id);
            return Ok("Güncelleme yapıldı");
        }

    }
}
