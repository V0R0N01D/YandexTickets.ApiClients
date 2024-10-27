namespace YandexTickets.ApiClients.Crm.Models.Enums;

/// <summary>
/// Определяет тип места.
/// </summary>
/// <remarks>
/// Возможные значения:
/// <list type="bullet">
/// <item>
/// <term>Unnumbered</term>
/// <description>Ненумерованное</description>
/// </item>
/// <item>
/// <term>Numbered</term>
/// <description>Нумерованное</description>
/// </item>
/// </list>
/// </remarks>
public enum PlaceType
{
	/// <summary>
	/// Ненумерованное
	/// </summary>
	Unnumbered = 0,

	/// <summary>
	/// Нумерованное
	/// </summary>
	Numbered = 1
}
