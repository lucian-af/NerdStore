using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NerdStore.Catalogo.Application.Services.Interfaces;
using NerdStore.Core.Communication.Interfaces;
using NerdStore.Core.Messages.Common.Notifications;
using NerdStore.Core.Settings;
using NerdStore.Vendas.Application.Commands.Models;
using NerdStore.Vendas.Application.Queries.Dtos.Interfaces;
using NerdStore.WebApi.Models;

namespace NerdStore.WebApi.Controllers
{
	[Authorize]
	[ApiController]
	public class CarrinhoControllerApi : ControllerBase
	{
		private readonly IProdutoAppService _produtoAppService;
		private readonly IPedidoQueries _pedidoQueries;
		private readonly IMediatorHandler _mediatorHandler;

		public CarrinhoControllerApi(INotificationHandler<DomainNotification> notifications,
								  IProdutoAppService produtoAppService,
								  IMediatorHandler mediatorHandler,
								  IPedidoQueries pedidoQueries)
			: base(notifications, mediatorHandler)
		{
			_produtoAppService = produtoAppService;
			_mediatorHandler = mediatorHandler;
			_pedidoQueries = pedidoQueries;
		}

		[HttpGet]
		[Route("api/carrinho")]
		public async Task<IActionResult> Get()
			=> Response(await _pedidoQueries.ObterCarrinhoCliente(IdCliente));

		[HttpPost]
		[Route("api/carrinho")]
		public async Task<IActionResult> Post([FromBody] ItemViewModel item)
		{
			var produto = await _produtoAppService.ObterPorId(item.Id);
			if (produto == null) return BadRequest();

			if (produto.QuantidadeEstoque < item.Quantidade)
			{
				NotificarErro("ErroValidacao", "Produto com estoque insuficiente");
			}

			var command = new AdicionarItemPedidoCommand(IdCliente, produto.Id, produto.Nome, item.Quantidade, produto.Valor);
			await _mediatorHandler.EnviarComando(command);

			return Response();
		}

		[HttpPut]
		[Route("api/carrinho/{id:guid}")]
		public async Task<IActionResult> Put(Guid id, [FromBody] ItemViewModel item)
		{
			var produto = await _produtoAppService.ObterPorId(id);
			if (produto == null) return BadRequest();

			var command = new AtualizarItemPedidoCommand(IdCliente, produto.Id, item.Quantidade);
			await _mediatorHandler.EnviarComando(command);

			return Response();
		}

		[HttpDelete]
		[Route("api/carrinho/{id:guid}")]
		public async Task<IActionResult> Delete(Guid id)
		{
			var produto = await _produtoAppService.ObterPorId(id);
			if (produto == null) return BadRequest();

			var command = new RemoverItemPedidoCommand(IdCliente, id);
			await _mediatorHandler.EnviarComando(command);

			return Response();
		}

		[AllowAnonymous]
		[HttpPost("api/login")]
		public IActionResult Login([FromBody] LoginViewModel login)
		{
			if (!string.IsNullOrWhiteSpace(login.Email) && !string.IsNullOrWhiteSpace(login.Senha))
			{
				return Ok(GerarJwt(login.Email));
			}

			NotificarErro("login", "Usuário ou Senha incorretos");
			return Response();
		}

		private static string GerarJwt(string email)
		{
			var claims = new List<Claim>
			{
				new Claim(ClaimTypes.Email, email)
			};

			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.ASCII.GetBytes(AuthenticationSettings.Secret);
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(claims),
				Expires = DateTime.UtcNow.AddHours(AuthenticationSettings.ExpiracaoHoras),
				Issuer = AuthenticationSettings.Emissor,
				Audience = AuthenticationSettings.ValidoEm,
				SigningCredentials = new SigningCredentials(
					new SymmetricSecurityKey(key),
					SecurityAlgorithms.HmacSha256Signature)
			};

			var token = tokenHandler.CreateToken(tokenDescriptor);
			var encodedToken = tokenHandler.WriteToken(token);

			return string.Format("Bearer {0}", encodedToken);
		}
	}
}
