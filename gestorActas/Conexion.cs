using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace gestorActas
{
    internal class Conexion
    {
        public static MySqlConnection conexion()
        {
            string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=admin123;database=test;";
            // Prepara la conexión
            //MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);
            //commandDatabase.CommandTimeout = 60;
            //MySqlDataReader reader;
            try
            {
                MySqlConnection conexionDB = new MySqlConnection(connectionString);
                return conexionDB;
            }
            catch (Exception ex)
            {
                // Mostrar cualquier excepción
                MessageBox.Show(ex.Message);
                return null;
            }

        }


    }
}
