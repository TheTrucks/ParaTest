using Microsoft.AspNetCore.Mvc;
using ParaTest.Models.Cityzen;

namespace ParaTest.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    [Produces("application/json")]
    public class CityzenJsonSetController : ControllerBase
    {
        private readonly ICityzenDataSetter _ctProvider;
        public CityzenJsonSetController(ICityzenDataSetter CtProvider)
        {
            _ctProvider = CtProvider;
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