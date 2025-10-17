using Gerenciador_Pessoal.Data;
using System.Globalization;

namespace Gerenciador_Pessoal
{
    public partial class App : Application
    {
        static BancoDados database;

        public static BancoDados Database
        {
            get
            {
                if (database == null)
                {
                    string dbPath = Path.Combine(FileSystem.AppDataDirectory, "financas.db3");
                    database = new BancoDados(dbPath);
                }
                return database;
            }
        }

        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }
    }
}
