namespace Dolby.UAP.Controls
{
    using System;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Media;
    using Windows.UI.Xaml.Media.Animation;

    public sealed partial class ProgressCircle : UserControl
    {
        public ProgressCircle()
        {
            this.InitializeComponent();
        }

        #region Dependency Properties

        public Brush FontBrush
        {
            get { return (Brush)GetValue(FontBrushProperty); }
            set
            {
                lblValue.Foreground = value;
                SetValue(FontBrushProperty, value);
            }
        }

        // Using a DependencyProperty as the backing store for FontBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FontBrushProperty =
            DependencyProperty.Register("FontBrush", typeof(Brush), typeof(ProgressCircle), null);



        public Brush Fill
        {
            get { return (Brush)GetValue(FillProperty); }
            set
            {
                Ring.Fill = value;
                SetValue(FillProperty, value);
            }
        }

        // Using a DependencyProperty as the backing store for BgBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FillProperty =
            DependencyProperty.Register("FillBrush", typeof(Brush), typeof(ProgressCircle), null);

        public Brush FillBackground
        {
            get { return (Brush)GetValue(FillBackgroundProperty); }
            set
            {
                BgFill.Fill = value;
                SetValue(FillBackgroundProperty, value);
            }
        }

        // Using a DependencyProperty as the backing store for BgBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FillBackgroundProperty =
            DependencyProperty.Register("FillBackgroundBrush", typeof(Brush), typeof(ProgressCircle), null);


        public double FontSize
        {
            get { return (double)GetValue(FontSizeProperty); }
            set
            {
                //lblValue.FontSize = value;
                SetValue(FontSizeProperty, value);
            }
        }

        // Using a DependencyProperty as the backing store for FontSize.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FontSizeProperty =
            DependencyProperty.Register("FontSize", typeof(double), typeof(ProgressCircle), new PropertyMetadata(42));

        public bool Animate
        {
            get { return (bool)GetValue(AnimateProperty); }
            set
            {
                //lblValue.FontSize = value;
                SetValue(AnimateProperty, value);
            }
        }

        // Using a DependencyProperty as the backing store for FontSize.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AnimateProperty =
            DependencyProperty.Register("Animate", typeof(bool), typeof(ProgressCircle), null);

        public TimeSpan Duration
        {
            get { return (TimeSpan)GetValue(DurationProperty); }
            set { SetValue(DurationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FontSize.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DurationProperty =
            DependencyProperty.Register("Duration", typeof(TimeSpan), typeof(ProgressCircle), new PropertyMetadata(new TimeSpan(0, 0, 1)));

        // Using a DependencyProperty as the backing store for Value.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(int), typeof(ProgressCircle), new PropertyMetadata(0,
                    OnValueChanged));


        public int Value
        {
            get
            {
                try
                {
                    return (int)GetValue(ValueProperty);
                }
                catch (Exception)
                {
                    return 0;
                }

            }
            set
            {
                //SetValue(ValueProperty, value);
            }
        }

        private static void OnValueChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var target = (ProgressCircle)sender;
            var oldValue = (int)e.OldValue;
            var newValue = (int)e.NewValue;


            var newGrad = percentToGrad(newValue);
            var oldGrad = percentToGrad(oldValue);

            if (newGrad >= 360)
            {
                newGrad = 359.9999;
            }

            if (target.Animate)
            {
                var storyboard = new Storyboard();

                var duration = target.Duration;

                var ringAnimation = new DoubleAnimation();
                ringAnimation.Duration = duration;
                ringAnimation.From = oldGrad;
                ringAnimation.To = newGrad;
                ringAnimation.EnableDependentAnimation = true;
                ringAnimation.EasingFunction = new ExponentialEase()
                {
                    EasingMode = EasingMode.EaseIn,
                    Exponent = -5
                };

                Storyboard.SetTarget(ringAnimation, target.Ring);
                Storyboard.SetTargetProperty(ringAnimation, "EndAngle");

                storyboard.Children.Add(ringAnimation);

                storyboard.Begin();
            }
            else
            {
                target.Ring.EndAngle = newGrad;
            }

            target.lblValue.Text = newValue.ToString();
        }
        #endregion

        private static double percentToGrad(int value)
        {
            double grad = 0;
            if (value > 0)
            {
                grad = (value * 360) / 100;
            }

            return grad;
        }
        private void ProgressCircle_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Value = Value;
        }
    }


}
