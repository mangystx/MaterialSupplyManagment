using System.ComponentModel.DataAnnotations.Schema;
using MaterialSupplyManagement.DAL;

namespace MaterialSupplyManagement;

public abstract class Material(string name, string type, string description) : IInfoProvider
{
	[Column(ColumnNames.Name)] public string Name { get; set; } = name;
	[Column(ColumnNames.Type)] public string Type { get; set; } = type;
	[Column(ColumnNames.Description)] public string Description { get; set; } = description;
	[Column(ColumnNames.LastUpdate)] public DateTimeOffset LastUpdate { get; set; } = DateTimeOffset.UtcNow;
	
	public abstract string GetInfoString();
	
	public void UpdateLastModified() => LastUpdate = DateTimeOffset.UtcNow;
}