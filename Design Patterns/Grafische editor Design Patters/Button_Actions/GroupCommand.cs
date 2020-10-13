using Grafische_editor_Design_Patters.Figures;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Design_Patters_Jaar2
{
    /*
     * CommandI for grouping different figures
     * Execute functions handles grouping
     */
    class GroupCommand : CommandI
    {
        private List<Component> FigsSel;
        private readonly List<Component> FigsAll;
        private Point Start, End;
        private Border BGroup;
        public GroupCommand(Point S, Point E, List<Component> FS, List<Component> FA, Border B)
        {
            FigsSel = FS;
            FigsAll = FA;
            Start = S;
            End = E;
            BGroup = B;
        }

        public void Execute()
        {
            /**
             * Check to see if last point is higher or same as first point.
             *  If last point is bigger / same the Start point will be End point and other way around.
             */
            if (End.X >= Start.X) {
                BGroup.SetValue(Canvas.LeftProperty, Start.X);
                BGroup.Width = End.X - Start.X;
            } else {
                BGroup.SetValue(Canvas.LeftProperty, End.X);
                BGroup.Width = Start.X - End.X;
            }

            if (End.Y >= Start.Y) {
                BGroup.SetValue(Canvas.TopProperty, Start.Y - 50);
                BGroup.Height = End.Y - Start.Y;
            } else {
                BGroup.SetValue(Canvas.TopProperty, End.Y - 50);
                BGroup.Height = Start.Y - End.Y;
            }

            Group G = new Group();
            
            /**
             * For each figure in the FigsAll list add them to the group
             */
            foreach (Figure F in FigsAll)
            {
                if (F.Left > Start.X && F.Left < End.X && F.Top > Start.Y && F.Top < End.Y && FigsSel.Count() != 0)
                {
                    //FigsSel[0].addGroup(F)
                    G.Add(F);
                }
            }
        }
    }
}
