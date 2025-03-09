using MauiAppMinhasCompras.Models;
using System.Threading.Tasks;

namespace MauiAppMinhasCompras.Views;

public partial class NovoProduto : ContentPage
{
	public NovoProduto()
	{
		InitializeComponent();
	}

    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
		try
		{
			//realizando o preenchimento da Model de Produto
			Produto p = new Produto
			{
				Descricao = txt_descricao.Text,
				Quantidade = Convert.ToDouble(txt_quantidade.Text),
				Preco = Convert.ToDouble(txt_preco.Text)
			};
			// realizando de forma assíncrona o método insert na Db
			await App.Db.Insert(p);
			//Avisando o usuário que o registro foi inserido
			await DisplayAlert("Sucesso!", "Registro Inserido", "OK");
		}
		catch (Exception ex)
		{
			await DisplayAlert("Ops", ex.Message, "OK");
		}
    }
}