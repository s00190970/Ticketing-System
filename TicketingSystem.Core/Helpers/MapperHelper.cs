using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace TicketingSystem.Core.Helpers
{
    public class MapperHelper<TModel, TDto>
    {
        public static TDto ConvertToDto(TModel model)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<TModel, TDto>();
            });

            IMapper mapper = config.CreateMapper();
            return mapper.Map<TModel, TDto>(model);
        }

        public static List<TDto> ConvertToDtos(List<TModel> models)
        {
            List<TDto> dtos = new List<TDto>();
            foreach (var model in models)
            {
                dtos.Add(ConvertToDto(model));
            }

            return dtos;
        }

        public static TModel ConvertToModel(TDto dto)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<TDto, TModel>();
            });

            IMapper mapper = config.CreateMapper();
            return mapper.Map<TDto, TModel>(dto);
        }
    }
}
