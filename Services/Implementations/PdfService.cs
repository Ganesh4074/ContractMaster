using ContractMaster.Models;
using ContractMaster.Services.Interfaces;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace ContractMaster.Services;

public class PdfService : IPdfService
{
    public byte[] GenerateContractPdf(Contract contract)
    {
        QuestPDF.Settings.License = LicenseType.Community;

        return Document.Create(document =>
        {
            document.Page(page =>
            {
                page.Size(PageSizes.A4);

                page.MarginHorizontal(60);
                page.MarginVertical(50);

                // Lato is included with your QuestPDF setup.
                // Do not use Arial unless you explicitly register it.
                page.DefaultTextStyle(x =>
                    x.FontSize(10)
                     .FontFamily("Lato"));

                page.Header()
                    .Element(header => BuildHeader(header, contract));

                page.Content()
                    .Element(content => BuildNdaContent(content, contract));

                page.Footer()
                    .Element(footer => BuildFooter(footer, contract));
            });
        }).GeneratePdf();
    }

    // ---------------------------------------------------------
    // HEADER
    // ---------------------------------------------------------

    private void BuildHeader(IContainer container, Contract contract)
    {
        container.Column(column =>
        {
            column.Item()
                .AlignCenter()
                .Text("NON-DISCLOSURE AGREEMENT")
                .Bold()
                .FontSize(18);

            column.Item()
                .PaddingTop(6)
                .AlignCenter()
                .Text("MUTUAL CONFIDENTIALITY AGREEMENT")
                .FontSize(10);

            column.Item()
                .PaddingTop(15)
                .LineHorizontal(1);
        });
    }

    // ---------------------------------------------------------
    // NDA CONTENT
    // ---------------------------------------------------------

