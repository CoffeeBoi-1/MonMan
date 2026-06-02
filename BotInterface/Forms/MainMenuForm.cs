using TelegramBotBase.Args;
using TelegramBotBase.Base;
using TelegramBotBase.Controls.Hybrid;
using TelegramBotBase.DependencyInjection;
using TelegramBotBase.Enums;
using TelegramBotBase.Form;

namespace BotInterface.Forms
{
    public class MainMenuForm : FormBase
    {
        private ButtonGrid? _buttons;

        public MainMenuForm()
        {
            //DeleteMode = EDeleteMode.OnLeavingForm;
            Init += OnInit;
        }

        private Task OnInit(object sender, InitEventArgs e)
        {
            _buttons = new ButtonGrid
            {
                KeyboardType = EKeyboardType.InlineKeyBoard,
                Title = "Menu"
            };

            var bf = new ButtonForm();
            bf.AddButtonRow(new ButtonBase("💸 Add Transaction", "add_transaction"));
            bf.AddButtonRow(new ButtonBase("📊 Review Accounts", "review_accounts"));

            _buttons.DataSource.ButtonForm = bf;
            _buttons.ButtonClicked += OnButtonClicked;

            AddControl(_buttons);
            return Task.CompletedTask;
        }

        private async Task OnButtonClicked(object sender, ButtonClickedEventArgs e)
        {
            if (e.Button == null) return;

            switch (e.Button.Value)
            {
                /*case "add_transaction":
                    await NavigateTo(new AddTransactionForm());
                    break;*/

                case "review_accounts":
                    var new_form = await this.NavigateTo<ReviewAccountsForm>();

                    if (new_form == null)
                    {
                        await Device.Send("Cant open ConfirmationForm");
                    }

                    break;
            }
        }
    }
}
