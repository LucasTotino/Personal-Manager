using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gerenciador_Pessoal.Behaviors
{
    public class MascaraValores:Behavior<Entry>
    {
        protected override void OnAttachedTo(Entry entry)
        {
            entry.TextChanged += OnTextChanged;
            base.OnAttachedTo(entry);
        }

        protected override void OnDetachingFrom(Entry entry)
        {
            entry.TextChanged -= OnTextChanged;
            base.OnDetachingFrom(entry);
        }

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is not Entry entry) return;

            if (string.IsNullOrWhiteSpace(e.NewTextValue)) return;

            var digits = new string(e.NewTextValue.Where(char.IsDigit).ToArray());

            if (string.IsNullOrEmpty(digits))
            {
                entry.Text = string.Empty;
                return;
            }

            if (decimal.TryParse(digits, out decimal number))
            {
                number /= 100;

                entry.TextChanged -= OnTextChanged;
                entry.Text = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", number);
                entry.TextChanged += OnTextChanged;

                entry.CursorPosition = entry.Text.Length;
            }
        }
    }
}
