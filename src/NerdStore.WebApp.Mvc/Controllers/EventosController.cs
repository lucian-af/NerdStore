﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NerdStore.Core.Data.Interfaces;

namespace NerdStore.WebApp.Mvc.Controllers
{
	public class EventosController : Controller
	{
		private readonly IEventSourcingRepository _eventSourcingRepository;

		public EventosController(IEventSourcingRepository eventSourcingRepository)
			=> _eventSourcingRepository = eventSourcingRepository;

		[HttpGet("eventos/{id:guid}")]
		public async Task<IActionResult> Index(Guid id)
		{
			var eventos = await _eventSourcingRepository.ObterEventos(id);
			return View(eventos);
		}
	}
}
