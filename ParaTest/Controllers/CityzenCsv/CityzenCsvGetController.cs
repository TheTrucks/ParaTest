using Microsoft.AspNetCore.Mvc;
using ParaTest.Models.Cityzen;
using System.Net;

namespace ParaTest.Controllers.CityzenCsv
{
    [ApiController]
    [Route("[controller]/[action]")]
    [Produces("text/csv")]
    public class CityzenCsvGetController : Controller
    {
        private readonly ICityzenDataGetter _ctProvider;
        private readonly ICityzenSerializer _cityzenToCsv;
        public CityzenCsvGetController(ICityzenDataGetter CtProvider, ICityzenSerializer CityzenSerializer)
        {
            _ctProvider = CtProvider;
            _cityzenToCsv = CityzenSerializer;
        }

        private async Task<IActionResult> SerializeModel(Cityzen? Model, string Filename)
        {
            if (Model == null)
                return new ContentResult
                {
                    StatusCode = (int)HttpStatusCode.NotFound,
                    ContentType = "text/plain",
                    Content = "Couldn't find cityzen in the DB"
                };

            return await SerializeModel(new[] { Model }, Filename);
        }
        private async Task<IActionResult> SerializeModel(IEnumerable<Cityzen>? Model, string Filename)
        {
            if (Model == null)
                return new ContentResult 
                { 
                    StatusCode = (int)HttpStatusCode.NotFound, 
                    ContentType = "text/plain", 
                    Content = "Couldn't find cityzens in the DB" 
                };

            using (var CityzenStream = _cityzenToCsv.Serialize(Model))
            {
                return File(await CityzenStream, "text/csv", Filename);
            }
        }

        [AcceptVerbs(new string[] { "HttpGet", "HttpPost" })]
        public async Task<IActionResult> GetByFullName(string FullName)
        {
            var Model = await _ctProvider.GetByFullName(FullName);
            return await SerializeModel(Model, "CityzenByFullName.csv");
        }

        [AcceptVerbs(new string[] { "HttpGet", "HttpPost" })]
        public async Task<IActionResult> GetBySnils(string Snils)
        {
            var Model = await _ctProvider.GetBySNILS(Snils);
            return await SerializeModel(Model, "CityzenBySNILS.csv");
        }

        [AcceptVerbs(new string[] { "HttpGet", "HttpPost" })]
        public async Task<IActionResult> GetByInn(string Inn)
        {
            var Model = await _ctProvider.GetByINN(Inn);
            return await SerializeModel(Model, "CityzenByINN.csv");
        }

        [AcceptVerbs(new string[] { "HttpGet", "HttpPost" })]
        public async Task<IActionResult> GetByBirthDate(DateTime BirthDate)
        {
            var Model = await _ctProvider.GetByBirthDate(BirthDate);
            return await SerializeModel(Model, "CityzenByBirthDate.csv");
        }

        [AcceptVerbs(new string[] { "HttpGet", "HttpPost" })]
        public async Task<IActionResult> GetByDeathDate(DateTime? DeathDate)
        {
            var Model = await _ctProvider.GetByDeathDate(DeathDate);
            return await SerializeModel(Model, "CityzenByDeathDate.csv");
        }

        [HttpGet("/[controller]/GetByDTO")]
        public async Task<IActionResult> GetByDTOGet(GetCityzenDTO InputModel)
        {
            var Model = await _ctProvider.GetByDTO(InputModel);
            return await SerializeModel(Model, "CityzenByDTO.csv");
        }
        [HttpPost("/[controller]/GetByDTO")]
        public async Task<IActionResult> GetByDTOPost([FromBody] GetCityzenDTO InputModel)
        {
            var Model = await _ctProvider.GetByDTO(InputModel);
            return await SerializeModel(Model, "CityzenByDTO.csv");
        }
    }
}
