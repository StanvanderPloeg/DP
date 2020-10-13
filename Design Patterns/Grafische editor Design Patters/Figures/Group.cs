using Design_Patters_Jaar2;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


// Group: GroupCommand nieuwe groep aangemaakt deze moet in algemene component lijst komen. 
// Group toevoegd aan lijst heb je dubbele items ( Figs / Group )
// Nieuwe Group toevoegen betekend figuren idivuidueel verwijderen.


namespace Grafische_editor_Design_Patters.Figures
{
    public class Group : Component
    {
        // List with all Figures that belong to the figure group
        private List<Component> ComponentList = new List<Component>();

        // List with all Ornaments belong to this figure
        public List<Ornament> Ornaments = new List<Ornament>();

        double X;
        double Y;
        double Height;
        double Width;


        public void Add(Component C)
        {
            ComponentList.Add(C);
        }

        public void Delete(Component C)
        {
            ComponentList.Remove(C);
        }

        public void Accept(IVisitor v)
        {
            v.Visit(this);
        }

        public void Deselect()
        {
            foreach(Component C in ComponentList)
            {
                C.Deselect();
            }
        }

        public void Resize(Point Start, Point End)
        {
            // Kan met Visitor
            throw new NotImplementedException();
        }

        public void Select()
        {
            foreach (Component C in ComponentList)
            {
                C.Select();
            }
        }

        public void SetPosition(Point P)
        {
            double DeltaX = X - this.X;
            double DeltaY = Y - this.Y;

            foreach(Component C in ComponentList)
            {
                P = C.GetPosition();

                P.X = P.X + DeltaX;
                P.Y = P.Y + DeltaY;

                C.SetPosition(P);
            }
        }

        public void CalcProperties()
        {
            this.X = 3000;
            this.Y = 3000;

            double GrX = 0;
            double GrY = 0;
            
            foreach(Component C in ComponentList)
            {
                // Get position of box component
                Point P = C.GetPosition();
                if(P.X < this.X)
                {
                    this.X = P.X;
                } 

                if (P.Y < this.Y)
                {
                    this.Y = P.Y;
                }

                P.X += C.GetWidth();
                P.Y += C.GetHeight();

                if (P.X > GrX)
                {
                    GrX = P.X;
                }

                if (P.Y > GrY)
                {
                    GrY = P.Y;
                }
            }

            this.Width = GrX - this.X;
            this.Height = GrY - this.Y;

        }

        public Point GetPosition()
        {
            CalcProperties();

            Point P = new Point();
            P.X = this.X;
            P.Y = this.Y;

            return P;
        }

        public double GetWidth()
        {
            CalcProperties();

            return this.Width;
        }

        public double GetHeight()
        {
            CalcProperties();

            return this.Height;
        }
    }
}
