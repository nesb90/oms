using System.Collections.Generic;
using System.Threading.Tasks;
using CustomerService.Models;
using CustomerService.Repositories;

namespace CustomerService.Services
{
    public class CustomerService
    {
        private readonly CustomerRepository _customerRepository;

        public CustomerService(CustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<List<Customer>> GetAllCustomersAsync()
        {
            return await _customerRepository.GetAllAsync();
        }

        public async Task<Customer> GetCustomerByIdAsync(int id)
        {
            return await _customerRepository.GetByIdAsync(id);
        }

        public async Task<Customer> CreateCustomerAsync(Customer customer)
        {
            // Add validation logic here if needed
            return await _customerRepository.CreateAsync(customer);
        }

        public async Task<Customer> UpdateCustomerAsync(int id, Customer customer)
        {
            // Add validation logic here if needed
            return await _customerRepository.UpdateAsync(id, customer);
        }

        public async Task<bool> DeleteCustomerAsync(int id)
        {
            return await _customerRepository.DeleteAsync(id);
        }
    }
}