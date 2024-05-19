using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Room
{
    private string name = "";
    private string description = "";
    private List<Room> exits = new List<Room>();
    private List<Prop> props = new List<Prop>();
    public Room GetExit(int index)
    {
        if (index >= exits.Count() || index < 0) return null;
        return exits[index];
    }
    public Room[] GetExits()
    {
        return exits.ToArray();
    }
    public void AddExit(Room room)
    {
        exits.Add(room);
    }
    public void SetName(string name)
    {
        this.name = name;
    }
    public string GetName()
    {
        return name;
    }
    public void SetDescription(string desc)
    {
        description = desc;
    }
    public string GetDescription()
    {
        return description;
    }
    public Prop[] GetProps()
    {
        return props.ToArray();
    }
    public void AddProp(Prop prop)
    {
        props.Add(prop);
    }
    public Prop GetProp(int index)
    {
        if (index >= props.Count() || index < 0) return null;
        return props[index];
    }
    public void RemoveProp(Prop prop)
    {
        props.Remove(prop);
    }
}
