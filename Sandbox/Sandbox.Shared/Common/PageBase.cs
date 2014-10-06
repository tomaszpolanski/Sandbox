// -----------------------------------------------------------------------
// <copyright file="PageBase.cs" company="NOKIA">
// copyright © 2014 Nokia.  All rights reserved.
// This material, including documentation and any related computer
// programs, is protected by copyright controlled by Nokia.  All
// rights are reserved.  Copying, including reproducing, storing,
// adapting or translating, any or all of this material requires the
// prior written consent of Nokia.  This material also contains
// confidential information which may not be disclosed to others
// without the prior written consent of Nokia.
// </copyright>
// -----------------------------------------------------------------------

using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using HERE.Common;
using System;
using System.Reactive.Linq;

namespace Sandbox.Common
{
    public class PageBase : Page
    {
        private IDisposable _backButtonSubscription;

        protected PageBase()
        {
            Loaded += OnLoaded;
            Unloaded += PageBase_Unloaded;

        }

        void PageBase_Unloaded(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as PageViewModel;
            if (viewModel != null)
            {
                viewModel.Dispose();
            }
            if (_backButtonSubscription != null)
            {
                _backButtonSubscription.Dispose();
            }
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as PageViewModel;
            if (viewModel != null)
            {
                viewModel.Navigator = new Navigator(Frame);
            }
#if WINDOWS_PHONE_APP
            _backButtonSubscription = Observable.FromEventPattern<Windows.Phone.UI.Input.BackPressedEventArgs>
                (h => Windows.Phone.UI.Input.HardwareButtons.BackPressed += h,
                 h => Windows.Phone.UI.Input.HardwareButtons.BackPressed -= h)
                 .Where(_ => ((PageViewModel)viewModel).Navigator.CanGoBack)
                 .Select(ev => ev.EventArgs)
                 .Subscribe(GoBack);
#endif
        }

#if WINDOWS_PHONE_APP
        void GoBack(Windows.Phone.UI.Input.BackPressedEventArgs e)
        {
            var viewModel = DataContext as PageViewModel;
            viewModel.Navigator.GoBack();
            e.Handled = true;
        }
#endif
    }
}