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
    class Load : IVisitor
    {

        private int RL = 0;
        private readonly Canvas DepPat;
        private Invoker ComI = new Invoker();
        private List<Figure> Figs_All;
        private IDec Dec;

        public Load(List<Figure> FA, Canvas C, IDec D)
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
                Figure child = LoadFig(result);
            }
            sr.Close();
        }

        private Figure LoadFig(string[] result)
        {
            int[] Pos = new int[4];
            Regex regex = new Regex("");
            MatchCollection matches;
            int GroupSize = 0;
            List<string[]> OrList = new List<string[]>();
            List<Figure> templist = new List<Figure>();
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

                        Rectangle newRectangle = new Rectangle()
                        {
                            Stroke = Brushes.DarkGray,
                            Fill = Brushes.Blue,
                            StrokeThickness = 4,
                            Width = Pos[2] - Pos[0],
                            Height = Pos[3] - Pos[1],
                        };
                        Figure RecFig = new Figure(newRectangle, "Rectangle", DepPat);
                        RecFig.SetPosition(Pos[0], Pos[1], Pos[2], Pos[3]);
                        templist = new List<Figure>
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
                            Figure child = LoadFig(result);
                            int childgroupsize = 0;
                            if (child != null)
                            {
                                RecFig.addGroup(child);
                                childgroupsize = child.getGroupS();
                            }
                        }
                        Figs_All.Add(RecFig);
                        return RecFig;
                    case "Ellipse":

                        Ellipse NewElipse = new Ellipse()
                        {
                            Stroke = Brushes.DarkGray,
                            Fill = Brushes.Blue,
                            StrokeThickness = 4,
                            Width = Pos[2] - Pos[0],
                            Height = Pos[3] - Pos[1],
                        };
                        Figure EllFigs = new Figure(NewElipse, "Ellipse", DepPat);
                        EllFigs.SetPosition(Pos[0], Pos[1], Pos[2], Pos[3]);
                        templist = new List<Figure>
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
                            Figure child = LoadFig(result);
                            int childgroupsize = 0;
                            if (child != null)
                            {
                                EllFigs.addGroup(child);
                                childgroupsize = child.getGroupS();
                            }
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
    }
}
