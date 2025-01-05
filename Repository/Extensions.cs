using Mapster;

namespace Repository
{
    public static class Extensions
    {
        public static IList<TDestination> ToVM<TSource, TDestination>(this IList<TSource> model) => model.Adapt<List<TDestination>>();
        public static TDestination FromVM<TSource, TDestination>(this TSource model) => model.Adapt<TDestination>();
        public static TDestination ToVM<TSource, TDestination>(this TSource model) => model.Adapt<TDestination>();
    }
}
