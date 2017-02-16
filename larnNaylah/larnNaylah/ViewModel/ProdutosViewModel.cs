using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace larnNaylah.ViewModel
{
    public class ProdutosViewModel : ViewModelBase
    {
        private ObservableCollection<Produto> _listaProdutos;

        private string _textoPesquisa;

        private int _quantidadeTotal;

        private Double _valorTotal;

        private int _quantidadeItemTotal;

        private Produto _produtoAtual;

        public ProdutosViewModel()
        {
            ListaProdutos = new ObservableCollection<Produto>();

            Search();

            PropertyChanged += ProdutosViewModel_PropertyChanged;
        }

        public Produto ProdutoAtual
        {
            get { return _produtoAtual; }
            set { Set(ref _produtoAtual, value); }
        }

        public ObservableCollection<Produto> ListaProdutos
        {
            get { return _listaProdutos; }
            set { Set(ref _listaProdutos, value); }
        }

        public string TextoPesquisa
        {
            get { return _textoPesquisa; }
            set { Set(ref _textoPesquisa, value); }
        }

        public int QuantidadeTotal
        {
            get { return _quantidadeTotal; }
            set { Set(ref _quantidadeTotal, value); }
        }

        public Double ValorTotal
        {
            get { return _valorTotal; }
            set { Set(ref _valorTotal, value); }
        }

        public int QuantidadeItemTotal
        {
            get { return _quantidadeItemTotal; }
            set { Set(ref _quantidadeItemTotal, value); }
        }

        public void Search()
        {
            var p = GetProdutos();

            if (!string.IsNullOrEmpty(TextoPesquisa))
            {
                p = p
                    .Where(x =>
                        x.Descricao.ToUpper().Contains(TextoPesquisa.ToUpper()) ||
                        x.Classe.ToUpper().Contains(TextoPesquisa.ToUpper()) ||
                        x.Codigo.Contains(TextoPesquisa)
                        ).ToList();
            }

            ListaProdutos = new ObservableCollection<Produto>(p);
        }

        public IList<Produto> GetProdutos()
        {
            var listOfPeople = new List<Produto>();
            listOfPeople.Add(new Produto() { Codigo = "001", Descricao = "Veludo", ValorUnitario = 20.29, quantidadeEstoque = 3, UnidadeMedida = "M", Classe = "Raro" });
            listOfPeople.Add(new Produto() { Codigo = "002", Descricao = "Algodão", ValorUnitario = 15.00, quantidadeEstoque = -9, UnidadeMedida = "M", Classe = "Comum" });
            listOfPeople.Add(new Produto() { Codigo = "003", Descricao = "Couro", ValorUnitario = 69.90, quantidadeEstoque = 19, UnidadeMedida = "M", Classe = "Raro" });

            return listOfPeople;
        }

        private void ProdutosViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(TextoPesquisa))
            {
                Search();
            }

        }
    }

    public class Produto
    {


        public string Codigo { get; set; }
        public string Descricao { get; set; }
        public Double ValorUnitario { get; set; }
        public int quantidadeEstoque { get; set; }
        public string UnidadeMedida { get; set; }
        public string Classe { get; set; }
    }

}


