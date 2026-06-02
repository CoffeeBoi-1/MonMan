using TelegramBotBase.Args;
using TelegramBotBase.Base;
using TelegramBotBase.Controls.Hybrid;
using TelegramBotBase.DependencyInjection;
using TelegramBotBase.Enums;
using TelegramBotBase.Form;

namespace BotInterface.Forms
{
    internal class ReviewAccountsForm : FormBase
    {
        private readonly ApiClient _api;
        private ButtonGrid _buttons = null!;

        public ReviewAccountsForm(ApiClient api)
        {
            _api = api;
            //DeleteMode = EDeleteMode.OnLeavingForm;
            Init += OnInit;
        }

        private async Task OnInit(object sender, InitEventArgs e)
        {
            _buttons = new ButtonGrid
            {
                KeyboardType = EKeyboardType.InlineKeyBoard
            };

            _buttons.ButtonClicked += OnButtonClicked;
            AddControl(_buttons);

            await LoadAccounts();
        }

        private async Task LoadAccounts()
        {
            var accounts = await _api.GetAccountsAsync();

            var bf = new ButtonForm();

            if (accounts == null)
            {
                _buttons.Title = "Review Accounts\n\nCould not reach server.";
            }
            else if (accounts.Count == 0)
            {
                _buttons.Title = "Review Accounts\n\nNo accounts found.";
            }
            else
            {
                var summary = string.Join("\n", accounts.Select(a => $"• {a.Name}"));
                _buttons.Title = $"Review Accounts\n\n{summary}";
            }

            bf.AddButtonRow(new ButtonBase("🔄 Refresh", "refresh"));
            bf.AddButtonRow(new ButtonBase("⬅️ Back", "back"));
            _buttons.DataSource.ButtonForm = bf;

            await _buttons.Render(null);
        }

        private async Task OnButtonClicked(object sender, ButtonClickedEventArgs e)
        {
            if (e.Button == null) return;

            switch (e.Button.Value)
            {
                case "back":
                    var new_form = await this.NavigateTo<MainMenuForm>();

                    if (new_form == null)
                    {
                        await Device.Send("Cant open ConfirmationForm");
                    }

                    break;

                case "refresh":
                    await LoadAccounts();
                    break;
            }
        }

    }
}
