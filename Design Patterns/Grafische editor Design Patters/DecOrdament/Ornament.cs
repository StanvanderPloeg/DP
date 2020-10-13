using Grafische_editor_Design_Patters.Figures;
using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;


namespace Design_Patters_Jaar2
{
    /**
     * Ornament Object, Made by Dec and linked to figure
     */
    public class Ornament
    {

        public double Top, Left, Bot, Right;
        public TextBlock OrnFig = new TextBlock();

        public bool IsGrouped;
        public readonly Canvas DepPat;
        protected string Text;
        protected string Loc;
        private readonly Shape Fig;
        //private TextBlock OrnFig = new TextBlock();
        public Ornament(Canvas C, String T, String L, Shape F)
        {
            OrnFig.Foreground = new SolidColorBrush(Colors.Black);

            DepPat = C;
            Fig = F;

            OrnFig.Text = T;
            Text = T;
            Loc = L;
            C.Children.Add(OrnFig);
            LocChange();
        }

        /**
         * Location the Ornament will be placed at, this is relative to the figure
         */
        public void LocChange()
        {
            double Left = 0, Top = 0;
            switch (Loc)
            {
                case "Top":
                    Left = Canvas.GetLeft(Fig);
                    Top = Canvas.GetTop(Fig) - 20;
                    break;
                case "Bot":
                    Left = Canvas.GetLeft(Fig);
                    Top = Canvas.GetBottom(Fig) + 20;
                    break;
                case "Left":
                    Left = Canvas.GetLeft(Fig) - 100;
                    Top = Canvas.GetTop(Fig);
                    break;
                case "Right":
                    Left = Canvas.GetRight(Fig) + 20;
                    Top = Canvas.GetTop(Fig);
                    break;
                default:
                    break;
            }
            Canvas.SetLeft(OrnFig, Left);
            Canvas.SetTop(OrnFig, Top);
        }
        // Get the Txt inside the Ornament
        public string GetText()
        {
            return Text;
        }
        // Get the location of the Ornament
        public string GetLocation()
        {
            return Loc;
        }

        internal void Add(Ornament or)
        {
            throw new NotImplementedException();
        }
    }
}
