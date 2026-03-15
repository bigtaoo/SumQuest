using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class GameNumbers
{
    private static Dictionary<ImageType, Image> Images = new();

    public static void Initialize(List<Image> numberImages)
    {
        foreach (var image in numberImages)
        {
            image.gameObject.SetActive(false);
            switch (image.name)
            {
                case "0": Images[ImageType.Number0] = image; break;
                case "1": Images[ImageType.Number1] = image; break;
                case "2": Images[ImageType.Number2] = image; break;
                case "3": Images[ImageType.Number3] = image; break;
                case "4": Images[ImageType.Number4] = image; break;
                case "5": Images[ImageType.Number5] = image; break;
                case "6": Images[ImageType.Number6] = image; break;
                case "7": Images[ImageType.Number7] = image; break;
                case "8": Images[ImageType.Number8] = image; break;
                case "9": Images[ImageType.Number9] = image; break;
                case "Hide": Images[ImageType.Hide] = image; break;
                case "Select": Images[ImageType.Select] = image; break;
                default: break;
            }
        }
    }

    public static Image GetNumberImage(int number)
    {
        if (number < 0 || number > 9)
        {
            return null;
        }
        var image =  number switch
        {
            0 => Images[ImageType.Number0],
            1 => Images[ImageType.Number1],
            2 => Images[ImageType.Number2],
            3 => Images[ImageType.Number3],
            4 => Images[ImageType.Number4],
            5 => Images[ImageType.Number5],
            6 => Images[ImageType.Number6],
            7 => Images[ImageType.Number7],
            8 => Images[ImageType.Number8],
            9 => Images[ImageType.Number9],
            _ => null,
        };
        var newImage = GameObject.Instantiate(image, image.transform.parent);
        newImage.gameObject.SetActive(true);
        newImage.raycastTarget = false;

        return newImage;
    }

    public static void DrawSelect(int index, Vector3 position)
    {
        var image = Images[ImageType.Select];
        image.gameObject.SetActive(true);       
        image.transform.position = position;
        image.raycastTarget = false;
        image.transform.SetAsLastSibling();
    }

    public static void HideSelect()
    {
        Images[ImageType.Select].gameObject.SetActive(false);
    }
}