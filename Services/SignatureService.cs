using ContractMaster.DTOs;
using ContractMaster.Exceptions;
using ContractMaster.Models;
using ContractMaster.Models.Enums;
using ContractMaster.Repositories.Interfaces;

namespace ContractMaster.Services;

public class SignatureService
{
    private readonly ISignatureRepository _repository;

    public SignatureService(ISignatureRepository repository)
    {
        _repository = repository;
    }

    public async Task CreateSignatures(
        int contractId,
        int version)
    {
        var exists = await _repository.ExistsAsync(
            contractId,
            version);

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
        UpdateSignatureDTO update)
    {
        var signature = await _repository.GetByIdAsync(id);

        if (signature is null)
        {
            return null;
        }

        if (signature.SignatureStatus == SignatureStatus.Signed)
        {
            throw new BusinessRuleException("Signature is already signed.");
        }

        if (update.SignatureStatus != SignatureStatus.Signed)
        {
            throw new BusinessRuleException(
                "Signature can only be marked as signed.");
        }

        signature.SignatureStatus = SignatureStatus.Signed;

        await _repository.UpdateAsync(signature);

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