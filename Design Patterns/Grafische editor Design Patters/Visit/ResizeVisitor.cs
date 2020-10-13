using Grafische_editor_Design_Patters.Figures;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;


namespace Design_Patters_Jaar2
{
    class ResizeVisitor 
    {
        private List<Component> Figs_All;
        private List<Component> FigsSel;
        private Point Start, End;
        private Canvas DepPat;
        private Invoker ComInv = new Invoker();
        public ResizeVisitor(ref List<Component> FA, ref List<Component> FS, Point S, Point E, ref Canvas C)
        {
            Figs_All = FA;
            FigsSel = FS;
            Start = S;
            End = E;
            DepPat = C;
        }
        public void RefreshPoints(Point S, Point E)
        {
            Start = S;
            End = E;
        }
       
        public void Visit(ResizeShape R)
        {
            foreach (Component F in FigsSel)
            {
                F.Resize(Start, End);
            }
        }


       

      
    }
}