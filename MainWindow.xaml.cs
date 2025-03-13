using System;
using System.Threading;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DeftSharp.Windows.Input.Keyboard;
using System.Diagnostics;

namespace test2;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    bool windowDown = true;

    public MainWindow()
    {
        InitializeComponent();

        var keyboardListener = new KeyboardListener();

        keyboardListener.SubscribeCombination([Key.LeftShift, Key.W], () => Trace.WriteLine($"The Shift + W was pressed"));

        double screenWidth = SystemParameters.PrimaryScreenWidth;
        double screenHeight = SystemParameters.PrimaryScreenHeight;

        double windowWidth = this.Width;
        double windowHeight = this.Height;

        this.Left = (screenWidth - windowWidth) / 2;
        this.Top = (screenHeight - windowHeight) - 800; // top ending position is - 600    
    }

    private void StartEnterAnimation()
    {
        double screenHeight = SystemParameters.PrimaryScreenHeight;
        double windowHeight = this.Height;
        this.Top = (screenHeight - windowHeight) - 800; // Start position

        DoubleAnimation anim = new DoubleAnimation
        {
            From = (screenHeight - windowHeight) - 800,
            To = (screenHeight - windowHeight) - 600,
            Duration = TimeSpan.FromSeconds(0.5),
            EasingFunction = new SineEase { EasingMode = EasingMode.EaseInOut }
        };

        this.BeginAnimation(Window.TopProperty, anim);

    }

    private void StartExitAnimation()
    {
        double screenHeight = SystemParameters.PrimaryScreenHeight;
        double windowHeight = this.Height;
        this.Top = (screenHeight - windowHeight) - 600; // Start position

        DoubleAnimation anim = new DoubleAnimation
        {
            From = (screenHeight - windowHeight) - 600,
            To = (screenHeight - windowHeight) - 800,
            Duration = TimeSpan.FromSeconds(0.5),
            EasingFunction = new SineEase { EasingMode = EasingMode.EaseInOut }
        };

        this.BeginAnimation(Window.TopProperty, anim);
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        this.Focus();
    }

    private void Window_KeyDown_1(object sender, KeyEventArgs e)
    {
        if (Keyboard.IsKeyDown(Key.LeftCtrl))
        {
            if (e.Key == Key.O && windowDown)
            {
                windowDown = false;
                StartEnterAnimation();
            }
            else if (e.Key == Key.O && !windowDown)
            {
                windowDown = true;
                StartExitAnimation();
            }
        }
    }
}