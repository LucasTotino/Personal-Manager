using Gerenciador_Pessoal.Models;
using System.Text.RegularExpressions;

namespace Gerenciador_Pessoal.Pages;

[QueryProperty(nameof(Transacao), "Transacao")]
public partial class NovaTransacao : ContentPage
{
    public Transacao Transacao { get; set; }

    public NovaTransacao()
	{
		InitializeComponent();
	}

    private void LimparCampos()
    {
        txtDescricao.Text = string.Empty;
        txtValor.Text = string.Empty;
        swReceita.IsToggled = false;
        dtpData.Date = DateTime.Now;
    }
    

    protected override async void OnAppearing() 
    {
        base.OnAppearing();

        if (Transacao != null)
        {
            // edição
            txtDescricao.Text = Transacao.Descricao;
            txtValor.Text = Transacao.Valor.ToString("F2");
            swReceita.IsToggled = Transacao.IsReceita;
            dtpData.Date = Transacao.Data;
        }
        else
        {
            // nova transação
            LimparCampos();
        }
    }

    private async void BtnSalvar_Clicked(object sender, EventArgs e)
    {
        try
        {
            // validações simples
            if (string.IsNullOrWhiteSpace(txtDescricao.Text))
            {
                await DisplayAlert("Erro", "Digite uma descrição.", "OK");
                return;
            }

            string valorText = txtValor.Text.Replace("R$", "").Trim();

            if (!decimal.TryParse(valorText, out decimal valor))
            {
                await DisplayAlert("Erro", "Digite um valor válido.", "OK");
                return;
            }

            bool edicao = Transacao != null && Transacao.Id > 0;

            // cria a transação
            var transacao = new Transacao
            {
                Id = Transacao?.Id ?? 0,
                Descricao = txtDescricao.Text,
                Valor = valor,
                IsReceita = swReceita.IsToggled,
                Data = dtpData.Date
            };

            // salva no banco
            await App.Database.SaveTransacaoAsync(transacao);

            await DisplayAlert("Sucesso", "Transação salva!", "OK");

            Transacao = null;

            // volta para tela anterior
            if (!edicao) {
                await Shell.Current.GoToAsync("//MainPage");
            }
            else
            {
                await Shell.Current.GoToAsync("..");
            };

                LimparCampos();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", $"Ocorreu um erro: {ex.Message}", "OK");
        }
    }

}