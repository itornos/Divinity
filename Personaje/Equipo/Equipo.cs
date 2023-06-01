using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Equipo
{
    void onChangeItem() { }
    bool inicializado() { return true; }

    void setItem(ItemController item) { }
}
