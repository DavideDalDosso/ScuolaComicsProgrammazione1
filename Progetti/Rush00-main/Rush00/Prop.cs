using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Prop
{
    private string name = "";
    private string description = "";
    private Action onInteract;
    private Action onPick;

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
    public void SetOnPick(Action onPick)
    {
        this.onPick = onPick;
    }
    public void SetOnInteract(Action onInteract)
    {
        this.onInteract = onInteract;
    }
    public bool Pick()
    {
        if (onPick == null) return false;
        onPick.Invoke();
        return true;
    }

    public bool Interact()
    {
        if (onInteract == null) return false;
        onInteract.Invoke();
        return true;
    }
}
