using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Design_Patters_Jaar2
{
    /**
     * CommandI for Drawing Rectangles on Canvas
     * Execute function handles everything regarding drawing of a Rectangle
     */
    class DrwRec : CommandI
    {
        private Point Start, End;
        private Canvas DepPat;
        private List<Figure> FigsAll;

        /**
        * DrwRec consist out of Start / End point from Mouse + List of all Figures
        */
        public DrwRec(Point S, Point E, Canvas C, List<Figure> AF)
        {
            Start = S;
            End = E;
            DepPat = C;
            FigsAll = AF;
        }

        public void Execute()
        {
            /**
            * Set main vars for Rectangle
            * Stroke:           DarkGray
            * Fill:             Blue
            * StrokeThickness:  3
            */
            Rectangle RecNew = new Rectangle()
            {
                Stroke = Brushes.DarkGray,
                Fill = Brushes.Blue,
                StrokeThickness = 3
            };

            /**
             * Check to see if last point is higher or same as first point.
             *  If last point is bigger / same the Start point will be End point and other way around.
             */
            if (End.X >= Start.X) {
                RecNew.SetValue(Canvas.LeftProperty, Start.X);
                RecNew.SetValue(Canvas.RightProperty, End.X);
                RecNew.Width = End.X - Start.X; 
            } else {
                RecNew.SetValue(Canvas.LeftProperty, End.X);
                RecNew.SetValue(Canvas.RightProperty, Start.X);
                RecNew.Width = Start.X - End.X;
            }

            if (End.Y >= Start.Y) {
                RecNew.SetValue(Canvas.TopProperty, Start.Y);
                RecNew.SetValue(Canvas.BottomProperty, End.Y);
                RecNew.Height = End.Y - Start.Y;
            } else {
                RecNew.SetValue(Canvas.TopProperty, End.Y);
                RecNew.SetValue(Canvas.BottomProperty, Start.Y);
                RecNew.Height = Start.Y - End.Y;
            }

            /**
             * Create a new Figure called RecFigs and add the ellipse to it. 
             * 
             * List Contains out:
             *      - Object
             *      - Type (Ellipse / Rectangle etc.)
             *      - Canvas
             */
            Figure RecFigs = new Figure(RecNew, "Rectangle", DepPat);
            FigsAll.Add(RecFigs);
        }
    }
}
