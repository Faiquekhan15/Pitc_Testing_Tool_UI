using System.Data.Odbc;
using System.Data;

namespace MDC_Server
{
    class DBGetSet
    {
        private string myconn = "DRIVER={MySQL ODBC 3.51 Driver};Database=world;Server=localhost;Port=2142;UID=root;PWD=TRANSFOPOWER@123@321;";
        public string Query { get; set; }

        public DataTable ExecuteReader()
        {
            DataTable dt = new DataTable();
            using (OdbcConnection connection = new OdbcConnection(myconn))
            {
                connection.Open();
                using (OdbcCommand command = new OdbcCommand(Query, connection))
                using (OdbcDataReader dr = command.ExecuteReader())
                {
                    dt.Load(dr);
                    dr.Close();
                }
                connection.Close();
            }
            return dt;
        }

        public void ExecuteNonQuery()
        {
            using (OdbcConnection connection = new OdbcConnection(myconn))
            {
                connection.Open();
                using (OdbcCommand command = new OdbcCommand(Query, connection))
                    command.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}
