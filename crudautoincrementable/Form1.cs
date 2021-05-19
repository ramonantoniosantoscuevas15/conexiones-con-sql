using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;

namespace crudautoincrementable
{
    public partial class Form1 : Form
    {
        

        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("Data Source=LAPTOP-5S1O5PDV;Initial Catalog=requisitos;Integrated Security=True");
        public int idestudiente;
        private void Form1_Load(object sender, EventArgs e)
        {
            GetstudentRecord();
        }

        private void GetstudentRecord()
        {
           
            SqlCommand cmd = new SqlCommand("select * from prueba2", con);
            DataTable dt = new DataTable();

            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();

            dataGridView1.DataSource = dt;
            

            
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (isvalid())
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO prueba2 VALUES (@nombre,@apellido,@cedula,@telefono,@direccion)",con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@nombre", textBox1.Text);
                cmd.Parameters.AddWithValue("@apellido", textBox2.Text);
                cmd.Parameters.AddWithValue("@cedula", textBox3.Text);
                cmd.Parameters.AddWithValue("@telefono", textBox4.Text);
                cmd.Parameters.AddWithValue("@direccion", textBox5.Text);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("datos ingresados corectamente");

                GetstudentRecord();
                resetarformcontrol();
            }
        }

        private bool isvalid()
        {
            if(textBox1.Text == string.Empty)
            {
                MessageBox.Show("ingrese el nombre", "failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            resetarformcontrol();
        }

        private void resetarformcontrol()
        {
            idestudiente = 0;
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();

            textBox1.Focus();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            idestudiente = Convert.ToInt32 (dataGridView1.SelectedRows[0].Cells[0].Value);
            textBox1.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            textBox2.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            textBox3.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            textBox4.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
            textBox5.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (idestudiente > 0)
            {
                if (idestudiente > 0)
                {
                    SqlCommand cmd = new SqlCommand("UPDATE prueba2 SET nombre = @nombre, apellido = @apellido, cedula = @cedula, telefono = @telefono, direccion = @direccion WHERE idestudiente = @id", con);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@nombre", textBox1.Text);
                    cmd.Parameters.AddWithValue("@apellido", textBox2.Text);
                    cmd.Parameters.AddWithValue("@cedula", textBox3.Text);
                    cmd.Parameters.AddWithValue("@telefono", textBox4.Text);
                    cmd.Parameters.AddWithValue("@direccion", textBox5.Text);
                    cmd.Parameters.AddWithValue("@id", this.idestudiente);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    MessageBox.Show("datos editados corectamente");

                    GetstudentRecord();
                    resetarformcontrol();
                }
                else
                {
                    MessageBox.Show("seleccione un estudiante a editar");
                }
            }
            else
            {
                MessageBox.Show("seleccione un estudiante a editar");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (idestudiente > 0)
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM prueba2 WHERE idestudiente = @id", con);
                cmd.CommandType = CommandType.Text;
                
                cmd.Parameters.AddWithValue("@id", this.idestudiente);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("datos eliminados correctamente");

                GetstudentRecord();
                resetarformcontrol();
            }
            else
            {
                MessageBox.Show("seleccione un estudiante a eliminal");
            }
        }
    }
}
