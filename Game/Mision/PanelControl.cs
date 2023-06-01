using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelControl : MonoBehaviour
{
    [SerializeField] Mision1 mision;

    private void OnDestroy()
    {
        if (mision.faseActual() == 1) {
            GameObject nuevo = Instantiate(gameObject, gameObject.transform.position, gameObject.transform.rotation);
            nuevo.GetComponent<PanelControl>().mision = mision;
        }
        mision.CompleteFase();
    }

    public void setMision(Mision1 mision) {
        this.mision = mision;
    }
}
