using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NerdStore.Catalogo.Application.Services.Interfaces;

namespace NerdStore.WebApp.Mvc.Controllers
{
	public class VitrineController : Controller
	{
		private readonly IProdutoAppService _produtoAppService;

		public VitrineController(IProdutoAppService produtoAppService)
			=> _produtoAppService = produtoAppService;

		[HttpGet]
		[Route("")]
		[Route("vitrine")]
		public async Task<IActionResult> Index()
			=> View(await _produtoAppService.ObterTodos());

		[HttpGet]
		[Route("produto-detalhe/{id}")]
		public async Task<IActionResult> ProdutoDetalhe(Guid id)
			=> View(await _produtoAppService.ObterPorId(id));
	}
}
