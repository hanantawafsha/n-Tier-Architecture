namespace NTierArchitecture.DAL.Models
{
    public class Address:BaseModel
    {
        public string City { get; set; }
        public string Street { get; set; }
        public ApplicationUser? User { get; set; }

    }
}