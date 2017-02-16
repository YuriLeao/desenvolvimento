using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using larnNaylah.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace larnNaylah.ViewModel
{
    public class ClienteViewModel : ViewModelBase
    {
        private string _nome;
        public string Nome
        {
            get { return _nome; }
            set { Set(ref _nome, value); }
        }

        private string _nomeUsual;
        public string NomeUsual
        {
            get { return _nomeUsual; }
            set { Set(ref _nomeUsual, value); }
        }

        private string _CNPJ;
        public string CNPJ
        {
            get { return _CNPJ; }
            set { Set(ref _CNPJ, value); }
        }

        private ContentPage _page;
        public ContentPage Page
        {
            get { return _page; }
            set { Set(ref _page, value); }
        }

        public ClienteViewModel(ContentPage page)
        {
            this.Page = page;
            Nome = "Yuri Leão";
        }

        public RelayCommand<String> BuscarCliente => new RelayCommand<String>((s) =>
        {
            try
            {
                if(!String.IsNullOrEmpty(Nome))
                {
                    Page.Navigation.PushAsync(new PedidoView(this));
                }
                
            }
            catch (Exception)
            {
            }

        });

    }
}
