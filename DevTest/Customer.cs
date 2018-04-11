namespace DevTest
{
    public class Customer : Entity
    {
        public Customer(string firstName, string lastName, Address address, string id = null) : base(id)
        {
            FirstName = firstName;
            LastName = lastName;
            Address = address;
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Address Address { get; set; }

        public static Customer Find(string id)
        {
            return Find(typeof(Customer), id) as Customer;
        }
       
    }
}
