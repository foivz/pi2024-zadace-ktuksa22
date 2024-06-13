using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Treca_Zadaca
{
    public partial class Osobni_nalaz : Form
    {
        public Osobni_nalaz()
        {
            InitializeComponent();
        }

        private void Osobni_nalaz_Load(object sender, EventArgs e)
        {
            // Poziv funkcije za popunjavanje DataGridView-a
            FillDataGridView();
        }

        private void FillDataGridView(string searchTerm = "")
        {
            
            string connectionString = "Server=31.147.206.65;Database=PI2324_ktuksa22_DB;User Id=PI2324_ktuksa22_User;Password=WLV7_OMG;";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    // SQL upit za dohvaćanje podataka iz tablice Nalaz
                    string query = "SELECT * FROM Nalazi";

                    if (!string.IsNullOrEmpty(searchTerm))
                    {
                        query += " WHERE Pretraga LIKE @searchTerm";
                    }

                    SqlDataAdapter da = new SqlDataAdapter(query, con);
                    if (!string.IsNullOrEmpty(searchTerm))
                    {
                        da.SelectCommand.Parameters.AddWithValue("@searchTerm", "%" + searchTerm + "%");
                    }

                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            // Poziv funkcije za popunjavanje DataGridView-a s parametrima pretrage
            FillDataGridView(textBoxSearch.Text);
        }

        
    }
}
