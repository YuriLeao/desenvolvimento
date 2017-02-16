using GalaSoft.MvvmLight;
using larnNaylah.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Globalization;

namespace larnNaylah.View
{
    public class PedidoView : ContentPage
    {
        private Grid grid;
        private Entry pesquisaEntry;
        private Grid quantidadeGrid;
        public PedidoView(ViewModelBase vmb)
        {
            BindingContext = vmb;
            SetBinding(ContentPage.TitleProperty, Binding.Create<ClienteViewModel>(cvm => cvm.Nome));

            var btToolbar = new ToolbarItem()
            {

                Text = "Pesquisar",
                Icon = "lupa",
            };
            btToolbar.Clicked += BtToolbar_Clicked;
            ToolbarItems.Add(btToolbar);
            var produtosViewModel = new ProdutosViewModel();
            pesquisaEntry = new Entry()
            {
                BindingContext = produtosViewModel
            };
            pesquisaEntry.Unfocused += PesquisaEntry_Focused;
            pesquisaEntry.SetBinding(Entry.TextProperty, Binding.Create<ProdutosViewModel>(pvm => pvm.TextoPesquisa, BindingMode.TwoWay));

            grid = new Grid() { };
            grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Star });
            grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });



            var produtoListView = new ListView()
            {
                BindingContext = produtosViewModel,
                HasUnevenRows = true

            };
            produtoListView.SetBinding(ListView.ItemsSourceProperty, Binding.Create<ProdutosViewModel>(vm => vm.ListaProdutos));
            produtoListView.SetBinding(ListView.SelectedItemProperty, Binding.Create<ProdutosViewModel>(vm => vm.ProdutoAtual,BindingMode.TwoWay));
            produtoListView.ItemTemplate = new DataTemplate(() =>
            {

                return new ViewCell()
                {
                    View = new ProdutoCell().View
                };

            });
            produtoListView.ItemSelected += ProdutoListView_ItemSelected;
            grid.Children.Add(produtoListView, 0, 1);

            var totaisGrid = new Grid();
            totaisGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Star });
            totaisGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });
            totaisGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });
            totaisGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });
            var totaisLabel = new Label() { FontAttributes = FontAttributes.Bold, FontSize = 16, Text = "Totais" };
            var itemTotalLabel = new Label() { FontAttributes = FontAttributes.Bold, FontSize = 16, Text = "Item" };
            var quantidadeTotalLabel = new Label() { FontAttributes = FontAttributes.Bold, FontSize = 16, Text = "Qtd." };
            var valorTotalLabel = new Label() { FontAttributes = FontAttributes.Bold, FontSize = 16, Text = "Valor" };
            totaisGrid.Children.Add(totaisLabel, 0, 0);
            totaisGrid.Children.Add(itemTotalLabel, 1, 0);
            totaisGrid.Children.Add(quantidadeTotalLabel, 2, 0);
            totaisGrid.Children.Add(valorTotalLabel, 3, 0);
            grid.Children.Add(totaisGrid, 0, 3);

            var cancelarButton = new Button() { BackgroundColor = Color.Purple, Text = "Cancelar", TextColor = Color.White };
            var confirmarButton = new Button() { BackgroundColor = Color.Purple, Text = "Confirmar", TextColor = Color.White };
            var buttonGrid = new Grid() { HorizontalOptions = LayoutOptions.Center };
            buttonGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });
            buttonGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });
            buttonGrid.Children.Add(cancelarButton, 0, 0);
            buttonGrid.Children.Add(confirmarButton, 1, 0);
            grid.Children.Add(buttonGrid, 0, 4);


            quantidadeGrid = new Grid() { BackgroundColor = Color.FromHex("#EED2EE") };
            quantidadeGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Star });
            quantidadeGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Star });
            var quantidadeLabel = new Label() { FontAttributes = FontAttributes.Bold, FontSize = 14 };
            quantidadeLabel.SetBinding(Label.TextProperty, Binding.Create<ProdutosViewModel>(pvm => pvm.ProdutoAtual));
            quantidadeGrid.Children.Add(quantidadeLabel, 0, 0);
            var quantidadeEntry = new Entry()
            {
                TextColor = Color.Purple,
                Keyboard = Keyboard.Numeric,
                HorizontalTextAlignment = TextAlignment.Center
            };
            quantidadeGrid.Children.Add(quantidadeEntry, 1, 0);
            grid.Children.Add(quantidadeGrid, 0, 2);


            Content = grid;
            

        }

        private void ProdutoListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e != null && e.SelectedItem != null )
            {
                //((ProdutosViewModel)BindingContext).RaisePropertyChanged<ProdutosViewModel>(vm => vm.ProdutoAtual.Descricao);
            }
        }

        private void PesquisaEntry_Focused(object sender, FocusEventArgs e)
        {

            pesquisaEntry.IsVisible = false;

        }

        private void BtToolbar_Clicked(object sender, EventArgs e)
        {
            grid.Children.Add(pesquisaEntry, 0, 0);
            pesquisaEntry.IsVisible = true;
            pesquisaEntry.Focus();
        }

        public class ProdutoCell : ViewCell
        {
            public ProdutoCell()
            {

                var descricaoClasseGrid = new Grid() { };
                descricaoClasseGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
                descricaoClasseGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
                var descricaoLabel = new Label() { FontAttributes = FontAttributes.Bold, FontSize = 14};
                descricaoLabel.SetBinding(Label.TextProperty, Binding.Create<Produto>(p => p.Descricao));
                descricaoClasseGrid.Children.Add(descricaoLabel, 0, 0);

                var classeLabel = new Label() { FontSize = 10,VerticalOptions = LayoutOptions.Start, Margin = new Thickness(0,-10,0,0)};
                classeLabel.SetBinding(Label.TextProperty, Binding.Create<Produto>(p => p.Classe));
                descricaoClasseGrid.Children.Add(classeLabel, 0, 1);




                var descricaoClasseCodigoGrid = new Grid();
                descricaoClasseCodigoGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Star });
                descricaoClasseCodigoGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Star });
                descricaoClasseCodigoGrid.Children.Add(descricaoClasseGrid, 0, 0);

                var codigoLabel = new Label() { FontAttributes = FontAttributes.Bold, FontSize = 14,HorizontalOptions = LayoutOptions.End,
                    VerticalOptions = LayoutOptions.Center
                };
                codigoLabel.SetBinding(Label.TextProperty, Binding.Create<Produto>(p => p.Codigo));
                descricaoClasseCodigoGrid.Children.Add(codigoLabel, 1, 0);

                var UMQuantidadeEstoqueValorUnitarioGrid = new Grid();
                UMQuantidadeEstoqueValorUnitarioGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Star });
                UMQuantidadeEstoqueValorUnitarioGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Star });
                UMQuantidadeEstoqueValorUnitarioGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Star });

                var UMLabel = new Label() { FontSize = 10 };
                UMLabel.SetBinding(Label.TextProperty, Binding.Create<Produto>(p => p.UnidadeMedida));
                UMQuantidadeEstoqueValorUnitarioGrid.Children.Add(UMLabel, 0, 0);
                var quantidadeEstoqueGrid = new Grid();
                quantidadeEstoqueGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Star });
                quantidadeEstoqueGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Star });
                var quantidadeEstoqueTextoLabel = new Label() { Text = "Qtd. Est.",FontSize = 10, HorizontalOptions = LayoutOptions.End };

                var quantidadeEstoqueLabel = new Label(){ FontSize = 10, HorizontalOptions = LayoutOptions.Start,Margin = new Thickness(2,0,0,0) };
                quantidadeEstoqueLabel.SetBinding(Label.TextProperty, new Binding(".", BindingMode.OneWay, new QuantidadeConverter()));
                
                quantidadeEstoqueGrid.Children.Add(quantidadeEstoqueTextoLabel,0,0);
                quantidadeEstoqueGrid.Children.Add(quantidadeEstoqueLabel, 1, 0);
                UMQuantidadeEstoqueValorUnitarioGrid.Children.Add(quantidadeEstoqueGrid, 1, 0);

                var valorUnitarioLabel = new Label() { FontAttributes = FontAttributes.Bold, FontSize = 14 ,HorizontalOptions = LayoutOptions.End};
                valorUnitarioLabel.SetBinding(Label.TextProperty, new Binding(".", BindingMode.OneWay, new ValorUnitarioConverter()));
                UMQuantidadeEstoqueValorUnitarioGrid.Children.Add(valorUnitarioLabel, 2, 0);

                var produtoGrid = new Grid();
                produtoGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Star });
                produtoGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Star });
                produtoGrid.Children.Add(descricaoClasseCodigoGrid, 0, 0);
                produtoGrid.Children.Add(UMQuantidadeEstoqueValorUnitarioGrid, 0,1);

                var produtoFrame = new Frame() { Padding = new Thickness(3,0,3,-2), Margin = new Thickness(2,2,2,2)};
                produtoFrame.Content = produtoGrid;
                View = produtoFrame;
            }
        }

        public class ValorUnitarioConverter : IValueConverter
        {


            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                var p = value as Produto;
                if (p == null)
                {
                    return null;
                }
                String valor = "R$ 0,00";
                try
                {
                    valor = System.Convert.ToString(p.ValorUnitario).Replace(".",",").Insert(0,"R$ ");
                    if (!valor.Contains(","))
                    {
                        valor += ",";
                    }
                    String[] valores =  valor.Split(',');
                    for(int i = valores[1].Length; i < 2; i++) {
                        valor += "0";
                    }
                }
                catch (Exception ex)
                {
                    valor = "R$ 0,00";
                }
                return valor;
            }

            //mode bindin toway
            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                throw new NotImplementedException();
            }
        }

        public class QuantidadeConverter : IValueConverter
        {


            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                var p = value as Produto;
                if (p == null)
                {
                    return null;
                }
                String quantidade = "0";
                try
                {
                    quantidade = System.Convert.ToString(p.quantidadeEstoque);
                    
                }
                catch (Exception ex)
                {
                    quantidade = "0";
                }
                return quantidade;
            }

            //mode bindin toway
            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                throw new NotImplementedException();
            }
        }
    }
}
