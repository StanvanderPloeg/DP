using Design_Patters_Jaar2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Grafische_editor_Design_Patters.Figures
{
    class Group : Component
    {
        // List with all Figures that belong to the figure group
        private List<Component> ComponentList = new List<Component>();

        // List with all Ornaments belong to this figure
        public List<Ornament> Ornaments = new List<Ornament>();

        double X;
        double Y;
        double Height;
        double Width;

        public void Accept(IVisitor v)
        {
            throw new NotImplementedException();
        }

        public void Deselect()
        {
            throw new NotImplementedException();
        }

        public void Resize(Point Start, Point End)
        {
            throw new NotImplementedException();
        }

        public void Select()
        {
            throw new NotImplementedException();
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

            // throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public double GetWidth()
        {
            throw new NotImplementedException();
        }

        public double GetHeight()
        {
            throw new NotImplementedException();
        }
    }
}
