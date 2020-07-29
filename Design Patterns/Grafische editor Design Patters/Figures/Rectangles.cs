using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Design_Patters_Jaar2
{
    /**
     * Contains the Rectangle specific items, moslty constructor
     */
    class Rectangles : IFig
    {
        Rectangle Rect;
        private static Rectangles _instance;
        Canvas Canvas;
        public static Rectangles Instance( Rectangle R, Canvas C)
        {
            if (_instance == null){ _instance = new Rectangles(R,C); }
            return _instance;
        }
        private Rectangles(Rectangle R, Canvas C )
        {
            Rect = R;
            Canvas = C;
        }

        public string toString()
        {
            return "Rectangle";
        }
        public void Draw(Shape S)
        {
            Canvas.Children.Add(S);
        }

        public Shape GetShape()
        {
            return Rect;
        }
    }
}
