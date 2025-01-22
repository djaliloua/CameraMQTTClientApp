using BaseViewModels;
using Microsoft.EntityFrameworkCore;
using OpenAIClient;
using Repository.Implementation;

namespace RepositoryEntityVMWrapper.Abstractions
{
    public abstract class ICameraRepoApi : GenericRepositoryViewModel<MQTTConfig, MQTTConfigViewModel>
    {
        protected ICameraRepoApi(DbContext dbContext) : base(null)
        {

        }
        protected ICameraRepoApi()
        {

        }

    }
    public abstract class ICameraRepo : GenericRepositoryViewModel<Models.MQTTConfig, MQTTConfigViewModel>
    {
        protected ICameraRepo(DbContext dbContext) : base(dbContext)
        {

        }
        protected ICameraRepo()
        {

        }

    }
}
