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

        private void buttonDodaj_Click(object sender, EventArgs e)
        {
            DodajNalazForm dodajNalazForm = new DodajNalazForm();
            dodajNalazForm.ShowDialog(); 
        }

        private void buttonObrisi_Click(object sender, EventArgs e)
        {
            string connectionString = "Server=31.147.206.65;Database=PI2324_ktuksa22_DB;User Id=PI2324_ktuksa22_User;Password=WLV7_OMG;";
            // Provjeri je li označen red u DataGridView-u
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Dohvati ID ili ključ koji se koristi za identifikaciju reda u bazi
                int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["Id"].Value);

                // Implementirajte logiku brisanja reda iz baze
                string query = "DELETE FROM Nalazi WHERE Id = @Id";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Id", id);

                    try
                    {
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();

                        // Provjerite je li red uspješno obrisan iz baze
                        if (rowsAffected > 0)
                        {
                            // Ako je uspješno, uklonite red iz DataGridView-a
                            dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[0].Index);
                            MessageBox.Show("Red je uspješno obrisan iz baze i DataGridView-a.");
                        }
                        else
                        {
                            MessageBox.Show("Nije uspjelo brisanje retka iz baze.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Došlo je do greške prilikom brisanja retka iz baze: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Nije označen red za brisanje.");
            }
        }
    }
}
