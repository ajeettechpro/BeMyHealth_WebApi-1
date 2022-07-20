using AutoMapper;
using BeMyHealth_WebApi.Constants;
using BeMyHealth_WebApi.ContextData;
using BeMyHealth_WebApi.Dto;
using BeMyHealth_WebApi.Helpers;
using BeMyHealth_WebApi.Models;
using BeMyHealth_WebApi.Services.CustomDietPlanService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BeMyHealth_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController] 
    public class CustomDietPlanController : ControllerBase
    {
        private readonly ICustomDietPlanService _customDietPlanService;
        private readonly IMapper _mapper;
        public CustomDietPlanController(ICustomDietPlanService customDietPlanService, IMapper mapper)
        {
            _customDietPlanService = customDietPlanService;
            _mapper = mapper;
        }
        [HttpPost("AddCustomDietPlan")]
        public async Task<ActionResult> AddCustomDietPlan(CustomDietPlanDto request)
        {
            try
            {
                if (request != null)
                {
                    CustomDietPlan customDietPlan = _mapper.Map<CustomDietPlan>(request);
                    var data = await _customDietPlanService.AddCustomDietPlan(customDietPlan);
                    return Ok(ApiResponse.CreateResponse(data, StatusConstants.StatusCode200));
                }
                return Ok(ApiResponse.CreateResponse("Please Enter Data", StatusConstants.StatusCode404));
            }
            catch (Exception)
            {
                return Ok(ApiResponse.CreateResponse(false, StatusConstants.StatusCode500));
            }
        }
        [HttpGet("GetCustomDietPlan")]
        public async Task<ActionResult> GetCustomDietPlan()
        {
            try
            {
                var data = await _customDietPlanService.GetCustomDietPlan();
                if (data == null)
                {
                    return Ok(ApiResponse.CreateResponse(false, StatusConstants.StatusCode404));
                }
                return Ok(ApiResponse.CreateResponse(data, StatusConstants.StatusCode200));
            }
            catch (Exception)
            {
                return Ok(ApiResponse.CreateResponse(false, StatusConstants.StatusCode500));
            }
        }
        [HttpPost("DeleteCustomDietPlan")]
        public async Task<ActionResult<bool>> DeleteDietPlan(int id)
        {
            try
            {
                var data = await _customDietPlanService.DeleteDietPlan(id);
                if(data != null)
                {
                    return Ok(ApiResponse.CreateResponse(true, StatusConstants.StatusCode200));
                }
                return Ok(ApiResponse.CreateResponse("Data Not Found", StatusConstants.StatusCode404));
            }
            catch(Exception)
            {
                return Ok(ApiResponse.CreateResponse(false, StatusConstants.StatusCode500));
            }
        }       
        [HttpPost("UpdateCustomDietPlan")]
        public async Task<ActionResult<bool>> UpdateCustomDietPlan(CustomDietPlan customDietPlan)
        {
            try
            {
                var data = await _customDietPlanService.UpdateCustomDietPlan(customDietPlan);
                if (data != false)
                {
                    return Ok(ApiResponse.CreateResponse(true, StatusConstants.StatusCode200));
                }
                return Ok(ApiResponse.CreateResponse("Id Not Found", StatusConstants.StatusCode404));
            }
            catch (Exception)
            {
                return Ok(ApiResponse.CreateResponse(string.Empty, StatusConstants.StatusCode500));
            }
        }
    }
}
