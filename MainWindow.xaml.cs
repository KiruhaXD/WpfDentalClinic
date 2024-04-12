using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfAppCallingApi.Models;

namespace WpfAppCallingApi
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        HttpClient client = new HttpClient();
        public MainWindow()
        {
            client.BaseAddress = new Uri("http://localhost:5096");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json")
                );
            InitializeComponent();
        }

        private void btnLoadPatients_Click(object sender, RoutedEventArgs e)
        {
            this.GetPatients();
        }

        private async void GetPatients() 
        {
            lblMessage.Content = "";
            var responce = await client.GetStringAsync("/patients/");
            var patients = JsonConvert.DeserializeObject<List<Patients>>(responce);
            dgPatient.DataContext = patients;
        }

        private async void SavePatients(Patients tablesPatients) 
        {
            /*try 
            {*/
                await client.PostAsJsonAsync("/patients/", tablesPatients);
            /*} 
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }*/

        }

        private async void UpdatePatients(Patients tablesPatients)
        {
            /*try 
            {*/
                await client.PutAsJsonAsync("/patients/" + tablesPatients.Id, tablesPatients);
            /*}
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }*/
        }

        private async void DeletePatients(int patientId)
        {
            await client.DeleteAsync("/patients/" + patientId);
        }

        private void btnSavePatient_Click(object sender, RoutedEventArgs e)
        {
            var patients = new Patients
            {
                Id = Convert.ToInt32(txtPatientId.Text),
                fullName = txtName.Text,
                dateBirth = txtDateBirth.Text,
                phoneNumber = txtPhoneNumber.Text,
                email = txtEmail.Text,
                address = txtAddress.Text
            };

            if (patients.Id == 0)
            {
                this.SavePatients(patients);
                lblMessage.Content = "Patient Saved";
            }

            else 
            {
                this.UpdatePatients(patients);
                lblMessage.Content = "Patient Updated";
            }

            txtPatientId.Text = 0.ToString();
            txtName.Text = "";
            txtDateBirth.Text = "";
            txtPhoneNumber.Text = "";
            txtEmail.Text = "";
            txtAddress.Text = "";
        }
        void btnEditPatients(object sender, RoutedEventArgs e) 
        {
            Patients tablesPatients = ((FrameworkElement)sender).DataContext as Patients;
            txtPatientId.Text = tablesPatients.Id.ToString();
            txtName.Text = tablesPatients.fullName;
            txtDateBirth.Text = tablesPatients.dateBirth;
            txtPhoneNumber.Text = tablesPatients.phoneNumber;
            txtEmail.Text = tablesPatients.email;
            txtAddress.Text = tablesPatients.address;
        }

        void btnDeletePatients(object sender, RoutedEventArgs e)
        {
            Patients tablesPatients = ((FrameworkElement)sender).DataContext as Patients;
            this.DeletePatients(tablesPatients.Id);
        }

        private void dgPatient_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
