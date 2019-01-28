using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CarouselItem<DataType> : MonoBehaviour
{
    DataType data;
    public DataType Data {
        get { return data; }
        protected set {
            data = Data;
        }
    }

    public virtual void SetData(DataType dataObj) {
        data = dataObj;
    }
	
}
