namespace Tjololo.DI.Interfaces;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public class TransientAttribute: Attribute
{
    private Type _type;
    
    public TransientAttribute(Type type)
    {
        _type = type;
    }
    
    public virtual Type Type
    {
        get {return _type;}
    }
}