using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Core.Data.IDataInterfaces.HL7Manager;
//using Core.HL7Utility.Base;
using Core.Models.Dtos.Requests.HL7;
using Core.Models.Extensions;

namespace Core.Data.Data.HL7Manager
{
    //public class HL7CoreData : DisposeBaseClass, IHL7CoreData
    //{
    //    //public async Task<Component> GetSegmentByNameWithVersionAsync(Hl7Request request)
    //    //{
    //    //    var components = new Component();
    //    //    try
    //    //    {                
    //    //        var strMsg = File.ReadAllText(request.FileName);
    //    //        using (var message = new Message(strMsg))
    //    //        {
    //    //            if (message.Segments.Any())
    //    //            {
    //    //                components = message.Segments
    //    //                                        .SelectMany(m => m.Fields, (m, f) => new {m, f})
    //    //                                            .SelectMany(@t => @t.f.Components, (@t, c) => new {@t, c})
    //    //                                                .Where(@t => @t.c.Id.Equals(request.SegmentName))
    //    //                                                    .Select(@t => @t.c).FirstOrDefault();
    //    //            }
    //    //        }
    //    //    }
    //    //    catch (Exception ex)
    //    //    {
    //    //        var strError = ex.Message;
    //    //    }            
    //    //    return await Task.Run(()=>  components);
    //    //}

    //    //public async Task<List<Segment>> GetSegmentsByNameAsync(Hl7Request request)
    //    //{
    //    //    var components = new List<Segment>();
    //    //    try
    //    //    {
    //    //        var strMsg = File.ReadAllText(request.FileName);
    //    //        using (var message = new Message(strMsg))
    //    //        {
    //    //            if (message.Segments.Any())
    //    //            {
    //    //                components =
    //    //                    message.Segments.Where(
    //    //                        x => x.Name.StartsWith(request.SegmentName, StringComparison.OrdinalIgnoreCase))
    //    //                        .ToList();
    //    //            }
    //    //        }
    //    //    }
    //    //    catch (Exception ex)
    //    //    {
    //    //        var error = ex.Message;
    //    //    }
    //    //    return await Task.Run(() => components);
    //    //}

    //    //public async Task<List<string>> GetAllSegmentNamesAsync(Hl7Request request)
    //    //{
    //    //    try
    //    //    {                
    //    //        var strMsg = File.ReadAllText(request.FileName);
    //    //        using (var message = new Message(strMsg))
    //    //        {
    //    //            if (message.Segments.Any())
    //    //            {
    //    //                return await Task.Run(() => message.SegmentNames);
    //    //            }
    //    //        }
    //    //    }
    //    //    catch (Exception ex)
    //    //    {
    //    //        var error = ex.Message;
    //    //    }
    //    //    return await Task.Run(() =>  new List<string>());
    //    //}

    //    //public async Task<List<Segment>> GetAllSegmentsAsync(Hl7Request request)
    //    //{
    //    //    try
    //    //    {                
    //    //        var strMsg = File.ReadAllText(request.FileName);
    //    //        using (var message = new Message(strMsg))
    //    //        {
    //    //            if (message.Segments.Any())
    //    //            {
    //    //                return await Task.Run(() => message.Segments);
    //    //            }
    //    //        }
    //    //    }
    //    //    catch (Exception ex)
    //    //    {
    //    //        var error = ex.Message;
    //    //    }
    //    //    return await Task.Run(() => new List<Segment>());
    //    //}

    //    //public async Task<List<Component>> GetAllSegmentsBySegmentNameWithVersionsAsync(Hl7Request request)
    //    //{
    //    //    var components = new List<Component>();
    //    //    try
    //    //    {
    //    //        var strMsg = File.ReadAllText(request.FileName);
    //    //        using (var message = new Message(strMsg))
    //    //        {
    //    //            if (message.Segments.Any())
    //    //            {                        
    //    //                components.AddRange(request.SegmentNames.Select(
    //    //                    item => message.Segments.SelectMany(m => m.Fields, (m, f) => new {m, f})
    //    //                        .SelectMany(@t => @t.f.Components, (@t, c) => new {@t, c})
    //    //                        .Where(@t => @t.c.Id.Equals(item, StringComparison.OrdinalIgnoreCase))
    //    //                        .Select(@t => @t.c)
    //    //                        .FirstOrDefault()).Where(co => co != null).Select(co => new Component
    //    //                        {
    //    //                            Id = co.Id,
    //    //                            Name = co.Name,
    //    //                            Value = co.Value,
    //    //                            IdParts =
    //    //                            {
    //    //                                ComponentIndex = co.IdParts.ComponentIndex,
    //    //                                FieldIndex = co.IdParts.FieldIndex,
    //    //                                SegmentName = co.IdParts.SegmentName
    //    //                            }
    //    //                        }));
    //    //            }
    //    //        }
    //    //    }
    //    //    catch (Exception ex)
    //    //    {
    //    //        var error = ex.Message;
    //    //    }
    //    //    return await Task.Run(() => components);
    //    //}
    //}
}
