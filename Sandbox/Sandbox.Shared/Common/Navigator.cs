// -----------------------------------------------------------------------
// <copyright file="Navigator.cs" company="NOKIA">
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

using System;
using Windows.UI.Xaml.Controls;
using HERE.Common;

namespace Sandbox.Common
{
    public class Navigator : INavigator
    {
        private readonly Frame _frame;

        public bool CanGoBack
        {
            get { return _frame.CanGoBack; }
        }

        public string CurrentSourcePageType
        {
            get { return _frame.CurrentSourcePageType.ToString(); }
        }

        public Navigator(Frame frame)
        {
            _frame = frame;

        }

        public event EventHandler<bool> CanGoBackChanged;

        public void GoBack()
        {
            _frame.GoBack();
        }

        public void Navigate(string sourcePage, object parameter)
        {
            _frame.Navigate(Type.GetType(sourcePage), parameter);
        }

        public void Navigate(string sourcePage)
        {
            Navigate(sourcePage, null);
        }

        public void Navigate(Uri uri)
        {
            throw new NotImplementedException();
        }
    }
}