    private void BuildNdaContent(
        IContainer container,
        Contract contract)
    {
        container.Column(column =>
        {
            column.Spacing(12);

            // Document information
            column.Item()
                .Element(x => BuildDocumentInformation(x, contract));

            // 1. Purpose
            column.Item()
                .Element(x => BuildSection(
                    x,
                    "1. PURPOSE",
                    """
                    This Non-Disclosure Agreement ("Agreement") is entered into
                    between the parties identified below for the purpose of
                    evaluating, discussing, or pursuing a potential business
                    relationship or transaction between the parties.
                    """));

            // 2. Parties
            column.Item()
                .Element(x => BuildParties(x, contract));

            // 3. Confidential Information
            column.Item()
                .Element(x => BuildSection(
                    x,
                    "2. CONFIDENTIAL INFORMATION",
                    """
                    "Confidential Information" means any non-public,
                    proprietary, technical, financial, commercial, business,
                    operational, customer, product, or other information
                    disclosed by one party to the other, whether disclosed
                    orally, electronically, visually, in writing, or in any
                    other form.

                    Confidential Information includes information that is
                    marked or identified as confidential as well as information
                    that a reasonable person would understand to be confidential
                    given the nature of the information and the circumstances
                    of disclosure.
                    """));

            // 4. Obligations
            column.Item()
                .Element(x => BuildSection(
                    x,
                    "3. OBLIGATIONS OF THE RECEIVING PARTY",
                    """
                    The receiving party agrees to:

                    • Keep all Confidential Information strictly confidential.

                    • Use Confidential Information only for the purpose
                      described in this Agreement.

                    • Not disclose Confidential Information to any third party
                      except as expressly permitted by this Agreement.

                    • Protect Confidential Information using at least the same
                      degree of care that it uses to protect its own confidential
                      information, and in no event less than reasonable care.

                    • Limit access to Confidential Information to employees,
                      officers, professional advisers, contractors, or other
                      representatives who have a legitimate need to know and
                      who are subject to confidentiality obligations.
                    """));

            // 5. Exclusions
            column.Item()
                .Element(x => BuildSection(
                    x,
                    "4. EXCLUSIONS FROM CONFIDENTIAL INFORMATION",
                    """
                    Confidential Information does not include information
                    that the receiving party can demonstrate:

                    • Was publicly available at the time of disclosure or
                      subsequently becomes publicly available through no breach
                      of this Agreement.

                    • Was lawfully known to the receiving party before disclosure.

                    • Was independently developed without use of or reference
                      to the Confidential Information.

                    • Was lawfully received from a third party without a duty
                      of confidentiality.
                    """));

            // 6. Required Disclosure
            column.Item()
                .Element(x => BuildSection(
                    x,
                    "5. REQUIRED DISCLOSURE",
                    """
                    If the receiving party is required by law, regulation,
                    court order, or governmental authority to disclose
                    Confidential Information, the receiving party may make the
                    required disclosure.

                    Where legally permitted, the receiving party shall provide
                    the disclosing party with reasonable prior notice and
                    cooperate with reasonable efforts to seek confidential
                    treatment or other appropriate protection.
                    """));

            // 7. Ownership
            column.Item()
                .Element(x => BuildSection(
                    x,
                    "6. OWNERSHIP AND NO LICENSE",
                    """
                    All Confidential Information remains the property of the
                    disclosing party.

                    Nothing in this Agreement grants the receiving party any
                    ownership interest, license, intellectual property right,
                    or other right in or to the Confidential Information except
                    the limited right to use it for the purpose described in
                    this Agreement.
                    """));

            // 8. Return / Destruction
            column.Item()
                .Element(x => BuildSection(
                    x,
                    "7. RETURN OR DESTRUCTION OF INFORMATION",
                    """
                    Upon written request by the disclosing party, or upon
                    termination of discussions between the parties, the
                    receiving party shall, subject to applicable legal and
                    regulatory retention requirements, return or destroy
                    Confidential Information in its possession or control.

                    Upon request, the receiving party shall confirm such return
                    or destruction in writing.
                    """));

            // 9. Term
            column.Item()
                .Element(x => BuildSection(
                    x,
                    "8. TERM AND CONFIDENTIALITY",
                    $"""
                    This Agreement becomes effective on
                    {contract.StartDate:dd MMMM yyyy} and shall remain in effect
                    until {contract.EndDate:dd MMMM yyyy}, unless terminated
                    earlier in accordance with its terms.

                    The confidentiality obligations under this Agreement shall
                    continue for the period agreed between the parties and shall
                    survive termination or expiration of this Agreement to the
                    extent required by its terms.
                    """));

            // 10. No Warranty
            column.Item()
                .Element(x => BuildSection(
                    x,
                    "9. NO WARRANTY",
                    """
                    Confidential Information is provided for the purposes
                    described in this Agreement. Except as expressly agreed in
                    writing, neither party makes any representation or warranty
                    regarding the accuracy or completeness of Confidential
                    Information.
                    """));

            // 11. Remedies
            column.Item()
                .Element(x => BuildSection(
                    x,
                    "10. REMEDIES",
                    """
                    Each party acknowledges that unauthorized disclosure or use
                    of Confidential Information may cause harm for which monetary
                    damages may not be an adequate remedy.

                    The disclosing party may therefore seek any remedies
                    available under applicable law, including injunctive or
                    equitable relief where appropriate.
                    """));

            // 12. General Provisions
            column.Item()
                .Element(x => BuildSection(
                    x,
                    "11. GENERAL PROVISIONS",
                    """
                    This Agreement constitutes the understanding between the
                    parties concerning the confidentiality of information
                    disclosed for the purpose described above.

                    Any amendment or modification must be made in writing and
                    agreed by the parties.

                    If any provision of this Agreement is determined to be
                    invalid or unenforceable, the remaining provisions shall
                    continue in effect to the extent permitted by applicable law.
                    """));

            // 13. Signatures
            column.Item()
                .PaddingTop(25)
                .Element(x => BuildSignatureSection(x, contract));
        });
    }

    // ---------------------------------------------------------
    // DOCUMENT INFORMATION
    // ---------------------------------------------------------

