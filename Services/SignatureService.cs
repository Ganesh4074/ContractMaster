using ContractMaster.DTOs;
using ContractMaster.Exceptions;
using ContractMaster.Models;
using ContractMaster.Models.Enums;
using ContractMaster.Repositories.Interfaces;

namespace ContractMaster.Services;

public class SignatureService
{
    private readonly ISignatureRepository _repository;
    private readonly ContractService _contractService;
    public SignatureService(ISignatureRepository repository, ContractService contractService)
    {
        _repository = repository;
        _contractService=contractService;
    }

    public async Task CreateSignatures(int contractId, int version)
    {
        //Check if same contract version exists
        var exists = await _repository.ExistsAsync(contractId, version);

        if (exists)
        {
            return;
        }

        var signatures = new List<Signature>
        {
            new Signature
            {
                ContractId = contractId,
                Version = version,
                SignatoryId = 0,
                SignatureType = SignatureType.Internal,
                SignatureStatus = SignatureStatus.NotSigned
            },

            new Signature
            {
                ContractId = contractId,
                Version = version,
                SignatoryId = 0,
                SignatureType = SignatureType.External,
                SignatureStatus = SignatureStatus.NotSigned
            }
        };

        await _repository.AddRangeAsync(signatures);
    }

    public async Task<List<GetSignatureDTO>> GetSignatures()
    {
        var signatures = await _repository.GetAllAsync();

        return signatures.Select(signature =>
            new GetSignatureDTO(
                signature.Id,
                signature.ContractId,
                signature.Version,
                signature.SignatoryId,
                signature.SignatureType,
                signature.SignatureStatus
            )
        ).ToList();
    }

    public async Task<GetSignatureDTO> UpdateSignature(
    int id,
    UpdateSignatureDTO update,
    int userId)
{
    var signature = await _repository.GetByIdAsync(id);

    if (signature is null)
    {
        return null!;
    }

    if (signature.SignatureStatus == SignatureStatus.Signed)
    {
        throw new BusinessRuleException(
            "Signature is already signed.");
    }

    if (update.SignatureStatus != SignatureStatus.Signed)
    {
        throw new BusinessRuleException(
            "Signature can only be marked as signed.");
    }

    signature.SignatureStatus = SignatureStatus.Signed;
    signature.SignatoryId = userId;

    await _repository.UpdateAsync(signature);

    var signatures = await _repository.GetByContractAndVersionAsync(
            signature.ContractId,
            signature.Version);

    var allSigned = signatures.All(
        s => s.SignatureStatus == SignatureStatus.Signed);

    if (allSigned)
    {
        await _contractService.UpdateStatus(signature.ContractId, signature.Version, ContractStatus.Active);
    }

    return new GetSignatureDTO(
        signature.Id,
        signature.ContractId,
        signature.Version,
        signature.SignatoryId,
        signature.SignatureType,
        signature.SignatureStatus
    );
}
}