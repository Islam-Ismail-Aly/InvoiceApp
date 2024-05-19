using AutoMapper;
using InvoiceApp.API.Utilities;
using InvoiceApp.Core.DTOs.Invoice;
using InvoiceApp.Core.DTOs.Store;
using InvoiceApp.Core.DTOs.Item;
using InvoiceApp.Core.DTOs.Unit;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ApiExplorerSettings(GroupName = "InvoiceAPIv1")]
    [Consumes("application/json")]
    public class InvoiceController : ControllerBase
    {
        private readonly IUnitOfWork<Invoice> _invoiceUnitOfWork;
        private readonly IUnitOfWork<Item> _itemUnitOfWork;
        private readonly IUnitOfWork<InvoiceDetails> _invoiceDetailsUnitOfWork;
        private readonly IUnitOfWork<Store> _storeUnitOfWork;
        private readonly IUnitOfWork<Unit> _unitsUnitOfWork;
        private readonly IMapper _mapper;

        public InvoiceController(IUnitOfWork<Invoice> invoiceUnitOfWork,
                                 IUnitOfWork<Item> itemUnitOfWork,
                                 IUnitOfWork<InvoiceDetails> invoiceDetailsUnitOfWork,
                                 IMapper mapper, IUnitOfWork<Unit> unitsUnitOfWork, IUnitOfWork<Store> storeUnitOfWork)
        {
            _invoiceUnitOfWork = invoiceUnitOfWork;
            _itemUnitOfWork = itemUnitOfWork;
            _invoiceDetailsUnitOfWork = invoiceDetailsUnitOfWork;
            _mapper = mapper;
            _unitsUnitOfWork = unitsUnitOfWork;
            _storeUnitOfWork = storeUnitOfWork;
        }

        [HttpGet("GetAllInvoicesWithStore")]
        public async Task<ActionResult<APIResponseResult<IEnumerable<InvoiceDto>>>> GetAllInvoices()
        {
            var invoices = _invoiceUnitOfWork.Entity.GetAllIncluding(i => i.Store);
            if (invoices == null || !invoices.Any())
                return NotFound(new APIResponseResult<IEnumerable<InvoiceDto>>("No Invoices found."));

            var invoiceDtos = _mapper.Map<IEnumerable<InvoiceDto>>(invoices);
            return Ok(new APIResponseResult<IEnumerable<InvoiceDto>>(invoiceDtos, "Invoices retrieved successfully."));
        }


        [HttpGet("GetAllInvoicesWithItems")]
        public async Task<ActionResult<APIResponseResult<IEnumerable<InvoiceItemsDto>>>> GetAllInvoicesWithItems()
        {
            var invoices = _invoiceUnitOfWork.Entity.GetAllIncluding(i => i.Store, i => i.Items);
            if (invoices == null || !invoices.Any())
                return NotFound(new APIResponseResult<IEnumerable<InvoiceItemsDto>>("No Invoices found."));

            var invoiceDtos = _mapper.Map<IEnumerable<InvoiceItemsDto>>(invoices);
            return Ok(new APIResponseResult<IEnumerable<InvoiceItemsDto>>(invoiceDtos, "Invoices retrieved successfully."));
        }

        [HttpGet("GetLastInvoice")]
        public async Task<ActionResult<APIResponseResult<LastInvoiceDto>>> GetLastInvoiceWithNextNumber()
        {
            var lastInvoice = _invoiceUnitOfWork.Entity.GetAllIncluding(i => i.Store)
                                                       .OrderByDescending(i => i.InvoiceNo)
                                                       .FirstOrDefault();
            if (lastInvoice == null)
            {
                return NotFound(new APIResponseResult<LastInvoiceDto>("No Invoice found."));
            }

            // Map the last invoice to the DTO
            var lastInvoiceDto = _mapper.Map<LastInvoiceDto>(lastInvoice);

            // Increment the last invoice number to suggest the next number
            lastInvoiceDto.InvoiceNo = lastInvoice.InvoiceNo + 1;

            //return Ok(new APIResponseResult<LastInvoiceDto>(lastInvoiceDto, "Invoice retrieved successfully with next invoice number."));
            return Ok(lastInvoiceDto);
        }

        [HttpGet("GetAllItems")]
        public async Task<ActionResult<APIResponseResult<IEnumerable<ListItemDto>>>> GetAllItems()
        {
            var items = await _itemUnitOfWork.Entity.GetAllAsync();

            if (items == null)
            {
                return NotFound(new APIResponseResult<ListItemDto>("Items not found."));
            }

            var itemDtos = _mapper.Map<IEnumerable<ListItemDto>>(items);

            //return Ok(new APIResponseResult<IEnumerable<ListItemDto>>(itemDtos, "Items retrieved successfully."));
            return Ok(itemDtos);
        }

        [HttpGet("GetAllStores")]
        public async Task<ActionResult<APIResponseResult<IEnumerable<StoreListDto>>>> GetAllStores()
        {
            var stores = await _storeUnitOfWork.Entity.GetAllAsync();

            if (stores == null)
                return NotFound(new APIResponseResult<StoreListDto>("Stores not found."));

            var itemDtos = _mapper.Map<IEnumerable<StoreListDto>>(stores);

            //return Ok(new APIResponseResult<IEnumerable<ListItemDto>>(itemDtos, "Items retrieved successfully."));
            return Ok(itemDtos);
        }

        [HttpGet("GetAllUnits")]
        public async Task<ActionResult<APIResponseResult<IEnumerable<UnitDto>>>> GetAllUnits()
        {
            var units = await _unitsUnitOfWork.Entity.GetAllAsync();

            if (units == null)
                return NotFound(new APIResponseResult<UnitDto>("Units not found."));

            var itemDtos = _mapper.Map<IEnumerable<UnitDto>>(units);

            //return Ok(new APIResponseResult<IEnumerable<ListItemDto>>(itemDtos, "Items retrieved successfully."));
            return Ok(itemDtos);
        }

        // Create a new invoice with items
        [HttpPost("CreateInvoice")]
        public async Task<ActionResult<APIResponseResult<InvoiceDto>>> CreateInvoice([FromBody] InvoiceDto invoiceDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                //var invoice = _mapper.Map<Invoice>(invoiceDto);
                var invoice = new Invoice()
                {
                    InvoiceDate = invoiceDto.InvoiceDate,
                    StoreId = invoiceDto.StoreId
                };
                await _invoiceUnitOfWork.Entity.InsertAsync(invoice);
                await _invoiceUnitOfWork.SaveAsync();

                foreach (var invoiceDetailDto in invoiceDto.Items)
                {
                    var invoiceDetail = _mapper.Map<InvoiceDetails>(invoiceDetailDto);
                    invoiceDetail.InvoiceId = invoice.InvoiceNo;
                    await _invoiceDetailsUnitOfWork.Entity.InsertAsync(invoiceDetail);
                    await _invoiceDetailsUnitOfWork.SaveAsync();
                }

                return Ok(invoiceDto);
            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message.ToString();
                return NotFound(errorMessage);
            }

            //return Ok(new APIResponseResult<InvoiceDto>(_mapper.Map<InvoiceDto>(invoice), "Invoice created successfully."));
        }

        // Update an existing invoice
        [HttpPut("UpdateInvoice/{id:int}")]
        public async Task<ActionResult<APIResponseResult<InvoiceDto>>> UpdateInvoice(int id, [FromBody] InvoiceDto invoiceDto)
        {
            var invoice = await _invoiceUnitOfWork.Entity.GetByIdAsync(id);
            if (invoice == null) return NotFound(new APIResponseResult<InvoiceDto>("Invoice not found."));

            _mapper.Map(invoiceDto, invoice);
            await _invoiceUnitOfWork.Entity.UpdateAsync(invoice);
            await _invoiceUnitOfWork.SaveAsync();
            var updatedInvoiceDto = _mapper.Map<InvoiceDto>(invoice);
            return Ok(new APIResponseResult<InvoiceDto>(updatedInvoiceDto, "Invoice updated successfully."));
        }

        // Delete an invoice
        [HttpDelete("DeleteInvoice/{id:int}")]
        public async Task<ActionResult<APIResponseResult<InvoiceDto>>> DeleteInvoice(int id)
        {
            var invoice = await _invoiceUnitOfWork.Entity.GetByIdAsync(id);
            if (invoice == null) return NotFound(new APIResponseResult<InvoiceDto>("Invoice not found."));

            await _invoiceUnitOfWork.Entity.DeleteAsync(invoice);
            await _invoiceUnitOfWork.SaveAsync();
            var deletedInvoiceDto = _mapper.Map<InvoiceDto>(invoice);
            return Ok(new APIResponseResult<InvoiceDto>(deletedInvoiceDto, "Invoice deleted successfully."));
        }

    }
}
