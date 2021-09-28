using eShopSolution.ViewModels.Utilities.Slides;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.ApiIntegration
{
    public interface ISlideApiClient
    {
        Task<List<SlideVm>> GetAll();
    }
}
