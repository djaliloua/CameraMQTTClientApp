using BaseViewModels;
using KiotaOpenAIClient.Client.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Implementation;

namespace RepositoryEntityVMWrapper.Abstractions
{
    public abstract class ICameraRepo : GenericRepositoryViewModel<MQTTConfig, MQTTConfigViewModel>
    {
        protected ICameraRepo(DbContext dbContext) : base(dbContext)
        {

        }
        protected ICameraRepo()
        {

        }

    }
}
