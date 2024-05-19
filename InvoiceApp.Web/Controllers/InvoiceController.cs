using InvoiceApp.Core.DTOs.Invoice;
using InvoiceApp.Core.DTOs.Item;
using InvoiceApp.Core.DTOs.Store;
using InvoiceApp.Core.DTOs.Unit;
using InvoiceApp.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace InvoiceApp.Web.Controllers
{
    public class InvoiceController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;

        public InvoiceController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var client = _clientFactory.CreateClient("BaseApiUrl");

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync("api/Invoice/GetLastInvoice");
            HttpResponseMessage responseStore = await client.GetAsync("api/Invoice/GetAllStores");
            HttpResponseMessage responseUnit = await client.GetAsync("api/Invoice/GetAllUnits");

            if (response.IsSuccessStatusCode && responseStore.IsSuccessStatusCode && responseUnit.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                var lastInvoice = JsonConvert.DeserializeObject<LastInvoiceDto>(content);

                string contentStore = await responseStore.Content.ReadAsStringAsync();
                var stores = JsonConvert.DeserializeObject<List<StoreListDto>>(contentStore);

                string contentUnit = await responseUnit.Content.ReadAsStringAsync();
                var units = JsonConvert.DeserializeObject<List<UnitDto>>(contentUnit);

                if (lastInvoice != null)
                {
                    var viewModel = new InvoiceViewModel
                    {
                        InvoiceNo = lastInvoice.InvoiceNo,
                        InvoiceDate = lastInvoice.InvoiceDate,
                    };

                    ViewBag.Stores = stores;
                    ViewBag.Units = units;

                    return View(viewModel);
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return StatusCode((int)response.StatusCode, "Error occurred while communicating with the API.");
            }
        }


        [HttpGet]
        public async Task<JsonResult> GetItems()
        {
            var client = _clientFactory.CreateClient("BaseApiUrl");

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync("api/Invoice/GetAllItems");
            HttpResponseMessage responseUnit = await client.GetAsync("api/Invoice/GetAllUnits");

            List<ItemViewModel> items = new List<ItemViewModel>();
            List<UnitDto> units = new List<UnitDto>();

            if (response.IsSuccessStatusCode)
            {
                var itemResponse = await response.Content.ReadAsStringAsync();
                items = JsonConvert.DeserializeObject<List<ItemViewModel>>(itemResponse);
            }

            if (responseUnit.IsSuccessStatusCode)
            {
                var unitResponse = await responseUnit.Content.ReadAsStringAsync();
                units = JsonConvert.DeserializeObject<List<UnitDto>>(unitResponse);
            }

            return Json(items); // Only returning items as per the route name
        }


        [HttpGet]
        public async Task<JsonResult> GetItemDetails(string name)
        {
            var client = _clientFactory.CreateClient("BaseApiUrl");

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync("api/Invoice/GetAllItems");

            List<ItemViewModel> items = new List<ItemViewModel>();

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                var results = JsonConvert.DeserializeObject<List<ListItemDto>>(content);

                if (results != null)
                {
                    foreach (var itemData in results)
                    {
                        items.Add(new ItemViewModel
                        {
                            Names = itemData.Names,
                            Price = itemData.Price,
                            Units = itemData.Units
                        });
                    }
                }
            }
            var item = items.FirstOrDefault(i => i.Names.Contains(name));

            return Json(item);
        }

        [HttpPost]
        public async Task<IActionResult> CreateInvoice(InvoiceDto invoiceDto)
        {
            var client = _clientFactory.CreateClient("BaseApiUrl");

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.PostAsJsonAsync("api/Invoice/CreateInvoice", invoiceDto);

            if (response.IsSuccessStatusCode)
            {
                return Ok();
            }
            else
            {
                string errorMessage = await response.Content.ReadAsStringAsync();
                return StatusCode((int)response.StatusCode, errorMessage);
            }
        }


    }
}
