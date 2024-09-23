using AutoMapper;
using TestNodesWeb.Api.Common.Models;
using TestNodesWeb.Api.Features.Journals.Queries.Models;

namespace TestNodesWeb.Api.Features.Journals.Queries.Profiles
{
    public class JournalMapProfile : Profile
    {
        public JournalMapProfile()
        {
            CreateMap<JournalShortInfo, MJournalInfo>();
        }
    }
}