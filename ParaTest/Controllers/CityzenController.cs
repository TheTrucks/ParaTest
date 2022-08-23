using Microsoft.AspNetCore.Mvc;
using ParaTest.Models.Cityzen;

namespace ParaTest.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    [Produces("application/json")]
    public class CityzenController : ControllerBase
    {
        private readonly CityzenDataProvider _ctProvider;
        public CityzenController(CityzenDataProvider CtProvider)
        {
            _ctProvider = CtProvider;
        }

        [AcceptVerbs(new string[] { "HttpGet", "HttpPost" })]
        public async Task<JsonResult> GetByFullName(string FullName)
        {
            return new JsonResult(await _ctProvider.GetByFullName(FullName));
        }

        [AcceptVerbs(new string[] { "HttpGet", "HttpPost" })]
        public async Task<JsonResult> GetBySnils(string Snils)
        {
            var GetCityzen = await _ctProvider.GetBySNILS(Snils);
            var Result = new JsonResult(GetCityzen);
            Result.StatusCode = GetCityzen != null 
                ? (int)System.Net.HttpStatusCode.OK 
                : (int)System.Net.HttpStatusCode.NotFound;
            return Result;
        }

        [AcceptVerbs(new string[] { "HttpGet", "HttpPost" })]
        public async Task<JsonResult> GetByInn(string Inn)
        {
            var GetCityzen = await _ctProvider.GetByINN(Inn);
            var Result = new JsonResult(GetCityzen);
            Result.StatusCode = GetCityzen != null
                ? (int)System.Net.HttpStatusCode.OK
                : (int)System.Net.HttpStatusCode.NotFound;
            return Result;
        }

        [AcceptVerbs(new string[] { "HttpGet", "HttpPost" })]
        public async Task<JsonResult> GetByBirthDate(DateTime BirthDate)
        {
            return new JsonResult(await _ctProvider.GetByBirthDate(BirthDate));
        }

        [AcceptVerbs(new string[] { "HttpGet", "HttpPost" })]
        public async Task<JsonResult> GetByDeathDate(DateTime? DeathDate)
        {
            return new JsonResult(await _ctProvider.GetByDeathDate(DeathDate));
        }

        [HttpGet("/[controller]/GetByDTO")]
        public async Task<JsonResult> GetByDTOGet(GetCityzenDTO InputModel)
        {
            return new JsonResult(await _ctProvider.GetByDTO(InputModel));
        }
        [HttpPost("/[controller]/GetByDTO")]
        public async Task<JsonResult> GetByDTOPost([FromBody] GetCityzenDTO InputModel)
        {
            return new JsonResult(await _ctProvider.GetByDTO(InputModel));
        }

        [AcceptVerbs(new string[] { "HttpPost", "HttpPut" })]
        public async Task<JsonResult> Insert([FromBody] Cityzen Input)
        {
            return GetSetJsonResult(await _ctProvider.Insert(Input));
        }

        [AcceptVerbs(new string[] { "HttpPost", "HttpPut" })]
        public async Task<JsonResult> Update([FromBody] Cityzen Input)
        {
            return GetSetJsonResult(await _ctProvider.Update(Input));
        }

        [AcceptVerbs(new string[] { "HttpPost", "HttpDelete" })]
        public async Task<JsonResult> Delete(long Id)
        {
            return GetSetJsonResult(await _ctProvider.Delete(Id));
        }

        private JsonResult GetSetJsonResult(ISetterResponse Input)
        {
            var Result = new JsonResult(Input);
            Result.StatusCode = Input.Success
                ? (int)System.Net.HttpStatusCode.OK
                : (int)System.Net.HttpStatusCode.NotFound;
            return Result;
        }
    }
}