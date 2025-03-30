using MauiAppMinhasCompras.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace MauiAppMinhasCompras.Views;

public partial class ListaProduto : ContentPage
{
	//usada para atualizar automaticamente a interface gráfica ao mudar a coleção, no caso aqui, da lista de produtos.
	ObservableCollection<Produto> lista = new ObservableCollection<Produto>();

	public ListaProduto()
	{
		InitializeComponent();

		lst_produtos.ItemsSource = lista;
	}

    protected async override void OnAppearing()
    {
		//utilização do OnAppering para abrir a lista de produtos todas as vezes que esta tela for aberta
		try
		{
			lista.Clear();
			List<Produto> tmp = await App.Db.GetAll();

			tmp.ForEach(i => lista.Add(i));
		}
		catch (Exception ex)
		{
			await DisplayAlert("Ops", ex.Message, "OK");
		}
		
    }

	// este bloco faz a navegação para a tela de adição de um novo prodto
    private void ToolbarItem_Clicked(object sender, EventArgs e)
    {
		try
		{
			Navigation.PushAsync(new Views.NovoProduto());

		}catch(Exception ex)
		{
			DisplayAlert("Ops", ex.Message, "OK");
		}
    }

	// bloco para executar a busca de itens pela barra na parte superior da aplicação
	// existe um foreach para trazer os dados em forma de tabela
    private async void txt_search_TextChanged(object sender, TextChangedEventArgs e)
    {

		try
		{
			string q = e.NewTextValue;

            lst_produtos.IsRefreshing = true;

            lista.Clear();

			List<Produto> tmp = await App.Db.Search(q);

			tmp.ForEach(i => lista.Add(i));
		}
		catch (Exception ex)

		{
			DisplayAlert("Ops", ex.Message, "OK");
		}
        finally
        {
            lst_produtos.IsRefreshing = false;
        }

    }
		
	// código para efeutar a soma através do evento do botão somar
    private void ToolbarItem_Clicked_1(object sender, EventArgs e)
    {
		double soma = lista.Sum(i => i.Total);

		string msg = $"O total é {soma:C}";

		DisplayAlert("Total dos Produtos", msg, "OK");
    }

	//bloco que utiliza o sender e o menu de contexto para que ao clicar com o botão direto, a opçao de remover
	//seja apresentada
    private async void MenuItem_Clicked(object sender, EventArgs e)
    {
        try
        {
			MenuItem selecionado = sender as MenuItem;

			Produto p = selecionado.BindingContext as Produto;

			bool confirm = await DisplayAlert(
				"Tem certeza?", $"Remover {p.Descricao}?", "Sim", "Não");

			if (confirm)
			{
				await App.Db.Delete(p.Id);
				lista.Remove(p); // obeservablecollection
			}
        }
        catch (Exception ex)

        {
           await DisplayAlert("Ops", ex.Message, "OK");
        }

    }

	//Bloco que navega para a página de edição do produto ao clicar 2x no item
    private void lst_produtos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
		try 
		{
			Produto p = e.SelectedItem as Produto;

			Navigation.PushAsync(new Views.EditarProduto
			{
				BindingContext = p,
			});
		}
		catch(Exception ex)
		{ 

		}
    }

    private async void lst_produtos_Refreshing(object sender, EventArgs e)
    {
		try
		{
			lista.Clear();
			List<Produto> tmp = await App.Db.GetAll();

			tmp.ForEach(i => lista.Add(i));
		}
		catch (Exception ex)
		{
			await DisplayAlert("Ops", ex.Message, "OK");
		}
		finally
		{
			lst_produtos.IsRefreshing = false;
		}
    }
}

