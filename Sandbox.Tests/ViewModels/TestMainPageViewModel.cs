using FakeItEasy;
using Microsoft.Practices.Prism.Mvvm.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sandbox.UILogic.ViewModels;

namespace Sandbox.Tests.ViewModels
{
    [TestClass]
    public class TestMainPageViewModel
    {
        private INavigationService _navigationService;
        private MainPageViewModel _viewModel;


        [TestInitialize]
        public void Initialize()
        {
            _navigationService = A.Fake<INavigationService>();
            _viewModel = new MainPageViewModel(_navigationService);
        }

        [TestMethod]
        public void PressingReactivePropertiesButtonsNavigatesToPage()
        {
            _viewModel.ShowReactivePropertiesCommand.Execute(null);

            A.CallTo(() => _navigationService.Navigate("ReactiveProperties", null)).MustHaveHappened();
        }
    }
}