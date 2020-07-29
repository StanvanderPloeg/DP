using System.Collections.Generic;
using System.Windows;
namespace Design_Patters_Jaar2
{

    class MoveShape : IVisitor
    {
        Point end, start;
        List<Figure> FigsSel;
        public MoveShape(Point s, Point e, List<Figure> SF)
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
    }
}
