using ContractMaster.Models.Enums;

namespace ContractMaster.DTOs;

public record UpdateSignatureDTO(
    SignatureStatus SignatureStatus
);