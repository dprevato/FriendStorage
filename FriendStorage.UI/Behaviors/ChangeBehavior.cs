using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;

namespace FriendStorage.UI.Behaviors
{
    public static class ChangeBehavior
    {
        private static readonly Dictionary<Type, DependencyProperty> _defaultProperties;

        static ChangeBehavior()
        {
            _defaultProperties = new Dictionary<Type, DependencyProperty>
            {
                [typeof(TextBox)] = TextBox.TextProperty,
                [typeof(CheckBox)] = ToggleButton.IsCheckedProperty,
                [typeof(DatePicker)] = DatePicker.SelectedDateProperty
            };
        }

        #region IsChanged attached property

        public static readonly DependencyProperty IsChangedProperty = DependencyProperty.RegisterAttached("IsChanged",
                                                                                                  typeof(bool),
                                                                                                  typeof(ChangeBehavior),
                                                                                                  new PropertyMetadata(default(bool)));

        public static bool GetIsChanged(DependencyObject obj) => (bool)obj.GetValue(IsChangedProperty);

        public static void SetIsChanged(DependencyObject obj, bool value) => obj.SetValue(IsChangedProperty, value);

        #endregion IsChanged

        #region OriginalValue attached property

        public static readonly DependencyProperty OriginalValueProperty = DependencyProperty.RegisterAttached("OriginalValue",
                                                                                                  typeof(object),
                                                                                                  typeof(ChangeBehavior),
                                                                                                  new PropertyMetadata(default(object)));

        public static object GetOriginalValue(DependencyObject obj) => (object)obj.GetValue(OriginalValueProperty);

        public static void SetOriginalValue(DependencyObject obj, object value) => obj.SetValue(OriginalValueProperty, value);

        #endregion OriginalValue


        #region IsActive attached property

        public static readonly DependencyProperty IsActiveProperty = DependencyProperty.RegisterAttached("IsActive",
                                                                                                  typeof(bool),
                                                                                                  typeof(ChangeBehavior),
                                                                                                  new PropertyMetadata(default(bool), OnIsActivePropertyChanged));

        private static void OnIsActivePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (_defaultProperties.ContainsKey(d.GetType()))
            {
                var defaultProperty = _defaultProperties[d.GetType()];
                if ((bool)e.NewValue)
                {
                    var binding = BindingOperations.GetBinding(d, defaultProperty);
                    if (binding != null)
                    {
                        var bindingPath = binding.Path.Path;
                        BindingOperations.SetBinding(d, IsChangedProperty, new Binding($"{bindingPath}IsChanged"));
                        CreateOriginalValueBinding(d, $"{bindingPath}OriginalValue");
                    }
                }
                else
                {
                    BindingOperations.ClearBinding(d, IsChangedProperty);
                    BindingOperations.ClearBinding(d, OriginalValueProperty);
                }
            }
        }

        public static bool GetIsActive(DependencyObject obj) => (bool)obj.GetValue(IsActiveProperty);

        public static void SetIsActive(DependencyObject obj, bool value) => obj.SetValue(IsActiveProperty, value);

        #endregion IsActive

        #region OriginalValueConverter attached property

        public static readonly DependencyProperty OriginalValueConverterProperty = DependencyProperty.RegisterAttached("OriginalValueConverter",
                                                                                                  typeof(IValueConverter),
                                                                                                  typeof(ChangeBehavior),
                                                                                                  new PropertyMetadata(default(IValueConverter), OnOriginalValueConverterPropertyChanged));

        private static void OnOriginalValueConverterPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var originalValueBinding = BindingOperations.GetBinding(d, OriginalValueProperty) as Binding;
            if (originalValueBinding != null)
            {
                CreateOriginalValueBinding(d, originalValueBinding.Path.Path);
            }
        }

        private static void CreateOriginalValueBinding(DependencyObject d, string originalValueBindingPath)
        {
            var newBinding = new Binding(originalValueBindingPath)
            {
                Converter = GetOriginalValueConverter(d)
            };
            BindingOperations.SetBinding(d, OriginalValueProperty, newBinding);
        }

        public static IValueConverter GetOriginalValueConverter(DependencyObject obj) => (IValueConverter)obj.GetValue(OriginalValueConverterProperty);

        public static void SetOriginalValueConverter(DependencyObject obj, IValueConverter value) => obj.SetValue(OriginalValueConverterProperty, value);

        #endregion OriginalValueConverter

    }
}
