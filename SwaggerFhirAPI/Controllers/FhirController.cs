using Microsoft.AspNetCore.Mvc;
using Hl7.Fhir.Serialization;
using Hl7.Fhir.Model;
using Newtonsoft.Json.Linq;
using System.Reflection.PortableExecutable;
using SwaggerFhirAPI.Data.IDAL;

namespace SwaggerFhirAPI.Controllers
{
    [ApiController]
    [Route("/fhir/r4")]
    public class FhirController : ControllerBase
    {
        private readonly IDAL_FhirController _dal;

        public FhirController(IDAL_FhirController dal)
        {
            _dal = dal;

        }

        [HttpGet("Patient", Name = "Patient")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces("application/fhir+json")]
        [Tags("Patient")]
        public async Task<ActionResult> GetPatient(string? _lastUpdated, string? identifier)
        {
            var result = await _dal.GetPatient(_lastUpdated, identifier);

            var serializerResult = new FhirJsonSerializer();
            var jsonResult = serializerResult.SerializeToString(result);

            return Content(jsonResult, "application/fhir+json");
        }
    }
}
