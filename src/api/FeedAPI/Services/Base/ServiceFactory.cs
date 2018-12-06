using FeedAPI.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeedAPI.Services.Base
{
    public class ServiceFactory
    {
        public static IApiService Create(Type type)
        {
            string name = type.Name;

            switch (name)
            {
                case "FeedsController":
                    return new FeedsService();
                case "WordsController":
                    return new WordsService();
                default:
                    return null;
            }
        }
    }
}
