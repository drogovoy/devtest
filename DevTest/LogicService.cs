using System;
using NUnit.Framework;

namespace DevTest
{
    public class LogicService
    {
        public void SetUp()
        {
            SaveTest();
            //UpdateCustomerById("1");
            UpdateCustomerByInstance("1");
            //TestFind();
        }
        private void SaveTest()
        {
            Address address = new Address("509 Golden Gate Dr", "Richboro", "PA", "18954");
            Customer customer = new Customer("Dima", "Rogovoy", address);
            Assert.IsNullOrEmpty(customer.Id);
            customer.Save();
            Address address2 = new Address("1600 Amphitheatre Pkwy", " Mountain View", "CA", "94043");
            Company company = new Company("Google", address2);
            company.Save();
        }
        private void TestFind()
        {
            //find Customer 1 from Customer store instance
            FileStore<Customer> custStore = new FileStore<Customer>();
            Entity customer1 = custStore.FindById("1");

            //find Company 1 from Company store instance
            FileStore<Company> companyStore = new FileStore<Company>();
            Entity company1 = companyStore.FindById("1");

            //find Customer 1 with Static Store
            Customer customer2 = Customer.Find("1");
            Console.Write(customer2.Id);

            //find Company 1 with Static Store
            Company company2 = Company.Find("1");
            Console.Write(company2.Id);

            //TestDeletes(customer2, company2);
        }

        private void TestDeletes(Customer cust, Company comp)
        {
            cust.Delete();
            comp.Delete();
        }

        private bool UpdateCustomerById(string id)
        {
            Customer customer = Customer.Find(id);
            if (customer == null) return false;
            customer.FirstName = "Ilona";
            customer.LastName = "Kruglikov";
            customer.Save();
            return true;
        }
        private bool UpdateCustomerByInstance(string id)
        {
            Address address = new Address("509 Golden Gate Dr", "Richboro", "PA", "18954");
            Customer customer = new Customer("Ilona", "Kruglikov", address,"1");
            customer.Save();
            return true;
        }

    }
}
