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
    class Load : IVisitor
    {

        private int RL = 0;
        private readonly Canvas DepPat;
        private Invoker ComI = new Invoker();
        private List<Component> Figs_All;
        private IDec Dec;

        public Load(List<Component> FA, Canvas C, IDec D)
        {
            Figs_All = FA;
            DepPat = C;
            Dec = D;
        }

        public void Visit(Figure F)
        {
            StreamReader sr = new StreamReader(@"D:\Stan\Documenten Lokaal\Projecten\Design Patterns\Dep.txt");
            List<string> read = new List<string>();
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                line = line.Replace("-", string.Empty);
                read.Add(line);
            }
            string[] result = read.ToArray();
            for (RL = 0; RL < result.Length; RL++)
            {
                Component child = LoadFig(result);
            }
            sr.Close();
        }

        private Component LoadFig(string[] result)
        {
            int[] Pos = new int[4];
            Regex regex = new Regex("");
            MatchCollection matches;
            int GroupSize = 0;
            List<string[]> OrList = new List<string[]>();
            List<Component> templist = new List<Component>();

            while (RL < result.Count())
            {
                regex = new Regex("ComponentList:(\\d*)");
                matches = regex.Matches(result[RL]);
                foreach (Match M in matches)
                {
                    GroupSize = Convert.ToInt16(M.Groups[1].ToString());
                    RL++;
                }
                while (result[RL].Contains("Ornament"))
                {
                    regex = new Regex("Ornament\\s(\\w*)\\s(.*)");
                    matches = regex.Matches(result[RL]);
                    foreach (Match M in matches)
                    {

                        string loc = M.Groups[1].ToString();
                        string or = M.Groups[2].ToString();
                        string[] Ornament = { loc, or };
                        OrList.Add(Ornament);
                        RL++;
                    }
                }
                regex = new Regex("(\\d+)\\s(\\d+)\\s(\\d+)\\s(\\d+)");

                //Point P = new Point();


                if (result.Count() > RL + 1 && regex.IsMatch(result[RL + 1]))
                {
                    matches = regex.Matches(result[RL + 1]);
                    Pos[0] = Convert.ToInt16(matches[0].Groups[1].Value);
                    Pos[1] = Convert.ToInt16(matches[0].Groups[2].Value);
                    Pos[2] = Convert.ToInt16(matches[0].Groups[3].Value);
                    Pos[3] = Convert.ToInt16(matches[0].Groups[4].Value);
                }

                switch (result[RL])
                {
                    case "Rectangle":

                        Point P = new Point();

                        P.X = Pos[2] - Pos[0];
                        P.Y = Pos[3] - Pos[1];

                        Rectangle newRectangle = new Rectangle()
                        {
                            Stroke = Brushes.DarkGray,
                            Fill = Brushes.Blue,
                            StrokeThickness = 4,
                            Width = P.X,
                            Height = P.Y,
                        };
                        Component RecFig = new Figure(newRectangle, "Rectangle", DepPat);

                        
                        RecFig.SetPosition(P);
                        templist = new List<Component>
                        {
                            RecFig
                        };
                        foreach (string[] str in OrList)
                        {
                            switch (str[0])
                            {
                                case "Top":
                                    Dec = new OrnamentDecTop();
                                    break;

                                case "Bot":
                                    Dec = new OrnamentDecBot();
                                    break;

                                case "Left":
                                    Dec = new OrnamentDecLeft();
                                    break;

                                case "Right":
                                    Dec = new OrnamentDecRight();
                                    break;
                                default:
                                    break;
                            }
                            ComI.AddOrnament(ref templist, str[1], Dec);
                        }
                        ComI.ExecuteCommands();

                        for (int c = 0; c < GroupSize; c++)
                        {
                            RL += 2;
                            Component child = LoadFig(result);
                            //int childgroupsize = 0;
                            //if (child != null)
                            //{
                            //    RecFig.addGroup(child);
                            //    childgroupsize = child.getGroupS();
                            //}
                        }
                        Figs_All.Add(RecFig);
                        return RecFig;
                    case "Ellipse":

                        Point p = new Point();

                        p.X = Pos[2] - Pos[0];
                        p.Y = Pos[3] - Pos[1];
                        Ellipse NewElipse = new Ellipse()
                        {
                            Stroke = Brushes.DarkGray,
                            Fill = Brushes.Blue,
                            StrokeThickness = 4,
                            Width = p.X,
                            Height = p.Y,
                        };
                        Component EllFigs = new Figure(NewElipse, "Ellipse", DepPat);

                        
                        EllFigs.SetPosition(p);
                        templist = new List<Component>
                        {
                            EllFigs
                        };
                        foreach (string[] str in OrList)
                        {
                            switch (str[0])
                            {
                                case "Top":
                                    Dec = new OrnamentDecTop();
                                    break;

                                case "Bot":
                                    Dec = new OrnamentDecBot();
                                    break;

                                case "Left":
                                    Dec = new OrnamentDecLeft();
                                    break;

                                case "Right":
                                    Dec = new OrnamentDecRight();
                                    break;
                                default:
                                    break;
                            }
                            ComI.AddOrnament(ref templist, str[1], Dec);
                        }
                        ComI.ExecuteCommands();

                        for (int c = 0; c < GroupSize; c++)
                        {
                            RL += 2;
                            Component child = LoadFig(result);
                            //int childgroupsize = 0;
                            //if (child != null)
                            //{
                                
                            //    EllFigs.Add(child);
                            //    childgroupsize = child.getGroupS();
                            //}
                        }
                        Figs_All.Add(EllFigs);
                        return EllFigs;

                    default:
                        RL++;
                        break;
                }
            }
            return null;
        }

        public void Visit(Grafische_editor_Design_Patters.Figures.Group group)
        {
            throw new NotImplementedException();
        }
    }
}
