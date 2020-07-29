namespace Design_Patters_Jaar2
{
    /// <summary>
    /// Interface visitor pattern
    /// Makes object visitable
    /// </summary>
   public interface IVisitable
    {
        void Accept(IVisitor v);
    }
}
