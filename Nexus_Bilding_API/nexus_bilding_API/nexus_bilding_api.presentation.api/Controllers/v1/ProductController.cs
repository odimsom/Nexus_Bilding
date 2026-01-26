using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using nexus_bilding_api.core.application.DTOs.Product;
using nexus_bilding_api.core.application.Features.Products.Commands;
using nexus_bilding_api.core.application.Features.Products.Queries;

namespace nexus_bilding_api.presentation.api.Controllers.v1;

[ApiVersion("1.0")]
public class ProductController : BaseApiController
{
    private readonly ICreateProductCommandHandler _createHandler;
    private readonly IUpdateProductCommandHandler _updateHandler;
    private readonly IDeleteProductCommandHandler _deleteHandler;
    private readonly IGetAllProductsQueryHandler _getAllHandler;
    private readonly IGetProductByIdQueryHandler _getByIdHandler;

    public ProductController(
        ICreateProductCommandHandler createHandler,
        IUpdateProductCommandHandler updateHandler,
        IDeleteProductCommandHandler deleteHandler,
        IGetAllProductsQueryHandler getAllHandler,
        IGetProductByIdQueryHandler getByIdHandler)
    {
        _createHandler = createHandler;
        _updateHandler = updateHandler;
        _deleteHandler = deleteHandler;
        _getAllHandler = getAllHandler;
        _getByIdHandler = getByIdHandler;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return HandleResult(await _getAllHandler.Handle());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        return HandleResult(await _getByIdHandler.Handle(id));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateProductDto command)
    {
        return HandleResult(await _createHandler.Handle(command));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, CreateProductDto command)
    {
        return HandleResult(await _updateHandler.Handle(id, command));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        return HandleResult(await _deleteHandler.Handle(id));
    }
}
