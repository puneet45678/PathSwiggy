using AutoMapper;
using CustomerApi.Models.Dto;
using CustomerApi.Models;
using CustomerApi.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using CustomerApi.Models;
using CustomerApi.Models.Dto;
using CustomerApi.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public CustomerController(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        // GET: api/customer
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerReadDTO>>> GetAllCustomers()
        {
            try
            {
                var customers = await _customerRepository.GetAllAsync();
                if (customers == null || customers.Count == 0)
                {
                    return NotFound(new { message = "No customers found." });
                }

                var customerDtos = _mapper.Map<List<CustomerReadDTO>>(customers);
                return Ok(customerDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error retrieving customers.", details = ex.Message });
            }
        }

        // GET: api/customer/{id}
        [HttpGet("{id}", Name = "GetCustomer")]
        public async Task<ActionResult<CustomerReadDTO>> GetCustomerById(Guid id)
        {
            try
            {
                var customer = await _customerRepository.GetAsync(c => c.Id == id);
                if (customer == null)
                {
                    return NotFound(new { message = "Customer not found." });
                }

                var customerDto = _mapper.Map<CustomerReadDTO>(customer);
                return Ok(customerDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error retrieving customer.", details = ex.Message });
            }
        }

        // POST: api/customer
        [HttpPost]
        public async Task<ActionResult<CustomerReadDTO>> CreateCustomer([FromBody] CustomerCreateDTO customerCreateDto)
        {
            if (customerCreateDto == null)
            {
                return BadRequest(new { message = "Invalid customer data." });
            }

            try
            {
                var customer = _mapper.Map<Customer>(customerCreateDto);
                customer.Id = Guid.NewGuid();
                customer.CreatedAt = DateTime.Now;

                await _customerRepository.CreateAsync(customer);

                var customerDto = _mapper.Map<CustomerReadDTO>(customer);
                return CreatedAtRoute("GetCustomer", new { id = customer.Id }, customerDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error creating customer.", details = ex.Message });
            }
        }

        // PUT: api/customer/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(Guid id, [FromBody] CustomerCreateDTO customerUpdateDto)
        {
            if (customerUpdateDto == null || id == Guid.Empty)
            {
                return BadRequest(new { message = "Invalid data provided." });
            }

            try
            {
                var customer = await _customerRepository.GetAsync(c => c.Id == id);
                if (customer == null)
                {
                    return NotFound(new { message = "Customer not found." });
                }

                _mapper.Map(customerUpdateDto, customer);
                customer.UpdatedAt = DateTime.Now;

                await _customerRepository.SaveAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error updating customer.", details = ex.Message });
            }
        }

        // DELETE: api/customer/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(Guid id)
        {
            try
            {
                var customer = await _customerRepository.GetAsync(c => c.Id == id);
                if (customer == null)
                {
                    return NotFound(new { message = "Customer not found." });
                }

                await _customerRepository.RemoveAsync(customer);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error deleting customer.", details = ex.Message });
            }
        }

        // POST: api/customer/{id}/add-loyalty-points
        [HttpPost("{id}/add-loyalty-points")]
        public async Task<IActionResult> AddLoyaltyPoints(Guid id, [FromBody] int points)
        {
            if (points <= 0)
            {
                return BadRequest(new { message = "Points must be a positive number." });
            }

            try
            {
                var customer = await _customerRepository.GetAsync(c => c.Id == id);
                if (customer == null)
                {
                    return NotFound(new { message = "Customer not found." });
                }

                await _customerRepository.AddLoyaltyPointsAsync(id, points);
                return Ok(new { message = "Loyalty points added successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error adding loyalty points.", details = ex.Message });
            }
        }

        // POST: api/customer/{id}/add-favorite-dish
        [HttpPost("{id}/add-favorite-dish")]
        public async Task<IActionResult> AddFavoriteDish(Guid id, [FromBody] string dish)
        {
            if (string.IsNullOrEmpty(dish))
            {
                return BadRequest(new { message = "Dish name cannot be empty." });
            }

            try
            {
                var customer = await _customerRepository.GetAsync(c => c.Id == id);
                if (customer == null)
                {
                    return NotFound(new { message = "Customer not found." });
                }

                await _customerRepository.AddFavoriteDishAsync(id, dish);
                return Ok(new { message = "Favorite dish added successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error adding favorite dish.", details = ex.Message });
            }
        }

        [HttpPost("{id}/add-order")]
        public async Task<IActionResult> AddOrder(Guid id, [FromBody] int orderId)
        {
            if (orderId <= 0)
            {
                return BadRequest(new { message = "Invalid order ID." });
            }

            try
            {
                var customer = await _customerRepository.GetAsync(c => c.Id == id);
                if (customer == null)
                {
                    return NotFound(new { message = "Customer not found." });
                }

                await _customerRepository.AddOrderIdAsync(id, orderId);
                return Ok(new { message = "Order added successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error adding order.", details = ex.Message });
            }
        }

        [HttpPost("{id}/add-reservation")]
        public async Task<IActionResult> AddReservation(Guid id, [FromBody] string reservation)
        {
            if (string.IsNullOrEmpty(reservation))
            {
                return BadRequest(new { message = "Reservation cannot be empty." });
            }

            try
            {
                var customer = await _customerRepository.GetAsync(c => c.Id == id);
                if (customer == null)
                {
                    return NotFound(new { message = "Customer not found." });
                }

                await _customerRepository.AddReservationAsync(id, reservation);
                return Ok(new { message = "Reservation added successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error adding reservation.", details = ex.Message });
            }
        }

        [HttpPost("{id}/add-feedback")]
        public async Task<IActionResult> AddFeedback(Guid id, [FromBody] string feedback)
        {
            if (string.IsNullOrEmpty(feedback))
            {
                return BadRequest(new { message = "Feedback cannot be empty." });
            }

            try
            {
                var customer = await _customerRepository.GetAsync(c => c.Id == id);
                if (customer == null)
                {
                    return NotFound(new { message = "Customer not found." });
                }

                await _customerRepository.AddFeedbackAsync(id, feedback);
                return Ok(new { message = "Feedback added successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error adding feedback.", details = ex.Message });
            }
        }
    }
}
