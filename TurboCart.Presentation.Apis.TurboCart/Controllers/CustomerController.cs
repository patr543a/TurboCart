using Microsoft.AspNetCore.Mvc;
using TurboCart.Application.Interfaces;
using TurboCart.Domain.Entities;

namespace TurboCart.Presentation.Apis.TurboCart.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomerController(ICustomerUseCase _customerUseCase)
    : ControllerBase
{
    [HttpGet("{customerId}")]
    public async Task<ActionResult<Customer>> Get(int customerId)
    {
        var customer = await _customerUseCase.GetCustomer(customerId);

        if (customer is null)
            return NotFound();

        return Ok(customer);
    }

    [HttpPost]
    public async Task<ActionResult<Customer>> Post([FromBody] Customer customer)
    {
        try
        {
            await _customerUseCase.AddCustomer(customer);
        }
        catch
        {
            return BadRequest();
        }

        return Ok(customer);
    }

    [HttpPut]
    public async Task<ActionResult<Customer>> Put([FromBody] Customer customer)
    {
        try
        {
            await _customerUseCase.UpdateCustomer(customer);
        }
        catch
        {
            return BadRequest();
        }

        return Ok(customer);
    }

    [HttpDelete("{customerId}")]
    public async Task<ActionResult> Delete(int customerId)
    {
        try
        {
            await _customerUseCase.DeleteCustomer(customerId);
        }
        catch
        {
            return BadRequest();
        }

        return Ok();
    }
}