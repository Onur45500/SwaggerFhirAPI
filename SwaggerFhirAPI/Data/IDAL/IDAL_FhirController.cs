using Hl7.Fhir.Model;
using System.Reflection.Metadata;

namespace SwaggerFhirAPI.Data.IDAL
{
    public interface IDAL_FhirController
    {
        Task<Bundle> GetPatient(string? _lastUpdated, string? identifier);
    }
}
