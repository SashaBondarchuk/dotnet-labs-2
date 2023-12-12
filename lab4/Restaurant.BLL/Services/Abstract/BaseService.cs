using AutoMapper;

namespace Restaurant.BLL.Services.Abstract
{
    public abstract class BaseService
    {
        private protected readonly IMapper _mapper;

        protected BaseService(IMapper mapper)
        {
            _mapper = mapper;
        }
    }
}
