using MauiAppMinhasCompras.Models;
using System.Collections.ObjectModel;

namespace MauiAppMinhasCompras.Views;

public partial class ListaProdutoPorPeriodo : ContentPage
{
    ObservableCollection<Produto> listaFiltrada = new ObservableCollection<Produto>();

    public ListaProdutoPorPeriodo()
    {
        InitializeComponent();
        lst_produtos.ItemsSource = listaFiltrada;
        startDatePicker.Date = DateTime.Now;
        endDatePicker.Date = DateTime.Now;
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        await CarregarProdutos();
    }

    private async Task CarregarProdutos()
    {
        try
        {

            listaFiltrada.Clear();
            List<Produto> tmp = await App.Db.GetAll();
            FiltrarProdutos(tmp);
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "OK");
        }
    }

    private void FiltrarProdutos(List<Produto> produtos)
    {
        DateTime dataInicio = startDatePicker.Date;
        DateTime dataFim = endDatePicker.Date;

        var filtrados = produtos.Where(p => p.Data >= dataInicio && p.Data <= dataFim).ToList();
        listaFiltrada.Clear();
        filtrados.ForEach(i => listaFiltrada.Add(i));
    }

    private async void OnFilterClicked(object sender, EventArgs e)
    {
        await CarregarProdutos();
    }

    private async void lst_produtos_Refreshing(object sender, EventArgs e)
    {
        await CarregarProdutos();
        
    }
}
