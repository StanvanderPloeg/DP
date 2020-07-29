using System.Windows;

namespace Design_Patters_Jaar2
{
    class ResizeShape : IVisitor
    {
        Point Start, End;
        public ResizeShape(Point S, Point E)
        {
            Start = S;
            End = E;
        }
        public void Visit(Figure F)
        {
            F.Resize(Start, End);
        }


    }
}
