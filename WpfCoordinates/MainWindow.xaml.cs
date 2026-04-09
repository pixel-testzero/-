using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WpfCoordinates
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private Point ConvertLocalToGlobal(UIElement element, Point localPoint)
        {
            return element.TransformToAncestor(Window.GetWindow(element))
                          .Transform(localPoint);
        }

        private Point ConvertGlobalToLocal(UIElement element, Point globalPoint)
        {
            return Window.GetWindow(element)
                         .TransformToDescendant(element)
                         .Transform(globalPoint);
        }

        private void ShowCoordinates_Click(object sender, RoutedEventArgs e)
        {
            Point localPoint = new Point(
                MyEllipse.ActualWidth / 2,
                MyEllipse.ActualHeight / 2);

            Point globalPoint = ConvertLocalToGlobal(MyEllipse, localPoint);

            Point backToLocal = ConvertGlobalToLocal(MyEllipse, globalPoint);

            LocalCoordsText.Text =
                $"Локальные координаты (центр Ellipse): ({localPoint.X:F1}, {localPoint.Y:F1})";

            GlobalCoordsText.Text =
                $"Глобальные координаты (в окне):         ({globalPoint.X:F1}, {globalPoint.Y:F1})";

            BackCoordsText.Text =
                $"Обратное преобразование (глобал → локал): ({backToLocal.X:F1}, {backToLocal.Y:F1})";
        }

        private void Element_Click(object sender, RoutedEventArgs e)
        {
            if (sender is UIElement element)
            {
                Point mouseLocal = Mouse.GetPosition(element);
                Point mouseGlobal = ConvertLocalToGlobal(element, mouseLocal);

                string name = (element as FrameworkElement)?.Name ?? "Element";
                ClickedElementText.Text =
                    $"Последний клик по {name}: " +
                    $"локал ({mouseLocal.X:F1}, {mouseLocal.Y:F1}), " +
                    $"глобал ({mouseGlobal.X:F1}, {mouseGlobal.Y:F1})";
            }
        }
    }
}