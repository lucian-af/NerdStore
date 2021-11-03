using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using NerdStore.Catalogo.Application.Dtos;
using NerdStore.Catalogo.Application.Services.Interfaces;
using NerdStore.Catalogo.Domain.Entidades;
using NerdStore.Catalogo.Domain.Interfaces;
using NerdStore.Core.Exceptions;

namespace NerdStore.Catalogo.Application.Services
{
	public class ProdutoAppService : IProdutoAppService
	{
		private readonly IProdutoRepository _produtoRepository;
		private readonly IEstoqueService _estoqueService;
		private readonly IMapper _mapper;

		public ProdutoAppService(IProdutoRepository produtoRepository,
								 IMapper mapper,
								 IEstoqueService estoqueService)
		{
			_produtoRepository = produtoRepository;
			_mapper = mapper;
			_estoqueService = estoqueService;
		}

		public async Task<IEnumerable<ProdutoDto>> ObterPorCategoria(int codigo)
			=> _mapper.Map<IEnumerable<ProdutoDto>>(await _produtoRepository.ObterPorCategoria(codigo));

		public async Task<ProdutoDto> ObterPorId(Guid id)
			=> _mapper.Map<ProdutoDto>(await _produtoRepository.ObterPorIdAsync(id));

		public async Task<IEnumerable<ProdutoDto>> ObterTodos()
			=> _mapper.Map<IEnumerable<ProdutoDto>>(await _produtoRepository.ObterTodosAsync());

		public async Task<IEnumerable<CategoriaDto>> ObterCategorias()
			=> _mapper.Map<IEnumerable<CategoriaDto>>(await _produtoRepository.ObterCategorias());

		public async Task AdicionarProduto(ProdutoDto produtoDto)
		{
			var produto = _mapper.Map<Produto>(produtoDto);
			await _produtoRepository.AdicionarAsync(produto);

			await _produtoRepository.UnitOfWork.Commit();
		}

		public async Task AtualizarProduto(ProdutoDto produtoDto)
		{
			var produto = _mapper.Map<Produto>(produtoDto);
			_produtoRepository.Atualizar(produto);

			await _produtoRepository.UnitOfWork.Commit();
		}

		public async Task<ProdutoDto> DebitarEstoque(Guid id, int quantidade)
		{
			if (!await _estoqueService.DebitarEstoque(id, quantidade))
				throw new DomainException("Falha ao debitar estoque");

			return _mapper.Map<ProdutoDto>(await _produtoRepository.ObterPorIdAsync(id));
		}

		public async Task<ProdutoDto> ReporEstoque(Guid id, int quantidade)
		{
			if (!await _estoqueService.ReporEstoque(id, quantidade))
				throw new DomainException("Falha ao repor estoque");

			return _mapper.Map<ProdutoDto>(await _produtoRepository.ObterPorIdAsync(id));
		}

		public void Dispose()
		{
			_produtoRepository?.Dispose();
			_estoqueService?.Dispose();
			GC.SuppressFinalize(this);
		}
	}
}
