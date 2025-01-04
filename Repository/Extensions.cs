using Mapster;

namespace Repository
{
    public static class Extensions
    {
        public static IList<TDestination> ToVM<TSource, TDestination>(this IList<TSource> model) => model.Adapt<List<TDestination>>();
    }
}
