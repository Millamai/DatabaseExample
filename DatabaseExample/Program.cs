using System.Data.SqlClient;

namespace DatabaseExample
{
    internal class Program
    {
        static string conn = "Server=localhost;Database=Cars;Integrated Security=True;;Encrypt=False";

        public static void Add(Car car)
        {
            SqlConnection connection = new SqlConnection(conn);
            try
            {
                connection.Open();
                //Her laves insert statement ligesom du ville gøre det i Management Studio, men med en lille forskel
                //@ repræsentere et parameter, som først indsættes senere (SQL injection angreb)
                string sql = "INSERT INTO Cars (Model, Brand, Price, YearOfManufacture, IsElectric) " +
                             "VALUES (@Model, @Brand, @Price, @YearOfManufacture, @IsElectric)";

                //Vi skal sende en kommando til databasen
                SqlCommand command = new SqlCommand(sql, connection);
                //Her bytter vi parametrene ud
                command.Parameters.AddWithValue("@Model", car.Model);
                command.Parameters.AddWithValue("@Brand", car.Brand);
                command.Parameters.AddWithValue("@Price", car.Price);
                command.Parameters.AddWithValue("@YearOfManufacture", car.YearOfManufacture);
                command.Parameters.AddWithValue("@IsElectric", car.IsElectric);
                //Her udfører vi kommandoen og eftersom vi ikke skal have
                //noget tilbage så bruger vi ExecuteNonQuery
                command.ExecuteNonQuery(); // Udfører INSERT-operationen
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close(); // Lukker forbindelsen manuelt
            }
        }



        public static void Update(Car car)
        {
            SqlConnection connection = new SqlConnection(conn);
            try
            {
                connection.Open();
                string sql = "UPDATE Cars SET Model = @Model, Brand = @Brand, Price = @Price, " +
                             "YearOfManufacture = @YearOfManufacture, IsElectric = @IsElectric " +
                             "WHERE CarID = @CarID";

                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@Model", car.Model);
                command.Parameters.AddWithValue("@Brand", car.Brand);
                command.Parameters.AddWithValue("@Price", car.Price);
                command.Parameters.AddWithValue("@YearOfManufacture", car.YearOfManufacture);
                command.Parameters.AddWithValue("@IsElectric", car.IsElectric);
                command.Parameters.AddWithValue("@CarID", car.Id);

                command.ExecuteNonQuery(); // Udfører UPDATE-operationen
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close(); // Lukker forbindelsen manuelt
            }
        }


        public static List<Car> GetAllCars()
        {
            // Opret en liste til at holde alle biler
            // Denne liste returneres når vi er færdige
            List<Car> cars = new List<Car>();

            // Opret forbindelse til databasen
            SqlConnection connection = new SqlConnection(conn);

            try
            {
                // Åbn forbindelsen
                // Vi skal altid have en åben forbindelse til databasen - det er et must
                connection.Open();

                // SQL-forespørgsel for at hente alle biler
                string sql = "SELECT CarID, Model, Brand, Price, YearOfManufacture, IsElectric FROM Cars";

                //En SqlCommand er et objekt i ADO.NET, der bruges til at udføre SQL-forespørgsler
                //Vi kan intet foretage os uden en command
                SqlCommand command = new SqlCommand(sql, connection);

                // Kør forespørgslen og hent data ved hjælp af SqlDataReader
                // En SqlDataReader er et objekt i ADO.NET, der bruges til at læse data fra en
                // SQL-forespørgsel, én række ad gangen, i fremadgående retning. 
                SqlDataReader reader = command.ExecuteReader();

                // Læs resultaterne
                // Der er en markør i reader, som hele tiden holder styr på hvilken record
                // vi har læst og vi stopper løkken efter sidste element
                while (reader.Read())
                {
                    // Opret et Car-objekt og fyld det med data fra databasen
                    // Bemærk at det er her vi "mapper" datatyper fra SQL til C#
                    // (0) fortæller at der er tale om første attribut osv.
                    // Hvis man ændrer i rækkefølgen skal disse også ændres
                    Car car = new Car(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetDouble(3), reader.GetDateTime(4), reader.GetBoolean(5));
                  
                    // Tilføj bilen til listen
                    cars.Add(car);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally //Bemærk at denne blok altid bliver afviklet
            {
                // Luk forbindelsen manuelt
                // VI SKAL ALTID LUKKE FORBINDELSEN
                connection.Close();
            }

            // Returner listen af biler
            return cars;
        }

        public static void Delete(int carID)
        {
            SqlConnection connection = new SqlConnection(conn);
            try
            {
                connection.Open();
                string sql = "DELETE FROM Cars WHERE carid = @CarID";

                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@CarID", carID);

                command.ExecuteNonQuery(); // Udfører DELETE-operationen
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close(); // Lukker forbindelsen manuelt
            }
        }

        static void Main(string[] args)
        {
            //Lad os først hente alle elementer i databasen

            List<Car> list = new List<Car>();
            list = GetAllCars();
            foreach (Car car in list)
            {
                Console.WriteLine(car);
            }
            Console.WriteLine();


            //Lad os oprette en ny bil
            Car c = new Car("Aygo", "Toyota", 999999.95, new DateTime(2020, 1, 1), false);
            
            //Vi tilføjer en ny bil og gemmer den i databasen
            Add(c);

            //Bemærk at Aygo ikke har noget id, førend der indlæses fra DB

            //Lad os så se hvad der nu ligger i databasen
            list = GetAllCars();
            foreach (Car car in list)
            {
                Console.WriteLine(car);
            }
            Console.WriteLine();

            //Prisen på den der Mustang er alt for lav, så den skal vi justere
            //Her bruges et lambda udtryk, det lærer vi om senere
            Car mustang = list.Find(c => c.Model.Equals("Mustang"));
            mustang.Price = 200000.0;

            //Nu opdaterer vi mustang i DB
            Update(mustang);
            
            list = GetAllCars();
            foreach (Car car in list)
            {
                Console.WriteLine(car);
            }
            Console.WriteLine();

            //Lad os slette Aygoen igen
            Car aygo = list.Find(c => c.Model.Equals("Aygo"));
            //Bemærk at dette Lambda udtryk finder første forekomst af en Aygo
            Delete(aygo.Id);


        }
    }
}
