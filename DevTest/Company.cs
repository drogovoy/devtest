namespace DevTest
{
    public class Company : Entity
    {
       
        public Company(string name, Address address, string id = null) : base(id)
        {
            Name = name;
            Address = address;
        }
        public string Name { get; set; }
        public Address Address { get; set; }

        public static Company Find(string id)
        {
            return Find(typeof(Company), id) as Company;
        }
    }
}
