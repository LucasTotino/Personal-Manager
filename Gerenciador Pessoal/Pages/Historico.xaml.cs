using Gerenciador_Pessoal.Models;

namespace Gerenciador_Pessoal.Pages;

public partial class Historico : ContentPage
{
	public Historico()
	{
		InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await CarregarHistorico();
    }
    private async Task CarregarHistorico()
    {
        var transacoes = await App.Database.GetTransacoesAsync();

        var historico = transacoes
            .OrderByDescending(t => t.Data)
            .ThenByDescending(t => t.Id)
            .ToList();

        lstHistorico.ItemsSource = historico;
    }

    private async void OnEditarClicked(object sender, EventArgs e)
    {
        var swipeItem = (SwipeItem)sender;
        var transacao = (Transacao)swipeItem.BindingContext;

        var parametros = new Dictionary<string, object>
    {
        { "Transacao", transacao }
    };

        await Shell.Current.GoToAsync(nameof(NovaTransacao), parametros);
    }

    private async void OnExcluirClicked(object sender, EventArgs e)
    {
        var swipeItem = (SwipeItem)sender;
        var transacao = (Transacao)swipeItem.BindingContext;

        var confirm = await DisplayAlert("Excluir",
            $"Deseja realmente excluir '{transacao.Descricao}'?",
            "Sim", "Não");

        if (confirm)
        {
            await App.Database.DeleteTransacaoAsync(transacao);
            await CarregarHistorico(); // método que você já deve ter para recarregar a lista
        }
    }
}