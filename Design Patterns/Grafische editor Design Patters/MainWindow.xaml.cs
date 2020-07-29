using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Design_Patters_Jaar2
{
    public partial class MainWindow : Window
    {
        /**
         * Enum - Shapes for different figures
         */
        private enum MyShape
        {
            Line, Ellipse, Rectangle, SelectBox, Move, Resize, Group, DeGroup
        }
        private MyShape currShape = MyShape.SelectBox;

        // Start / End Point for shape
        static Point Start;
        static Point End;

        public static List<Figure> Fig_All = new List<Figure>();
        public static List<Figure> FigsSel = new List<Figure>();
        private IDec decorator = new OrnamentDecTop();

        // Invoker / Visitor Pattern
        private Invoker Invoke_Pat = new Invoker();
        private Visitor Vis_Pat;
        
        // Default OrnamentLocation
        private string OrnamentLocation = "Top";

        public MainWindow()
        {
            InitializeComponent();
            ResetCanvas();
            Vis_Pat = new Visitor(ref Fig_All, ref FigsSel, Start, End, ref DepPat);
        }
        
        /**
         * Btn Click Events for different buttons
         */
        private void BtnEllipse_Click(object sender, RoutedEventArgs e)
        {
            currShape = MyShape.Ellipse;
        }
        private void BtnRectangle_Click(object sender, RoutedEventArgs e)
        {
            currShape = MyShape.Rectangle;
        }
        private void BtnSelect_Click(object sender, RoutedEventArgs e)
        {
            currShape = MyShape.SelectBox;
        }
        private void BtnMove_Click(object sender, RoutedEventArgs e)
        {
            currShape = MyShape.Move;
        }
        private void BtnResize_Click(object sender, RoutedEventArgs e)
        {
            currShape = MyShape.Resize;
        }
        private void BtnGroup_Click(object sender, RoutedEventArgs e)
        {
            currShape = MyShape.Group;
        }
        private void BtnUngroup_Click(object sender, RoutedEventArgs e)
        {
            currShape = MyShape.DeGroup;
        }

        /*
         * Make box for ornament visible
         */
        private void BtnOrdanment_Click(object sender, RoutedEventArgs e)
        {
            OrnamentTextBox.Visibility = Visibility.Visible;
            OrnamentLocationBox.Visibility = Visibility.Visible;
            OrnamentTextBox.Focus();
        }

        /**
         * Selection of location for ornament in relation to the selected figures
         */
        private void OrnamentTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                switch (OrnamentLocation)
                {
                    case "Top":
                        decorator = new OrnamentDecTop();
                        break;

                    case "Bot":
                        decorator = new OrnamentDecBot();
                        break;

                    case "Left":
                        decorator = new OrnamentDecLeft();
                        break;

                    case "Right":
                        decorator = new OrnamentDecRight();
                        break;
                    default:
                        break;
                }

                Invoke_Pat.AddOrnament(ref FigsSel, OrnamentTextBox.Text, decorator);
                Invoke_Pat.ExecuteCommands();
                OrnamentTextBox.Visibility = Visibility.Hidden;
                OrnamentLocationBox.Visibility = Visibility.Hidden;
            }
        }
        private void RadioButtonChecked(object sender, RoutedEventArgs e)
        {
            if (!(sender is RadioButton radioButton))
                return;
            OrnamentLocation = radioButton.Content.ToString();
        }
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            Fig_All[0].Accept(new Save(Fig_All));

        }
        private void BtnLoad_Click(object sender, RoutedEventArgs e)
        {
            ResetCanvas();
            Figure tempfiguur = new Figure(new Rectangle(), "Temp", DepPat);
            tempfiguur.Accept(new Load(Fig_All, DepPat, decorator));
        }
        private void BtnUndo_Click(object sender, RoutedEventArgs e)
        {
            Invoke_Pat.Undo(DepPat, Fig_All, SelectBorder,BGroup);
        }
        private void BtnRedo_Click(object sender, RoutedEventArgs e)
        {
            Invoke_Pat.Redo();
        }

        // Mouse Events execute different action depanding on selected myshape enum
        public void DepPat_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Start = e.GetPosition(this);
            End = Start;
            Start.Y -= 91;// 91 change due to size of menu
            End.Y -= 91;
            if (currShape == MyShape.SelectBox)
            {
                SelectBorder.Visibility = Visibility.Visible;
                Canvas.SetLeft(SelectBorder, Start.X);
                Canvas.SetBottom(SelectBorder, End.Y);                
            }
            else
                SelectBorder.Visibility = Visibility.Hidden;

            if (currShape == MyShape.Group || currShape == MyShape.DeGroup)
                BGroup.Visibility = Visibility.Visible;
            else
                BGroup.Visibility = Visibility.Hidden;
        }
        private void DepPat_MouseUp(object sender, MouseButtonEventArgs e)
        {

            if (currShape == MyShape.SelectBox)
            {
                SelectBorder.Visibility = Visibility.Hidden;
                SelectBorder.Width = 0;
                SelectBorder.Height = 0;
            }
            if (currShape == MyShape.Group || currShape == MyShape.DeGroup)
            {
                BGroup.Visibility = Visibility.Hidden;
                BGroup.Width = 0;
                BGroup.Height = 0;
            }

            switch (currShape)
            {
                case MyShape.Ellipse:
                    Invoke_Pat.Ellipse(Start, End, DepPat, Fig_All);
                    break;
                case MyShape.Rectangle:
                    Invoke_Pat.Rectangle(Start, End, DepPat, Fig_All);
                    break;
                case MyShape.SelectBox:
                    Invoke_Pat.SelectShape(Start, End, DepPat, Fig_All, ref FigsSel, SelectBorder);
                    break;
                case MyShape.Move:
                    Vis_Pat.RefreshPoints(Start, End);
                    foreach (Figure F in FigsSel)
                    {
                        F.Accept(new MoveShape(Start, End,FigsSel));
                    }
                    break;
                case MyShape.Resize:
                    Vis_Pat.RefreshPoints(Start, End);
                    foreach(Figure F in FigsSel)
                    {
                        F.Accept(new ResizeShape(Start,End));
                    }
                    break;
                case MyShape.Group:
                    Invoke_Pat.Group(Start, End, Fig_All, ref FigsSel, BGroup);
                    break;
                case MyShape.DeGroup:
                    Invoke_Pat.DeGroup(Start, End, Fig_All, ref FigsSel, BGroup);
                    break;
                default:
                    return;
            }
            // ExecuteCommands for command that was made by previous switchcase
            Invoke_Pat.ExecuteCommands();
            Start = new Point();
            End = new Point();
            

        }

        // If select / Group is being used: Move border / change end point
        private void DepPat_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                End = e.GetPosition(this);
                End.Y -= 91;
                double moveX = End.X - Start.X;
                double moveY = End.Y - Start.Y;
                if (currShape == MyShape.SelectBox)
                {
                    SelectBorder.Visibility = Visibility.Visible;
                    Canvas.SetLeft(SelectBorder, Start.X);
                    Canvas.SetTop(SelectBorder, Start.Y);
                    Canvas.SetRight(SelectBorder, End.X);
                    Canvas.SetBottom(SelectBorder, End.Y);
                    if (moveX > 0 && moveY > 0)
                    {
                        SelectBorder.Width = moveX;
                        SelectBorder.Height = moveY;
                    }
                }
                if (currShape == MyShape.Group || currShape == MyShape.DeGroup)
                {
                    Canvas.SetLeft(BGroup, Start.X);
                    Canvas.SetTop(BGroup, Start.Y);
                    Canvas.SetRight(BGroup, End.X);
                    Canvas.SetBottom(BGroup, End.Y);
                    if (moveX > 0 && moveY > 0)
                    {
                        BGroup.Width = moveX;
                        BGroup.Height = moveY;
                    }
                }
            }
            else
            {
                SelectBorder.Visibility = Visibility.Hidden;
                BGroup.Visibility = Visibility.Hidden;
            }
        }

        Border SelectBorder = new Border() //Selectborder definition
        {
            BorderBrush = Brushes.DarkGray,
            BorderThickness = new Thickness(1),
            Padding = new Thickness(5),

        };
        Border BGroup = new Border() //Groupborder definition
        {
            BorderBrush = Brushes.DarkGray,
            BorderThickness = new Thickness(2),
            Padding = new Thickness(10),
        };

        // Reset Canvas and all figures
        private void ResetCanvas()
        {
            DepPat.Children.Clear();
            Fig_All.Clear();
            DepPat.Children.Add(SelectBorder);
            DepPat.Children.Add(BGroup);
            Canvas.SetLeft(SelectBorder, -100);
            Canvas.SetLeft(BGroup, -100);
        }

    }
}
