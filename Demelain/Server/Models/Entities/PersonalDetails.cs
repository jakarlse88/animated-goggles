namespace Demelain.Server.Models.Entities
{
    public partial class PersonalDetails : EntityBase
    {
        public override int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string JobTitle { get; set; }
        public string Biography { get; set; }
    }
}