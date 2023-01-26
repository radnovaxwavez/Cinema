using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkiaSharp;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Cinema
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SeatsPage : ContentPage
    {
        public SeatsPage(Ticket ticket)
        {
            InitializeComponent();
            SelectedTicket = ticket;
            Init();
            this.BindingContext = this;
        }

        public Ticket SelectedTicket { get; set; } //A lot of things seem to be built by this same class format

        Dictionary<int, int> data = new Dictionary<int, int>(); //What is the Directionary class type? + note how data class is used in the rest of this page

        SKPaint availablePaint = new SKPaint() { Style = SKPaintStyle.Stroke, Color = SKColor.Parse("#343352") };

        SKPaint reservedPaint = new SKPaint() { Style = SKPaintStyle.StrokeAndFill, Color = SKColor.Parse("#343352") };

        SKPaint yourSeatPaint = new SKPaint() { Style = SKPaintStyle.StrokeAndFill, Color = SKColor.Parse("#9747FF") };

        SKPaint textPaint = new SKPaint() { TextSize = 40, Color = SKColor.Parse("#343352") };

        private void Init()
        {
            var rand = new Random();
            for (int i = 0; i < 120; i++)
            {
                data.Add(i, rand.Next(0, 2));
            }
        }

        private void SKCanvasView_PaintSurface(object sender, SkiaSharp.Views.Forms.SKPaintSurfaceEventArgs e)
        {
            var canvas = e.Surface.Canvas; //using second object shown in brackets above, Would be worth looking in to how this works
            var x = 60;
            var y = 60;
            var row = -1;
            var columns = 14;
            var itemHeight = 60;
            var itemWidth = 60;
            var margin = 20;
            var cornerRadius = 4;
            var items = 0;
            for (int i = 0; i < data.Count; i++) //pulling from the Data class shown on line 26, further affected by Init() which affects the int values of the data clss itself
            {
                if (items == 0)
                {
                    row += 1;
                    items = GetColumn(row); //Program below this class being called
                    var count = (columns - items) / 2; //variable count = columns minus items
                    var offset = (count * itemWidth) + (count * margin); //variable offset = sum of count multiplied by itemwidth plus the sum of the count multiplied by the margin
                    x = 60 + offset; //Remember offset variable gets multiplied count and width and adds it to multiplied count and margin
                    y = (itemWidth + (itemWidth + margin) * row); //Note how to Y coordinates of the grid are created
                }
                else
                {
                    x += itemHeight + margin;
                }

                var seatColorIndex = data[i]; //Beginning of the system that determines colours fo rthe value of the seat (available, unavailable, etc)
                if (SelectedTicket.Seats.Any(z => z == i))
                    seatColorIndex = 2;

                canvas.DrawRoundRect(new SKRoundRect(new SKRect(x, y, x + itemHeight, y + itemWidth), cornerRadius), GetColor(seatColorIndex));

                items -= 1;

                if (items == 0) //If items = 0, draw
                {
                    canvas.DrawText($"{row + 1}", 0, y + margin + (itemHeight / 2), textPaint);
                }
            }
        }

        SKPaint GetColor(int seatColor)
        {
            switch (seatColor)
            {
                case 0: //switch appears to work kind of like if, else statements
                default:
                    return availablePaint;
                case 1:
                    return reservedPaint;
                case 2:
                    return yourSeatPaint;
            }
        }

        private int GetColumn(int row) //Program to determine how many seats are in a given row
        {
            switch (row) //Look into switch statements
            {
                case 0: //Row 0 returns 8 lines in the row
                    return 8;
                case 1: //Rows 1 and 9 return 10 lines in the row
                case 9: //In the case that the row is equal to 9, return 10 lines, is how this works i think
                    return 10;
                case 2: //Rows 2, 3 and 8 return 12 lines in the row
                case 3:
                case 8:
                    return 12;
                default: //Any other rows should return 14 lines in the row
                    return 14;
            }
        }
    }
}