namespace Design_Patters_Jaar2
{
    /// <summary>
    /// Interface for visitor
    /// For object that visits vistiable object
    /// </summary>
   public interface IVisitor
    {
        void Visit(Figure F);
        void Visit(Grafische_editor_Design_Patters.Figures.Group group);
    }
}
