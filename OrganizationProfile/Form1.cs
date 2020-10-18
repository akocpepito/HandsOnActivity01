using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Text.RegularExpressions;


namespace OrganizationProfile
{
    public partial class frmRegistration : Form
    {
        private string _FullName;
        private int _Age;
        private long _ContactNo;
        private long _StudentNo;
        private Boolean good = false;

        /////return methods 
        public long StudentNumber(string studNum)
        {
            if (Regex.IsMatch(studNum, @"^[0-9]+$"))
            {
                _StudentNo = long.Parse(studNum);
            }
            else
            {
                throw new FormatException();
            }

            return _StudentNo;
        }

        public long ContactNo(string Contact)
        {     
            if (Regex.IsMatch(Contact, @"^[0-9]{10,11}$"))
            {
                _ContactNo = long.Parse(Contact);
            }
            else
            {
                throw new IndexOutOfRangeException();
            }
                    
            return _ContactNo;
        }

        public string FullName(string LastName, string FirstName, string MiddleInitial)
        {
            if (Regex.IsMatch(LastName, @"^[a-zA-Z]+$") && Regex.IsMatch(FirstName, @"^[a-zA-Z]+$") && Regex.IsMatch(MiddleInitial, @"^[a-zA-Z]+$"))
            {
                _FullName = LastName + ", " + FirstName + ", " + MiddleInitial;
            }
            else
            {
                throw new ArgumentNullException();
            }

            return _FullName;
        }

        public int Age(string age)
        {
            if (Regex.IsMatch(age, @"^[0-9]{1,3}$"))
            {
                _Age = Int32.Parse(age);
            }
            else
            {
                throw new OverflowException();
            }

            return _Age;
        }

        public frmRegistration()
        {
            InitializeComponent();
        }

        private void frmRegistration_Load(object sender, EventArgs e)
        {
            string[] ListOfProgram = new string[]
                {
                    "BS Information Technology",
                    "BS Computer Science",
                    "BS Information Systems",
                    "BS in Accountancy",
                    "BS in Hospitality Management",
                    "BS in Tourism Management"
                };
            for (int i = 0; i < 6; i++)
            {
                cbPrograms.Items.Add(ListOfProgram[i].ToString());
            }

            cbGender.Items.Add("Male");
            cbGender.Items.Add("Female");
        }



        private void btnRegister_Click(object sender, EventArgs e)
        {
            try
            {
                good = false;

                StudentInformationClass.SetFullName = FullName(txtLastName.Text, txtFirstName.Text, txtMiddleInitial.Text);
                StudentInformationClass.SetStudentNo = StudentNumber(txtStudentNo.Text);
                StudentInformationClass.SetProgram = cbPrograms.Text;
                StudentInformationClass.SetGender = cbGender.Text;
                StudentInformationClass.SetContactNo = ContactNo(txtContactNo.Text);
                StudentInformationClass.SetAge = Age(txtAge.Text);
                StudentInformationClass.SetBirthday = dateTimePicker1.Value.ToString("yyyy-MM-dd");

                good = true;
                frmConfirmation frm = new frmConfirmation();
                frm.ShowDialog();
            } // end ni try
            catch (IndexOutOfRangeException ioo)
            {
                MessageBox.Show("Error: Contact number must be at least 10-11 digits long.");

            } // end ni catch
            catch (ArgumentNullException ane)
            {
                MessageBox.Show("Error: Name must not contain numbers or symbols, or empty.");
            }
            catch (FormatException fe)
            {
                MessageBox.Show("Error: Wrong Student number format.");
            }
            catch (OverflowException oe)
            {
                MessageBox.Show("Error: Age range out of bounds");
            }
            finally
            {
                if (good == true)
                {
                    MessageBox.Show("Student information recorded.");
                }
            }
        }
    }

}
