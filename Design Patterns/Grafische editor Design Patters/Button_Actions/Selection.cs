using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;


namespace Design_Patters_Jaar2
{
    /**
     * ComI for Selecting Figures
     * 
     * Execute fills the selected figures list with all objects inside the radius. (Figure / Object needs to be fully within border)
     */
    class Selection : CommandI
    {
        private List<Figure> FigSel;
        private readonly List<Figure> FigAll;
        private Point Start, End;
        private Border SelectRad;

        /**
         * Selection takes the following vars:
         * 
         * Point S -- Start Point
         * Point E -- End Point
         * List -- List containing all figures
         * List -- List containing selected figs
         * Border -- Area that is selected
         */
        public Selection(Point S, Point E, List<Figure> FA, ref List<Figure> FS, Border SB)
        {
            FigSel = FS;
            FigAll = FA;
            SelectRad = SB;
            Start = S;
            End = E;
        }

        public void Execute()
        {
            /**
             * Check to see if the last point is bigger then first point. 
             * If end point is bigger turn vars around.
             */
            if (End.X >= Start.X) {
                SelectRad.SetValue(Canvas.LeftProperty, Start.X);
                SelectRad.Width = End.X - Start.X;
            } else {
                SelectRad.SetValue(Canvas.LeftProperty, End.X);
                SelectRad.Width = Start.X - End.X;
            }

            if (End.Y >= Start.Y) {
                SelectRad.SetValue(Canvas.TopProperty, Start.Y - 50);
                SelectRad.Height = End.Y - Start.Y;
            } else {
                SelectRad.SetValue(Canvas.TopProperty, End.Y - 50);
                SelectRad.Height = Start.Y - End.Y;
            }

            // Clear selection list
            FigSel.Clear();
            foreach (Figure F in FigAll)
            {
                F.Deselect();
                if (F.Left > Start.X && F.Right < End.X && F.Top > Start.Y && F.Bot < End.Y)
                {
                    FigSel.Add(F);
                    F.Select();
                }
            }
        }
    }
}
