using Infrastructuur.Models;
using Infrastructuur.Repositories.Classes;
using Infrastructuur.Repositories.Interfaces;
using MongoDB.Bson.IO;
using MongoDB.Driver.Core.Configuration;
using System.Net.Http;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Newtonsoft.Json;
using Infrastructuur.Dtos;
namespace NotificationUserInterface
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IMongoRepository<Logger> _logger;
        
        public MainWindow(IMongoRepository<Logger> logger)
        {
            _logger = logger;
        }

        public MainWindow() : this(new MongoRepository<Logger>("YOUR_MONGO_DB", "Notification", "NotificationCollection"))
        {
            InitializeComponent();
        }
        private async void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            await UpdateListBox();
        }
        private async void emailButton_Click(object sender, RoutedEventArgs e)
        {
            await NotificationAsync<EmailReciever>("notification-email",new NotificationEntity<EmailReciever>()
            {
                Message = new EmailReciever()
                {
                    ToEmail = "reciever@hotmailc.com",
                    ToName = "reciever"
                }
            });
            await UpdateListBox();
        }

    

        private async void smsButton_Click(object sender, RoutedEventArgs e)
        {
            await NotificationAsync<SmsReciever>("notification-sms", new NotificationEntity<SmsReciever>()
            {
                Message = new SmsReciever()
                {
                    ToPhone = "44498545",
                }
            });
            await UpdateListBox();
        }
        private static async Task NotificationAsync<T>(string endPoint, NotificationEntity<T> notification)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    // URL of the API endpoint
                    string url = $"https://localhost:7129/{endPoint}";
                    var json = Newtonsoft.Json.JsonConvert.SerializeObject(notification);
                    // Create the HTTP content
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    // Send the POST request
                    HttpResponseMessage response = await client.PostAsync(url, content);

                    // Check the response
                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = await response.Content.ReadAsStringAsync();
                        MessageBox.Show("Success! Response: " + responseBody);
                    }
                    else
                    {
                        MessageBox.Show($"Failed. Status Code: {response.StatusCode}");
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }        }
        private async Task UpdateListBox()
        {
            var logData = await _logger.GetAllAsync();
            lbLogging.ItemsSource = null;
            lbLogging.ItemsSource = logData;
        }
    }
}