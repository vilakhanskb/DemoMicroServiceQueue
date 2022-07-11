using CustomerService.Interface;

namespace CustomerService.Service
{
    public class CustomerFunc: ICustomerFunc
    {
        //Create, Read, Update, Delete (CRUD)
        public bool Create(string title)
        {
            return true;
        }

        public bool Delete(string id)
        {
            throw new NotImplementedException();
        }

        public string Read()
        {
            return "My read func";
        }

        public bool Update(string title, string id)
        {
            throw new NotImplementedException();
        }
    }
}
