using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Treca_Zadaca
{
    public partial class DodajNalazForm : Form
    {
        public DodajNalazForm()
        {
            InitializeComponent();
            LoadComboBoxJedinica();
        }

        private void DodajNalazForm_Load(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void buttonOdustani_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LoadComboBoxJedinica()
        {
            // Primjer dinamičkog dodavanja opcija iz nekog izvora
            string[] jedinice = new string[] { "mg/dL", "mmol/L", "g/L", "U/L", "mEq/L" };

            comboBoxJedinica.Items.AddRange(jedinice);
            comboBoxJedinica.SelectedIndex = 0; // Postavite početnu selekciju ako je potrebno
        }


        private void buttonSpremi_Click_1(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(textBoxId.Text) ||
        string.IsNullOrWhiteSpace(textBoxPretraga.Text) ||
        string.IsNullOrWhiteSpace(textBoxRezultat.Text) ||
        string.IsNullOrWhiteSpace(textBoxOpis.Text) ||
        string.IsNullOrWhiteSpace(textBoxPreporuka.Text) ||
        string.IsNullOrWhiteSpace(textBoxLijek.Text) ||
        comboBoxJedinica.SelectedItem == null)
            {
                MessageBox.Show("Molimo popunite sva polja prije spremanja.");
                return; // Prekini izvršavanje ako neka polja nisu popunjena
            }
            string connectionString = "Server=31.147.206.65;Database=PI2324_ktuksa22_DB;User Id=PI2324_ktuksa22_User;Password=WLV7_OMG;";
            string query = "INSERT INTO Nalazi (Id, Pretraga, Rezultat, Jedinica, Opis, Preporuka, Lijek) VALUES (@Id, @Pretraga, @Rezultat, @Jedinica, @Opis, @Preporuka, @Lijek)";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Id", int.Parse(textBoxId.Text));
                        cmd.Parameters.AddWithValue("@Pretraga", textBoxPretraga.Text);
                        cmd.Parameters.AddWithValue("@Rezultat", textBoxRezultat.Text);
                        cmd.Parameters.AddWithValue("@Jedinica", comboBoxJedinica.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@Opis", textBoxOpis.Text);
                        cmd.Parameters.AddWithValue("@Preporuka", textBoxPreporuka.Text);
                        cmd.Parameters.AddWithValue("@Lijek", textBoxLijek.Text);
                        cmd.ExecuteNonQuery();
                    }
                    MessageBox.Show("Podaci su uspješno dodani.");
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Greška: " + ex.Message);
                }
            }
        }

        
    }
}
