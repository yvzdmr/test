using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Carousel<CarouselType, DataType> : MonoBehaviour where CarouselType : CarouselItem<DataType>
{
    public CarouselType CarouselPrefab;
    public RectTransform CarouselContainer;

    public List<CarouselType> carouselItems = new List<CarouselType>();


    public void CreateItems(List<DataType> dataList) {
        CreateItems(dataList.ToArray());
    }

    public virtual void CreateItems(DataType[] dataList) {
        Clear();
        carouselItems = new List<CarouselType>();
        foreach(DataType data in dataList) {

            CarouselType carousel = GameObject.Instantiate(CarouselPrefab, CarouselContainer);
            carouselItems.Add(carousel);
            carousel.SetData(data);
            OnItemCreated(carousel);
        }
    }

    protected virtual void OnItemCreated(CarouselType item) {

    }

    public void Clear() {
        while(carouselItems.Count > 0) {
            CarouselType item = carouselItems[0];
            carouselItems.RemoveAt(0);
            GameObject.Destroy(item.gameObject);
        }
    }
}
