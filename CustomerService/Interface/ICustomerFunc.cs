namespace CustomerService.Interface
{
    public interface ICustomerFunc
    {
        //Create, Read, Update, Delete (CRUD)
        bool Create(string title);
        string Read();
        bool Update(string title, string id);
        bool Delete(string id);
    }
}
