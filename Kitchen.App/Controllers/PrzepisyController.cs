using AutoMapper;
using Kitchen.App.Models;
using Kitchen.Library.Data;
using Kitchen.Library.DataModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kitchen.App.Controllers
{
    public class PrzepisyController : Controller
    {
        private readonly IPrzepisyData _przepisyData;
        private readonly IMapper _mapper;
        private readonly ICategoryStructData _categoryStructData;
        private readonly IPochodzenieData _pochodzenieData;

        public PrzepisyController(IPrzepisyData przepisyData,
            IMapper mapper,
            ICategoryStructData categoryStructData,
            IPochodzenieData pochodzenieData)
        {
            _przepisyData = przepisyData;
            _mapper = mapper;
            _categoryStructData = categoryStructData;
            _pochodzenieData = pochodzenieData;
        }
        [HttpGet("Przepisy/index/{id}")]
        public async Task<IActionResult> Index(int id)
        {
            IEnumerable<PrzepisData> _data = await _przepisyData.Get(id);
            return View(_mapper.Map<IEnumerable<PrzepisViewModel>>(_data));
        }
        public async Task<IActionResult> Details(int id)
        {
            var det = await _przepisyData.GetById(id);
            return View(_mapper.Map<PrzepisDetailsViewModel>(det));
        }
        [Authorize]
        [HttpGet("/przepisy/add")]
        public async Task<IActionResult> Add()
        {
            ViewData["Categories"] = await _categoryStructData.GetCategories();
            ViewData["Regions"] = _mapper.Map<IEnumerable<RegionViewModel>>(await _pochodzenieData.Get());

            return View();
        }
        [Authorize]
        [HttpPost("/przepisy/add")]
        [ValidateAntiForgeryToken]
        public async Task Add(PrzepisDetailsViewModel model, IFormCollection collection)
        {
            model.UserId = HttpContext.User.Claims.Where(x => x.Type.Contains("nameidentifier")).Single().Value;

            await _przepisyData.Add(_mapper.Map<PrzepisData>(model));
        }
        //TODO Moje przepisy i usuwanie
        public async Task<IActionResult> GetMy()
        {
            var userId = HttpContext.User.Claims.Where(x => x.Type.Contains("nameidentifier")).Single().Value;
            return View();
        }
    }
}
