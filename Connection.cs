namespace TourneyPlaner
{
    /// <summary>
    /// The connection string for the MSSQL server.
    /// </summary>
    public class Connection
    {
        public string ConnectionString()
        {
            //string connectionString = "Data Source=ZBC-S-NICK9281;Initial Catalog=HjemmeTest;User ID=HjemmeLogin;Password=Kode1234!";
            string connectionString = "Data Source=192.168.1.4;Initial Catalog=TourneyPlannerDev;User ID=TourneyAdmin;Password=Kode1234!";
            return connectionString;
        }

    }
}
