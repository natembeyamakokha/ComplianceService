using System.ComponentModel.DataAnnotations;

namespace Compliance.Domain.Form.Compliance;

public record WorldCheckRequest([Required] string CustomerName, string EntityType = "INDIVIDUAL");
