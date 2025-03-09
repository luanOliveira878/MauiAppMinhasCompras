using MauiAppMinhasCompras.Helpers;

namespace MauiAppMinhasCompras
{
    public partial class App : Application
    {
        //declaração de membro privado (campo)
        static SQLiteDatabaseHelper _db;
        
        //tornando o membro publico (propriedade)
        public static SQLiteDatabaseHelper Db

        {
            // Este bloco descreve que sempre que
            // eu chamar o Db eu tenho que dar um return no campo Db

            get
            {
                //Verificando se já existe um objeto no campo Db, caso não tenha eu instancio e o objeto e retorno Db
                if (_db == null)
                {
                    //encapsulamento para garantir que seja encontrado o caminho correto e também a indicação do diretório
                    //onde o arquivo está armazenado
                    string path = Path.Combine(
                        Environment.GetFolderPath(
                            Environment.SpecialFolder.LocalApplicationData),
                        "banco_sqlite_compras.db3");

                    _db = new SQLiteDatabaseHelper(path);
                }

                return _db;
            }
        }

        public App()
        {
            InitializeComponent();

            //MainPage = new AppShell();
            MainPage = new NavigationPage(new Views.ListaProduto());
        }

    }

}