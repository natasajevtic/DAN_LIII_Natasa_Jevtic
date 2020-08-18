using System;
using System.Windows;

namespace Zadatak_1.Validations
{
    class Wrapper : DependencyObject
    {
        public static readonly DependencyProperty FirstDayProperty =
        DependencyProperty.Register("FirstDay", typeof(DateTime),
        typeof(Wrapper), new PropertyMetadata(DateTime.MinValue));

        public DateTime FirstDay
        {
            get { return (DateTime)GetValue(FirstDayProperty); }
            set { SetValue(FirstDayProperty, value); }
        }
    }
}