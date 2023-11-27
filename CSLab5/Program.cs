using System.Data.SqlClient;

Console.WriteLine("Hello, World! \n");


while (true)
{
    Console.WriteLine("a) Запит на вибірку \n");
    Console.WriteLine("b) запит на вибірку з використанням спеціальних функцій: LIKE, IS NULL, IN, BETWEEN; \n");
    Console.WriteLine("с) запит зі складним критерієм; \n");
    Console.WriteLine("d) запит з унікальними значеннями; \n");
    Console.WriteLine("e) запит з використанням обчислювального поля; \n");
    Console.WriteLine("f) запит з групуванням по заданому полю, використовуючи умову групування; \n");
    Console.WriteLine("g) запит із сортування по заданому полю в порядку зростання та спадання значень; \n");
    Console.WriteLine("h) запит з використанням дій по модифікації записів. \n");

    Console.WriteLine("Введіть завдання з 5 лабораторної: a b c...");
    string ex = Console.ReadLine().ToString();

    Ex exercise = new Ex();

    switch (ex)
    {
        case "a":
            exercise.a();
            break;
        case "b":
            exercise.b();
            break;
        case "c":
            exercise.c();
            break;
        case "d":
            exercise.d();
            break;
        case "e":
            exercise.e();
            break;
        case "f":
            exercise.f();
            break;
        case "g":
            exercise.g();
            break;
        case "h":
            exercise.h();
            break;
        default:
            break;
    }

    Console.WriteLine("\n Щоб продовжити натисність Enter \n");
    Console.ReadLine();

}



class Ex
{
    SqlConnection connection;
    public Ex()
    {
        string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SA2_Kyrylenko;Integrated Security=True;Connect Timeout=30;Encrypt=False;";

        SqlConnection connection = new SqlConnection(connectionString);
        
        connection.Open();

        this.connection = connection;
        
    }

    public void ShowMailingList(SqlDataReader reader)
    {
        Console.WriteLine($"ID: {reader["id"]}, Theme: {reader["theme"]}, Description: {reader["mail_descripiton"]}, To subscriber: {reader["to_subscriber"]}, Send Date: {reader["send_date"]}");
    }

    public void ShowMailingListWithSubscriber(SqlDataReader reader)
    {
        Console.WriteLine($"" +
                $"ID: {reader["id"]}, " +
                $"Theme: {reader["theme"]}," +
                $" Description: {reader["mail_descripiton"]}, " +
                $"To subscriber: {reader["to_subscriber"]}, " +
                $" Send Date: {reader["send_date"]}, " +
                $"User Full Name: {reader["full_name"]}, " +
                $"User address: {reader["address"]}, " +
                $"Username: {reader["username"]}, " +
                $"Password: {reader["password"]}, "
            );
    }

    public void a()
    {
        string sqlQuery = "SELECT * FROM mailing_list";

        SqlCommand command = new SqlCommand(sqlQuery, this.connection);

        SqlDataReader reader = command.ExecuteReader();
            
        while (reader.Read())
        {
            this.ShowMailingList(reader);
        }

        reader.Close();
    }

    public void b()
    {
        Console.WriteLine("SELECT * FROM mailing_list WHERE theme LIKE: ");

        string sqlQueryOne = "SELECT * FROM mailing_list WHERE theme LIKE @Theme";

        using (SqlCommand command = new SqlCommand(sqlQueryOne, this.connection))
        {
            string theme = Console.ReadLine().ToString();

            command.Parameters.AddWithValue("@Theme", $"%{theme}%");

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                this.ShowMailingList(reader);
            }

            reader.Close();
        }

        Console.WriteLine("\n SELECT * FROM mailing_list WHERE to_subscriber IN: ");

        string sqlQueryTwo = "SELECT * FROM mailing_list WHERE to_subscriber IN (@FirstValue, @SecondValue)";

        using (SqlCommand command = new SqlCommand(sqlQueryTwo, this.connection))
        {
            Console.WriteLine("First value:");
            string firstValue = Console.ReadLine().ToString();

            Console.WriteLine("Second value:");
            string secondValue = Console.ReadLine().ToString();

            command.Parameters.AddWithValue("@FirstValue", firstValue);
            command.Parameters.AddWithValue("@SecondValue", secondValue);

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                this.ShowMailingList(reader);
            }

