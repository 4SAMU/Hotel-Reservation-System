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

namespace CPH_RESERVATION_SYSTEM
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            dateTimePicker1.ValueChanged += new EventHandler(txtcheckin_ValueChanged);
            dateTimePicker2.ValueChanged += new EventHandler(txtcheckin_ValueChanged);
            //
            dateTimePicker1.ValueChanged += new EventHandler(dateTimePicker2_ValueChanged);
            dateTimePicker2.ValueChanged += new EventHandler(dateTimePicker2_ValueChanged);
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\USER\Documents\CPHOTEL.mdf;Integrated Security=True;Connect Timeout=30");

        private void aboutCPHRESERVATIONToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Continental Palm Hotel system");
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        //calculate the number of nights
        private void txtcheckin_ValueChanged(object sender, EventArgs e)
        {
            DateTime startTime = Convert.ToDateTime(dateTimePicker1.Text);
            DateTime stopTime = Convert.ToDateTime(dateTimePicker2.Text);
            if (stopTime >= startTime)
            {
                int a = 1;
                txtNights.Text = ((Convert.ToInt32(a)) + Convert.ToInt32(stopTime.Subtract(startTime).Days)).ToString();
            }
        }
        //calculate the number of nights
        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            DateTime startTime = Convert.ToDateTime(dateTimePicker1.Text);
            DateTime stopTime = Convert.ToDateTime(dateTimePicker2.Text);
            if (stopTime >= startTime)
            {
                int a = 1;
                txtNights.Text = ((Convert.ToInt32(a)) + Convert.ToInt32(stopTime.Subtract(startTime).Days)).ToString();
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //populate datagrid data
            populate();                          
            
        }

        private void populate()
        {
            Con.Open();
            string query = "select * from CPhtable";
            SqlDataAdapter da = new SqlDataAdapter(query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            DVG.DataSource = ds.Tables[0];
            Con.Close();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtDiscount.Text== "15")
            {
                Double x = 0.85;
                txtRoomCharge.Text = (Double.Parse(txtPrice.Text) * Double.Parse(txtNights.Text)* (Convert.ToDouble(x))).ToString();
            }
            else if(txtDiscount.Text=="10")
            {
                Double n = 0.1;
                txtRoomCharge.Text = (Double.Parse(txtPrice.Text) * Double.Parse(txtNights.Text)* (Convert.ToDouble(n))).ToString();

            }
            else if (txtDiscount.Text == "0")
            {

                txtRoomCharge.Text = (Double.Parse(txtPrice.Text) * Double.Parse(txtNights.Text)).ToString();
            }

            txtTotal.Text = (Double.Parse(txtPrice.Text) + Double.Parse(txtTax.Text)).ToString();


            if (txtTotal.Text == "0")
            {
                MessageBox.Show("Please compute total charge");
            }
            else
            {
                txtTotal.Text = (Double.Parse(txtPrice.Text) + Double.Parse(txtTax.Text)).ToString();

                Con.Open();
                String query = "insert into CPhtable values('" + txtFirstName.Text + "' , '" + txtGender.Text + "' ,'" + txtNights.Text + "', '" + txtRoomtype.Text + "', '" + txtPrice.Text + "', '" + txtRoomCharge.Text + "','" + txtGuestType.Text + "','" + txtDiscount.Text + "','" + txtTax.Text + "','" + txtTotal.Text + "','" + txtCity.Text + "', '" + txtHomePhone.Text + "','" + txtMobile.Text + "')";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Customer details Successfully Added");
                Con.Close();
            }



        }

        private void rdMALE_CheckedChanged(object sender, EventArgs e)
        {
            if (rdMALE.Checked)
            {
                txtGender.Text = "MALE";
            }
            else if (rdFemale.Checked)
            {
                txtGender.Text = "FEMALE";
            }
        }

        private void rdFemale_CheckedChanged(object sender, EventArgs e)
        {
            if (rdMALE.Checked)
            {
                txtGender.Text = "MALE";
            }
            else if (rdFemale.Checked)
            {
                txtGender.Text = "FEMALE";
            }
        }

        private void rdStandard_CheckedChanged(object sender, EventArgs e)
        {
            if (rdStandard.Checked)
            {
                Double b = 4.5;
                txtRoomtype.Text = "Standard";
                txtPrice.Text = "119";
                txtTax.Text = ((Convert.ToDouble(b))*Double.Parse(txtNights.Text)).ToString();

            }
            else if (rdDeluxe.Checked)
            {
                Double c = 6.5;
                txtRoomtype.Text = "Deluxe";
                txtPrice.Text = "159";
                txtTax.Text = (Double.Parse(txtNights.Text) * (Convert.ToDouble(c))).ToString();
            }
            else if (rdLuxury.Checked)
            {
                Double d = 10;
                txtRoomtype.Text = "Luxury suite";
                txtPrice.Text = "239";
                txtTax.Text = (Double.Parse(txtNights.Text) * (Convert.ToDouble(d))).ToString();
            }

        }

        private void rdDeluxe_CheckedChanged(object sender, EventArgs e)
        {
            if (rdStandard.Checked)
            {
                Double b = 4.5;
                txtRoomtype.Text = "Standard";
                txtPrice.Text = "119";
                txtTax.Text = (Double.Parse(txtNights.Text) * (Convert.ToDouble(b))).ToString();

            }
            else if (rdDeluxe.Checked)
            {
                Double c = 6.5;
                txtRoomtype.Text = "Deluxe";
                txtPrice.Text = "159";
                txtTax.Text = (Double.Parse(txtNights.Text) * (Convert.ToDouble(c))).ToString();
            }
            else if (rdLuxury.Checked)
            {
                Double d = 10;
                txtRoomtype.Text = "Luxury suite";
                txtPrice.Text = "239";
                txtTax.Text = (Double.Parse(txtNights.Text) * (Convert.ToDouble(d))).ToString();
            }
        }

        private void rdLuxury_CheckedChanged(object sender, EventArgs e)
        {
            if (rdStandard.Checked)
            {
                Double b = 4.5;
                txtRoomtype.Text = "Standard";
                txtPrice.Text = "119";
                txtTax.Text = (Double.Parse(txtNights.Text) * (Convert.ToDouble(b))).ToString();

            }
            else if (rdDeluxe.Checked)
            {
                Double c = 6.5;
                txtRoomtype.Text = "Deluxe";
                txtPrice.Text = "159";
                txtTax.Text = (Double.Parse(txtNights.Text) * (Convert.ToDouble(c))).ToString();
            }
            else if (rdLuxury.Checked)
            {
                Double d = 10;
                txtRoomtype.Text = "Luxury suite";
                txtPrice.Text = "239";
                txtTax.Text = (Double.Parse(txtNights.Text) * (Convert.ToDouble(d))).ToString();
            }
        }

        private void rdEmployee_CheckedChanged(object sender, EventArgs e)
        {
            //guest type radio buttons
            if (rdEmployee.Checked)
            {
                txtGuestType.Text = "Employee";
                txtDiscount.Text = "15";
            }
            else if (rdVIP.Checked)
            {
                txtGuestType.Text = "VIP";
                txtDiscount.Text = "10";
            }
            else if (rdOthers.Checked)
            {
                txtGuestType.Text = "Others";
                txtDiscount.Text = "0";
            }
        }

        private void rdVIP_CheckedChanged(object sender, EventArgs e)
        {
            //guest type radio buttons
            if (rdEmployee.Checked)
            {
                txtGuestType.Text = "Employee";
                txtDiscount.Text = "15";
            }
            else if (rdVIP.Checked)
            {
                txtGuestType.Text = "VIP";
                txtDiscount.Text = "10";
            }
            else if (rdOthers.Checked)
            {
                txtGuestType.Text = "Others";
                txtDiscount.Text = "0";
            }
        }

        private void rdOthers_CheckedChanged(object sender, EventArgs e)
        {
            //guest type radio buttons
            if (rdEmployee.Checked)
            {
                txtGuestType.Text = "Employee";
                txtDiscount.Text = "15";
            }
            else if (rdVIP.Checked)
            {
                txtGuestType.Text = "VIP";
                txtDiscount.Text = "10";
            }
            else if (rdOthers.Checked)
            {
                txtGuestType.Text = "Others";
                txtDiscount.Text = "0";
            }

        }

        private void rEFRESHPAGEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1();
            form.Show();
            this.Hide();
        }

        //cellclick on gridview to feed data on textboxes
        private void DVG_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtFirstName.Text = DVG.SelectedRows[0].Cells[0].Value.ToString();
                txtGender.Text = DVG.SelectedRows[0].Cells[1].Value.ToString();
                txtNights.Text = DVG.SelectedRows[0].Cells[2].Value.ToString();
                txtRoomCharge.Text = DVG.SelectedRows[0].Cells[3].Value.ToString();
                txtPrice.Text = DVG.SelectedRows[0].Cells[4].Value.ToString();
                txtRoomCharge.Text = DVG.SelectedRows[0].Cells[5].Value.ToString();
                txtGuestType.Text = DVG.SelectedRows[0].Cells[6].Value.ToString();
                txtDiscount.Text = DVG.SelectedRows[0].Cells[7].Value.ToString();
                txtTax.Text = DVG.SelectedRows[0].Cells[8].Value.ToString();
                txtTotal.Text = DVG.SelectedRows[0].Cells[9].Value.ToString();
                txtCity.Text = DVG.SelectedRows[0].Cells[10].Value.ToString();
                txtHomePhone.Text = DVG.SelectedRows[0].Cells[11].Value.ToString();
                txtMobile.Text = DVG.SelectedRows[0].Cells[12].Value.ToString();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1();
            form.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Con.Open();
            String query = "update CPhtable set GENDER='" + txtGender.Text + "', NIGHTS='" + txtNights.Text + "',ROOM_TYPE='" + txtRoomtype.Text + "',PRICE='" + txtPrice.Text + "',ROOM_CHARGE='" + txtRoomCharge.Text + "',GUEST_TYPE='" + txtGuestType.Text + "',DISCOUNT='" + txtDiscount.Text + "',TAX='" + txtTax.Text + "',ADDRESS='" + txtCity.Text + "',HOME_PHONE='" + txtHomePhone.Text + "',MOBILE_PHONE='" + txtMobile.Text + "' where NAME='" + txtFirstName.Text + "'";
            SqlCommand cmd = new SqlCommand(query, Con);
            cmd.ExecuteNonQuery();
            MessageBox.Show("updated successfully");
            Con.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Con.Open();
            string query = "delete from CPhtable where NAME='" + txtFirstName.Text + "'";
            SqlCommand cmd = new SqlCommand(query, Con);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Deleted Successfully");
            Con.Close();
        }
    }
    
}
