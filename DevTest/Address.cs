namespace DevTest
{
    public class Address : Entity
    {
        public string Address1 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }

        public Address(string address1, string city, string state, string zipCode, string id= null) : base(id)
        {
            Address1 = address1;
            City = city;
            State = state;
            ZipCode = zipCode;
        }

        public static Address Find(string id)
        {
            return Find(typeof(Address), id) as Address;
        }

    }
}
