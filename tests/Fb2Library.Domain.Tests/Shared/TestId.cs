using Fb2Library.Domain.Shared;

namespace Fb2Library.Domain.Tests.Shared
{
    internal sealed record Test1Id : GuidIdentity, IIdentityFabric<Test1Id, Guid>
    {
        public Test1Id(Guid value) : base(value) { }
        public static Test1Id From(Guid value) => new(value);
        public static Test1Id New() => From(Guid.NewGuid());
        public static implicit operator Test1Id(Guid value) => From(value);
    }

    internal sealed record Test2Id : GuidIdentity, IIdentityFabric<Test2Id, Guid>
    {
        public Test2Id(Guid value) : base(value) { }
        public static Test2Id From(Guid value) => new(value);
        public static Test2Id New() => From(Guid.NewGuid());
    }
}
