using Mapster;

namespace Repository
{
    public static class Extensions
    {
        public static TDestination FromVM<TSource, TDestination>(this TSource model) => model.Adapt<TDestination>();
        public static TDestination ToVM<TSource, TDestination>(this TSource model) => model.Adapt<TDestination>();
    }
}
