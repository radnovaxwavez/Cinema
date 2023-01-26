using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace Cinema
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            Tickets = GetTickets();
            this.BindingContext = this; //What is this doing?
        }

        public ObservableCollection<Ticket> Tickets { get; set; }
        public Ticket SelectedTicket { get; set; }

        private ObservableCollection<Ticket> GetTickets() //Note use of Binding in CodepBehind
        {
            return new ObservableCollection<Ticket> //Clearly there's a dataset in here that allows this
            {
                 new Ticket { MovieTitle = "Bad Boys For LIfe", Image = "BadBoys.jpg", ShowingDate = DateTime.Now.AddDays(15), Seats = new int[] { 61, 62, 63 } }, 
                 new Ticket { MovieTitle = "The Old Guard", Image = "OldGuard.jpg", ShowingDate = DateTime.Now.AddDays(22), Seats = new int[] { 111, 112 } },
                 new Ticket { MovieTitle = "Tenet", Image = "Tenet.jpg", ShowingDate = DateTime.Now.AddDays(25), Seats = new int[] { 12, 25, 35 } },
                 new Ticket { MovieTitle = "No Time To Die", Image = "TimeToDie.jpg", ShowingDate = DateTime.Now.AddDays(20), Seats = new int[] { 99 } }
            };
        }

        private void TicketSelected(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null)
            {
                this.Navigation.PushAsync(new SeatsPage(SelectedTicket));  //SelectedTicket is being referenced from the SeatsPage.Xaml.cs file
            };                                                             //Essentially it's passing SelectedTicket through SeatsPage
        }
    }
    public class Ticket  //Need to know more about getters and setters
    {
        public string MovieTitle { get; set; }

        public DateTime ShowingDate { get; set; }

        public string Image { get; set; }

        public int[] Seats { get; set; }
    }
}