    private void BuildDocumentInformation(
        IContainer container,
        Contract contract)
    {
        container
            .Border(1)
            .Padding(10)
            .Column(column =>
            {
                column.Spacing(5);

                column.Item()
                    .Text(text =>
                    {
                        text.Span("Contract ID: ")
                            .Bold();

                        text.Span(contract.ContractNumber.ToString());
                    });

                column.Item()
                    .Text(text =>
                    {
                        text.Span("Version: ")
                            .Bold();

                        text.Span(contract.Version.ToString());
                    });

                column.Item()
                    .Text(text =>
                    {
                        text.Span("Contract Type: ")
                            .Bold();

                        text.Span(contract.ContractType.ToString());
                    });

                column.Item()
                    .Text(text =>
                    {
                        text.Span("Effective Date: ")
                            .Bold();

                        text.Span(
                            contract.StartDate.ToString("dd MMMM yyyy"));
                    });
            });
    }

    // ---------------------------------------------------------
    // PARTIES
    // ---------------------------------------------------------

    private void BuildParties(
        IContainer container,
        Contract contract)
    {
        container.Column(column =>
        {
            column.Item()
                .Text("PARTIES")
                .Bold()
                .FontSize(12);

            column.Item()
                .PaddingTop(6)
                .Text(
                    $"Disclosing / Contracting Party: " +
                    $"{contract.CreatedByEmail}");

            column.Item()
                .Text(
                    $"Counterparty: {contract.CounterPartyName}");

            column.Item()
                .Text(
                    $"Counterparty Email: {contract.CounterPartyEmail}");

            column.Item()
                .PaddingTop(6)
                .Text(
                    "The parties agree to the terms and conditions " +
                    "set forth in this Agreement.");
        });
    }

    // ---------------------------------------------------------
    // SECTION
    // ---------------------------------------------------------

    private void BuildSection(
        IContainer container,
        string title,
        string content)
    {
        container.Column(column =>
        {
            column.Item()
                .Text(title)
                .Bold()
                .FontSize(12);

            column.Item()
                .PaddingTop(4)
                .Text(content)
                .LineHeight(1.3f);
        });
    }

    // ---------------------------------------------------------
    // SIGNATURES
    // ---------------------------------------------------------

    private void BuildSignatureSection(
        IContainer container,
        Contract contract)
    {
        container.Column(column =>
        {
            column.Item()
                .Text("12. SIGNATURES")
                .Bold()
                .FontSize(12);

            column.Item()
                .PaddingTop(5)
                .Text(
                    "By signing below, the parties acknowledge that they " +
                    "have read, understood, and agreed to the terms of " +
                    "this Agreement.");

            column.Item()
                .PaddingTop(25)
                .Row(row =>
                {
                    row.RelativeItem()
                        .PaddingRight(20)
                        .Element(x =>
                            BuildSignatureBlock(
                                x,
                                "PARTY A"));

                    row.RelativeItem()
                        .PaddingLeft(20)
                        .Element(x =>
                            BuildSignatureBlock(
                                x,
                                "PARTY B"));
                });
        });
    }

    private void BuildSignatureBlock(
        IContainer container,
        string party)
    {
        container.Column(column =>
        {
            column.Item()
                .Text(party)
                .Bold();

            column.Item()
                .PaddingTop(35)
                .LineHorizontal(1);

            column.Item()
                .PaddingTop(5)
                .Text("Authorized Signature");

            column.Item()
                .PaddingTop(20)
                .LineHorizontal(1);

            column.Item()
                .PaddingTop(5)
                .Text("Name");

            column.Item()
                .PaddingTop(20)
                .LineHorizontal(1);

            column.Item()
                .PaddingTop(5)
                .Text("Date");
        });
    }

    // ---------------------------------------------------------
    // FOOTER
    // ---------------------------------------------------------

    private void BuildFooter(
        IContainer container,
        Contract contract)
    {
        container.Column(column =>
        {
            column.Item()
                .LineHorizontal(1);

            column.Item()
                .PaddingTop(5)
                .Row(row =>
                {
                    row.RelativeItem()
                        .Text(
                            $"ContractMaster | " +
                            $"{contract.ContractNumber} | " +
                            $"Version {contract.Version}")
                        .FontSize(8);

                    row.RelativeItem()
                        .AlignRight()
                        .Text(text =>
                        {
                            text.Span("Page ");
                            text.CurrentPageNumber();
                            text.Span(" of ");
                            text.TotalPages();
                        });
                });
        });
    }
}