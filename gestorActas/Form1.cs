using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using MySql.Data.MySqlClient;

namespace gestorActas
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            String id = tbId.Text;
            String archivo = rutaArchivo.Text;
            String fecha = dtReunion.Value.ToString("MM-dd-yyyy HH:mm:ss");
            String presidente = tbPresidente.Text;
            String lugar = tbLugar.Text;
            String resumen = tbResumen.Text;
            if (!string.IsNullOrWhiteSpace(archivo) && !string.IsNullOrWhiteSpace(fecha) && !string.IsNullOrWhiteSpace(presidente) && !string.IsNullOrWhiteSpace(lugar) && !string.IsNullOrWhiteSpace(resumen))
            {
                string sql = "INSERT INTO registros (id, fecha, lugar, presidente, resumen, archivo) VALUE ('" + id + "','"+ fecha +"','" + lugar + "','" + presidente + "','" + resumen + "','" + archivo + "')";
                //MessageBox.Show(sql);
                MySqlConnection conexionDb = Conexion.conexion();
                conexionDb.Open();
                try
                {
                    MySqlCommand comando = new MySqlCommand(sql, conexionDb);
                    comando.ExecuteNonQuery();
                    MessageBox.Show("Registro Guardado");
                    rutaArchivo.Text = "";
                    tbPresidente.Text = "";
                    tbResumen.Text = "";
                    tbLugar.Text = "";
                    dtReunion.Value = DateTime.Now;

                    //INSERT INTO `Registros` (`id`, `fecha`, `lugar`, `presidente`, `resumen`) VALUES (NULL, 'hgfhfg', 'hfghfg', 'hfghf', 'fghfg')
                }
                catch(MySqlException ex)
                {
                    MessageBox.Show("Error al guardar: " + ex.Message);
                }
                finally
                {
                    conexionDb.Close();
                }
            }
            else
            {
                MessageBox.Show("Favor de completar los campos");
                //conexionDb.Close();
            }
        }

        private void btnSeleccion_Click(object sender, EventArgs e)
        {
            //configuracion  de algunos parametros del openFileDialog
            // directorio inicial donde se abrira
            openFileDialog1.InitialDirectory = "C:\\";
            // filtro de archivos.
            openFileDialog1.Filter = "Archivos de texto (*.pdf)|*.pdf";

            // codigo para abrir el cuadro de dialogo
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string str_RutaArchivo = openFileDialog1.FileName;
                    rutaArchivo.Text = str_RutaArchivo;

                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            rutaArchivo.Text="";
            tbPresidente.Text = "";
            tbResumen.Text = "";
            tbLugar.Text = "";
            dtReunion.Value = DateTime.Now;
        }

        private void btnLimpiarLeer_Click(object sender, EventArgs e)
        {
            tbLeerId.Clear();
            dtLeerFecha.Value = DateTime.Now;
            tbLeerArchivo.Clear();
            tbLeerPresidente.Clear();
            tbLeerLugar.Clear();
            tbLeerResumen.Clear();
        }

        private void btnConsultarLeer_Click(object sender, EventArgs e)
        {
            String id = tbLeerId.Text;
            MySqlDataReader reader = null;

            string sql = "SELECT id, fecha, lugar, presidente, resumen, archivo FROM Registros WHERE id LIKE '" + id + "' LIMIT 1";
            MySqlConnection conexionBD = Conexion.conexion();
            conexionBD.Open();
            try
            {
                MySqlCommand comando = new MySqlCommand(sql, conexionBD);
                reader = comando.ExecuteReader();
                if(reader.HasRows)
                {
                    while(reader.Read())
                    {
                        //tbLeerId.Text = reader.GetString(0);
                        //dtLeerFecha.Value = reader.GetString(1);
                        tbLeerLugar.Text = reader.GetString(2);
                        tbLeerPresidente.Text = reader.GetString(3);
                        tbLeerResumen.Text = reader.GetString(4);
                        tbLeerArchivo.Text = reader.GetString(5);
                    }
                }
                else
                {
                    MessageBox.Show("No se encontro Codigo: " + id);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrio un error " + ex.Message);
                //throw;
            }finally
            {
                conexionBD.Close();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            String id = tbEditarId.Text;
            MySqlDataReader reader = null;

            string sql = "SELECT id, fecha, lugar, presidente, resumen, archivo FROM Registros WHERE id LIKE '" + id + "' LIMIT 1";
            MySqlConnection conexionBD = Conexion.conexion();
            conexionBD.Open();
            try
            {
                MySqlCommand comando = new MySqlCommand(sql, conexionBD);
                reader = comando.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        //tbLeerId.Text = reader.GetString(0);
                        //dtLeerFecha.Value = reader.GetString(1);
                        tbEditarLugar.Text = reader.GetString(2);
                        tbEditarPresidente.Text = reader.GetString(3);
                        tbEditarResumen.Text = reader.GetString(4);
                        tbEditarArchivo.Text = reader.GetString(5);
                    }
                }
                else
                {
                    MessageBox.Show("No se encontro Codigo: " + id);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrio un error " + ex.Message);
                //throw;
            }
            finally
            {
                conexionBD.Close();
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            //string sql = "SELECT id, fecha, lugar, presidente, resumen, archivo FROM Registros WHERE id LIKE '" + id + "' LIMIT 1";
            String id = tbEditarId.Text;
            String fecha = dtEditarFecha.Text;
            String lugar = tbEditarLugar.Text;
            String presidente = tbEditarPresidente.Text;
            String resumen = tbEditarResumen.Text;
            String archivo = tbEditarArchivo.Text;
            string sql = "UPDATE registros SET id='"+id+"', fecha='"+fecha+"', lugar='"+lugar+"', presidente='"+presidente+"', resumen='"+resumen+"', archivo='"+archivo+"' WHERE id='"+id+"'";
            MySqlConnection conexionDB = Conexion.conexion();
            conexionDB.Open();

            try 
            { 
                MySqlCommand comando = new MySqlCommand(sql, conexionDB);
                comando.ExecuteNonQuery();
                MessageBox.Show("Registro modificado");
                //tbEditarId.Clear();
                dtEditarFecha.Value = DateTime.Now;
                tbEditarLugar.Clear();
                tbEditarPresidente.Clear();
                tbEditarResumen.Clear();
                tbEditarArchivo.Clear();
            }catch(MySqlException ex)
            {
                MessageBox.Show("Error al Editar: "+ex.Message);
            }finally
            {
                conexionDB.Close();
            }
        }

        private void tbEliminarId_TextChanged(object sender, EventArgs e)
        {
            String id = tbEliminarId.Text;
            MySqlDataReader reader = null;

            string sql = "SELECT id, fecha, lugar, presidente, resumen, archivo FROM Registros WHERE id LIKE '" + id + "' LIMIT 1";
            MySqlConnection conexionBD = Conexion.conexion();
            conexionBD.Open();
            try
            {
                MySqlCommand comando = new MySqlCommand(sql, conexionBD);
                reader = comando.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        //tbLeerId.Text = reader.GetString(0);
                        //dtLeerFecha.Value = reader.GetString(1);
                        tbEliminarLugar.Text = reader.GetString(2);
                        tbEliminarPresidente.Text = reader.GetString(3);
                        tbEliminarResumen.Text = reader.GetString(4);
                        tbEliminarArchivo.Text = reader.GetString(5);
                    }
                }
                else
                {
                    MessageBox.Show("No se encontro Codigo: " + id);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrio un error " + ex.Message);
                //throw;
            }
            finally
            {
                conexionBD.Close();
            }
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            String id = tbEliminarId.Text;
            String sql = "DELETE FROM Registros WHERE id='"+id+"'";

            MySqlConnection conexionBD = Conexion.conexion();
            conexionBD.Open();
            try
            {
                MySqlCommand comando = new MySqlCommand(sql, conexionBD);
                comando.ExecuteNonQuery();
                MessageBox.Show("Registro Borrado");
                dtEditarFecha.Value = DateTime.Now;
                tbEditarLugar.Clear();
                tbEditarPresidente.Clear();
                tbEditarResumen.Clear();
                tbEditarArchivo.Clear();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar: " + ex.Message);
                //throw;
            }
        }
    }
}
