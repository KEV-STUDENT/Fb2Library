using Fb2Library.Domain.Exceptions;
using Fb2Library.Domain.Shared;

namespace Fb2Library.Domain.Genres
{
    public sealed class Genre : Entity<GenreId>
    {
        private readonly GenreCode _code;
        private readonly string _description;

        private Genre(GenreCode code, string description) : base()
        {
            _code = code;
            _description = description;
        }

        public GenreCode Code => _code;

        public string Description => _description;

        public static Genre Create(string code, string description = "")
        {
            if (string.IsNullOrWhiteSpace(code)) throw new DomainException("Code for Genre must be specify");

            if (description is null) throw new ArgumentNullException(nameof(description), "Description is null");

            return new(GenreCode.Create(code), description);
        }

        protected override GenreId GetNewId() => GenreId.New();
    }
}
