using ContractMaster.DTOs;
using ContractMaster.Exceptions;
using ContractMaster.Models;
using ContractMaster.Models.Enums;
using ContractMaster.Repositories.Interfaces;
using ContractMaster.Services.Interfaces;

namespace ContractMaster.Services;

public class ContractService:IContractService
{
    private readonly IContractRepository _repository;
    private readonly IPdfService _pdfService;
    private readonly IBlobStorageService _blobStorageService;
    public ContractService(IContractRepository repository, IPdfService pdfService, IBlobStorageService blobStorageService)
    {
        _pdfService=pdfService;
        _repository = repository;
        _blobStorageService=blobStorageService;
    }

    public async Task CreateContract(NewContractDTO newContract, string email)
    {
        var exists = await _repository.ExistsAsync(newContract.ContractNumber, newContract.Version);

        if (exists)
        {
            throw new InvalidOperationException("Contract already exists for this version.");
        }

        Contract contract = new()
        {
            ContractNumber = newContract.ContractNumber,
            Version = newContract.Version,
            ContractType = newContract.ContractType,
            Status = newContract.Status,
            StartDate = newContract.StartDate,
            EndDate = newContract.EndDate,
            CounterPartyName = newContract.CounterPartyName,
            CounterPartyEmail = newContract.CounterPartyEmail,
            CreatedByEmail = email,
            CreatedAt = newContract.CreatedAt
        };
        var pdf = _pdfService.GenerateContractPdf(contract);
        var blobName = $"contracts/{contract.ContractNumber}/v{contract.Version}.pdf";
        await _blobStorageService.UploadPdfAsync(pdf, contract.ContractNumber.ToString(), contract.Version);

        contract.PdfBlobName = blobName;

        await _repository.AddAsync(contract);

    }

    public async Task<List<ContractDTO>> GetContracts()
    {
        var contracts = await _repository.GetAllAsync();

        return contracts.Select(contract => new ContractDTO(
            contract.Id,
            contract.ContractNumber,
            contract.Version,
            contract.ContractType,
            contract.Status,
            contract.StartDate,
            contract.EndDate,
            contract.CreatedByEmail,
            contract.CounterPartyName,
            contract.CounterPartyEmail,
            contract.CreatedAt
        )).ToList();
    }

    public async Task DeleteById(int id)
    {
        var contract = await _repository.GetByIdAsync(id);

        if(contract == null)
        {
            throw new FileNotFoundException();
        }

        await _repository.DeleteAsync(contract);
    }
    public async Task UpdateStatus(int contractId, int version, ContractStatus status)
    {
        var contract = await _repository.GetByContractAndVersionAsync(contractId, version);

        if(contract is null)
        {
            throw new BusinessRuleException("Contract not found.");
        }

        contract.Status = status;

        await _repository.UpdateAsync(contract);
    }
}