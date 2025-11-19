using AutoMapper;
using AppointmentBooking.Core.Collections;

namespace AppointmentBooking.Core.AutoMapper;

public class DefaultMappings : Profile
{
    public DefaultMappings()
    {
        CreateMap(typeof(PagedList<>), typeof(IPagedList<>))
            .ConvertUsing(typeof(PagedListConverter<,>));
    }
}