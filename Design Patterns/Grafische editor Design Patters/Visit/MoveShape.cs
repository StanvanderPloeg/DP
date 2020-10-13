using Grafische_editor_Design_Patters.Figures;
using System.Collections.Generic;
using System.Windows;
namespace Design_Patters_Jaar2
{

    class MoveShape : IVisitor
    {
        Point end, start;
        List<Component> FigsSel;
        public MoveShape(Point s, Point e, List<Component> SF)
        {
            start = s;
            end = e;
            FigsSel = SF;
        }
        public void Visit(Figure F)
        {
            double moveX = end.X - start.X;
            double moveY = end.Y - start.Y;

            if (!FigsSel.Contains(F.Parent))
                F.Move(moveX, moveY);

        }
        public void Visit(Group group)
        {
            throw new System.NotImplementedException();
        }
    }
}
