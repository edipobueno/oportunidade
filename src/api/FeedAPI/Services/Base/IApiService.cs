using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace FeedAPI.Services.Base
{
    public interface IApiService : IDisposable
    {
        void Load();

        IActionResult GetTop(int quantity);

        IActionResult Get();
    }
}
