namespace Bidro.Validation.ValidationObjects;

public class ReviewValidityObject
{
    //TO DO: Add constructor
    //Need to add DTOs
    public string Content { get; }
    public int Rating { get; }
}

public class ReviewValidityObjectDb
{
    //public ReviewValidityObjectDb()

    public Guid UserId { get; set; }
    public Guid FirmId { get; set; }
}