            reader.Close();
        }


        Console.WriteLine("\n SELECT * FROM mailing_list WHERE to_subscriber send_date BETWEEN First date AND Second date: ");

        string sqlQueryThree = "SELECT * FROM mailing_list WHERE send_date BETWEEN @FirstValue AND  @SecondValue";

        using (SqlCommand command = new SqlCommand(sqlQueryThree, this.connection))
        {
            Console.WriteLine("First date:");
            string firstValue = Console.ReadLine().ToString();

            Console.WriteLine("Second date:");
            string secondValue = Console.ReadLine().ToString();

            command.Parameters.AddWithValue("@FirstValue", firstValue);
            command.Parameters.AddWithValue("@SecondValue", secondValue);

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                this.ShowMailingList(reader);
            }

            reader.Close();
        }
    }

    public void c()
    {

        string sqlQuery = "SELECT m.*, s.* FROM mailing_list m JOIN subscriber s ON m.to_subscriber = s.id WHERE m.theme LIKE @Theme AND m.send_date > @SendDate AND s.username LIKE @Username;";

        Console.WriteLine("SELECT m.*, s.* FROM mailing_list m JOIN subscriber s ON m.to_subscriber = s.id");

        using (SqlCommand command = new SqlCommand(sqlQuery, this.connection))
        {
            Console.WriteLine("WHERE m.theme LIKE:");
            string theme = Console.ReadLine().ToString();

            Console.WriteLine("AND m.send_date >:");
            string sendDate = Console.ReadLine().ToString();

            Console.WriteLine("AND s.username LIKE:");
            string userName = Console.ReadLine().ToString();

            command.Parameters.AddWithValue("@Theme", $"%{theme}%");
            command.Parameters.AddWithValue("@SendDate", sendDate);
            command.Parameters.AddWithValue("@Username", $"%{userName}%");

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                this.ShowMailingListWithSubscriber(reader);
            }

            reader.Close();
        }
    }

    public void d()
    {

        string sqlQuery = "SELECT * FROM mailing_list WHERE id = @Value";

        Console.WriteLine("SELECT * FROM mailing_list WHERE id = ");

        using (SqlCommand command = new SqlCommand(sqlQuery, this.connection))
        {
            string value = Console.ReadLine().ToString();

            command.Parameters.AddWithValue("@Value", $"{value}");

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                this.ShowMailingList(reader);
            }

            reader.Close();
        }


    }

    public void e()
    {

        string sqlQuery = "SELECT * FROM mailing_list WHERE SEND_DATE > @SendDate;";

        Console.WriteLine("SELECT * FROM mailing_list WHERE SEND_DATE > ");

        using (SqlCommand command = new SqlCommand(sqlQuery, this.connection))
        {
            string value = Console.ReadLine().ToString();

            command.Parameters.AddWithValue("@SendDate", $"{value}");

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                this.ShowMailingList(reader);
            }

            reader.Close();
        }
    }

    public void f()
    {

        string sqlQuery = "SELECT m.theme, COUNT(*) AS theme_count FROM mailing_list m GROUP BY m.theme HAVING COUNT(*) > @Count;";

        Console.WriteLine("SELECT m.theme, COUNT(*) AS theme_count FROM mailing_list m GROUP BY m.theme HAVING COUNT(*) > ");

        using (SqlCommand command = new SqlCommand(sqlQuery, this.connection))
        {
            string value = Console.ReadLine().ToString();

            command.Parameters.AddWithValue("@Count", value);

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine($"Theme: {reader["theme"]}, Theme Count: {reader["theme_count"]}");
            }

            reader.Close();
        }
    }


    public void g()
    {
        Console.WriteLine("Sort order: DESC or ASC");
        string sqlSortOrder = Console.ReadLine().ToString();

        string sqlQuery = "SELECT * FROM mailing_list ORDER BY send_date " + sqlSortOrder;

        using (SqlCommand command = new SqlCommand(sqlQuery, this.connection))
        {
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                this.ShowMailingList(reader);
            }

            reader.Close();
        }
    }

    public void h()
    {

        string sqlQuery = "UPDATE mailing_list SET theme = @NewValue WHERE theme LIKE @oldValue;";

        Console.WriteLine(sqlQuery);

        Console.WriteLine("New Value: ");
        string newValue = Console.ReadLine().ToString();

        using (SqlCommand command = new SqlCommand(sqlQuery, this.connection))
        {
          
            Console.WriteLine("Old Value: ");

            string oldValue = Console.ReadLine().ToString();

            command.Parameters.AddWithValue("@NewValue", newValue);
            command.Parameters.AddWithValue("@OldValue", $"%{oldValue}%");

            SqlDataReader reader = command.ExecuteReader();

            reader.Close();
        }

        string sqlQueryTwo = "SELECT * FROM mailing_list WHERE theme LIKE @NewValue";

        using (SqlCommand command = new SqlCommand(sqlQueryTwo, this.connection))
        {
            command.Parameters.AddWithValue("@NewValue", $"%{newValue}%");

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                this.ShowMailingList(reader);
            }

            reader.Close();
        }
    }

}

