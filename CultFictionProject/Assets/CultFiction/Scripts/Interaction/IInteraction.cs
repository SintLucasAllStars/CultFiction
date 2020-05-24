using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteraction 
{
    void Grab(PlayerController pc);
    void Drop();
    void Use();
}
