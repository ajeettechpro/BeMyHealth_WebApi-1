using AutoMapper;
using BeMyHealth_WebApi.Constants;
using BeMyHealth_WebApi.Dto;
using BeMyHealth_WebApi.Helpers;
using BeMyHealth_WebApi.Models;
using BeMyHealth_WebApi.Services.SubscriptionService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BeMyHealth_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionController : ControllerBase
    {
        private readonly ISubscriptionService _subscriptionService;
        private readonly IMapper _mapper;
        public SubscriptionController(ISubscriptionService subscriptionService, IMapper mapper)
        {
            _subscriptionService = subscriptionService;
            _mapper = mapper;

        }
        [HttpPost("AddSubscription")]
        public async Task<ActionResult> AddSubscription(CustomSubscriptionDto request )
        {
            try
            {
                if(request != null)
                {
                    CustomSubscription customSubscription = _mapper.Map<CustomSubscription>(request);
                    var data = await _subscriptionService.AddSubscription(customSubscription);
                    return Ok(ApiResponse.CreateResponse(data, StatusConstants.StatusCode200));
                }
                return Ok(ApiResponse.CreateResponse("Please Enter Data", StatusConstants.StatusCode404));
            }
            catch (Exception)
            {
                return Ok(ApiResponse.CreateResponse(false, StatusConstants.StatusCode500));
            }
        }
        [HttpGet("GetSubscription")]

        public async Task<ActionResult> GetSubscription()
        {
            try
            {
                var data = await _subscriptionService.GetSubscription();
                if (data == null)
                {
                    return Ok(ApiResponse.CreateResponse(false, StatusConstants.StatusCode404));
                }
                return Ok(ApiResponse.CreateResponse(data, StatusConstants.StatusCode200));
            }
            catch(Exception)
            {
                return Ok(ApiResponse.CreateResponse(false, StatusConstants.StatusCode500));
            }
        }
        [HttpPost("DeleteSubscription")]
        public async Task<ActionResult<bool>> DeleteSubscription(int id)
        {
            try
            {
                var data = await _subscriptionService.DeleteSubscription(id);
                if (data != null)
                {
                    return Ok(ApiResponse.CreateResponse(true, StatusConstants.StatusCode200));
                }
                return Ok(ApiResponse.CreateResponse("Data Not Found", StatusConstants.StatusCode404));
            }
            catch (Exception)
            {
                return Ok(ApiResponse.CreateResponse(false, StatusConstants.StatusCode500));
            }
        }

        [HttpPost("UpdateSubscription")]
        public async Task<ActionResult<bool>> UpdateSubscription(CustomSubscription customSubscription)
        {
            try
            {
                var data = await _subscriptionService.UpdateSubscription(customSubscription);
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
