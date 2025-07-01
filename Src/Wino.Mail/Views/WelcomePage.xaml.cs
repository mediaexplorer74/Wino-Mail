//using CommunityToolkit.Labs.WinUI.MarkdownTextBlock;
using Wino.Views.Abstract;

namespace Wino.Views
{
    public sealed partial class WelcomePage : WelcomePageAbstract
    {
        // TEMP class created
        private readonly MarkdownConfig _config;

        public WelcomePage()
        {
            InitializeComponent();

            _config = new MarkdownConfig();
        }
    }
}
