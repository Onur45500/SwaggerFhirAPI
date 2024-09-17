using Hl7.Fhir.Model;
using SwaggerFhirAPI.Models;
using SwaggerFhirAPI.Data.IDAL;
using System.Reflection.Metadata;
using SwaggerFhirAPI.Mapping;

namespace SwaggerFhirAPI.Data.DAL
{
    public class DAL_FhirController : IDAL_FhirController
    {
        private List<TBPatientModel> _patients = new List<TBPatientModel>
        {
            new TBPatientModel { Id = 1, Identifier = "564646546", Name = "Jane", Surname = "Charle", DateOfBirth = new Date("2024-07-29"), Gender="Male", Height=108, Weight=70, LastUpdated = DateTimeOffset.Parse("2024-07-29T16:40:12.6494675+02:00")},
            new TBPatientModel { Id = 2, Identifier = "46546546", Name = "John", Surname = "Misra", DateOfBirth = new Date("2024-07-29"), Gender="Male", Height=108, Weight=70, LastUpdated = DateTimeOffset.Parse("2024-07-29T15:37:12.6494675+02:00")},
            new TBPatientModel { Id = 3, Identifier = "789789789", Name = "John", Surname = "Smith", DateOfBirth = new Date("2024-07-29"), Gender="Male", Height=108, Weight=70, LastUpdated = DateTimeOffset.Parse("2024-07-27T16:35:12.6494675+02:00")},
            new TBPatientModel { Id = 4, Identifier = "123123123", Name = "Jane", Surname = "Doe", DateOfBirth = new Date("2024-07-29"), Gender="Male", Height=108, Weight=70, LastUpdated = DateTimeOffset.Parse("2024-07-26T16:29:12.6494675+02:00")},
        };

        public async Task<Bundle> GetPatient(string? _lastUpdated, string? identifier)
        {
            var patients = _patients.AsEnumerable();

            if (!string.IsNullOrEmpty(identifier))
            {
                patients = patients.Where(p => p.Identifier.ToString() == identifier);
            }

            if (!string.IsNullOrEmpty(_lastUpdated))
            {
                DateTimeOffset lastUpdatedOffset;
                string modifier = null;
                string dateString = _lastUpdated;

                // Check for known filter operators
                var knownModifiers = new List<string> { "eq", "ne", "gt", "lt", "ge", "le", "sa", "eb", "ap" };
                if (_lastUpdated.Length > 2 && knownModifiers.Contains(_lastUpdated.Substring(0, 2)))
                {
                    modifier = _lastUpdated.Substring(0, 2);
                    dateString = _lastUpdated.Substring(2);
                }

                if (DateTimeOffset.TryParse(dateString, out lastUpdatedOffset))
                {
                    switch (modifier)
                    {
                        case "eq":
                            patients = patients.Where(p => p.LastUpdated == lastUpdatedOffset);
                            break;
                        case "ne":
                            patients = patients.Where(p => p.LastUpdated != lastUpdatedOffset);
                            break;
                        case "gt":
                            patients = patients.Where(p => p.LastUpdated > lastUpdatedOffset);
                            break;
                        case "lt":
                            patients = patients.Where(p => p.LastUpdated < lastUpdatedOffset);
                            break;
                        case "ge":
                            patients = patients.Where(p => p.LastUpdated >= lastUpdatedOffset);
                            break;
                        case "le":
                            patients = patients.Where(p => p.LastUpdated <= lastUpdatedOffset);
                            break;
                        case "ap":
                            var range = TimeSpan.FromDays(1);
                            patients = patients.Where(p => p.LastUpdated >= lastUpdatedOffset - range && p.LastUpdated <= lastUpdatedOffset + range);
                            break;
                        default:
                            patients = patients.Where(p => p.LastUpdated == lastUpdatedOffset);
                            break;
                    }
                }
                else
                {
                    throw new InvalidOperationException("Error parsing _lastUpdated");
                }
            }
            var fhirPatients = patients.Select(MapToFhir.MapToFhirPatient).ToList();

            var bundle = new Bundle
            {
                Type = Bundle.BundleType.Searchset,
                Entry = fhirPatients.Select(fp => new Bundle.EntryComponent { Resource = fp }).ToList(),
                Total = fhirPatients.Count
            };

            return await System.Threading.Tasks.Task.FromResult(bundle);
        }
    }
}
