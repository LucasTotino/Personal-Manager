using CommunityToolkit.Maui;
using Gerenciador_Pessoal.Pages;
using System.Globalization;

namespace Gerenciador_Pessoal
{
    public partial class MainPage : ContentPage
    {
        

        public MainPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await AtualizarSaldo();
            await CarregarUltimasTransacoes();
        }

        private async Task AtualizarSaldo()
        {
            var transacoes = await App.Database.GetTransacoesAsync();

            decimal receitas = transacoes
                .Where(t => t.IsReceita)
                .Sum(t => t.Valor);

            decimal despesas = transacoes
                .Where(t => !t.IsReceita)
                .Sum(t => t.Valor);

            decimal saldo = receitas - despesas;

            lblSaldo.Text = $"R$ {saldo:N2}";
        }

        private async Task CarregarUltimasTransacoes()
        {
            var transacoes = await App.Database.GetTransacoesAsync();

            var ultimas = transacoes
                .OrderByDescending(t => t.Data)
                .ThenByDescending(t => t.Id)
                .Take(5)
                .ToList();

            lstUltimas.ItemsSource = ultimas;
        }
    }

}
