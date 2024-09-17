using Hl7.Fhir.Model;

namespace SwaggerFhirAPI.Models
{
    public class TBPatientModel
    {
        public int Id { get; set; }
        public string Identifier { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public Date? DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public decimal Height { get; set; }
        public decimal Weight { get; set; }
        public DateTimeOffset LastUpdated { get; set; }
    }
}
