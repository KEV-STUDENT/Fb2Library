namespace Fb2Library.Domain.Shared
{
    public interface IIdentityFabric<T1, T2> where T1 : Identity<T2>
    {
        public abstract static T1 New();
        public abstract static T1 From(T2 value);
    }
}
