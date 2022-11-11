public interface IElement
{
    public void Attach(IVisitor visitor);
    public void Accept(IVisitor visitor);
}