using System.Windows.Controls;
using System.Windows.Shapes;

namespace Design_Patters_Jaar2
{
    /**
     * Contains the Ellipse specific items, moslty constructor
     */
    class Ellipses : IFig
    {
        readonly Ellipse Ell;
        private static Ellipses _instance;
        private Canvas Canvas;
        public static Ellipses Instance(Ellipse E, Canvas C)
        {
            if (_instance == null) { _instance = new Ellipses(E, C); }
            return _instance;
        }

        private Ellipses(Ellipse E, Canvas C)
        {
            Ell = E;
            Canvas = C;
        }

        public string toString()
        {
            return "Ellipse";
        }

        // Draw Shape
        public void Draw(Shape S)
        {
            Canvas.Children.Add(S);
        }

        // Get selected shape
        public Shape GetShape()
        {
            return Ell;
        }
    }
}
