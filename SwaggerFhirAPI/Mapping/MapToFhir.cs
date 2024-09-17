using Hl7.Fhir.Model;
using SwaggerFhirAPI.Models;

namespace SwaggerFhirAPI.Mapping
{
    public class MapToFhir
    {
        public static Patient MapToFhirPatient(TBPatientModel model)
        {
            TimeZoneInfo cestZone = TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time");

            DateTimeOffset lastUpdatedCEST = TimeZoneInfo.ConvertTime(model.LastUpdated, cestZone);

            var patient = new Patient
            {
                Id = model.Id.ToString(),
                Name = new List<HumanName>
                {
                    new HumanName
                    {
                        Family = model.Surname,
                        Given = new List<string> { model.Name }
                    }
                },
                BirthDate = model.DateOfBirth?.ToString(),
                Gender = MapToFhirGender(model.Gender),
                Identifier = new List<Identifier>
                {
                    new Identifier
                    {
                        Value = model.Identifier
                    },
                },
                Meta = new Meta
                {
                    LastUpdated = lastUpdatedCEST
                }
            };

            return patient;
        }

        public static AdministrativeGender MapToFhirGender(string gender)
        {
            return gender.ToLower() switch
            {
                "male" => AdministrativeGender.Male,
                "female" => AdministrativeGender.Female,
                "other" => AdministrativeGender.Other,
                "unknown" => AdministrativeGender.Unknown,
                _ => AdministrativeGender.Unknown
            };
        }
    }
}
