using Calabonga.EntityFramework;
using Calabonga.PagedListLite;
using ExpressMapper;

namespace Calabonga.Framework.Demo
{
    public class Mapper : IEntityFrameworkMapper
    {
        private IMappingServiceProvider _mapper;

        public Mapper()
        {
            if (_mapper == null)
            {
                _mapper = ExpressMapper.Mapper.Instance;
                RegisterMaps();
            }
        }

        public void RegisterMaps()
        {
            _mapper.Register<Person, PersonViewModel>();
            _mapper.Register<UpdateViewModel, Person>();
            _mapper.RegisterCustom<PagedList<Person>, PagedList<PersonViewModel>, PagedListResolver>();
        }

        public TDestionation Map<TSource, TDestionation>(TSource source)
        {
            return _mapper.Map<TSource, TDestionation>(source);
        }

        public void Map<TDestionation, TSource>(TDestionation model, TSource item)
            where TDestionation : class, IEntityId
            where TSource : class, IEntityId
        {
            _mapper.Map(typeof(TDestionation), typeof(TSource), model, item);
        }
    }
}