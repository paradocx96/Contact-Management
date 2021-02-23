using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;


namespace Contact_Management
{
    public partial class contactHome : Form
    {
        public int ContactID;
        public string Name;
        public int ContactNo;
        public string City;
        public string Address;
        string connetionString;
        SqlConnection connection;
        
        public contactHome()
        {
            InitializeComponent();
        }

        // Get the databse connection
        public string getConnectionString()
        {
            string conString;

            conString = @"Data Source=PARADOCX-PC;Initial Catalog=ContactManagement;Integrated Security=True";

            return conString;
        }

        private void contactHome_Load(object sender, EventArgs e)
        {
            getAllContact();
        }

        // Get all data from db
        public void getAllContact()
        {
            // Star the connection
            connetionString = getConnectionString();
            connection = new SqlConnection(connetionString);
            connection.Open();

            // Sql Query
            string sql = "SELECT * FROM dbo.contact";

            SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
            SqlCommandBuilder commandBuilder = new SqlCommandBuilder(adapter);

            // adding data to table
            DataTable table = new DataTable();
            adapter.Fill(table);
            dataGridView1.DataSource = table;

            //Close db connection
            connection.Close();
        }

        // Insert data to db
        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Star the connection
            connetionString = getConnectionString();
            connection = new SqlConnection(connetionString);
            connection.Open();

            // Get value
            Name = txtContactName.Text;
            ContactNo = Int32.Parse(txtContactNo.Text);
            City = txtContactLocation.Text;
            Address = txtContactAddress.Text;

            // Sql Query
            string query = "INSERT INTO dbo.contact values (@name,@contactNo,@city,@address)";

            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@name", Name);
            cmd.Parameters.AddWithValue("@contactNo", ContactNo);
            cmd.Parameters.AddWithValue("@city", City);
            cmd.Parameters.AddWithValue("@address", Address);

            int result = cmd.ExecuteNonQuery();

            //Check the result
            if (result > 0)
            {
                MessageBox.Show("Contact Successfully Added");
                ClearBoxes();
            }
            else
            {
                MessageBox.Show("Contact Adding Failed");
            }

            //Close db connection
            connection.Close();

            getAllContact();
        }

        // Update data method
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // Star the connection
            connetionString = getConnectionString();
            connection = new SqlConnection(connetionString);
            connection.Open();

            // Get value
            ContactID = Int32.Parse(txtContactID.Text);
            Name = txtContactName.Text;
            ContactNo = Int32.Parse(txtContactNo.Text);
            City = txtContactLocation.Text;
            Address = txtContactAddress.Text;

            // Sql Query
            string sql = "UPDATE dbo.contact SET name=@name,contactNo=@contactNo,city=@city,address=@address WHERE contactId=@contactId";

            SqlCommand cmd = new SqlCommand(sql, connection);

            cmd.Parameters.AddWithValue("@name", Name);
            cmd.Parameters.AddWithValue("@contactNo", ContactNo);
            cmd.Parameters.AddWithValue("@city", City);
            cmd.Parameters.AddWithValue("@address", Address);
            cmd.Parameters.AddWithValue("@contactId", ContactID);

            int result = cmd.ExecuteNonQuery();

            //Check the result
            if (result > 0)
            {
                MessageBox.Show("Contact Successfully Updated");
                ClearBoxes();
            }
            else
            {
                MessageBox.Show("Contact Update Failed");
            }

            //Close db connection
            connection.Close();

            getAllContact();
        }

        // Delete data method
        private void btnDelete_Click(object sender, EventArgs e)
        {
            // Star the connection
            connetionString = getConnectionString();
            connection = new SqlConnection(connetionString);
            connection.Open();

            // Get value
            ContactID = Int32.Parse(txtContactID.Text);

            // Sql Query
            string sql = "DELETE FROM dbo.contact WHERE contactId=@contactId";

            SqlCommand cmd = new SqlCommand(sql, connection);

            cmd.Parameters.AddWithValue("@contactId",ContactID);

            int result = cmd.ExecuteNonQuery();
            
            //Check the result
            if (result > 0)
            {
                MessageBox.Show("Contact Successfully Updated");
                ClearBoxes();
            }
            else
            {
                MessageBox.Show("Contact Update Failed");
            }

            //Close db connection
            connection.Close();

            getAllContact();
        }

        // Search Box Method
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            // Get the search keyword
            string keyword = txtSearch.Text;

            // Star the connection
            connetionString = getConnectionString();
            connection = new SqlConnection(connetionString);
            connection.Open();

            // Sql Query
            string query = "SELECT * FROM dbo.contact WHERE contactId LIKE '%"+keyword+"%' OR name LIKE '%"+keyword+"%' OR contactNo LIKE '%"+keyword+"%' OR city LIKE '%"+keyword+ "%' OR address LIKE '%"+keyword+"%'";

            SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
        }

        // Close the application
        private void closeWindow_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Clear input boxes
        public void ClearBoxes()
        {
            txtContactID.Text = "";
            txtContactName.Text = "";
            txtContactNo.Text = "";
            txtContactLocation.Text = "";
            txtContactAddress.Text = "";
        }

        // Get selected data to text boxes
        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            // Get data to textboxes
            int rowIndex = e.RowIndex;

            txtContactID.Text = dataGridView1.Rows[rowIndex].Cells[0].Value.ToString();
            txtContactName.Text = dataGridView1.Rows[rowIndex].Cells[1].Value.ToString();
            txtContactNo.Text = dataGridView1.Rows[rowIndex].Cells[2].Value.ToString();
            txtContactLocation.Text = dataGridView1.Rows[rowIndex].Cells[3].Value.ToString();
            txtContactAddress.Text = dataGridView1.Rows[rowIndex].Cells[4].Value.ToString();
        }

        // Reset button Method
        private void btnReset_Click(object sender, EventArgs e)
        {
            ClearBoxes();
            getAllContact();
        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }
    }
}
