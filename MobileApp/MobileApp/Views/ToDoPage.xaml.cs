using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ToDoPage : ContentPage
    {
        public ObservableCollection<ToDoItem> ToDos { get; set; } = new ObservableCollection<ToDoItem>();

        public ToDoService Service {get;} = new ToDoService();

        public ToDoPage()
        {
            Title = "My To Dos";

            InitializeComponent();

            BindableLayout.SetItemsSource(ItemsContainer, ToDos);

            Root.Children.Add(new Button
            {
                Text = "Add",
                Command = new Command(() => AddItem()),
            });
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            ToDoItem[] result = await Service.GetTodos();

            foreach (ToDoItem item in result)
            {
                ToDos.Add(item);
            }

        }

        private void AddItem()
        {
            ToDos.Add(new ToDoItem { Title = DateTime.Now.ToString() });
        }
    }

    public class ToDoService
    {
        public async Task<ToDoItem[]> GetTodos()
        {
            HttpClient http = new HttpClient
            {
                BaseAddress = new Uri("https://495b-186-26-118-196.ngrok.io")
            };

            string json = await http.GetStringAsync("/api/todos");

            return JsonConvert.DeserializeObject<ToDoItem[]>(json);
        }
    }

    public class ToDoItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsComplete { get; set; }
    }
}