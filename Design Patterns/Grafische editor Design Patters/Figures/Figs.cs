using Grafische_editor_Design_Patters.Figures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Design_Patters_Jaar2
{
    /**
     * Abstract class with all drawable figures
     * Consist out of Move, Resize and Group functions
     * 
     * Ordanments are linked to canvas
     */
    public class Figure : Component
    {

        public double Top, Left, Bot, Right;
        public Shape Fig;
        public bool IsGrouped;
        public Figure Parent { get; private set; }
        public string Type;
        public readonly Canvas DepPat;
        private readonly IFig DeliFig;
        public Figure(Shape S, string T, Canvas C)
        {
            Fig = S;
            Type = T;
            DepPat = C;
            //SetPosition(Canvas.GetLeft(S), Canvas.GetTop(S), Canvas.GetRight(S), Canvas.GetBottom(S));

            if (S.GetType() == typeof(Rectangle))
                DeliFig = Rectangles.Instance((Rectangle)S, DepPat);
            if (S.GetType() == typeof(Ellipse))
                DeliFig = Ellipses.Instance((Ellipse)S, DepPat);

            DeliFig.Draw(S);
        }

        // Set Location of figure, not recursive
        public void SetPosition(Point P)
        {
            /*
             * Positie zetten van X / Y op de canvas van het figuur
             */
            Top = P.X;
            Left = P.Y;

            Canvas.SetTop(Fig, Top);
            Canvas.SetLeft(Fig, Left);

            // Top = T;
            // Left = L;
            // Bot = B;
            // Right = R;
            //Canvas.SetLeft(Fig, Left);
            //Canvas.SetTop(Fig, Top);
            //Canvas.SetRight(Fig, Right);
            //Canvas.SetBottom(Fig, Bot);
        }
     
        public void SetWidth()
        {

        }

        public void SetHeight()
        {

        }

        public Point GetPosition()
        {
            Point P = new Point();
            P.X = Left;
            P.Y = Top;

            return P;
        }

        public double GetWidth()
        {
            return Right - Left;
        }

        public double GetHeight() 
        {
            return Bot - Top;
        }

        // Adds new Ornament to Figure
        public Shape GetShape()
        {
            return Fig;
        }

        // Virtual function for selecting 
        public virtual void Select()
        {
            Fig.Stroke = Brushes.Black;
        }

        // Virtual function for deselecting 
        public virtual void Deselect()
        {
            Fig.Stroke = Brushes.DarkGray;
        }

        // Return List with figures
        public List<Figure> getGroup()
        {
            return FigList_Gr;
        }

        // get size of Group
        public int getGroupS()
        {
            int size = 0;
            if (FigList_Gr.Count() == 0)
                return -1;
            foreach (Figure F in FigList_Gr)
            {

                size += F.getGroupS();
                if (size == -1)
                    size = 1;
            }
            return size;
        }
        
        // addGroup new figure to group list
        public void addGroup(Figure F)
        {
            if (this != F && F.IsGrouped != true)
            {
                FigList_Gr.Add(F);
                F.AddParent(this);
                F.IsGrouped = true;
            }
        }

        public void AddParent(Figure F)
        {
            if (Parent == null)
            {
                Parent = F;
            }
        }

        // Delete figure from group
        public void delGroup(Figure F)
        {
            List<Figure> RemFigs = new List<Figure>();
            foreach (Figure Figs in FigList_Gr)
            {
                if (Figs == F)
                {
                    RemFigs.Add(F);
                }
            }
            foreach (Figure Figs in RemFigs)
            {
                Figs.Parent = null;
                FigList_Gr.Remove(Figs);
                F.IsGrouped = false;
            }
        }

        // Checks current position, turns left / right if needed 
        public void ControlPosition()
        {
            Left = Canvas.GetLeft(Fig);
            Top = Canvas.GetTop(Fig);
            Right = Canvas.GetRight(Fig);
            Bot = Canvas.GetBottom(Fig);

            double LT;
            if (Left > Right)
            {
                LT = Right;
                Right = Left;
                Left = LT;
            }
            if (Top > Bot)
            {
                LT = Top;
                Top = Bot;
                Bot = LT;
            }
            Canvas.SetLeft(Fig, Left);
            Canvas.SetTop(Fig, Top);
            Canvas.SetRight(Fig, Right);
            Canvas.SetBottom(Fig, Bot);

        }

        // Resurvive move function, moves everyting inside a group
        public void Move(double x, double y)
        {
            Left += x;
            Top += y;
            Right += x;
            Bot += y;
            Canvas.SetLeft(Fig, Canvas.GetLeft(Fig) + x);
            Canvas.SetTop(Fig, Canvas.GetTop(Fig) + y);
            Canvas.SetRight(Fig, Canvas.GetRight(Fig) + x);
            Canvas.SetBottom(Fig, Canvas.GetBottom(Fig) + y);
            foreach (Ornament OR in Ornaments) {
                OR.LocChange();
            }
            ControlPosition();
            foreach (Figure F in FigList_Gr) {

                F.Move(x, y);
            }

        }

        // Resursive resize function, resizes everyting inside a group
        public void Resize(Point Start, Point End)
        {
            double SizeX = End.X - Start.X;
            double SizeY = End.Y - Start.Y;

            Bot = Canvas.GetTop(Fig) + SizeY;
            Right = Canvas.GetLeft(Fig) + SizeX;
            Fig.Height = SizeY;
            Fig.Width  = SizeX;
            SetPosition(Canvas.GetLeft(Fig), Canvas.GetTop(Fig), Canvas.GetLeft(Fig) + SizeX, Canvas.GetTop(Fig) + SizeY);

            ControlPosition();

            foreach (Figure F in FigList_Gr)
            {
                F.Resize(Start, End);
            }
        }

        public void Accept(IVisitor v)
        {
            foreach (Figure F in FigList_Gr)
            {
                F.Accept(v);
            }
            v.Visit(this);
        }

        public List<Ornament> GetOrnament()
        {
            return Ornaments;
        }

    }
}