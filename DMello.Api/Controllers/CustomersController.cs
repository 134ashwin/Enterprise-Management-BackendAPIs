using System.Collections.Generic;
using System.Threading.Tasks;
using DMello.Application.Customers;
using DMello.Application.Customers.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace DMello.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        // POST /api/customers -> Create customer
        [HttpPost]
        public async Task<ActionResult<CustomerResponseDto>> Create([FromBody] CustomerCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _customerService.CreateCustomerAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        // GET /api/customers -> Fetch all customers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerResponseDto>>> GetAll()
        {
            var result = await _customerService.GetAllCustomersAsync();
            return Ok(result);
        }

        // GET /api/customers/b2b -> Fetch B2B list
        [HttpGet("b2b")]
        public async Task<ActionResult<IEnumerable<CustomerResponseDto>>> GetB2BCustomers()
        {
            var result = await _customerService.GetB2BCustomersAsync();
            return Ok(result);
        }

        // GET /api/customers/b2c -> Fetch B2C list
        [HttpGet("b2c")]
        public async Task<ActionResult<IEnumerable<CustomerResponseDto>>> GetB2CCustomers()
        {
            var result = await _customerService.GetB2CCustomersAsync();
            return Ok(result);
        }

         //GET /api/customers/{id} -> Fetch single customer
        [HttpGet("{id:int}")]
        public async Task<ActionResult<CustomerResponseDto>> GetById(int id)
        {
            var result = await _customerService.GetByIdAsync(id);
            return result == null ? NotFound() : Ok(result);
        }
    }
}
