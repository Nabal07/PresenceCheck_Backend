namespace PresenceCheck.Api.Domain;

public class Convidado
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public bool Confirmado { get; set; }
    public DateTime? DataConfirmacao { get; set; }
}