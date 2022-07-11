using CustomerService.Interface;

namespace CustomerService.Service
{
    public class CustomerRead : ICustomerFunc
    {
        public bool Create(string title)
        {
            return false;
        }

        public bool Delete(string id)
        {
            throw new NotImplementedException();
        }

        public string Read()
        {
            return "My read in customer read";
        }

        public bool Update(string title, string id)
        {
            throw new NotImplementedException();
        }
    }
}
