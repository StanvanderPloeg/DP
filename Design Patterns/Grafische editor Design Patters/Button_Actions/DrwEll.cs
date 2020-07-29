using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Design_Patters_Jaar2
{
    /**
     * CommandI for Drawing Ellipse on Canvas
     * Execute function handles everything regarding drawing of Ellipse
     */
    class DrwEll : CommandI
    {
        private Point Start, End;
        private Canvas DepPat;
        private List<Figure> FigsAll;

        /**
         * DrwEll consist out of Start / End point from Mouse + List of all Figures
         */
        public DrwEll(Point S, Point E, Canvas C, List<Figure> FA)
        {
            Start = S;
            End = E;
            DepPat = C;
            FigsAll = FA;
        }

        public void Execute()
        {
            /**
             * Set main vars for Ell
             * Stroke:              DarkGray
             * Fill:                Blue
             * StrokeThickness:     3
             */
            Ellipse EllNew = new Ellipse()
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
                EllNew.SetValue(Canvas.LeftProperty, Start.X);      
                EllNew.SetValue(Canvas.RightProperty, End.X);
                EllNew.Width = End.X - Start.X;
            } else {
                EllNew.SetValue(Canvas.LeftProperty, End.X);
                EllNew.SetValue(Canvas.RightProperty, Start.X);
                EllNew.Width = Start.X - End.X;
            }

            if (End.Y >= Start.Y) {
                EllNew.SetValue(Canvas.TopProperty, Start.Y);
                EllNew.SetValue(Canvas.BottomProperty, End.Y);
                EllNew.Height = End.Y - Start.Y;
            } else {
                EllNew.SetValue(Canvas.TopProperty, End.Y);
                EllNew.SetValue(Canvas.BottomProperty, Start.Y);
                EllNew.Height = Start.Y - End.Y;
            }

            /**
             * Create a new Figure called EllFigs and add the ellipse to it. 
             * 
             * List Contains out:
             *      - Object
             *      - Type (Ellipse / Rectangle etc.)
             *      - Canvas
             */
            Figure EllFigs = new Figure(EllNew, "Ellipse", DepPat);
            FigsAll.Add(EllFigs);
        }
    }
}
