using System.Linq;
using Core.Models.Dtos.Requests.HL7;
//using Core.HL7TriggerEvents;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
//using Core.HL7TriggerEvents.Base;

namespace APIServices.Controllers.HL7Manager
{
    public class HL7Controller : ServicesBaseController
    {
        /// <summary>
        /// Fetching Segment with Segment Name & Version - ex:- 'MSH-9.1'
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetSegmentByNameWithVersionAsync")]
        public async Task<IHttpActionResult> GetSegmentByName([FromBody] Hl7Request request)
        {
            if (string.IsNullOrWhiteSpace(request.SegmentName) && string.IsNullOrWhiteSpace(request.FileName))
            {
                return BadRequest();
            }
            request.FileName = HL7FileLocation + request.FileName;
            var response = await _contextHL7Core.GetSegmentByNameWithVersionAsync(request);            
            return response != null ? (IHttpActionResult) Ok(response) : NotFound();
        }

        /// <summary>
        /// Fetching Segment with Segment Name - ex:- 'MSH'
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetSegmentsByNameAsync")]
        public async Task<IHttpActionResult> GetSegmentsByNameAsync([FromBody] Hl7Request request)
        {
            if (string.IsNullOrWhiteSpace(request.SegmentName) && string.IsNullOrWhiteSpace(request.FileName))
            {
                return BadRequest();
            }
            request.FileName = HL7FileLocation + request.FileName;
            var response = await _contextHL7Core.GetSegmentsByNameAsync(request);
            return response != null ? (IHttpActionResult)Ok(response) : NotFound();
        }

        /// <summary>
        /// Fetching All Segment Names
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetAllSegmentNamesAsync")]
        public async Task<IHttpActionResult> GetAllSegmentNamesAsync([FromBody] Hl7Request request)
        {
            if (string.IsNullOrWhiteSpace(request.FileName))
            {
                return BadRequest();
            }
            request.FileName = HL7FileLocation + request.FileName;
            var response = await _contextHL7Core.GetAllSegmentNamesAsync(request);
            return Ok(response);
        }

        /// <summary>
        /// Fetching All Segments
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetAllSegmentsAsync")]
        public async Task<IHttpActionResult> GetAllSegmentsAsync([FromBody] Hl7Request request)
        {
            if (string.IsNullOrWhiteSpace(request.FileName))
            {
                return BadRequest();
            }
            request.FileName = HL7FileLocation + request.FileName;
            var response = await _contextHL7Core.GetAllSegmentsAsync(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("GetAllSegmentsBySegmentNameWithVersionsAsync")]
        public async Task<IHttpActionResult> GetAllSegmentsBySegmentNameWithVersionsAsync(Hl7Request request)
        {
            if ((request.SegmentNames == null && request.SegmentNames.Any()) &&
                string.IsNullOrWhiteSpace(request.FileName))
            {
                return BadRequest();
            }
            request.FileName = HL7FileLocation + request.FileName;
            var response = await _contextHL7Core.GetAllSegmentsBySegmentNameWithVersionsAsync(request);
            return Ok(response);
        }

        [HttpPost]
        [Route("GetAllTriggerTypesAsync")]
        public async Task<IHttpActionResult> GetAllTriggerTypesAsync()
        {            
            var result = await _contextTriggerEvents.GetAllTriggerTypesAsync();
            return Ok(result);
        }

        [HttpPost]
        [Route("GetAllTriggeredSegmentsByTriggerNameAsync")]
        public async Task<IHttpActionResult> GetAllTriggeredSegmentsByTriggerNameAsync(string triggerName)
        {
            if (string.IsNullOrWhiteSpace(triggerName))
            {
                return BadRequest();
            }
            var result = await _contextTriggerEvents.GetAllSegmentsByTriggerEventAsync(triggerName);
            return Ok(result);
        }

        [HttpPost]
        [Route("GetAllSubSegmentsBySegmentNamesAsync")]
        public async Task<IHttpActionResult> GetAllSubSegmentsBySegmentNamesAsync(string[] segmentNames)
        {
            var segs = segmentNames.Where(s => !string.IsNullOrWhiteSpace(s)).Select(x => x).ToArray();
            if (!segs.Any())
            {
                return BadRequest();
            }
            var result = await _contextTriggerEvents.GetAllSubSigmentBySegmentAndTriggerName(segs);
            return Ok(result);
        }
    }
}