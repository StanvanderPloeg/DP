using Grafische_editor_Design_Patters.Figures;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
namespace Design_Patters_Jaar2
{
    class Save : IVisitor
    {

        private List<Component> FigAll;

        public Save(List<Component> AF)
        {
            FigAll = AF;
        }
        public void Visit(Figure F)
        {
            StreamWriter sw = new StreamWriter(@"D:\Stan\Documenten Lokaal\Projecten\Design Patterns\Dep.txt");
            int RecusionLevel = 1;
            foreach (Figure f in FigAll)
            {
                if (f.IsGrouped == false)
                {
                    //sw.WriteLine("ComponentList:" + f.getGroup().Count().ToString());
                    //foreach (Ornament OR in f.GetOrnament())
                    //{
                    //    sw.WriteLine("ornament " + OR.GetLocation() + " " + OR.GetText() + " ");
                    //}
                    sw.WriteLine(f.Type);
                    int Left = Convert.ToInt16(Canvas.GetLeft(f.GetShape()));
                    int Top = Convert.ToInt16(Canvas.GetTop(f.GetShape()));
                    int Right = Convert.ToInt16(Canvas.GetRight(f.GetShape()));
                    int Bot = Convert.ToInt16(Canvas.GetBottom(f.GetShape()));
                    sw.WriteLine(Left + " " + Top + " " + Right + " " + Bot);
                    SaveChild(f, sw, RecusionLevel);
                }
            }
            sw.Close();
        }

        public void Visit(Grafische_editor_Design_Patters.Figures.Group group)
        {
            throw new NotImplementedException();
        }

        private void SaveChild(Figure F, StreamWriter sw, int Reclvl)
        {
            //foreach (Figure Figs in F.getGroup())
            //{
            //    for (int i = 0; i < Reclvl; i++)
            //    {
            //        sw.Write("-");
            //    }
            //    //sw.WriteLine("ComponentList:" + Figs.getGroup().Count().ToString() + " ");
            //    for (int i = 0; i < Reclvl; i++)
            //    {
            //        sw.Write("-");
            //    }
            //    //foreach (Ornament OR in Figs.GetOrnament())
            //    //{
            //    //    sw.WriteLine("ornament " + OR.GetLocation() + " " + OR.GetText() + "");
            //    //}
            //    for (int i = 0; i < Reclvl; i++)
            //    {
            //        sw.Write("-");
            //    }
            //    sw.WriteLine(Figs.Type);
            //    for (int i = 0; i < Reclvl; i++)
            //    {
            //        sw.Write("-");
            //    }
            //    int Left = Convert.ToInt16(Canvas.GetLeft(Figs.GetShape()));
            //    int Top = Convert.ToInt16(Canvas.GetTop(Figs.GetShape()));
            //    int Right = Convert.ToInt16(Canvas.GetRight(Figs.GetShape()));
            //    int Bot = Convert.ToInt16(Canvas.GetBottom(Figs.GetShape()));
            //    sw.WriteLine(Left + " " + Top + " " + Right + " " + Bot); if (Figs.getGroup().Count != 0)
            //        Reclvl++;
            //    SaveChild(Figs, sw, Reclvl);
            //}
        }

    }
}
