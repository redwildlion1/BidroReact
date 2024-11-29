using System.ComponentModel.DataAnnotations;
using Bidro.Config.NoIdeeaForAName;
using Bidro.FrontEndBuildBlocks.Forms;

namespace Bidro.Listings;

public abstract class ListingComponents
{
    public sealed record Location(
        Guid CountyId,
        Guid CityId,
        string Address,
        string PostalCode,
        Guid ListingId)
    {
        public required County County { get; init; }
        public required City City { get; init; }
        public required Listing Listing { get; init; }
    }

    public sealed record Contact(string Name, string Email, string Phone, Guid ListingId)
    {
        public required Listing Listing { get; init; }
    }

    public sealed record FormAnswer(
        string Value,
        Guid Id,
        Guid FormQuestionId,
        Guid ListingId)
    {
        public required FormQuestion Question { get; init; }
        public required Listing Listing { get; init; }
    }
}