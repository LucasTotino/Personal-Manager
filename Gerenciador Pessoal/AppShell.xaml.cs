using Gerenciador_Pessoal.Pages;

namespace Gerenciador_Pessoal
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            CurrentItem = this.Items[0].Items[1];

            Routing.RegisterRoute(nameof(NovaTransacao), typeof(NovaTransacao));
        }
    }

}

