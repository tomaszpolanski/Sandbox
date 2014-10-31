using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Sandbox.Commands
{
    public class SelectionCommand
    {
        [SuppressMessage("Microsoft.Security",
            "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes")]
        public static readonly DependencyProperty
            CommandProperty =
                DependencyProperty.RegisterAttached("Command",
                    typeof(ICommand),
                    typeof(SelectionCommand),
                    new PropertyMetadata(null, CommandPropertyChanged));

        public static void SetCommand(DependencyObject attached, ICommand value)
        {
            attached.SetValue(CommandProperty, value);
        }

        public static ICommand GetCommand(DependencyObject attached)
        {
            return (attached.GetValue(CommandProperty) as ICommand);
        }

        private static void CommandPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            // first check if the component is a list
            if (obj is ListViewBase)
            {
                var list = obj as ListViewBase;
                list.SelectionChanged += (sender, e) => ExecuteCommand(obj, e.AddedItems);
            }

        }

        private static void ExecuteCommand(DependencyObject attached, object argument)
        {
            ICommand command = GetCommand(attached);
            if (command != null && command.CanExecute(argument))
            {
                command.Execute(argument);
            }
        }
    }
}
