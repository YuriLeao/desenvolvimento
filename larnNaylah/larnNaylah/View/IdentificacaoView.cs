using larnNaylah.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace larnNaylah.View
{
    public class IdentificacaoView : ContentPage
    {
        public IdentificacaoView()
        {
            BindingContext = new ClienteViewModel(this);
           
            
            var grid = new Grid();
            grid.RowDefinitions.Add(new RowDefinition() { Height = 80 });
            grid.RowDefinitions.Add(new RowDefinition() { Height = 45 });
            grid.RowDefinitions.Add(new RowDefinition() { Height = 70 });
            var CNPJEntry = new Entry()
            {
                FontSize = 16,
                TextColor = Color.Purple,
                Keyboard = Keyboard.Numeric,
                HorizontalTextAlignment = TextAlignment.Center
            };
            CNPJEntry.SetBinding(Entry.TextProperty, Binding.Create<ClienteViewModel>(cvm => cvm.CNPJ, BindingMode.TwoWay));
            var confirmarButton = new Button()
            {
                BackgroundColor = Color.Purple,
                TextColor = Color.White,
                Text = "Entrar"
            };
            confirmarButton.SetBinding(Button.CommandProperty, Binding.Create<ClienteViewModel>(cvm => cvm.BuscarCliente));
            var pessoaImage = new Image()
            {
                HorizontalOptions = LayoutOptions.Center,
                HeightRequest = 150,
                Source = "pessoa"
            };
            grid.Children.Add(pessoaImage, 0, 0);
            grid.Children.Add(CNPJEntry, 0, 1);
            grid.Children.Add(confirmarButton, 0, 2);

            var stackLayout = new StackLayout()
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center
            };

            stackLayout.Children.Add(grid);


            Content = stackLayout;
        }
        
    }
}
