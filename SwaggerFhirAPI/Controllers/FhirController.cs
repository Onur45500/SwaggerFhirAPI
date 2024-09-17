using Microsoft.AspNetCore.Mvc;
using SwaggerFhirAPI.Data.IDAL;

namespace SwaggerFhirAPI.Controllers
{
    [ApiController]
    [Route("/fhir/r4")]
    public class FhirController
    {
        private readonly IDAL_FhirController _dal;

        public FhirController(IDAL_FhirController dal)
        {
            _dal = dal;

        }


    }
}
