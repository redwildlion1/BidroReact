using System.ComponentModel.DataAnnotations;
using Bidro.FrontEndBuildBlocks.Forms;

namespace Bidro.Listings;

public abstract class ListingComponents
{
    public sealed record Location
    {
        public Location(Guid CountyId,
            Guid CityId,
            string Address,
            string PostalCode,
            Listing Listing,
            Guid ListingId)
        {
            this.CountyId = CountyId;
            this.CityId = CityId;
            this.Address = Address;
            this.PostalCode = PostalCode;
            this.Listing = Listing;
            this.ListingId = ListingId;
        }
        public Guid CountyId { get; init; }
        public Guid CityId { get; init; }
        public string Address { get; init; }
        public string PostalCode { get; init; }
        public Listing Listing { get; init; }
        [Key]
        public Guid ListingId { get; init; }
    }

    public sealed record Contact
    {
        public Contact
            (string Name, string Email, string Phone, Listing Listing, Guid ListingId)
        {
            this.Name = Name;
            this.Email = Email;
            this.Phone = Phone;
            this.Listing = Listing;
            this.ListingId = ListingId;
        }

        [StringLength(50)]
        public string Name { get; init; }
        [StringLength(50)]
        public string Email { get; init; }
        [StringLength(15)]
        public string Phone { get; init; }
        public Listing Listing { get; init; }
        [Key]
        public Guid ListingId { get; init; }
        
    }

    public sealed record FormAnswer
    {
        public FormAnswer(string Value,
            Guid QuestionId,
            FormQuestion Question,
            Guid FormQuestionId,
            Listing Listing,
            Guid ListingId)
        {
            this.Value = Value;
            this.QuestionId = QuestionId;
            this.Question = Question;
            this.FormQuestionId = FormQuestionId;
            this.Listing = Listing;
            this.ListingId = ListingId;
        }
        public string Value { get; init; }
        [Required]
        public Guid QuestionId { get; init; }
        public FormQuestion Question { get; init; }
        [Required]
        public Guid FormQuestionId { get; init; }
        public Listing Listing { get; init; }
        [Key]
        public Guid ListingId { get; init; }
        
    }
}