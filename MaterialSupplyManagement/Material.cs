using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MaterialSupplyManagement.DAL;

namespace MaterialSupplyManagement;

public abstract class Material : IInfoProvider
{
    private string _name;
    private string _type;
    private string _description;

    [Column(ColumnNames.Name)]
    [Length(1, 32, ErrorMessage = "Too long or too short name")]
    public string Name
    {
        get => _name;
        set
        {
            _name = value;
            UpdateLastModified();
        }
    }

    [Column(ColumnNames.Type)]
    [Length(1, 32, ErrorMessage = "Too long or too short type name")]
    public string Type
    {
        get => _type;
        set
        {
            _type = value;
            UpdateLastModified();
        }
    }

    [Column(ColumnNames.Description)]
    public string Description
    {
        get => _description;
        set
        {
            _description = value;
            UpdateLastModified();
        }
    }

    [Column(ColumnNames.LastUpdate)]
    public DateTimeOffset LastUpdate { get; set; }
    
    public Material()
    {
        UpdateLastModified();
    }

    public Material(string name, string type, string description)
    {
        _name = name;
        _type = type;
        _description = description;
        UpdateLastModified();
    }

    public abstract string GetInfoString();

    public void UpdateLastModified() => LastUpdate = DateTimeOffset.UtcNow;
}