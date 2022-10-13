using Assignment2.Models;

namespace Assignment2.Data
{
    public static class DbInitializer
    {
        // Adds dummy data in case no data is in the database yet
        public static void Initialize(DataContext context)
        {
            // Assume if there are any models in the database, everything
            // is already loaded
            if (context.Models.Any())
            {
                return;
            }

            var models = new Model[]
            {
                new Model
                {
                    FirstName = "Jacek",
                    LastName = "Zygotski",
                    Email = "jzygot@gmail.com",
                    PhoneNo = "42424242",
                    AddresLine1 = "SomeStreet 111",
                    AddresLine2 = "",
                    Zip = "321",
                    City = "YourFavCityFam",
                    BirthDay = DateTime.Parse("10-02-1989"),
                    Height = 180,
                    ShoeSize = 39,
                    HairColor = "Blue",
                    Comments = "No comment on this guy",
                    Jobs = new List<Job>(),
                    Expenses = new List<Expense>()
                },

                new Model
                {
                    FirstName = "Cathrine",
                    LastName = "Vagabond",
                    Email = "CathTheCat@gmail.com",
                    PhoneNo = "19919919",
                    AddresLine1 = "SomeOtherStreet 11",
                    AddresLine2 = "",
                    Zip = "321",
                    City = "YourFavCityFam",
                    BirthDay = DateTime.Parse("20-04-2001"),
                    Height = 200,
                    ShoeSize = 47,
                    HairColor = "Coral",
                    Comments = "Doesn't like dogs",
                    Jobs = new List<Job>(),
                    Expenses = new List<Expense>()
                },
                new Model
                {
                    FirstName = "Nadia",
                    LastName = "Hilarton",
                    Email = "nadiaaaaaYay@gmail.com",
                    PhoneNo = "24161521",
                    AddresLine1 = "Baker's street 11",
                    AddresLine2 = "",
                    Zip = "12351",
                    City = "London",
                    BirthDay = DateTime.Parse("18-05-2003"),
                    Height = 167,
                    ShoeSize = 37,
                    HairColor = "Juicy Orange",
                    Comments = "Who colours their hair orange!??",
                    Jobs = new List<Job>(),
                    Expenses = new List<Expense>()
                }
            };

            foreach (Model m in models)
            {
                context.Models.Add(m);
            }

            var jobs = new Job[]
            {
                new Job()
                {
                    Customer = "Chanel",
                    StartDate = DateTime.Parse("05-04-2022"),
                    Days = 5,
                    Location = "New York",
                    Comments = "",
                    Models = new List<Model>(),
                    Expenses = new List<Expense>()
                },

                new Job()
                {
                    Customer = "Best Perfumes",
                    StartDate = DateTime.Parse("10-10-2022"),
                    Days = 10,
                    Location = "Madrid",
                    Comments = "No comments",
                    Models = new List<Model>(),
                    Expenses = new List<Expense>()
                },
                new Job()
                {
                    Customer = "Sweet Ass Underwear",
                    StartDate = DateTime.Parse("25-04-2024"),
                    Days = 45,
                    Location = "Iran",
                    Comments = "Remember burka, fam",
                    Models = new List<Model>(),
                    Expenses = new List<Expense>()
                }
            };

            foreach (Job j in jobs)
            {
                context.Jobs.Add(j);
            }

            context.SaveChanges();
        }
    }
}
